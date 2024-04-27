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
            ClassicAssert.AreEqual(new SvnRevision(PoshSvnRevisionType.Head), new SvnRevision("head"));
            ClassicAssert.AreEqual(new SvnRevision(PoshSvnRevisionType.Head), new SvnRevision("Head"));
            ClassicAssert.AreEqual(new SvnRevision(PoshSvnRevisionType.Head), new SvnRevision("HEAD"));
            ClassicAssert.AreEqual(new SvnRevision(PoshSvnRevisionType.Head), new SvnRevision("  HEAD  "));
            ClassicAssert.AreEqual(new SvnRevision(PoshSvnRevisionType.Previous), new SvnRevision("  PREV  "));
            ClassicAssert.AreEqual(new SvnRevision(PoshSvnRevisionType.Base), new SvnRevision("rBase"));
            ClassicAssert.AreEqual(new SvnRevision(PoshSvnRevisionType.Committed), new SvnRevision("Committed"));
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
            ClassicAssert.AreEqual(new PoshSvnRevisionRange(10, 20), new PoshSvnRevisionRange("10:20"));
            ClassicAssert.AreEqual(new PoshSvnRevisionRange(10, 20), new PoshSvnRevisionRange("10: 20"));
            ClassicAssert.AreEqual(new PoshSvnRevisionRange(10, 20), new PoshSvnRevisionRange("10       : 20"));
            ClassicAssert.AreEqual(new PoshSvnRevisionRange(10, 20), new PoshSvnRevisionRange("r10 : r20"));
            ClassicAssert.AreEqual(new PoshSvnRevisionRange(10, 20), new PoshSvnRevisionRange("r10:r20"));
        }

        [Test]
        public void WordRangeTests()
        {
            ClassicAssert.AreEqual(
                new PoshSvnRevisionRange(
                    new SvnRevision("40"),
                    new SvnRevision("head")),
                new PoshSvnRevisionRange("40:head"));

            ClassicAssert.AreEqual(
                new PoshSvnRevisionRange(
                    new SvnRevision("40"),
                    new SvnRevision("head")),
                new PoshSvnRevisionRange("r40 : head"));

            ClassicAssert.AreEqual(
                new PoshSvnRevisionRange(
                    new SvnRevision("PREV"),
                    new SvnRevision("HEAD")),
                new PoshSvnRevisionRange("PREV:HEAD"));
        }

        [Test]
        public void SingleRevisionRangeTests()
        {
            ClassicAssert.AreEqual(
                new PoshSvnRevisionRange(15, 15),
                new PoshSvnRevisionRange("15"));

            ClassicAssert.AreEqual(
                new PoshSvnRevisionRange(15, 15),
                new PoshSvnRevisionRange(" r15"));
        }

        [Test]
        public void RangeExceptionTests()
        {
            Assert.Throws<ArgumentException>(() => new PoshSvnRevisionRange("no_revison:123"));
            Assert.Throws<ArgumentException>(() => new PoshSvnRevisionRange(""));
            Assert.Throws<ArgumentException>(() => new PoshSvnRevisionRange(":"));
            Assert.Throws<ArgumentException>(() => new PoshSvnRevisionRange("1:2:3"));
            Assert.Throws<ArgumentException>(() => new PoshSvnRevisionRange("HEADS:10"));
            Assert.Throws<ArgumentException>(() => new PoshSvnRevisionRange("12 3"));
        }
    }
}
