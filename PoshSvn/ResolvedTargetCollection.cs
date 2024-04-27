// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Collections.Generic;

namespace PoshSvn
{
    public class ResolvedTargetCollection
    {
        private readonly ICollection<SvnResolvedTarget> targets;

        public ResolvedTargetCollection(ICollection<SvnResolvedTarget> targets)
        {
            this.targets = targets;
        }

        public IEnumerable<SharpSvn.SvnTarget> EnumerateSharpSvnTargets()
        {
            foreach (SvnResolvedTarget target in targets)
            {
                yield return target.ConvertToSharpSvnTarget();
            }
        }

        public ICollection<SharpSvn.SvnTarget> ConvertToSharpSvnTargets()
        {
            List<SharpSvn.SvnTarget> rv = new List<SharpSvn.SvnTarget>();

            foreach (SvnResolvedTarget target in targets)
            {
                rv.Add(target.ConvertToSharpSvnTarget());
            }

            return rv;
        }
    }
}
