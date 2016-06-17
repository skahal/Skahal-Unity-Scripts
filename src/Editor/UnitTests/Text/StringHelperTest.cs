using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Shakal.Text.UnitTests
{    
    [Category("Text")]
    public class StringHelperTest
    {
        [Test]
        public void ToAcroynm_OneWord_OneLetter()
        {
            var actual = "Skahal".ToAcronym();
            Assert.AreEqual("S", actual);
        }

        [Test]
        public void ToAcroynm_TwoWords_TwoLetters()
        {
            var actual = "Skahal Studios".ToAcronym();
            Assert.AreEqual("SS", actual);
        }

        [Test]
        public void ToAcroynm_MoreThanOneSeparator_Letters()
        {
            var actual = "Ska.hal Studios".ToAcronym(3, 2, ".", " ");
            Assert.AreEqual("SHS", actual);
        }


        [Test]
        public void ToAcroynm_MaxLength_TwoLetters()
        {
            var actual = "Skahal Studios Company".ToAcronym(2);
            Assert.AreEqual("SS", actual);
        }

        [Test]
        public void ToAcroynm_MinWordLength_TwoLetters()
        {
            var actual = "José de Souza".ToAcronym(2, 3);
            Assert.AreEqual("JS", actual);
        }
    }
}
