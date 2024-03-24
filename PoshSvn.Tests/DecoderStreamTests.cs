// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Text;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace PoshSvn.Tests
{
    [TestFixture]
    public class DecoderStreamTests
    {
        private class TestTextStream : ITextStream
        {
            private readonly StringBuilder sb = new StringBuilder();

            public void Write(char[] chars, int startIndex, int charCount)
            {
                sb.Append(chars, startIndex, charCount);
            }

            public void Dispose()
            {
            }

            public string Content => sb.ToString();
        }

        [Test]
        public void EmptyTest()
        {
            var writer = new TestTextStream();
            using (var stream = new DecoderStream(writer, Encoding.UTF8))
            {
            }

            ClassicAssert.AreEqual("", writer.Content);
        }

        [Test]
        public void SimpleTest()
        {
            var writer = new TestTextStream();
            using (var stream = new DecoderStream(writer, Encoding.UTF8))
            {
                stream.WriteByte((byte)'a');
                stream.WriteByte((byte)'b');
            }

            ClassicAssert.AreEqual("ab", writer.Content);
        }

        [Test]
        public void UTF8SequenceTest1()
        {
            var writer = new TestTextStream();
            using (var stream = new DecoderStream(writer, Encoding.UTF8))
            {
                stream.WriteByte(0xE2);
                stream.WriteByte(0x82);
                stream.WriteByte(0xAC);
            }

            ClassicAssert.AreEqual("\u20AC", writer.Content);
        }

        [Test]
        public void UTF8BOMTest()
        {
            var writer = new TestTextStream();
            using (var stream = new DecoderStream(writer, Encoding.UTF8))
            {
                // UTF-8 BOM
                stream.WriteByte(0xEF);
                stream.WriteByte(0xBB);
                stream.WriteByte(0xBF);

                stream.WriteByte(0xE2);
                stream.WriteByte(0x82);
                stream.WriteByte(0xAC);
            }

            ClassicAssert.AreEqual("\uFEFF\u20AC", writer.Content);
        }

        [Test]
        public void UTF8SequenceFlushTest()
        {
            var writer = new TestTextStream();
            using (var stream = new DecoderStream(writer, Encoding.UTF8))
            {
                stream.WriteByte(0xE2);
                stream.Flush();
                stream.WriteByte(0x82);
                stream.Flush();
                stream.WriteByte(0xAC);
            }

            ClassicAssert.AreEqual("\u20AC", writer.Content);
        }

        [Test]
        public void IncompleteUTF8SequenceTest()
        {
            var writer = new TestTextStream();
            using (var stream = new DecoderStream(writer, Encoding.UTF8))
            {
                stream.WriteByte(0xE2);
                stream.WriteByte(0x82);
            }

            ClassicAssert.AreEqual("\uFFFD", writer.Content);
        }

        [Test]
        [TestCase("abdefedfdsfdsfsdfds")]
        public void RoundTripUTF32Test(string text)
        {
            var writer = new TestTextStream();
            using (var stream = new DecoderStream(writer, Encoding.UTF32))
            {
                var bytes = Encoding.UTF32.GetBytes(text);

                foreach (var b in bytes)
                {
                    stream.WriteByte(b);
                }
            }

            ClassicAssert.AreEqual(text, writer.Content);
        }
    }
}
