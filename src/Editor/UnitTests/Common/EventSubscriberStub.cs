using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Skahal.Common.UnitTests
{
    class EventSubscriberStub : IEventSubscriber
    {
        public EventSubscriberStub(EventPublisherStub publisher)
        {
            enabled = true;
            publisher.Event1 += (s, e) =>
            {
                Event1RaisedCount++;
            };

            publisher.Event2 += (s, e) =>
            {
                Event2RaisedCount++;
            };
        }

        public bool enabled { get; set; }
        
        public int Event1RaisedCount { get; private set; }
        public int Event2RaisedCount { get; private set; }
    }
}
