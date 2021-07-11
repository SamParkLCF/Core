using System;

namespace LCF.Core
{
    public class ObjectDeathInformation : IObjectDeathInformation
    {
        public ObjectDeathInformation(DateTime deathTime, TimeSpan liveTime, bool isALive)
        {
            ObjectDeathTime = deathTime;
            ObjectLiveTime = liveTime;
            IsObjectAlive = isALive;
        }
        public DateTime ObjectDeathTime { get; }
        public TimeSpan ObjectLiveTime { get; }
        public bool IsObjectAlive { get; }

        public override string ToString() => JsonHelper.SerializeObject(this);
    }
}
