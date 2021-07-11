using System;

namespace LCF.Core
{
    /// <summary>
    /// Supports cloning and deep cloning of an instance.
    /// </summary>
    public interface ICloneableBase : ICloneable
    {
        /// <summary>
        /// Creates a new object that is a deep copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a deep copy of this instance.</returns>
        object DeepClone();
    }
}
