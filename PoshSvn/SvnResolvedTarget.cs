// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn
{
    public class SvnResolvedTarget
    {
        private readonly string path;
        private readonly Uri url;
        private readonly bool isUrl;
        private readonly SvnRevision revision;

        public SvnResolvedTarget(string path, Uri url, bool isUrl, SvnRevision revision)
        {
            this.path = path;
            this.url = url;
            this.isUrl = isUrl;
            this.revision = revision;
        }

        public bool TryGetUrl(out Uri url)
        {
            if (isUrl)
            {
                url = this.url;
                return true;
            }
            else
            {
                url = null;
                return false;
            }
        }

        public bool TryGetPath(out string path)
        {
            if (isUrl)
            {
                path = null;
                return false;
            }
            else
            {
                path = this.path;
                return true;
            }
        }

        public SharpSvn.SvnTarget ConvertToSharpSvnTarget()
        {
            if (isUrl)
            {
                return new SharpSvn.SvnUriTarget(url, GetSharpSvnRevision());
            }
            else
            {
                return new SharpSvn.SvnPathTarget(path, GetSharpSvnRevision());
            }
        }

        public SharpSvn.SvnUriTarget ConvertToSharpSvnUriTarget(string paramName)
        {
            if (isUrl)
            {
                return new SharpSvn.SvnUriTarget(url, GetSharpSvnRevision());
            }
            else
            {
                throw new ArgumentException("The target is not an Url.", paramName);
            }
        }

        public SharpSvn.SvnRevision GetSharpSvnRevision()
        {
            if (revision == null)
            {
                return null;
            }
            else
            {
                return revision.ToSharpSvnRevision();
            }
        }

        public void ThrowIfHasOperationalRevision(string paramName)
        {
            if (revision != null)
            {
                throw new ArgumentException("A peg revision is not allowed here", paramName);
            }
        }
    }
}
