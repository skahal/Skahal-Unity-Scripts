using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using Skahal.Logging;

namespace Shakal.Logging.UnitTests
{    
    [Category("Logging")]
    public class PrefixedLogStrategyTest
    {
        [Test]
        public void AnyMethod_Prefix_MessagePrefixed()
        {
            var underlyingLogStrategy = MockRepository.GenerateMock<ISHLogStrategy>();
            underlyingLogStrategy.Expect(u => u.Debug("TEST - 1 {0} {1}", 2, 3));
            underlyingLogStrategy.Expect(u => u.Warning("TEST - 4 {0} {1}", 5, 6));
            underlyingLogStrategy.Expect(u => u.Error("TEST - 7 {0} {1}", 8, 9));
            underlyingLogStrategy.Expect(u => u.Error("TEST - error"));
            var target = new PrefixedLogStrategy(underlyingLogStrategy, "TEST - ");
            target.Debug("1 {0} {1}", 2, 3);
            target.Warning("4 {0} {1}", 5, 6);
            target.Error("7 {0} {1}", 8, 9);
            target.Error(new Exception("error"));

            underlyingLogStrategy.VerifyAllExpectations();
        }
    }
}
