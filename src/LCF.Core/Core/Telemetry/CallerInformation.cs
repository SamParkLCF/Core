using System.Runtime.CompilerServices;

namespace LCF.Core
{
    public class CallerInformation : ICallerInformation
    {
        public CallerInformation([CallerMemberName] string name = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            Name = name;
            FilePath = filePath;
            LineNumber = lineNumber;
        }

        public string Name { get; protected set; }
        public string FilePath { get; protected set; }
        public int LineNumber { get; protected set; }

        public override string ToString() => JsonHelper.SerializeObject(this);
    }
}
