using System;

namespace LCF.Core
{
    public class ObjectBornInformation : IObjectBornInformation
    {
        public ObjectBornInformation(DateTime birthTime)
        {
            ObjectBirthTime = birthTime;
        }
        
        public DateTime ObjectBirthTime { get; }

        public override string ToString() => JsonHelper.SerializeObject(this);
    }
}
