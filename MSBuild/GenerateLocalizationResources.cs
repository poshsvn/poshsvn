//
// Copyright 2008-2009 The SharpSvn Project
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace MSBuild
{
    public class GenerateLocalizationResources : Task
    {
        [Required]
        public string Prefix { get; set; }

        [Required]
        public string OutputDirectory { get; set; }

        [Required]
        public string[] InputFiles { get; set; }

        public override bool Execute()
        {
            Log.LogMessage("Generating localization resources...");

            GenerateResxFiles(Prefix, OutputDirectory, InputFiles);

            return true;
        }

        private static void GenerateResxFiles(string prefix, string toDir, string[] files)
        {
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                FileInfo toFile = new FileInfo(Path.Combine(toDir, prefix + "." + Path.GetFileNameWithoutExtension(fileInfo.Name).Replace('_', '-') + ".resx"));

                XmlWriterSettings xws = new XmlWriterSettings();
                xws.Indent = true;
                using (XmlWriter xw = XmlWriter.Create(toFile.FullName, xws))
                {
                    xw.WriteStartDocument();
                    xw.WriteStartElement("root");

                    WriteHeader(xw, "resmimetype", "text/microsoft-resx");
                    WriteHeader(xw, "version", "2.0");
                    WriteHeader(xw, "reader", "System.Resources.ResXResourceReader, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
                    WriteHeader(xw, "writer", "System.Resources.ResXResourceWriter, System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
                    SortedDictionary<string, Msg> preDefined = new SortedDictionary<string, Msg>(StringComparer.OrdinalIgnoreCase);

                    try
                    {
                        foreach (Msg msg in PoParser.ReadMessages(fileInfo))
                        {
                            Msg alt;

                            if (!string.IsNullOrEmpty(msg.Comment) && msg.Comment.StartsWith("../svn"))
                                continue; // Not for translation

                            if (preDefined.TryGetValue(msg.Key, out alt))
                            {
                                if (!string.Equals(msg.Value, alt.Value, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Console.Error.WriteLine("{0}({1}): Warning: Duplicate key found with different translation", fileInfo.FullName, msg.Line);
                                    Console.Error.WriteLine(" 1st key:   {0}", alt.Key);
                                    Console.Error.WriteLine(" 1st value: {0}", alt.Value);
                                    Console.Error.WriteLine(" 2nd key:   {0}", msg.Key);
                                    Console.Error.WriteLine(" 2nd value: {0}", msg.Value);
                                    Console.Error.WriteLine("Ignored the 2nd entry");
                                }
                                continue;
                            }

                            preDefined.Add(msg.Key, msg);

                            if (!string.IsNullOrEmpty(msg.Comment))
                                xw.WriteComment(msg.Comment);
                            if (!string.IsNullOrEmpty(msg.Flags))
                                xw.WriteComment(msg.Comment);

                            WriteData(xw, msg.Key, msg.Value);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new InvalidOperationException(string.Format("While reading {0}", fileInfo.FullName), e);
                    }

                    xw.WriteEndElement();
                }
            }
        }

        static void WriteHeader(XmlWriter xw, string key, string value)
        {
            xw.WriteStartElement("resheader");
            xw.WriteAttributeString("name", key);
            xw.WriteElementString("value", value);
            xw.WriteEndElement();
        }

        static string XmlXmlNs = "http://www.w3.org/XML/1998/namespace";
        private static void WriteData(XmlWriter xw, string key, string value)
        {
            xw.WriteStartElement("data");
            xw.WriteAttributeString("name", key);
            xw.WriteAttributeString("xml", "space", XmlXmlNs, "preserve");
            xw.WriteElementString("value", value);
            xw.WriteEndElement();
        }
    }

    class Msg
    {
        readonly string _id;
        readonly string _value;
        readonly string _comment;
        readonly string _flags;
        readonly int _line;

        public Msg(string id, string value, string comment, string flags, int line)
        {
            _id = id;
            _value = value;
            _comment = comment;
            _flags = flags;
            _line = line;
        }

        public string Key
        {
            get { return _id; }
        }

        public string Value
        {
            get { return _value; }
        }

        public string Comment
        {
            get { return _comment; }
        }
        public string Flags
        {
            get { return _flags; }
        }

        public int Line
        {
            get { return _line; }
        }
    }

    class Token
    {
        readonly string _key;
        readonly string _value;
        readonly string _comment;
        readonly string _flags;
        readonly int _line;
        readonly int _percent;

        public Token(string key, string value, string comment, string flags, int line, int percent)
        {
            _key = key;
            _value = value;
            _comment = comment ?? "";
            _flags = flags ?? "";
            _line = line;
            _percent = percent;
        }

        public string Key
        {
            get { return _key; }
        }

        public string Value
        {
            get { return _value; }
        }

        public string Comment
        {
            get { return _comment; }
        }

        public string Flags
        {
            get { return _flags; }
        }

        public int Line
        {
            get { return _line; }
        }

        public int Percent
        {
            get { return _percent; }
        }
    }

    class PoParser
    {
        public static IEnumerable<Msg> ReadMessages(FileInfo file)
        {
            Token msgid;
            Encoding encoding = null;
            bool again;

            do
            {
                msgid = null;
                again = false;

                foreach (Token token in ReadTokens(file, encoding ?? Encoding.ASCII))
                {
                    switch (token.Key)
                    {
                        case "msgid":
                            msgid = token;
                            break;
                        case "msgstr":
                            if (msgid == null)
                                throw new InvalidOperationException();

                            string flags = msgid.Flags + token.Flags;
                            if (msgid.Value == "")
                            {
                                if (encoding == null)
                                {
                                    foreach (string line in token.Value.Split('\n'))
                                    {
                                        string l = line.Trim(); // Trim whitespace and '\r'

                                        if (l.StartsWith("Content-Type:", StringComparison.OrdinalIgnoreCase))
                                        {
                                            l = l.Substring(l.IndexOf(':') + 1).Trim();
                                            int c = l.IndexOf("CHARSET=", StringComparison.OrdinalIgnoreCase);

                                            if (c >= 0)
                                            {
                                                string cc = l.Substring(l.IndexOf('=', c) + 1);

                                                if (cc.Equals("UTF-8", StringComparison.OrdinalIgnoreCase) || cc.Equals("UTF8", StringComparison.OrdinalIgnoreCase))
                                                    encoding = Encoding.UTF8;
                                                else if (cc.Equals("UTF-7", StringComparison.OrdinalIgnoreCase) || cc.Equals("UTF7", StringComparison.OrdinalIgnoreCase))
                                                    encoding = Encoding.UTF7;
                                                else
                                                    try
                                                    {
                                                        encoding = Encoding.GetEncoding(cc);
                                                    }
                                                    catch
                                                    {
                                                        Console.Error.WriteLine(string.Format("{0}(1): Warning: Ignoring unknown encoding '{1}'", file.FullName, cc));
                                                        continue;
                                                    }

                                                again = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (string.IsNullOrEmpty(token.Value))
                            { /* Skip: No translation available */ }
                            else if (flags.Contains(", fuzzy"))
                            { /* Skip: Fuzzy translations of c-format strings */ }
                            else if (msgid.Percent != token.Percent && flags.Contains(", c-format"))
                            {
                                Console.Error.WriteLine(string.Format("{0}({1}): Warning: Percent mismatch in token: {2} vs {3} ({4})", file.FullName, msgid.Line, CEscape(msgid.Value), CEscape(token.Value), CEscape(token.Flags)));
                            }
                            else
                                yield return new Msg(msgid.Value, token.Value, msgid.Comment ?? token.Comment, flags, msgid.Line);

                            msgid = null;
                            break;
                        case "msgid_plural":
                        case "msgstr_plural":
                            break;
                        case "msgstr[0]":
                        case "msgstr[1]":
                        case "msgstr[2]":
                        case "msgstr[3]":
                        case "msgstr[4]":
                            break;
                        default:
                            Console.Error.WriteLine(string.Format("{0}({1}): Warning: Ignoring unexpected token: {2}", file.FullName, token.Line, token.Key));
                            Console.Error.Flush();
                            throw new InvalidOperationException();
                    }

                    if (again)
                        break;
                }
            }
            while (again);

            if (msgid != null)
                throw new InvalidOperationException("MsgId without value: " + CEscape(msgid.Value));


        }

        private static string CEscape(string p)
        {
            return p.Replace("\n", "\\n").Replace("\r", "\\r");
        }

        private static IEnumerable<Token> ReadTokens(FileInfo file, Encoding encoding)
        {
            using (StreamReader sr = (encoding != null) ? new StreamReader(file.FullName, encoding) : new StreamReader(file.FullName))
            {
                int lineNumber = 0;
                string line;
                StringBuilder sbToken = new StringBuilder();
                StringBuilder sbTextBuilding = new StringBuilder();
                string comment = null;
                string flags = null;
                bool inString = false;
                bool inToken = false;
                int percent = 0;

                while (null != (line = sr.ReadLine()))
                {
                    lineNumber++;
                    if (!inString)
                    {
                        line = line.TrimStart();

                        if (line.StartsWith("#", StringComparison.Ordinal))
                        {
                            if (line.StartsWith("#:"))
                                comment = line.Substring(2).TrimStart();
                            else if (line.StartsWith("#,"))
                                flags = line.Substring(1).TrimStart();

                            continue; // Skip line
                        }
                    }
                    else
                        sbTextBuilding.Append('\n');

                    int i = 0;
                    while (i < line.Length)
                    {
                        if (inString)
                        {
                            switch (line[i])
                            {
                                case '\\':
                                    if (++i >= line.Length)
                                        break; // Append newline on next line
                                    switch (line[i])
                                    {
                                        case 'a':
                                            sbTextBuilding.Append('\a');
                                            break;
                                        case 'b':
                                            sbTextBuilding.Append('\b');
                                            break;
                                        case 'f':
                                            sbTextBuilding.Append('\f');
                                            break;
                                        case 'n':
                                            sbTextBuilding.Append('\n');
                                            break;
                                        case 'r':
                                            sbTextBuilding.Append('\r');
                                            break;
                                        case 't':
                                            sbTextBuilding.Append('\t');
                                            break;
                                        case 'v':
                                            sbTextBuilding.Append('\v');
                                            break;
                                        case '\'':
                                        case '\"':
                                        case '?':
                                            sbTextBuilding.Append(line[i]);
                                            break;

                                        case '0':
                                        case '1':
                                        case '2':
                                        case '3':
                                        case '4':
                                        case '5':
                                        case '6':
                                        case '7':
                                        // Octal number
                                        case 'x':
                                            throw new NotImplementedException("Character c escapes not implemented yet");
                                    }
                                    break;
                                case '"':
                                    inString = false;
                                    break;
                                case '%':
                                    percent++;
                                    goto default;
                                default:
                                    sbTextBuilding.Append(line[i]);
                                    break;
                            }
                        }
                        else if (line[i] == '\"')
                        {
                            inString = true;
                            inToken = false;
                        }
                        else if (char.IsWhiteSpace(line, i))
                        {
                            inToken = false;
                        }
                        else
                        {
                            if (!inToken && sbToken.Length > 0)
                            {
                                yield return new Token(sbToken.ToString(), sbTextBuilding.ToString(), comment, flags, lineNumber, percent);
                                percent = 0;
                                comment = null;
                                flags = null;

                                sbToken.Length = 0;
                                sbTextBuilding.Length = 0;

                            }

                            sbToken.Append(line[i]);
                            inToken = true;
                        }
                        i++;
                    }

                    if (string.IsNullOrEmpty(line) && sbToken.Length > 0)
                    {
                        yield return new Token(sbToken.ToString(), sbTextBuilding.ToString(), comment, flags, lineNumber, percent);
                        percent = 0;
                        comment = null;
                        flags = null;

                        sbToken.Length = 0;
                        sbTextBuilding.Length = 0;
                    }
                }

                if (!inToken && sbToken.Length > 0)
                    yield return new Token(sbToken.ToString(), sbTextBuilding.ToString(), comment, flags, lineNumber, percent);
            }
        }
    }
}
