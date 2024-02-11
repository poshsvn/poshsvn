using System;
using System.IO;

namespace SvnPosh.Tests.TestUtils
{
    public static class IOUtils
    {
        public static void DeleteDir(string path)
        {
            DeleteContent(path);
            Directory.Delete(path);
        }

        private static void DeleteContent(string dir)
        {

            foreach (var child in new DirectoryInfo(dir).EnumerateFileSystemInfos())
            {
                bool isDir = child.Attributes.HasFlag(FileAttributes.Directory);

                if (isDir)
                {
                    DeleteContent(child.FullName);
                }

                try
                {
                    child.Delete();
                }
                catch (UnauthorizedAccessException)
                {
                    child.Attributes = child.Attributes & ~FileAttributes.ReadOnly;
                    child.Delete();
                }
            }
        }
    }
}
