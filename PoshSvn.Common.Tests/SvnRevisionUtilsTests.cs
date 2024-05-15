// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace PoshSvn.Common.Tests
{
    public class SvnRevisionUtilsTests
    {
        [Test]
        public void CreateRangeFromRevisionOrChange_RevisionRange_Test()
        {
            var actual = SvnRevisionUtils.CreateRangeFromRevisionOrChange(
                new SvnRevisionRange("123:456"),
                null,
                SvnRevisionUtils.WorkingChangesRange);

            ClassicAssert.AreEqual(
                new SvnRevisionRange("123:456"),
                actual);
        }

        [Test]
        public void CreateRangeFromRevisionOrChange_Default_Test()
        {
            var actual = SvnRevisionUtils.CreateRangeFromRevisionOrChange(
                null,
                null,
                SvnRevisionUtils.WorkingChangesRange);

            ClassicAssert.AreEqual(
                SvnRevisionUtils.WorkingChangesRange,
                actual);
        }

        [Test]
        [Ignore("TODO:")]
        public void CreateRangeFromRevisionOrChange_RevisionChange_Test()
        {
            var actual = SvnRevisionUtils.CreateRangeFromRevisionOrChange(
                null,
                new SvnRevisionChange("789"),
                SvnRevisionUtils.WorkingChangesRange);

            ClassicAssert.AreEqual(
                new SvnRevisionRange("788:789"),
                actual);
        }

        [Test]
        public void CreateRangeFromRevisionOrChange_Error_CannotCombineRevisionAndChange_Test()
        {
            Assert.Throws<ArgumentException>(() => SvnRevisionUtils.CreateRangeFromRevisionOrChange(
                new SvnRevisionRange("123:456"),
                new SvnRevisionChange("789"),
                SvnRevisionUtils.WorkingChangesRange));
        }
    }
}
