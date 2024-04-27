// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;

namespace PoshSvn
{
    public class TargetCollection
    {
        public List<SharpSvn.SvnTarget> Targets;
        public List<string> Paths { get; }
        public List<Uri> Uris { get; }
        public bool HasUris => Uris.Count > 0;
        public bool HasPaths => Paths.Count > 0;

        protected TargetCollection(List<SharpSvn.SvnTarget> targets, List<string> paths, List<Uri> uris)
        {
            Targets = targets;
            Paths = paths;
            Uris = uris;
        }

        public static TargetCollection Parse(IEnumerable targets)
        {
            List<SharpSvn.SvnTarget> targetsList = new List<SharpSvn.SvnTarget>();
            List<string> pathsList = new List<string>();
            List<Uri> urisList = new List<Uri>();

            foreach (object target in targets)
            {
                targetsList.Add(ConvertTargetToSvnTarget(target));

                if (target is string path)
                {
                    pathsList.Add(path);
                }
                else if (target is Uri uri)
                {
                    urisList.Add(uri);
                }
            }

            return new TargetCollection(targetsList, pathsList, urisList);
        }

        public static SharpSvn.SvnTarget ConvertTargetToSvnTarget(object target)
        {
            if (target is string path)
            {
                return SharpSvn.SvnPathTarget.FromString(path, true);
            }
            else if (target is Uri uri)
            {
                return SharpSvn.SvnUriTarget.FromString(uri.ToString(), true);
            }
            else
            {
                throw new ArgumentException(string.Format("Target can only be 'string' or 'Uri', but was '{0}'", target.GetType()), "Target");
            }
        }

        public void ThrowIfHasPathsAndUris()
        {
            if (HasPaths && HasUris)
            {
                throw new ArgumentException("Cannot mix repository and working copy targets", "Target");
            }
        }
    }
}
