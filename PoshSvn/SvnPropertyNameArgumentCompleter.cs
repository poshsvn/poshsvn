// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Management.Automation.Language;
using SharpSvn;

namespace PoshSvn
{
    public class SvnPropertyNameArgumentCompleter : IArgumentCompleter
    {
        public IEnumerable<CompletionResult> CompleteArgument(string commandName,
                                                              string parameterName,
                                                              string wordToComplete,
                                                              CommandAst commandAst,
                                                              IDictionary fakeBoundParameters)
        {
            // TODO: use real isRevprop
            foreach (string property in GetPropertiesToComplete(false))
            {
                if (property.StartsWith(wordToComplete))
                {
                    yield return new CompletionResult(property);
                }
            }
        }

        private static IEnumerable<string> GetPropertiesToComplete(bool isRevprop)
        {
            if (isRevprop)
            {
                return SvnPropertyNames.AllSvnRevisionProperties;
            }
            else
            {
                return SvnPropertyNames.AllSvnNodeProperties;
            }
        }
    }
}
