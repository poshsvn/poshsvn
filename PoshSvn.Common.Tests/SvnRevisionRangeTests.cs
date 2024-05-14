// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using NUnit.Framework.Legacy;
using NUnit.Framework;

namespace PoshSvn.Common.Tests
{
    public class SvnRevisionRangeTests
    {
        [Test]
        public void NumberRangeTests()
        {
            ClassicAssert.AreEqual(new SvnRevisionRange(10, 20), new SvnRevisionRange("10:20"));
            ClassicAssert.AreEqual(new SvnRevisionRange(10, 20), new SvnRevisionRange("10: 20"));
            ClassicAssert.AreEqual(new SvnRevisionRange(10, 20), new SvnRevisionRange("10       : 20"));
            ClassicAssert.AreEqual(new SvnRevisionRange(10, 20), new SvnRevisionRange("r10 : r20"));
            ClassicAssert.AreEqual(new SvnRevisionRange(10, 20), new SvnRevisionRange("r10:r20"));
        }

        [Test]
        public void WordRangeTests()
        {
            ClassicAssert.AreEqual(
                new SvnRevisionRange(
                    new SvnRevision("40"),
                    new SvnRevision("head")),
                new SvnRevisionRange("40:head"));

            ClassicAssert.AreEqual(
                new SvnRevisionRange(
                    new SvnRevision("40"),
                    new SvnRevision("head")),
                new SvnRevisionRange("r40 : head"));

            ClassicAssert.AreEqual(
                new SvnRevisionRange(
                    new SvnRevision("PREV"),
                    new SvnRevision("HEAD")),
                new SvnRevisionRange("PREV:HEAD"));
        }

        [Test]
        public void SingleRevisionRangeTests()
        {
            ClassicAssert.AreEqual(
                new SvnRevisionRange(15, 15),
                new SvnRevisionRange("15"));

            ClassicAssert.AreEqual(
                new SvnRevisionRange(15, 15),
                new SvnRevisionRange(" r15"));
        }

        [Test]
        public void RangeExceptionTests()
        {
            Assert.Throws<ArgumentException>(() => new SvnRevisionRange("no_revison:123"));
            Assert.Throws<ArgumentException>(() => new SvnRevisionRange(""));
            Assert.Throws<ArgumentException>(() => new SvnRevisionRange(":"));
            Assert.Throws<ArgumentException>(() => new SvnRevisionRange("1:2:3"));
            Assert.Throws<ArgumentException>(() => new SvnRevisionRange("HEADS:10"));
            Assert.Throws<ArgumentException>(() => new SvnRevisionRange("12 3"));
        }
    }
}
