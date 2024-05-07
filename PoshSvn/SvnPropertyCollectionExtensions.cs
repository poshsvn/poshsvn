// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Collections.Generic;

namespace PoshSvn
{
    public static class SvnPropertyCollectionExtensions
    {
        public static SvnProperty[] ToPoshSvnPropertyCollection(this SharpSvn.SvnPropertyCollection properties)
        {
            List<SvnProperty> rv = new List<SvnProperty>(properties.Count);

            foreach (SharpSvn.SvnPropertyValue property in properties)
            {
                rv.Add(new SvnProperty
                {
                    Name = property.Key,
                    Value = property.StringValue,
                    // TODO: ?
                    // Path = property.Target.TargetName
                });
            }

            return rv.ToArray();
        }
    }
}
