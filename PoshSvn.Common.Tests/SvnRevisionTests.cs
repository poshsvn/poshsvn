// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace PoshSvn.Common.Tests
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
    }
}
