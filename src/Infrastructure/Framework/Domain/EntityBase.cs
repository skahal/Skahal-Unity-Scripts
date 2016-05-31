using System;

namespace Skahal.Infrastructure.Framework.Domain
{
	/// <summary>
	/// Classe base para entidades.
	/// </summary>
	[Serializable]
	public abstract class EntityBase : IEntity
	{
		#region Properties
		/// <summary>
		/// Obtém ou define o id.
		/// </summary>
		public long Id { get; set; }
		#endregion
	}
}
