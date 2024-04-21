// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using PoshSvn.Common;

namespace PoshSvn.Tests
{
    [TestFixture]
    public class LineDecoderTextStreamTests
    {
        private class TestTextLineStream : ITextLineStream
        {
            private readonly List<string> lines = new List<string>();

            public void WriteLine(string line)
            {
                lines.Add(line);
            }

            public string[] Lines => lines.ToArray();
        }

        [Test]
        public void EmptyTest()
        {
            var output = new TestTextLineStream();

            using (var decoder = new LineDecoderTextStream(output))
            {
                decoder.Write("");
            }

            CollectionAssert.IsEmpty(output.Lines);
        }

        [Test]
        public void SimpleTest()
        {
            var output = new TestTextLineStream();

            using (var decoder = new LineDecoderTextStream(output))
            {
                decoder.Write("line1\r\n\nline2\r\n");
            }

            CollectionAssert.AreEqual(
                new[]
                {
                    "line1",
                    "",
                    "line2"
                },
                output.Lines);
        }

        [Test]
        public void NoFinalNewLineTest()
        {
            var output = new TestTextLineStream();

            using (var decoder = new LineDecoderTextStream(output))
            {
                decoder.Write("line1\r\nline2");
            }

            CollectionAssert.AreEqual(
                new[]
                {
                    "line1",
                    "line2"
                },
                output.Lines);
        }
    }
}
