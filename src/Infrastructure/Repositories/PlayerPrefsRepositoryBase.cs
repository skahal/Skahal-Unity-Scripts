using System;
using System.Linq;
using System.Linq.Expressions;
using Skahal.Infrastructure.Framework.Domain;
using Skahal.Infrastructure.Framework.Repositories;
using UnityEngine;
using System.Collections.Generic;
using Skahal.Serialization;

namespace Skahal.Infrastructure.Repositories
{
	public abstract class PlayerPrefsRepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
	{
		#region Methods
		public virtual IQueryable<TEntity> All ()
		{
			var allIds = GetAllIds ();
			var result = new List<TEntity> ();
			
			foreach (var id in allIds) {
				if (!String.IsNullOrEmpty (id)) {
					var r = SHSerializer.DeserializeFromString<TEntity> (PlayerPrefs.GetString(GetKey (long.Parse(id))));
					result.Add(r);
				}			
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
			PlayerPrefs.SetString (key, serialized);
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
			return PlayerPrefs.GetString (GetAllIdsKey(), "").Split(',');
		}
		#endregion
	}
}
