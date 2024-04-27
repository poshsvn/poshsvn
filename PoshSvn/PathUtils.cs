// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Management.Automation;

namespace PoshSvn
{
    public class PathUtils
    {
        public static string FormatRelativePath(EngineIntrinsics context, string path)
        {
            PathInfo root = context.SessionState.Path.CurrentFileSystemLocation;

            return GetRelativePath(root.Path, path);
        }

        public static string GetRelativePath(string root, string path)
        {
            if (path.StartsWith(root + "\\"))
            {
                return path.Substring(root.Length + 1);
            }
            else if (root == path)
            {
                return ".";
            }
            else
            {
                return path;
            }
        }
    }
}
