// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Collections.Generic;

namespace PoshSvn
{
    public class ResolvedTargetCollection
    {
        public List<SvnResolvedTarget> Targets;
        public List<string> Paths { get; }
        public List<Uri> Urls { get; }

        public bool HasUris => Urls.Count > 0;
        public bool HasPaths => Paths.Count > 0;

        public ResolvedTargetCollection(IEnumerable<SvnResolvedTarget> targets)
        {
            Targets = new List<SvnResolvedTarget>();
            Paths = new List<string>();
            Urls = new List<Uri>();

            foreach (SvnResolvedTarget target in targets)
            {
                if (target.TryGetPath(out string path))
                {
                    Paths.Add(path);
                }
                else if (target.TryGetUrl(out Uri url))
                {
                    Urls.Add(url);
                }

                Targets.Add(target);
            }
        }

        public IEnumerable<SharpSvn.SvnTarget> EnumerateSharpSvnTargets()
        {
            foreach (SvnResolvedTarget target in Targets)
            {
                yield return target.ConvertToSharpSvnTarget();
            }
        }

        public ICollection<SharpSvn.SvnTarget> ConvertToSharpSvnTargets()
        {
            List<SharpSvn.SvnTarget> rv = new List<SharpSvn.SvnTarget>();

            foreach (SvnResolvedTarget target in Targets)
            {
                rv.Add(target.ConvertToSharpSvnTarget());
            }

            return rv;
        }

        public void ThrowIfHasPathsAndUris(string paramName)
        {
            if (HasPaths && HasUris)
            {
                throw new ArgumentException("Cannot mix repository and working copy targets", paramName);
            }
        }

        public void ThrowIfHasAnyOperationalRevisions(string paramName)
        {
            foreach (SvnResolvedTarget target in Targets)
            {
                target.ThrowIfHasOperationalRevision(paramName);
            }
        }
    }
}
