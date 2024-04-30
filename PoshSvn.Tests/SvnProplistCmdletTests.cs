// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.Tests.TestUtils;

namespace PoshSvn.Tests
{
    public class SvnProplistCmdletTests
    {
        [Test]
        public void SimpleTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\test");
                sb.RunScript(@"svn-commit wc -m test");
                sb.RunScript(@"svn-propset name value wc\test");

                var actual = sb.RunScript(@"svn-proplist wc\test");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnProperty
                        {
                            Name = "name",
                            Value = "value",
                            Path = Path.Combine(sb.WcPath, "test"),
                        }
                    },
                    actual);
            }
        }

        [Test]
        public void CurrentDirectoryTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\test");
                sb.RunScript(@"svn-commit wc -m test");
                sb.RunScript(@"svn-propset name value wc\test");

                var actual = sb.RunScript(@"cd wc\test; svn-proplist");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnProperty
                        {
                            Name = "name",
                            Value = "value",
                            Path = Path.Combine(sb.WcPath, "test"),
                        }
                    },
                    actual);
            }
        }

        [Test]
        public void RemoteTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\test");
                sb.RunScript(@"svn-commit wc -m test");
                sb.RunScript(@"svn-propset name value wc\test");
                sb.RunScript(@"svn-commit wc -m 'setup props'");

                var actual = sb.RunScript($@"svn-proplist {sb.ReposUrl}/test");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnProperty
                        {
                            Name = "name",
                            Value = "value",
                            Path = $"{sb.ReposUrl}/test",
                        }
                    },
                    actual);
            }
        }

        [Test]
        public void RemoteManyNonRecurseTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\Project1");
                sb.RunScript(@"svn-mkdir wc\test");
                sb.RunScript(@"svn-propset svn:ignore ""bin`nobj`n.vs`n"" wc");
                sb.RunScript(@"svn-propset 'svn:mergeinfo' '/subversion/branches/resolve-incoming-add:1762797-1764284' wc");
                sb.RunScript(@"svn-propset svn:ignore ""bin`nobj`n.vs`nx64`nx86"" wc\Project1");
                sb.RunScript(@"svn-propset foo 'this is a foo value!!' wc\test");
                sb.RunScript(@"svn-propset bar 'this is a bar value!!1!' wc\test");

                sb.RunScript(@"svn-commit wc -m 'setup props'");

                var actual = sb.RunScript($@"svn-proplist {sb.ReposUrl} | Sort-Object -Property Name");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnProperty
                        {
                            Name = "svn:ignore",
                            Value = "bin\r\nobj\r\n.vs\r\n",
                            Path = $"{sb.ReposUrl}",
                        },
                        new SvnProperty
                        {
                            Name = "svn:mergeinfo",
                            Value = "/subversion/branches/resolve-incoming-add:1762797-1764284",
                            Path = $"{sb.ReposUrl}",
                        },
                    },
                    actual);
            }
        }

        [Test]
        public void RemoteManyRecurseTest()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\Project1");
                sb.RunScript(@"svn-mkdir wc\test");
                sb.RunScript(@"svn-propset svn:ignore ""bin`nobj`n.vs`n"" wc");
                sb.RunScript(@"svn-propset 'svn:mergeinfo' '/subversion/branches/resolve-incoming-add:1762797-1764284' wc");
                sb.RunScript(@"svn-propset svn:ignore ""bin`nobj`n.vs`nx64`nx86"" wc\Project1");
                sb.RunScript(@"svn-propset foo 'this is a foo value!!' wc\test");
                sb.RunScript(@"svn-propset bar 'this is a bar value!!1!' wc\test");

                sb.RunScript(@"svn-commit wc -m 'setup props'");

                var actual = sb.RunScript($@"svn-proplist {sb.ReposUrl} -Depth Infinity | Sort-Object -Property Name");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnProperty
                        {
                            Name = "bar",
                            Value = "this is a bar value!!1!",
                            Path = $"{sb.ReposUrl}/test",
                        },
                        new SvnProperty
                        {
                            Name = "foo",
                            Value = "this is a foo value!!",
                            Path = $"{sb.ReposUrl}/test",
                        },
                        new SvnProperty
                        {
                            Name = "svn:ignore",
                            Value = "bin\r\nobj\r\n.vs\r\n",
                            Path = $"{sb.ReposUrl}",
                        },
                        new SvnProperty
                        {
                            Name = "svn:ignore",
                            Value = "bin\r\nobj\r\n.vs\r\nx64\r\nx86\r\n",
                            Path = $"{sb.ReposUrl}/Project1",
                        },
                        new SvnProperty
                        {
                            Name = "svn:mergeinfo",
                            Value = "/subversion/branches/resolve-incoming-add:1762797-1764284",
                            Path = $"{sb.ReposUrl}",
                        },
                    },
                    actual);
            }
        }

        [Test]
        public void SimpleTest2()
        {
            using (var sb = new WcSandbox())
            {
                sb.RunScript(@"svn-mkdir wc\test");
                sb.RunScript(@"svn-commit wc -m test");
                sb.RunScript(@"svn-propset name value wc\test");
                sb.RunScript(@"svn-propset foo bar wc\test");
                sb.RunScript(@"svn-propset 'svn:mergeinfo' '/subversion/branches/resolve-incoming-add:1762797-1764284' wc\test");

                var actual = sb.RunScript(@"svn-proplist wc\test | Sort-Object -Property Name");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnProperty
                        {
                            Name = "foo",
                            Value = "bar",
                            Path = Path.Combine(sb.WcPath, "test"),
                        },
                        new SvnProperty
                        {
                            Name = "name",
                            Value = "value",
                            Path = Path.Combine(sb.WcPath, "test"),
                        },
                        new SvnProperty
                        {
                            Name = "svn:mergeinfo",
                            Value = "/subversion/branches/resolve-incoming-add:1762797-1764284",
                            Path = Path.Combine(sb.WcPath, "test"),
                        },
                    },
                    actual);
            }
        }

        [Test]
        public void SimpleTest3NonRecurse()
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

                var actual = sb.RunScript(@"svn-proplist wc | Sort-Object -Property Name,Value");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnProperty
                        {
                            Name = "svn:ignore",
                            Value = "bin\r\nobj\r\n.vs\r\n",
                            Path = Path.Combine(sb.WcPath),
                        },
                        new SvnProperty
                        {
                            Name = "svn:mergeinfo",
                            Value = "/subversion/branches/resolve-incoming-add:1762797-1764284",
                            Path = Path.Combine(sb.WcPath),
                        },
                    },
                    actual);
            }
        }

        [Test]
        public void SimpleTest3()
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

                var actual = sb.RunScript(@"svn-proplist wc -Depth Infinity | Sort-Object -Property Name,Value");

                PSObjectAssert.AreEqual(
                    new object[]
                    {
                        new SvnProperty
                        {
                            Name = "bar",
                            Value = "this is a bar value!!1!",
                            Path = Path.Combine(sb.WcPath, "test"),
                        },
                        new SvnProperty
                        {
                            Name = "foo",
                            Value = "this is a foo value!!",
                            Path = Path.Combine(sb.WcPath, "test"),
                        },
                        new SvnProperty
                        {
                            Name = "svn:ignore",
                            Value = "bin\r\nobj\r\n.vs\r\n",
                            Path = Path.Combine(sb.WcPath),
                        },
                        new SvnProperty
                        {
                            Name = "svn:ignore",
                            Value = "bin\r\nobj\r\n.vs\r\nx64\r\nx86\r\n",
                            Path = Path.Combine(sb.WcPath, "Project1"),
                        },
                        new SvnProperty
                        {
                            Name = "svn:mergeinfo",
                            Value = "/subversion/branches/resolve-incoming-add:1762797-1764284",
                            Path = Path.Combine(sb.WcPath),
                        },
                    },
                    actual);
            }
        }
    }
}
