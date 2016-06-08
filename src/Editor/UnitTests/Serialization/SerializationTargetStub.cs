using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skahal.Serialization.UnitTest
{
    [Serializable]
    public class SerializationTargetStub
    {
        public string Text { get; set; }
        public int Integer { get; set; }
        public bool Boolean { get; set; }
    }
}
