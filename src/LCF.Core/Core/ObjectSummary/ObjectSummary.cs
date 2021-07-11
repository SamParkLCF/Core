using System;

namespace LCF.Core
{
    public class ObjectSummary : IObjectSummary
    {
        public ObjectSummary(Guid id, string name, string filePath, string assemblyName, string baseTypes,
            IObjectLifeTimeSummary lifeTimeSummary)
        {
            ObjectId = id;
            ObjectName = name;
            ObjectFilePath = filePath;
            AssemblyName = assemblyName;
            BaseTypes = baseTypes;
            ObjectLifeTimeSummary = lifeTimeSummary;
        }
        public Guid ObjectId { get; }
        public string ObjectName { get; }
        public string ObjectFilePath { get; }
        public IObjectLifeTimeSummary ObjectLifeTimeSummary { get; }
        public string AssemblyName { get; }
        public string BaseTypes { get; }

        public override string ToString() => JsonHelper.SerializeObject(this);
    }
}
