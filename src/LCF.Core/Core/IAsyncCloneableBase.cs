using System.Threading.Tasks;

namespace LCF.Core
{
    /// <summary>
    /// Supports cloning and deep cloning of an instance. But in async way
    /// </summary>
    public interface IAsyncCloneableBase
    {
        /// <summary>
        /// Creates a new object that is a copy of the current instance. But in async way
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        Task<object> CloneAsync();
        /// <summary>
        /// Creates a new object that is a deep copy of the current instance. But in async way
        /// </summary>
        /// <returns>A new object that is a deep copy of this instance.</returns>
        Task<object> DeepCloneAsync();
    }
}
