using System;
using NUnit.Framework;

namespace Skahal.Common.UnitTests
{
	[TestFixture]
	public class ThrowTest
	{
		[Test]
		public void AnyNull_AllPropertiesNotNull_NoException()
		{
			Throw.AnyNull (new { a = 1, b = "2", c = 3l });
		}

		[Test]
		public void AnyNull_AnyPropertieNull_Exception()
		{
			Assert.Catch<ArgumentNullException> (() => {
				Throw.AnyNull (new { a = (string) null, b = "2", c = 3l });
			});

			Assert.Catch<ArgumentNullException> (() => {
				Throw.AnyNull (new { a = 1, b = (object) null, c = 3l });
			});

			Assert.Catch<ArgumentNullException> (() => {
				Throw.AnyNull (new { a = 1, b = "2", c = (string) null });
			});
		}
	}
}

