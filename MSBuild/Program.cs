// Copyright (c) Timofei Zhakov. All rights reserved.

using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace MSBuild
{
    public class ConfigureFile : Task
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public string MatchExpression { get; set; }

        [Required]
        public string ReplacementText { get; set; }

        public override bool Execute()
        {
            string text = File.ReadAllText(FileName);

            text = Regex.Replace(text, MatchExpression, ReplacementText);

            File.WriteAllText(FileName, text);

            return true;
        }
    }
}
