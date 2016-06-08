using System;
using System.Linq;
using System.Linq.Expressions;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;
using UnityEngine;
using System.Collections.Generic;
using Skahal.Serialization;
using Skahal.Logging;

namespace Skahal.Infrastructure.Repositories
{
	public abstract class PlayerPrefsRepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot, new()
	{
		#region Methods
		public virtual IQueryable<TEntity> All ()
		{
			var allIds = GetAllIds ();            

			var result = new List<TEntity> ();
			
			foreach (var id in allIds) {
                var longId = long.Parse(id);
                var key = GetKey(longId);
                var rawValue = PlayerPrefs.GetString(key);

                Debug.LogFormat("Deserializing entity with key {0}. Raw value: '{1}'", key, rawValue);

                TEntity entity;

                if (String.IsNullOrEmpty(rawValue))
                {
                    entity = new TEntity()
                    {
                        Id = longId
                    };
                }
                else
                {
                    try
                    {
                        entity = SHSerializer.DeserializeFromString<TEntity>(rawValue);
                    }
                    catch(FormatException ex)
                    {
                        throw new FormatException(String.Format("Error trying to deserialize value key '{0}' with raw value: {1}", key, rawValue), ex);
                    }
                }

				result.Add(entity);
			}
				
			return result.AsQueryable();
		}

		public virtual TEntity Create (TEntity entity)
		{
			entity.Id = GetLastId () + 1;
			
			Modify(entity);
			SetLastId (entity.Id);
			
			return entity;
		}

		public virtual void Delete(TEntity entity)
		{
			PlayerPrefs.DeleteKey(GetKey(entity.Id));
		}

		public virtual void Delete (int id)
		{
			PlayerPrefs.DeleteKey (GetKey (id));
		}

		public virtual void Modify (TEntity entity)
		{
			var serialized = SHSerializer.SerializeToString (entity);

			var key = GetKey (entity.Id);

            SHLog.Debug("Serializing key '{0}' with raw value: '{1}'", key, serialized);
			PlayerPrefs.SetString (key, serialized);
		}

        public void Clear()
        {
            PlayerPrefs.DeleteKey(GetAllIdsKey());
        }
		#endregion	
		
		#region Fields
		private string GetKey (long id)
		{
			return String.Format ("PlayerPrefsRepository_{0}_{1}", typeof(TEntity).Name, id);
		}
		
		private long GetLastId ()
		{
			var lastKey = PlayerPrefs.GetString (String.Format ("PlayerPrefsRepository_{0}__LastId", typeof(TEntity).Name), "0");
			
			return long.Parse (lastKey);
		}
		
		private void SetLastId (long id)
		{
			PlayerPrefs.SetString (String.Format ("PlayerPrefsRepository_{0}__LastId", typeof(TEntity).Name), id.ToString ());
			
			if (id > 1) {
				var currentIds = PlayerPrefs.GetString (GetAllIdsKey(), "");
				PlayerPrefs.SetString (GetAllIdsKey(), currentIds + "," + id);
			}
		}
		
		private string GetAllIdsKey ()
		{
			return String.Format ("PlayerPrefsRepository_{0}__AllIds", typeof(TEntity).Name);	
		}
		
		private string[] GetAllIds()
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
