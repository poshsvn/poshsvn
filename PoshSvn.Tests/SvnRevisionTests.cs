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
            ClassicAssert.AreEqual(new PoshSvnRevision(123), new PoshSvnRevision("123"));
            ClassicAssert.AreEqual(new PoshSvnRevision(123), new PoshSvnRevision(" 123"));
            ClassicAssert.AreEqual(new PoshSvnRevision(123), new PoshSvnRevision(" 123  "));
            ClassicAssert.AreEqual(new PoshSvnRevision(123), new PoshSvnRevision("r123"));
            ClassicAssert.AreEqual(new PoshSvnRevision(123), new PoshSvnRevision("rrrrr 123"));
            ClassicAssert.AreEqual(new PoshSvnRevision(123), new PoshSvnRevision("rrrrr 123   "));
        }

        [Test]
        public void WordRevisionTests()
        {
            ClassicAssert.AreEqual(new PoshSvnRevision(PoshSvnRevisionType.Head), new PoshSvnRevision("head"));
            ClassicAssert.AreEqual(new PoshSvnRevision(PoshSvnRevisionType.Head), new PoshSvnRevision("Head"));
            ClassicAssert.AreEqual(new PoshSvnRevision(PoshSvnRevisionType.Head), new PoshSvnRevision("HEAD"));
            ClassicAssert.AreEqual(new PoshSvnRevision(PoshSvnRevisionType.Head), new PoshSvnRevision("  HEAD  "));
            ClassicAssert.AreEqual(new PoshSvnRevision(PoshSvnRevisionType.Previous), new PoshSvnRevision("  PREV  "));
            ClassicAssert.AreEqual(new PoshSvnRevision(PoshSvnRevisionType.Base), new PoshSvnRevision("rBase"));
            ClassicAssert.AreEqual(new PoshSvnRevision(PoshSvnRevisionType.Committed), new PoshSvnRevision("Committed"));
        }

        [Test]
        public void DateRevisionTests()
        {
            // TODO:
        }

        [Test]
        public void ExceptionTests()
        {
            Assert.Throws<ArgumentException>(() => new PoshSvnRevision("no_revison"));
            Assert.Throws<ArgumentException>(() => new PoshSvnRevision(""));
            Assert.Throws<ArgumentException>(() => new PoshSvnRevision("r"));
            Assert.Throws<ArgumentException>(() => new PoshSvnRevision("HEADS"));
            Assert.Throws<ArgumentException>(() => new PoshSvnRevision("12 3"));
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
                    new PoshSvnRevision("40"),
                    new PoshSvnRevision("head")),
                new PoshSvnRevisionRange("40:head"));

            ClassicAssert.AreEqual(
                new PoshSvnRevisionRange(
                    new PoshSvnRevision("40"),
                    new PoshSvnRevision("head")),
                new PoshSvnRevisionRange("r40 : head"));
            
            ClassicAssert.AreEqual(
                new PoshSvnRevisionRange(
                    new PoshSvnRevision("PREV"),
                    new PoshSvnRevision("HEAD")),
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
