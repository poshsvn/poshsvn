// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnPropertyTests
    {
        [Test]
        public void OutputFormatTableFromPropsetTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\test");
                sb.RunScript(@"svn-commit wc -m test");
                var actual = sb.FormatObject(sb.RunScript(@"svn-propset name value wc\test"), "Format-Table");

                ClassicAssert.AreEqual(
                    new object[]
                    {
                        @"",
                        @"",
                        @"    Properties on 'wc\test':",
                        @"",
                        @"Name Value",
                        @"---- -----",
                        @"name",
                        @"",
                        @"",
                    },
                    actual);
            }
        }

        [Test]
        public void ManyProperties()
        {
            using (var sb = new WcSandbox())
            {
                var actual = sb.FormatObject(new object[]
                {
                    new SvnProperty
                    {
                        Name = "name",
                        Value = "value",
                        Path = Path.Combine(sb.WcPath, "test"),
                    },
                    new SvnProperty
                    {
                        Name = "foo",
                        Value = "bar",
                        Path = Path.Combine(sb.WcPath, "test"),
                    },
                    new SvnProperty
                    {
                        Name = "svn:mergeinfo",
                        Path = Path.Combine(sb.WcPath, "test"),
                        Value =
                            "/subversion/branches/resolve-incoming-add:1762797-1764284\n" +
                            "/subversion/branches/revprop-cache:1298521-1326293\n" +
                            "/subversion/branches/revprop-caching-ng:1620597,1620599\n" +
                            "/subversion/branches/revprop-packing:1143907,1143971,1143997,1144017,1144499,1144568,1146145\n" +
                            "/subversion/branches/shelve:1802592-1815226\n" +
                            "/subversion/branches/shelve-checkpoint:1801593-1801923,1801970,1817320,1828508,1828521\n" +
                            "/subversion/branches/shelving-v3:1853394-1853901\n" +
                            "/subversion/branches/subtree-mergeinfo:876734-878766\n" +
                            "/subversion/branches/svn-auth-x509:1603509-1655900\n" +
                            "/subversion/branches/svn-info-detail:1660035-1662618\n" +
                            "/subversion/branches/svn-mergeinfo-enhancements:870119-870195,870197-870288\n" +
                            "/subversion/branches/svn-mergeinfo-normalizer:1642232-1695991\n" +
                            "/subversion/branches/svn-patch-improvements:918519-934609\n" +
                            "/subversion/branches/svn_mutex:1141683-1182099\n" +
                            "/subversion/branches/svnpatch-diff:865738-876477\n" +
                            "/subversion/branches/svnraisetc:874709-875149\n" +
                            "/subversion/branches/svnserve-logging:869828-870893\n" +
                            "/subversion/branches/swig-py3:1813660-1869353\n" +
                            "/subversion/branches/tc-issue-3334:874697-874773\n" +
                            "/subversion/branches/tc-merge-notify:874017-874062\n" +
                            "/subversion/branches/tc-resolve:874191-874239",
                    },
                },
                "Format-Table");

                ClassicAssert.AreEqual(
                    new object[]
                    {
                        @"",
                        @"",
                        @"    Properties on 'wc\test':",
                        @"",
                        @"Name          Value",
                        @"----          -----",
                        @"name          value",
                        @"foo           bar",
                        @"svn:mergeinfo /subversion/branches/resolve-incoming-add:1762797-1764284...",
                        @"",
                        @"",
                    },
                    actual);
            }
        }

        [Test]
        public void ManyGroups()
        {
            using (var sb = new WcSandbox())
            {
                var actual = sb.FormatObject(new object[]
                {
                    new SvnProperty
                    {
                        Name = "svn:ignore",
                        Value = "bin\nobj\n.vs\n",
                        Path = Path.Combine(sb.WcPath),
                    },
                    new SvnProperty
                    {
                        Name = "svn:mergeinfo",
                        Value = "/subversion/branches/resolve-incoming-add:1762797-1764284",
                        Path = Path.Combine(sb.WcPath),
                    },
                    new SvnProperty
                    {
                        Name = "svn:ignore",
                        Value = "bin\nobj\n.vs\nx64\nx86\n",
                        Path = Path.Combine(sb.WcPath, "Project1"),
                    },
                    new SvnProperty
                    {
                        Name = "foo",
                        Value = "this is a foo value!!",
                        Path = Path.Combine(sb.WcPath, "test"),
                    },
                    new SvnProperty
                    {
                        Name = "bar",
                        Value = "this is a bar value!!1!",
                        Path = Path.Combine(sb.WcPath, "test"),
                    },
                },
                "Format-Table");

                ClassicAssert.AreEqual(
                    new object[]
                    {
                        @"",
                        @"",
                        @"    Properties on 'wc':",
                        @"",
                        @"Name          Value",
                        @"----          -----",
                        @"svn:ignore    bin...",
                        @"svn:mergeinfo /subversion/branches/resolve-incoming-add:1762797-1764284",
                        @"",
                        @"",
                        @"    Properties on 'wc\Project1':",
                        @"",
                        @"Name       Value",
                        @"----       -----",
                        @"svn:ignore bin...",
                        @"",
                        @"",
                        @"    Properties on 'wc\test':",
                        @"",
                        @"Name Value",
                        @"---- -----",
                        @"foo  this is a foo value!!",
                        @"bar  this is a bar value!!1!",
                        @"",
                        @"",
                    },
                    actual);
            }
        }
    }
}
