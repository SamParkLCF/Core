using System;

namespace LCF.Core
{
    /// <summary>
    /// Provides information about object death
    /// </summary>
    public interface IObjectDeathInformation
    {
        /// <summary>
        /// Gets the time that define the Object death
        /// </summary>
        DateTime ObjectDeathTime { get; }
        /// <summary>
        /// Gets total time of object living
        /// </summary>
        TimeSpan ObjectLiveTime { get; }
        /// <summary>
        /// Indicates that the object is alive or not
        /// </summary>
        bool IsObjectAlive { get; }
    }
}
