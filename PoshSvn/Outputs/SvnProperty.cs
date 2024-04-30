// Copyright (c) Timofei Zhakov. All rights reserved.

namespace PoshSvn
{
    public class SvnProperty
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Path { get; set; }

        public SvnProperty()
        {
        }

        internal SvnProperty(SharpSvn.SvnPropertyValue property)
        {
            Name = property.Key;
            Value = property.StringValue;
            Path = property.Target.TargetName;
        }
    }
}
