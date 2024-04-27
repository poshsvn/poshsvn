// Copyright (c) Timofei Zhakov. All rights reserved.

using System;

namespace PoshSvn
{
    public class SvnResolvedTarget
    {
        private string path;
        private Uri url;
        private bool isUrl;
        private SvnRevision revision;

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

        //public bool TryConvertToUriTarget(bool allowOperationalRevision, out SvnUriTarget svnUriTarget)
        //{
        //    return SvnUriTarget.TryParse(Value, allowOperationalRevision, out svnUriTarget);
        //}

        //public bool TryConvertToSharpSvnTarget()
        //{
        //    if (Type == SvnTargetType.Url)
        //    {
        //        return TryConvertToUriTarget();
        //    }
        //    else
        //    {

        //    }
        //}
    }
}
