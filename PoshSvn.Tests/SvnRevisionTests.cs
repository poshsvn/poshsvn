// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using SharpSvn;

namespace PoshSvn.Tests
{
    public class SvnRevisionTests
    {
        [Test]
        public void NumberRevisionTests()
        {
            ClassicAssert.AreEqual(new SvnRevision(123), SvnRevisionParser.ParseSvnRevision("123"));
            ClassicAssert.AreEqual(new SvnRevision(123), SvnRevisionParser.ParseSvnRevision(" 123"));
            ClassicAssert.AreEqual(new SvnRevision(123), SvnRevisionParser.ParseSvnRevision(" 123  "));
            ClassicAssert.AreEqual(new SvnRevision(123), SvnRevisionParser.ParseSvnRevision("r123"));
            ClassicAssert.AreEqual(new SvnRevision(123), SvnRevisionParser.ParseSvnRevision("rrrrr 123"));
            ClassicAssert.AreEqual(new SvnRevision(123), SvnRevisionParser.ParseSvnRevision("rrrrr 123   "));
        }

        [Test]
        public void WordRevisionTests()
        {
            ClassicAssert.AreEqual(new SvnRevision(SvnRevisionType.Head), SvnRevisionParser.ParseSvnRevision("head"));
            ClassicAssert.AreEqual(new SvnRevision(SvnRevisionType.Head), SvnRevisionParser.ParseSvnRevision("Head"));
            ClassicAssert.AreEqual(new SvnRevision(SvnRevisionType.Head), SvnRevisionParser.ParseSvnRevision("HEAD"));
            ClassicAssert.AreEqual(new SvnRevision(SvnRevisionType.Head), SvnRevisionParser.ParseSvnRevision("  HEAD  "));
            ClassicAssert.AreEqual(new SvnRevision(SvnRevisionType.Previous), SvnRevisionParser.ParseSvnRevision("  PREV  "));
            ClassicAssert.AreEqual(new SvnRevision(SvnRevisionType.Base), SvnRevisionParser.ParseSvnRevision("rBase"));
            ClassicAssert.AreEqual(new SvnRevision(SvnRevisionType.Committed), SvnRevisionParser.ParseSvnRevision("Committed"));
        }

        [Test]
        public void DateRevisionTests()
        {
            // TODO:
        }

        [Test]
        public void ExceptionTests()
        {
            Assert.Throws<ArgumentException>(() => SvnRevisionParser.ParseSvnRevision("no_revison"));
            Assert.Throws<ArgumentException>(() => SvnRevisionParser.ParseSvnRevision(""));
            Assert.Throws<ArgumentException>(() => SvnRevisionParser.ParseSvnRevision("r"));
            Assert.Throws<ArgumentException>(() => SvnRevisionParser.ParseSvnRevision("HEADS"));
            Assert.Throws<ArgumentException>(() => SvnRevisionParser.ParseSvnRevision("12 3"));
        }

        [Test]
        public void NumberRangeTests()
        {
            ClassicAssert.AreEqual(new SvnRevisionRange(10, 20), SvnRevisionParser.ParseSvnRevisionRange("10:20"));
            ClassicAssert.AreEqual(new SvnRevisionRange(10, 20), SvnRevisionParser.ParseSvnRevisionRange("10: 20"));
            ClassicAssert.AreEqual(new SvnRevisionRange(10, 20), SvnRevisionParser.ParseSvnRevisionRange("10       : 20"));
            ClassicAssert.AreEqual(new SvnRevisionRange(10, 20), SvnRevisionParser.ParseSvnRevisionRange("r10 : r20"));
            ClassicAssert.AreEqual(new SvnRevisionRange(10, 20), SvnRevisionParser.ParseSvnRevisionRange("r10:r20"));
        }

        [Test]
        public void WordRangeTests()
        {
            ClassicAssert.AreEqual(
                new SvnRevisionRange(
                    SvnRevisionParser.ParseSvnRevision("40"),
                    SvnRevisionParser.ParseSvnRevision("head")),
                SvnRevisionParser.ParseSvnRevisionRange("40:head"));

            ClassicAssert.AreEqual(
                new SvnRevisionRange(
                    SvnRevisionParser.ParseSvnRevision("40"),
                    SvnRevisionParser.ParseSvnRevision("head")),
                SvnRevisionParser.ParseSvnRevisionRange("r40 : head"));
            
            ClassicAssert.AreEqual(
                new SvnRevisionRange(
                    SvnRevisionParser.ParseSvnRevision("PREV"),
                    SvnRevisionParser.ParseSvnRevision("HEAD")),
                SvnRevisionParser.ParseSvnRevisionRange("PREV:HEAD"));
        }

        [Test]
        public void SingleRevisionRangeTests()
        {
            ClassicAssert.AreEqual(
                new SvnRevisionRange(15, 15),
                SvnRevisionParser.ParseSvnRevisionRange("15"));

            ClassicAssert.AreEqual(
                new SvnRevisionRange(15, 15),
                SvnRevisionParser.ParseSvnRevisionRange(" r15"));
        }

        [Test]
        public void RangeExceptionTests()
        {
            Assert.Throws<ArgumentException>(() => SvnRevisionParser.ParseSvnRevisionRange("no_revison:123"));
            Assert.Throws<ArgumentException>(() => SvnRevisionParser.ParseSvnRevisionRange(""));
            Assert.Throws<ArgumentException>(() => SvnRevisionParser.ParseSvnRevisionRange(":"));
            Assert.Throws<ArgumentException>(() => SvnRevisionParser.ParseSvnRevisionRange("1:2:3"));
            Assert.Throws<ArgumentException>(() => SvnRevisionParser.ParseSvnRevisionRange("HEADS:10"));
            Assert.Throws<ArgumentException>(() => SvnRevisionParser.ParseSvnRevisionRange("12 3"));
        }
    }
}
