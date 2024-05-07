// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Language;

namespace PoshSvn
{
    public class EncodingArgumentCompletions : IArgumentCompleter
    {
        private readonly string[] commonEncodings =
        {
            "ANSI",
            "Ascii",
            "BigEndianUnicode",
            "BigEndianUtf32",
            "OEM",
            "Unicode",
            "Utf7",
            "Utf8",
            "Utf8Bom",
            "Utf8NoBom",
            "Utf32",
        };

        public IEnumerable<CompletionResult> CompleteArgument(string commandName,
                                                              string parameterName,
                                                              string wordToComplete,
                                                              CommandAst commandAst,
                                                              IDictionary fakeBoundParameters)
        {
            foreach (string encoding in commonEncodings)
            {
                if (encoding.StartsWith(wordToComplete))
                {
                    yield return new CompletionResult(encoding, encoding, CompletionResultType.ParameterValue, encoding);
                }
            }
        }
    }
}
