// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using SharpSvn;

namespace PoshSvn.Tests
{
    public class TargetCollectionTests
    {
        [Test]
        public void BasicTest()
        {
            ResolvedTargetCollection targets = new ResolvedTargetCollection(new SvnResolvedTarget[]
            {
                new SvnResolvedTarget(@"C:\wc\dir_1", null, false, null),
                new SvnResolvedTarget(@"C:\wc\dir_2", null, false, null),
                new SvnResolvedTarget(@"C:\wc\dir_3", null, false, null),
                new SvnResolvedTarget(null, new Uri("http://svn.example.com/repos/dir_4"), true, null)
            });

            CollectionAssert.AreEqual(targets.Paths, new[]
            {
                @"C:\wc\dir_1",
                @"C:\wc\dir_2",
                @"C:\wc\dir_3",
            });

            CollectionAssert.AreEqual(targets.Urls, new[]
            {
                new Uri("http://svn.example.com/repos/dir_4")
            });

            CollectionAssert.AreEqual(targets.EnumerateSharpSvnTargets(), new SharpSvn.SvnTarget[]
            {
                SvnPathTarget.FromString(@"C:\wc\dir_1"),
                SvnPathTarget.FromString(@"C:\wc\dir_2"),
                SvnPathTarget.FromString(@"C:\wc\dir_3"),
                SvnUriTarget.FromString("http://svn.example.com/repos/dir_4")
            });

            CollectionAssert.AreEqual(targets.ConvertToSharpSvnTargets(), new SharpSvn.SvnTarget[]
            {
                SvnPathTarget.FromString(@"C:\wc\dir_1"),
                SvnPathTarget.FromString(@"C:\wc\dir_2"),
                SvnPathTarget.FromString(@"C:\wc\dir_3"),
                SvnUriTarget.FromString("http://svn.example.com/repos/dir_4")
            });
        }

        [Test]
        public void HasPathAndUriThrow()
        {
            ResolvedTargetCollection targets = new ResolvedTargetCollection(new SvnResolvedTarget[]
            {
                new SvnResolvedTarget(@"C:\wc\dir_1", null, false, null),
                new SvnResolvedTarget(@"C:\wc\dir_2", null, false, null),
                new SvnResolvedTarget(@"C:\wc\dir_3", null, false, null),
                new SvnResolvedTarget(null, new Uri("http://svn.example.com/repos/dir_4"), true, null)
            });

            Assert.Throws<ArgumentException>(() => targets.ThrowIfHasPathsAndUris("Target"));
        }

        [Test]
        public void HasOnlyPathDoNotThrow()
        {
            ResolvedTargetCollection targets = new ResolvedTargetCollection(new SvnResolvedTarget[]
            {
                new SvnResolvedTarget(@"C:\wc\dir_1", null, false, null),
                new SvnResolvedTarget(@"C:\wc\dir_2", null, false, null),
                new SvnResolvedTarget(@"C:\wc\dir_3", null, false, null),
            });

            targets.ThrowIfHasPathsAndUris("Target");
        }

        [Test]
        public void HasOnlyUriDoNotThrow()
        {
            ResolvedTargetCollection targets = new ResolvedTargetCollection(new SvnResolvedTarget[]
            {
                new SvnResolvedTarget(null, new Uri("http://svn.example.com/repos/dir_4"), true, null),
                new SvnResolvedTarget(null, new Uri("http://svn.example.com/repos/dir_4"), true, null),
                new SvnResolvedTarget(null, new Uri("http://svn.example.com/repos/dir_4"), true, null),
            });

            Assert.DoesNotThrow(() => targets.ThrowIfHasPathsAndUris("Target"));
        }
    }
}
