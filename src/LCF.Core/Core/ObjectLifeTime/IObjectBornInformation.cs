using System;

namespace LCF.Core
{
    /// <summary>
    /// Provides information about object born.
    /// </summary>
    public interface IObjectBornInformation
    {
        /// <summary>
        /// Gets the time that defines object birth.
        /// </summary>
        DateTime ObjectBirthTime { get; }
    }
}
