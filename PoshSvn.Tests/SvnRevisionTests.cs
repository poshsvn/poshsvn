// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace PoshSvn.Tests
{
    public class SvnRevisionTests
    {
        [Test]
        public void NumberRevisionTests()
        {
            ClassicAssert.AreEqual(new SvnRevision(123), new SvnRevision("123"));
            ClassicAssert.AreEqual(new SvnRevision(123), new SvnRevision(" 123"));
            ClassicAssert.AreEqual(new SvnRevision(123), new SvnRevision(" 123  "));
            ClassicAssert.AreEqual(new SvnRevision(123), new SvnRevision("r123"));
            ClassicAssert.AreEqual(new SvnRevision(123), new SvnRevision("rrrrr 123"));
            ClassicAssert.AreEqual(new SvnRevision(123), new SvnRevision("rrrrr 123   "));
        }

        [Test]
        public void WordRevisionTests()
        {
            ClassicAssert.AreEqual(new SvnRevision(SvnRevisionType.Head), new SvnRevision("head"));
            ClassicAssert.AreEqual(new SvnRevision(SvnRevisionType.Head), new SvnRevision("Head"));
            ClassicAssert.AreEqual(new SvnRevision(SvnRevisionType.Head), new SvnRevision("HEAD"));
            ClassicAssert.AreEqual(new SvnRevision(SvnRevisionType.Head), new SvnRevision("  HEAD  "));
            ClassicAssert.AreEqual(new SvnRevision(SvnRevisionType.Previous), new SvnRevision("  PREV  "));
            ClassicAssert.AreEqual(new SvnRevision(SvnRevisionType.Base), new SvnRevision("rBase"));
            ClassicAssert.AreEqual(new SvnRevision(SvnRevisionType.Committed), new SvnRevision("Committed"));
        }

        [Test]
        public void DateRevisionTests()
        {
            // TODO:
        }

        [Test]
        public void ExceptionTests()
        {
            Assert.Throws<ArgumentException>(() => new SvnRevision("no_revison"));
            Assert.Throws<ArgumentException>(() => new SvnRevision(""));
            Assert.Throws<ArgumentException>(() => new SvnRevision("r"));
            Assert.Throws<ArgumentException>(() => new SvnRevision("HEADS"));
            Assert.Throws<ArgumentException>(() => new SvnRevision("12 3"));
        }

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
