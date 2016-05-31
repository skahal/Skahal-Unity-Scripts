using System;
using System.Linq;
using System.Linq.Expressions;
using Skahal.Infrastructure.Framework.Domain;

namespace Skahal.Infrastructure.Framework.Repositories
{
	/// <summary>
	/// Define a interface básica para um repositório de entidades.
	/// </summary>
	/// <typeparam name="TEntity">O tipo de entidade que o repositório trabalha.</typeparam>
	public interface IRepository<TEntity>  where TEntity : IAggregateRoot
	{
		#region Methods
		/// <summary>
		/// Obtém uma consulta de todas as instâncias.
		/// </summary>
		/// <returns>O IQueryable.</returns>
		IQueryable<TEntity> All();

		/// <summary>
		/// Cria a entidade informada no repositório.
		/// </summary>
		/// <param name="entity">A entidade.</param>
		/// <returns>A entidade criada.</returns>
		TEntity Create(TEntity entity);

		/// <summary>
		/// Exclui a entidade do repositório.
		/// </summary>
		/// <param name="entity">A entidade.</param>
		void Delete(TEntity entity);

		/// <summary>
		/// Exclui a entidade com o id informado.
		/// </summary>
		/// <param name="id">O id da entidade a ser removida.</param>
		void Delete(int id);

		/// <summary>
		/// Modifica a entidade no repositório.
		/// </summary>
		/// <param name="entity">A entidade a ser modificada.</param>
		void Modify(TEntity entity);
		#endregion
	}
}
