// Copyright (c) Timofei Zhakov. All rights reserved.

using System;
using System.Management.Automation;
using SharpSvn;

namespace PoshSvn
{
    public class SvnResolvedTarget
    {
        private string path;
        private Uri url;
        private bool isUrl;

        public SvnResolvedTarget(string path, Uri url, bool isUrl)
        {
            this.path = path;
            this.url = url;
            this.isUrl = isUrl;
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
                return new SharpSvn.SvnUriTarget(url);
            }
            else
            {
                return new SharpSvn.SvnPathTarget(path);
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
