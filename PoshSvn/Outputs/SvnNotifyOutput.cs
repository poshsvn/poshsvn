// Copyright (c) Timofei Zhakov. All rights reserved.

using System.Management.Automation;

namespace PoshSvn
{
    public class SvnNotifyOutput
    {
        public SvnNotifyAction Action { get; set; }
        public string ActionString => GetActionString(Action);
        public string Path { get; set; }

        public override string ToString()
        {
            return Format(ActionString, Path);
        }

        public string ToString(EngineIntrinsics context)
        {
            return Format(ActionString, PathUtils.FormatRelativePath(context, Path));
        }

        private static string Format(string actionString, string path)
        {
            return string.Format("{0,-7} {1}", actionString, path);
        }

        // TODO: Add more logic.
        // TODO: Add some spaces before action like in svn.exe
        // - Reference: https://svn.rinrab.com/!/#rinrab/view/r884/subversion/1.14.3/subversion/svn/notify.c?line=751
        // - Reference: https://svn.rinrab.com/!/#rinrab/view/r884/subversion/1.14.3/subversion/svn/notify.c?line=1096
        public static string GetActionString(SvnNotifyAction action)
        {
            switch (action)
            {
                case SvnNotifyAction.UpdateAdd:
                case SvnNotifyAction.Add:
                case SvnNotifyAction.CommitAdded:
                case SvnNotifyAction.CommitAddCopy:
                    return "A";

                case SvnNotifyAction.UpdateDelete:
                case SvnNotifyAction.Delete:
                case SvnNotifyAction.CommitDeleted:
                    return "D";

                case SvnNotifyAction.CommitModified:
                    return "M";

                case SvnNotifyAction.UpdateUpdate:
                case SvnNotifyAction.RecordMergeInfo:
                    return "U";

                case SvnNotifyAction.CommitReplaced:
                case SvnNotifyAction.CommitReplacedWithCopy:
                case SvnNotifyAction.UpdateReplace:
                    return "R";

                case SvnNotifyAction.LockLocked:
                    return "+L";

                case SvnNotifyAction.LockUnlocked:
                    return "-L";

                case SvnNotifyAction.TreeConflict:
                    return "C";

                case SvnNotifyAction.UpgradedDirectory:
                    return "Upgraded";

                // case SvnNotifyAction.UpdateMerge: return "G";
                // case SvnNotifyAction.UpdateExist: return "E";

                default:
                    return action.ToString();
            }
        }
    }
}
