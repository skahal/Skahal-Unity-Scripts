using Skahal.Infrastructure.Framework.Domain;

namespace Skahal.Infrastructure.Repositories.UnitTests
{
    public interface IEntityStub : IAggregateRoot
    {
        string Name { get; set; }
    }
}