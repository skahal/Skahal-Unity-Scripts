using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Skahal.Threading;

namespace Shakal.Threading.UnitTests
{    
    [Category("Threading")]
	public class CoroutineAsyncActionProviderTest
    {
        [Test]
        public void Start_DelayAndAction_Action()
        {
			int x = 1;
			var target = new CoroutineAsyncActionProvider ();
			var actual = target.Start (10, () => x = 2);
			actual.Cancel ();

			Assert.AreEqual(1, x);
        }
    }
}
