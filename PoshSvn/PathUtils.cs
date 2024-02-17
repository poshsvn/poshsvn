namespace PoshSvn
{
    public class PathUtils
    {
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
