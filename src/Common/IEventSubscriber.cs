using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skahal.Common
{
    public interface IEventSubscriber
    {
        bool enabled { get; set; } // Using 'enabled' to allow be used by Unity components :(.
    }
}
