using SharpSvn;

namespace PoshSvn
{
    public class SvnNotifyOutput
    {
        public SvnNotifyAction Action { get; set; }
        public string ActionString => SvnUtils.GetActionStringShort(Action);
        public string Path { get; set; }
    }
}
