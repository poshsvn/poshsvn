// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Language;

namespace PoshSvn
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ArgumentEncodingCompletionsAttribute : Attribute, IArgumentCompleter
    {
        private readonly string[] completions =
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
            string pattern = string.IsNullOrWhiteSpace(wordToComplete) ? "*" : wordToComplete + "*";
            WildcardPattern wordToCompletePattern = WildcardPattern.Get(pattern, WildcardOptions.IgnoreCase);

            foreach (string str in completions)
            {
                if (wordToCompletePattern.IsMatch(str))
                {
                    yield return new CompletionResult(str, str, CompletionResultType.ParameterValue, str);
                }
            }
        }
    }
}
