using NUnit.Framework;
using Skahal.Serialization;
using UnityEngine;
using System.Linq;

namespace Skahal.Infrastructure.Repositories.UnitTests
{
    [Category("Infrastructure.Repositories")]
    [Category("Unity")]
    public class GenericPlayerPrefsRepositoryTest
    {
        [Test]
        public void Delete_ExistsNoInterface_RemoveValueAndKeyFromList()
        {
            var target = new GenericPlayerPrefsRepository<EntityStub>();
            target.Clear();

            var created1 = target.Create(new EntityStub { Name = "T1" });
            var allIds = target.GetAllIds();
            Assert.AreEqual(1, allIds.Length);
            Assert.AreEqual(created1.Id.ToString(), allIds[0]);

            var created2 = target.Create(new EntityStub { Name = "T2" });
            allIds = target.GetAllIds();
            Assert.AreEqual(2, allIds.Length);
            Assert.AreEqual(created1.Id.ToString(), allIds[0]);
            Assert.AreEqual(created2.Id.ToString(), allIds[1]);

            target.Delete(created1);
            allIds = target.GetAllIds();
            Assert.AreEqual(1, allIds.Length);
            Assert.AreEqual(created2.Id.ToString(), allIds[0]);

            var created3 = target.Create(new EntityStub { Name = "T3" });
            allIds = target.GetAllIds();
            Assert.AreEqual(2, allIds.Length);
            Assert.AreEqual(created2.Id.ToString(), allIds[0]);
            Assert.AreEqual(created3.Id.ToString(), allIds[1]);

            target.Delete(created3);
            target.Delete(created2);
            allIds = target.GetAllIds();
            Assert.AreEqual(0, allIds.Length);
        }

        [Test]
        public void Delete_ExistsWithInterface_RemoveValueAndKeyFromList()
        {
            var target = new GenericPlayerPrefsRepository<IEntityStub, EntityStub>();
            target.Clear();

            var created1 = target.Create(new EntityStub { Name = "T1" });
            var allIds = target.GetAllIds();
            Assert.AreEqual(1, allIds.Length);
            Assert.AreEqual(created1.Id.ToString(), allIds[0]);
            Assert.AreEqual("T1", target.All().First().Name);

            var created2 = target.Create(new EntityStub { Name = "T2" });
            allIds = target.GetAllIds();
            Assert.AreEqual(2, allIds.Length);
            Assert.AreEqual(created1.Id.ToString(), allIds[0]);
            Assert.AreEqual(created2.Id.ToString(), allIds[1]);
            Assert.AreEqual("T1", target.All().First().Name);
            Assert.AreEqual("T2", target.All().Last().Name);

            target.Delete(created1);
            allIds = target.GetAllIds();
            Assert.AreEqual(1, allIds.Length);
            Assert.AreEqual(created2.Id.ToString(), allIds[0]);

            var created3 = target.Create(new EntityStub { Name = "T3" });
            allIds = target.GetAllIds();
            Assert.AreEqual(2, allIds.Length);
            Assert.AreEqual(created2.Id.ToString(), allIds[0]);
            Assert.AreEqual(created3.Id.ToString(), allIds[1]);

            target.Delete(created3);
            target.Delete(created2);
            allIds = target.GetAllIds();
            Assert.AreEqual(0, allIds.Length);
        }
    }
}