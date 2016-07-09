using Skahal.Infrastructure.Framework.Domain;

namespace Skahal.Infrastructure.Repositories
{
    /// <summary>
    /// PlayerPrefs genertic repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class GenericPlayerPrefsRepository<TEntity> : PlayerPrefsRepositoryBase<TEntity>
         where TEntity : class, IAggregateRoot, new()
    {		
        public GenericPlayerPrefsRepository()
            : base(() => new TEntity())
        {
        }
	}

    /// <summary>
    /// PlayerPrefs genertic repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class GenericPlayerPrefsRepository<TEntityInterface, TEntityClass> : PlayerPrefsRepositoryBase<TEntityInterface>
        where TEntityInterface: IAggregateRoot 
        where TEntityClass : class, TEntityInterface, new()         
    {
        public GenericPlayerPrefsRepository()
            : base(() => new TEntityClass())
        {
        }
    }
}
