// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using SharpSvn;

namespace PoshSvn
{
    public class TargetCollection
    {
        public List<SvnTarget> Targets;
        public List<string> Paths { get; }
        public List<Uri> Uris { get; }
        public bool HasUris => Uris.Count > 0;
        public bool HasPaths => Paths.Count > 0;

        protected TargetCollection(List<SvnTarget> targets, List<string> paths, List<Uri> uris)
        {
            Targets = targets;
            Paths = paths;
            Uris = uris;
        }

        public static TargetCollection Parse(IEnumerable targets)
        {
            List<SvnTarget> targetsList = new List<SvnTarget>();
            List<string> pathsList = new List<string>();
            List<Uri> urisList = new List<Uri>();

            foreach (object target in targets)
            {
                if (target is string path)
                {
                    targetsList.Add(SvnPathTarget.FromString(path));
                    pathsList.Add(path);
                }
                else if (target is Uri uri)
                {
                    targetsList.Add(SvnUriTarget.FromUri(uri));
                    urisList.Add(uri);
                }
                else
                {
                    throw new ArgumentException("Target can only be 'string' or 'Uri'", "Target");
                }
            }

            return new TargetCollection(targetsList, pathsList, urisList);
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
