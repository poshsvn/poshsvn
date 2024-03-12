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
    }
}
