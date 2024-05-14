// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace PoshSvn.Common.Tests
{
    public class SvnRevisionChangeTests
    {
        [Test]
        public void SimpleTest()
        {
            ClassicAssert.AreEqual(new SvnRevisionChange("123"), new SvnRevisionRange(122, 123));
        }

        [Test]
        public void StartsWithRTest()
        {
            ClassicAssert.AreEqual(new SvnRevisionChange("r123"), new SvnRevisionRange(122, 123));
        }

        [Test]
        public void NegativeTest()
        {
            ClassicAssert.AreEqual(new SvnRevisionChange("-123"), new SvnRevisionRange(123, 122));
        }

        [Test]
        public void NegativeAndStartsWithRTest()
        {
            ClassicAssert.AreEqual(new SvnRevisionChange("-r123"), new SvnRevisionRange(123, 122));
        }

        [Test]
        public void BadNegativeTest()
        {
            Assert.Throws<ArgumentException>(() => new SvnRevisionChange("r-123"));
        }

        [Test]
        public void BadNonNumericTestHead()
        {
            Assert.Throws<ArgumentException>(() => new SvnRevisionChange("HEAD"));
        }

        [Test]
        public void BadNonNumericTestQQQQQQQQQ()
        {
            Assert.Throws<ArgumentException>(() => new SvnRevisionChange("qqq"));
        }
    }
}
