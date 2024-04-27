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
            TargetCollection targets = TargetCollection.Parse(new object[]
            {
                @"C:\wc\dir_1",
                @"C:\wc\dir_2",
                @"C:\wc\dir_3",
                new Uri("http://svn.example.com/repos/dir_4")
            });

            CollectionAssert.AreEqual(targets.Paths, new[]
            {
                @"C:\wc\dir_1",
                @"C:\wc\dir_2",
                @"C:\wc\dir_3",
            });

            CollectionAssert.AreEqual(targets.Uris, new[]
            {
                new Uri("http://svn.example.com/repos/dir_4")
            });

            CollectionAssert.AreEqual(targets.Targets, new SharpSvn.SvnTarget[]
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
            TargetCollection targets = TargetCollection.Parse(new object[]
            {
                @"C:\wc\dir_1",
                new Uri("http://svn.example.com/repos/dir_4")
            });

            Assert.Throws<ArgumentException>(() => targets.ThrowIfHasPathsAndUris());
        }

        [Test]
        public void HasOnlyPathDoNotThrow()
        {
            TargetCollection targets = TargetCollection.Parse(new object[]
            {
                @"C:\wc\dir_1",
                @"C:\wc\dir_2",
            });

            Assert.DoesNotThrow(() => targets.ThrowIfHasPathsAndUris());
        }

        [Test]
        public void HasOnlyUriDoNotThrow()
        {
            TargetCollection targets = TargetCollection.Parse(new object[]
            {
                new Uri("http://svn.example.com/repos/dir_1"),
                new Uri("http://svn.example.com/repos/dir_2"),
                new Uri("http://svn.example.com/repos/dir_3"),
            });

            Assert.DoesNotThrow(() => targets.ThrowIfHasPathsAndUris());
        }

        [Test]
        public void BadPath()
        {
            Assert.Throws<ArgumentException>(() => TargetCollection.Parse(new object[]
            {
                @"C:\wc\dir_1",
                @"C:\wc\dir_2",
                "http://svn.example.com/repos/dir_1"
            }));
        }
    }
}
