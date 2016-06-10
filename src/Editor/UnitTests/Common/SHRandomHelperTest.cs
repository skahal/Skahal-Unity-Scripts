using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using Skahal.Serialization;
using Skahal.Common;
using System.Linq;
using System.Collections.Generic;

namespace Skahal.Serialization.UnitTest
{
    public class SHRandomHelperTest
    {
        [Test]
        public void NextEnum_NonEnum_Exception()
        {
            Assert.Catch<System.ArgumentException>(() => SHRandomHelper.NextEnum<int>());
        }

        [Test]
        public void NextEnum_Enum_RandomValues()
        {
            var possibleValues = System.Enum.GetValues(typeof(System.DayOfWeek)).Cast<System.DayOfWeek>();
            var generatedValues = new List<System.DayOfWeek>();

            for (int i = 0; i < 1000; i++)
            {
                var actual = SHRandomHelper.NextEnum<System.DayOfWeek>();
                Assert.IsTrue(possibleValues.Contains(actual));
                generatedValues.Add(actual);
            }

            Assert.IsTrue(generatedValues.Contains(System.DayOfWeek.Friday));
            Assert.IsTrue(generatedValues.Contains(System.DayOfWeek.Monday));
            Assert.IsTrue(generatedValues.Contains(System.DayOfWeek.Saturday));
            Assert.IsTrue(generatedValues.Contains(System.DayOfWeek.Sunday));
            Assert.IsTrue(generatedValues.Contains(System.DayOfWeek.Thursday));
            Assert.IsTrue(generatedValues.Contains(System.DayOfWeek.Tuesday));
            Assert.IsTrue(generatedValues.Contains(System.DayOfWeek.Wednesday));
        }
    }
}