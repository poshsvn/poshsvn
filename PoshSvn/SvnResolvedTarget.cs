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
    }
}
