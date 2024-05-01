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
        public const string RevisionPropertyParameterName = "RevisionProperty";

        public IEnumerable<CompletionResult> CompleteArgument(string commandName,
                                                              string parameterName,
                                                              string wordToComplete,
                                                              CommandAst commandAst,
                                                              IDictionary fakeBoundParameters)
        {
            bool isRevprop = GetIsRevisionProperty(fakeBoundParameters);

            foreach (string property in GetPropertiesToComplete(isRevprop))
            {
                if (property.StartsWith(wordToComplete))
                {
                    yield return new CompletionResult(property);
                }
            }
        }

        private static bool GetIsRevisionProperty(IDictionary fakeBoundParameters)
        {
            if (fakeBoundParameters.Contains(RevisionPropertyParameterName))
            {
                return true;
            }
            else
            {
                return false;
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
