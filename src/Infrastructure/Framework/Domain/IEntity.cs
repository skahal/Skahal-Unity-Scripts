namespace Skahal.Infrastructure.Framework.Domain
{
	/// <summary>
	/// Define a interface básica de uma entidade.
	/// </summary>
	public interface IEntity
	{
		#region Properties
		/// <summary>
		/// Obtém ou define o id.
		/// </summary>
		long Id { get; set; }
		#endregion
	}
}
