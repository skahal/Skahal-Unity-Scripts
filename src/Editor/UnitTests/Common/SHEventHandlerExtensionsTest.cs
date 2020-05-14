using System;
using NUnit.Framework;
using UnityEngine;
using Skahal.Common;

namespace Skahal.Common.UnitTests
{
    [TestFixture]
    [Category("Common")]
    public class SHEventHandlerExtensionsTest
    {
        [Test]
        public void Raise_EventEmptyEventArgs_EventRaisedOnlyOnEnabledMonoBehaviour()
        {
            var publisher = new EventPublisherStub();
            var subscriber1 = new EventSubscriberStub(publisher);
            var subscriber2 = new EventSubscriberStub(publisher);
            var subscriber3 = new EventSubscriberStub(publisher);

            subscriber2.enabled = false;
            publisher.RaiseEvent1();
            Assert.AreEqual(1, subscriber1.Event1RaisedCount);
            Assert.AreEqual(0, subscriber2.Event1RaisedCount);
            Assert.AreEqual(1, subscriber3.Event1RaisedCount);

            publisher.RaiseEvent1();
            Assert.AreEqual(2, subscriber1.Event1RaisedCount);
            Assert.AreEqual(0, subscriber2.Event1RaisedCount);
            Assert.AreEqual(2, subscriber3.Event1RaisedCount);

            subscriber1.enabled = false;
            subscriber2.enabled = true;
            publisher.RaiseEvent1();
            Assert.AreEqual(2, subscriber1.Event1RaisedCount);
            Assert.AreEqual(1, subscriber2.Event1RaisedCount);
            Assert.AreEqual(3, subscriber3.Event1RaisedCount);
        }

        [Test]
        public void Raise_EventCustonEventArgs_EventRaisedOnlyOnEnabledMonoBehaviour()
        {
            var publisher = new EventPublisherStub();
            var subscriber1 = new EventSubscriberStub(publisher);
            var subscriber2 = new EventSubscriberStub(publisher);
            var subscriber3 = new EventSubscriberStub(publisher);

            subscriber2.enabled = false;
            publisher.RaiseEvent2();
            Assert.AreEqual(1, subscriber1.Event2RaisedCount);
            Assert.AreEqual(0, subscriber2.Event2RaisedCount);
            Assert.AreEqual(1, subscriber3.Event2RaisedCount);

            publisher.RaiseEvent2();
            Assert.AreEqual(2, subscriber1.Event2RaisedCount);
            Assert.AreEqual(0, subscriber2.Event2RaisedCount);
            Assert.AreEqual(2, subscriber3.Event2RaisedCount);

            subscriber1.enabled = false;
            subscriber2.enabled = true;
            publisher.RaiseEvent2();
            Assert.AreEqual(2, subscriber1.Event2RaisedCount);
            Assert.AreEqual(1, subscriber2.Event2RaisedCount);
            Assert.AreEqual(3, subscriber3.Event2RaisedCount);
        }

    }
}

