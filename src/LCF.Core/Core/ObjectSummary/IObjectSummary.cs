using System;

namespace LCF.Core
{
    public interface IObjectSummary
    {
        Guid ObjectId { get; }
        string ObjectName { get; }
        string ObjectFilePath { get; }
        string AssemblyName { get; }
        string BaseTypes { get; }
        IObjectLifeTimeSummary ObjectLifeTimeSummary { get; }
    }
}
