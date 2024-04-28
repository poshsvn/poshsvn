// Copyright (c) Timofei Zhakov. All rights reserved.

using NUnit.Framework.Legacy;
using NUnit.Framework;
using System;

namespace PoshSvn.Common.Tests
{
    public class PegRevisionParserTests
    {
        [Test]
        public void ParsePegRevisionTargetTest1()
        {
            PegRevision.ParsePegRevisionTarget(@"C:\path\to\file", out var remainingTarget, out var revision);
            ClassicAssert.AreEqual(@"C:\path\to\file", remainingTarget);
            ClassicAssert.AreEqual(null, revision);
        }

        [Test]
        public void ParsePegRevisionTargetTest2()
        {
            PegRevision.ParsePegRevisionTarget(@"C:\path\to\file@123", out var remainingTarget, out var revision);
            ClassicAssert.AreEqual(@"C:\path\to\file", remainingTarget);
            ClassicAssert.AreEqual(new SvnRevision("123"), revision);
        }

        [Test]
        public void ParsePegRevisionTargetTest3()
        {
            PegRevision.ParsePegRevisionTarget(@"http://svn.example.com/svn/repo/trunk/test.txt@HEAD", out var remainingTarget, out var revision);
            ClassicAssert.AreEqual(@"http://svn.example.com/svn/repo/trunk/test.txt", remainingTarget);
            ClassicAssert.AreEqual(new SvnRevision("HEAD"), revision);
        }

        [Test]
        public void ParsePegRevisionTargetTest4()
        {
            Assert.Throws<ArgumentException>(() => PegRevision.ParsePegRevisionTarget(
                @"http://svn.example.com/svn/repo/trunk/test.txt@the_incorrect_revision",
                out var remainingTarget,
                out var revision));
        }
    }
}
