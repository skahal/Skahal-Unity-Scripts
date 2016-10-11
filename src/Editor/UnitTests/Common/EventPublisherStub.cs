using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skahal.Common.UnitTests
{
    class EventPublisherStub
    {
        public event EventHandler Event1;
        public event EventHandler<CustomStubEventArgs> Event2;


        public void RaiseEvent1()
        {
            Event1.Raise(this);
        }

        public void RaiseEvent2()
        {
            Event2.Raise(this, new CustomStubEventArgs());
        }
    }
}
