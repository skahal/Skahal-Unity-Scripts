using System;

namespace Skahal.Infrastructure.Framework.Domain
{
	/// <summary>
	/// Base class for aggregate root entities.
	/// </summary>
	[Serializable]
	public abstract class AggregateRootBase : EntityBase, IAggregateRoot
	{        
	}
}
