using System;

using Humanizer.Bytes;

namespace LCF.Core
{
    /// <summary>
    /// Provides memory functionalities which related to the object
    /// </summary>
    public interface IObjectMemory
    {
        /// <summary>
        /// Returns the current generation number of the the object.
        /// </summary>
        /// <returns>The current generation number of obj.</returns>
        int GetMyGeneration();
        /// <summary>
        /// Requests that the common language runtime not call the finalizer for the specified object.
        /// </summary>
        void SuppressMeForFinalize();
        /// <summary>
        /// References the specified object, which makes it ineligible for garbage collection 
        /// from the start of the current routine to the point where this method is called.
        /// </summary>
        void KeepMeAlive();
        /// <summary>
        /// Requests that the system call the finalizer for the specified object for which 
        /// System.GC.SuppressFinalize(System.Object) has previously been called.
        /// </summary>
        void ReRegisterMeForFinalize();

        /// <summary>
        /// Gets the total number of bytes allocated to the current thread since the beginning of its lifetime.
        /// </summary>
        /// <returns>The total number of bytes allocated to the current thread since the beginning of its lifetime. With Humanized</returns>
        ByteSize GetAllocatedBytesForCurrentThread();
        /// <summary>
        /// Retrieves the number of bytes currently thought to be allocated. A parameter
        /// indicates whether this method can wait a short interval before returning, to
        /// allow the system to collect garbage and finalize objects.<para/>
        /// this method WAITS for garbage collection to occur before returning
        /// </summary>
        /// <returns>A number that is the best available approximation of the number of bytes currently 
        /// allocated in managed memory.</returns>
        ByteSize GetTotalMemory_WITH_ForceFullCollection();
        /// <summary>
        /// Retrieves the number of bytes currently thought to be allocated. A parameter
        /// indicates whether this method can wait a short interval before returning, to
        /// allow the system to collect garbage and finalize objects.<para/>
        /// this method DOES NOT WAIT for garbage collection to occur before returning
        /// </summary>
        /// <returns>A number that is the best available approximation of the number of bytes currently 
        /// allocated in managed memory.</returns>
        ByteSize GetTotalMemory_WITHOUT_ForceFullCollection();
        /// <summary>
        /// Gets a count of the bytes allocated over the lifetime of the process.<para/>
        /// gather a precise number, Gathering a precise value entails a significant performance penalty.
        /// </summary>
        /// <returns>The total number of bytes allocated over the lifetime of the process.</returns>
        ByteSize GetTotalAllocatedBytes_WITH_Precise();
        /// <summary>
        /// Gets a count of the bytes allocated over the lifetime of the process.
        /// </summary>
        /// <returns>The total number of bytes allocated over the lifetime of the process.</returns>
        ByteSize GetTotalAllocatedBytes_WITHOUT_Precise();

        /// <summary>
        /// Forces an immediate garbage collection in object generation. WITH GC FORCED collection mode
        /// </summary>
        /// <param name="blocking">true to perform a blocking garbage collection; false to perform a background garbage collection where possible.</param>
        /// <param name="compacting">true to compact the small object heap; false to sweep only.</param>
        void CollectMyGenerationByForced(bool blocking = false, bool compacting = false);
        /// <summary>
        /// Forces an immediate garbage collection in object generation. WITH GC OPTIMIZED collection mode
        /// </summary>
        /// <param name="blocking">true to perform a blocking garbage collection; false to perform a background garbage collection where possible.</param>
        /// <param name="compacting">true to compact the small object heap; false to sweep only.</param>
        void CollectMyGenerationByOptimizedMode(bool blocking = false, bool compacting = false);

        /// <summary>
        /// Forces a garbage collection for ALL generations. WITH GC FORCED collection mode
        /// </summary>
        /// <param name="blocking">true to perform a blocking garbage collection; false to perform a background garbage collection where possible.</param>
        /// <param name="compacting">true to compact the small object heap; false to sweep only.</param>
        void CollectAllGenerationsByForcedMode(bool blocking = false, bool compacting = false);
        /// <summary>
        /// Forces a garbage collection for ALL generations. WITH GC OPTIMIZED collection mode
        /// </summary>
        /// <param name="blocking">true to perform a blocking garbage collection; false to perform a background garbage collection where possible.</param>
        /// <param name="compacting">true to compact the small object heap; false to sweep only.</param>
        void CollectAllGenerationsByOptimizedMode(bool blocking = false, bool compacting = false);

        /// <summary>
        /// Returns, in a specified time-out period, the status of a registered notification
        /// for determining whether a full, blocking garbage collection by the common language runtime is imminent.
        /// </summary>
        /// <param name="millisecondsTimeout"> The length of time to wait before a notification status can be obtained. 
        /// Specify -1 to wait indefinitely.</param>
        /// <returns>The status of the registered garbage collection notification.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">millisecondsTimeout must be either non-negative or less than or equal to System.Int32.MaxValue or -1.</exception>
        GCNotificationStatus WaitForFullGCApproach(int millisecondsTimeout = 0);
        /// <summary>
        /// Returns, in a specified time-out period, the status of a registered notification
        /// for determining whether a full, blocking garbage collection by the common language runtime has completed.
        /// </summary>
        /// <param name="millisecondsTimeout"> The length of time to wait before a notification status can be obtained. 
        /// Specify -1 to wait indefinitely.</param>
        /// <returns>The status of the registered garbage collection notification.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">millisecondsTimeout must be either non-negative or less than or equal to System.Int32.MaxValue or -1.</exception>
        GCNotificationStatus WaitForFullGCComplete(int millisecondsTimeout = 0);
        /// <summary>
        /// Suspends the current thread until the thread that is processing the queue of finalizers has emptied that queue.
        /// </summary>
        void WaitForPendingFinalizers();
    }
}
