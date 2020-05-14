using System;
using System.Linq;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;
using UnityEngine;
using System.Collections.Generic;
using Skahal.Serialization;
using Skahal.Logging;

namespace Skahal.Infrastructure.Repositories
{
    public abstract class PlayerPrefsRepositoryBase<TEntity> : IRepository<TEntity> where TEntity : IAggregateRoot
    {
        #region Fields
        private Func<TEntity> m_createNew;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Skahal.Infrastructure.Repositories.PlayerPrefsRepositoryBase`1"/> class.
        /// </summary>
        protected PlayerPrefsRepositoryBase(Func<TEntity> createNew)
        {
            m_createNew = createNew;
            ClearEmptyEntities = true;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="Skahal.Infrastructure.Repositories.PlayerPrefsRepositoryBase{TEntity}"/> clear empty entities.
        /// </summary>
        /// <value><c>true</c> if clear empty entities; otherwise, <c>false</c>.</value>
        public bool ClearEmptyEntities { get; set; }
        #endregion

        #region Methods
        public virtual IQueryable<TEntity> All()
        {
            var allIds = GetAllIds();

            var result = new List<TEntity>();

            foreach (var id in allIds)
            {
                var longId = long.Parse(id);
                var key = GetKey(longId);
                var rawValue = PlayerPrefs.GetString(key);

                Debug.LogFormat("Deserializing entity with key {0}. Raw value: '{1}'", key, rawValue);

                TEntity entity;

                if (!ClearEmptyEntities && String.IsNullOrEmpty(rawValue))
                {
                    entity = m_createNew();
                    entity.Id = longId;
                }
                else
                {
                    try
                    {
                        entity = SHSerializer.DeserializeFromString<TEntity>(rawValue);

                        if (ClearEmptyEntities && entity == null)
                        {
                            Debug.LogWarningFormat("Clear entity with key {0} because it is null.", key);
                            Delete(longId);
                            continue;
                        }
                    }
                    catch (FormatException ex)
                    {
                        throw new FormatException(String.Format("Error trying to deserialize value key '{0}' with raw value: {1}", key, rawValue), ex);
                    }
                }

                result.Add(entity);
            }

            return result.AsQueryable();
        }

        public virtual TEntity Create(TEntity entity)
        {
            entity.Id = GetLastId() + 1;

            Modify(entity);
            SetLastId(entity.Id);

            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            Delete(entity.Id);
        }

        public virtual void Delete(long id)
        {
            PlayerPrefs.DeleteKey(GetKey(id));

            var ids = PlayerPrefs.GetString(GetAllIdsKey(), "");
            ids = ids.Replace(",{0}".With(id), String.Empty);
            PlayerPrefs.SetString(GetAllIdsKey(), ids);
        }

        public virtual void Modify(TEntity entity)
        {
            var serialized = SHSerializer.SerializeToString(entity);

            var key = GetKey(entity.Id);

            SHLog.Debug("Serializing key '{0}' with raw value: '{1}'", key, serialized);
            PlayerPrefs.SetString(key, serialized);
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(GetAllIdsKey());
        }
        #endregion

        #region Fields
        private string GetKey(long id)
        {
            return String.Format("PlayerPrefsRepository_{0}_{1}", typeof(TEntity).Name, id);
        }

        private long GetLastId()
        {
            var lastKey = PlayerPrefs.GetString(String.Format("PlayerPrefsRepository_{0}__LastId", typeof(TEntity).Name), "0");

            return long.Parse(lastKey);
        }

        private void SetLastId(long id)
        {
            PlayerPrefs.SetString(String.Format("PlayerPrefsRepository_{0}__LastId", typeof(TEntity).Name), id.ToString());

            if (id > 1)
            {
                var currentIds = PlayerPrefs.GetString(GetAllIdsKey(), "");
                PlayerPrefs.SetString(GetAllIdsKey(), currentIds + "," + id);
            }
        }

        private string GetAllIdsKey()
        {
            return String.Format("PlayerPrefsRepository_{0}__AllIds", typeof(TEntity).Name);
        }

        public string[] GetAllIds()
        {
            var ids = PlayerPrefs.GetString(GetAllIdsKey(), "");
            SHLog.Debug("Ids '{0}' found on {1}.", ids, GetType().Name);

            return ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(id => !String.IsNullOrEmpty(id.Trim()))
                .ToArray();
        }
        #endregion
    }
}
