using Skahal.Infrastructure.Framework.Domain;
using System;

namespace Skahal.Infrastructure.Repositories.UnitTests
{
	[Serializable]
   	public class EntityStub : EntityBase, IAggregateRoot
    {
		public string Name { get; set; }
    }
}