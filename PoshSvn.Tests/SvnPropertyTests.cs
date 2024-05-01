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
                        @"name value",
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
                            "/subversion/branches/shelve:1802592-1815226\n",
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
        public void FormatListTest()
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
                            "/subversion/branches/shelve:1802592-1815226\n",
                    },
                },
                "Format-List");

                ClassicAssert.AreEqual(
                    new object[]
                    {
                        @"",
                        @"",
                        @"    Properties on 'wc\test':",
                        @"",
                        @"",
                        @"Name  : name",
                        @"Value : value",
                        @"",
                        @"Name  : foo",
                        @"Value : bar",
                        @"",
                        @"Name  : svn:mergeinfo",
                        @"Value : /subversion/branches/resolve-incoming-add:1762797-1764284",
                        @"        /subversion/branches/revprop-cache:1298521-1326293",
                        @"        /subversion/branches/revprop-caching-ng:1620597,1620599",
                        @"        /subversion/branches/revprop-packing:1143907,1143971,1143997,1144017,1144499,1144568,1146145",
                        @"        /subversion/branches/shelve:1802592-1815226",
                        @"",
                        @"",
                        @"",
                        @"",
                    },
                    actual);
            }
        }

        [Test]
        public void FormatCustomTest()
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
                            "/subversion/branches/shelve:1802592-1815226\n",
                    },
                },
                "Format-Custom");

                ClassicAssert.AreEqual(
                    new object[]
                    {
                        @"",
                        @"",
                        @"    Properties on 'wc\test':",
                        @"",
                        @"name",
                        @"  value",
                        @"",
                        @"foo",
                        @"  bar",
                        @"",
                        @"svn:mergeinfo",
                        @"  /subversion/branches/resolve-incoming-add:1762797-1764284",
                        @"  /subversion/branches/revprop-cache:1298521-1326293",
                        @"  /subversion/branches/revprop-caching-ng:1620597,1620599",
                        @"  /subversion/branches/revprop-packing:1143907,1143971,1143997,1144017,1144499,1144568,1146145",
                        @"  /subversion/branches/shelve:1802592-1815226",
                        @"",
                        @"",
                        @"",
                        @"",
                    },
                    actual);
            }
        }

        [Test]
        public void FormatListManyGroupsTest()
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
                "Format-List");

                ClassicAssert.AreEqual(
                    new object[]
                    {
                        @"",
                        @"",
                        @"    Properties on 'wc':",
                        @"",
                        @"",
                        @"Name  : svn:ignore",
                        @"Value : bin",
                        @"        obj",
                        @"        .vs",
                        @"",
                        @"",
                        @"Name  : svn:mergeinfo",
                        @"Value : /subversion/branches/resolve-incoming-add:1762797-1764284",
                        @"",
                        @"",
                        @"",
                        @"    Properties on 'wc\Project1':",
                        @"",
                        @"",
                        @"Name  : svn:ignore",
                        @"Value : bin",
                        @"        obj",
                        @"        .vs",
                        @"        x64",
                        @"        x86",
                        @"",
                        @"",
                        @"",
                        @"",
                        @"    Properties on 'wc\test':",
                        @"",
                        @"",
                        @"Name  : foo",
                        @"Value : this is a foo value!!",
                        @"",
                        @"Name  : bar",
                        @"Value : this is a bar value!!1!",
                        @"",
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

        [Test]
        public void ListFormatTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\test");
                sb.RunScript(@"svn-commit wc -m test");
                sb.RunScript(@"svn-propset name value wc\test");
                sb.RunScript(@"svn-propset foo bar wc\test");
                sb.RunScript(@"svn-propset 'svn:mergeinfo' '/subversion/branches/resolve-incoming-add:1762797-1764284' wc\test");

                var actual = sb.FormatObject(sb.RunScript(@"svn-proplist wc\test | Sort-Object -Property Name"), "Format-Table");

                CollectionAssert.AreEquivalent(
                    new object[]
                    {
                        @"",
                        @"",
                        @"    Properties on 'wc\test':",
                        @"",
                        @"Name          Value",
                        @"----          -----",
                        @"foo           bar",
                        @"name          value",
                        @"svn:mergeinfo /subversion/branches/resolve-incoming-add:1762797-1764284",
                        @"",
                        @"",
                    },
                    actual);
            }
        }

        [Test]
        public void ListFormatManyGroupsRecurseTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\Project1");
                sb.RunScript(@"svn-mkdir wc\test");
                sb.RunScript(@"svn-commit wc -m test");
                sb.RunScript(@"svn-propset svn:ignore ""bin`nobj`n.vs`n"" wc");
                sb.RunScript(@"svn-propset 'svn:mergeinfo' '/subversion/branches/resolve-incoming-add:1762797-1764284' wc");
                sb.RunScript(@"svn-propset svn:ignore ""bin`nobj`n.vs`nx64`nx86"" wc\Project1");
                sb.RunScript(@"svn-propset foo 'this is a foo value!!' wc\test");
                sb.RunScript(@"svn-propset bar 'this is a bar value!!1!' wc\test");

                var actual = sb.FormatObject(sb.RunScript(@"svn-proplist -Depth Infinity wc | Sort-Object -Property Name"), "Format-Table");

                CollectionAssert.AreEquivalent(
                    new object[]
                    {
                        @"",
                        @"",
                        @"    Properties on 'wc\test':",
                        @"",
                        @"Name Value",
                        @"---- -----",
                        @"bar  this is a bar value!!1!",
                        @"foo  this is a foo value!!",
                        @"",
                        @"",
                        @"    Properties on 'wc':",
                        @"",
                        @"Name       Value",
                        @"----       -----",
                        @"svn:ignore bin...",
                        @"",
                        @"",
                        @"    Properties on 'wc\Project1':",
                        @"",
                        @"Name       Value",
                        @"----       -----",
                        @"svn:ignore bin...",
                        @"",
                        @"",
                        @"    Properties on 'wc':",
                        @"",
                        @"Name          Value",
                        @"----          -----",
                        @"svn:mergeinfo /subversion/branches/resolve-incoming-add:1762797-1764284",
                        @"",
                        @"",
                    },
                    actual);
            }
        }
    }
}
