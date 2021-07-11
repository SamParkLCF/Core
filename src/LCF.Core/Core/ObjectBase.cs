using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Humanizer;
using Humanizer.Bytes;

using Serilog.Events;

namespace LCF.Core
{
    public class ObjectBase : IObjectBase
    {
        #region Constructors
        public ObjectBase()
        {
            EnableInteriorLogger();
            InteriorLogs = new ConcurrentQueue<ITelemetryObject>();
            ObjectId = Guid.NewGuid();
            ObjectBirthTime = DateTime.Now;
            LogInteriorInformation($"Object initialized",
                new List<ITelemetryParameters>()
                {
                    new TelemetryParameters<Guid>(nameof(ObjectId),ObjectId),
                    new TelemetryParameters<DateTime>(nameof(ObjectBirthTime),ObjectBirthTime)
                });
        }
        public ObjectBase(Guid objectId)
        {
            EnableInteriorLogger();
            InteriorLogs = new ConcurrentQueue<ITelemetryObject>();
            ObjectId = objectId;
            ObjectBirthTime = DateTime.Now;
            LogInteriorInformation($"Object initialized",
                new List<ITelemetryParameters>()
                {
                    new TelemetryParameters<Guid>(nameof(ObjectId),ObjectId),
                    new TelemetryParameters<DateTime>(nameof(ObjectBirthTime),ObjectBirthTime)
                });
        }
        #endregion

        public Guid ObjectId { get; private set; }

        #region Cloning
        public virtual object Clone() => MemberwiseClone();
        public virtual async Task<object> CloneAsync() => await Task.FromResult(MemberwiseClone());
        public virtual object DeepClone()
        {
            if (Clone() is not ObjectBase _result)
                _result = MemberwiseClone() as ObjectBase;

            _result.ObjectId = Guid.NewGuid();
            _result.ObjectBirthTime = DateTime.Now;
            _result.ObjectDeathTime = DateTime.MinValue;

            return _result;
        }
        public virtual async Task<object> DeepCloneAsync() => await Task.FromResult(DeepClone());
        #endregion

        #region Disposing
#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
        public void Dispose()
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        {
            Dispose(disposing: true);

            ObjectDeathTime = DateTime.Now;

            LogInteriorInformation($"Object Disposed",
                new List<ITelemetryParameters>()
                {
                    new TelemetryParameters<DateTime>(nameof(ObjectDeathTime),ObjectDeathTime),
                    new TelemetryParameters<string>(nameof(ObjectLiveTime),ObjectLiveTime.Humanize())
                });

            SuppressMeForFinalize();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
        }
        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore();

            Dispose(disposing: false);

            ObjectDeathTime = DateTime.Now;

            LogInteriorInformation($"Object Disposed",
                new List<ITelemetryParameters>()
                {
                    new TelemetryParameters<DateTime>(nameof(ObjectDeathTime),ObjectDeathTime),
                    new TelemetryParameters<string>(nameof(ObjectLiveTime),ObjectLiveTime.Humanize())
                });

            SuppressMeForFinalize();
        }
        protected virtual async ValueTask DisposeAsyncCore()
        {
            await Task.CompletedTask;
        }
        #endregion

        #region ObjectBornInformation
        public DateTime ObjectBirthTime { get; private set; } = DateTime.MinValue;
        #endregion

        #region ObjectDeathInformation
        public DateTime ObjectDeathTime { get; private set; } = DateTime.MinValue;
        public TimeSpan ObjectLiveTime => ObjectDeathTime == DateTime.MinValue ? DateTime.Now - ObjectBirthTime : ObjectDeathTime - ObjectBirthTime;
        public bool IsObjectAlive => ObjectBirthTime != DateTime.MinValue && ObjectDeathTime == DateTime.MinValue;
        #endregion

        #region Object Summary
        public virtual IObjectLifeTimeSummary GetObjectLifeTimeSummary()
        {
            IObjectBornInformation _objectBornInformation = new ObjectBornInformation(ObjectBirthTime);
            IObjectDeathInformation _objectDeathInformation = new ObjectDeathInformation(ObjectDeathTime, ObjectLiveTime, IsObjectAlive);

            return new ObjectLifeTimeSummary(_objectBornInformation, _objectDeathInformation);
        }
        public virtual IObjectSummary GetObjectSummary()
        {
            Type _type = GetType();
            IObjectSummary _Summary = new ObjectSummary(ObjectId,
                _type.Name, _type.FullName, _type.AssemblyQualifiedName, GetBaseTypes(_type),
                GetObjectLifeTimeSummary());

            return _Summary;
        }
        public override string ToString() => GetObjectSummary().ToString();
        public string GetBaseTypes(Type type)
        {
            if (type.BaseType != null)
                return $"[{type.BaseType.Name}] -> " + GetBaseTypes(type.BaseType);
            else
                return "[END]";
        }
        #endregion

        #region ObjectMemory
        public int GetMyGeneration() => GC.GetGeneration(this);
#pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
        public void SuppressMeForFinalize() => GC.SuppressFinalize(this);
#pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        public void KeepMeAlive() => GC.KeepAlive(this);
        public void ReRegisterMeForFinalize() => GC.ReRegisterForFinalize(this);

        public ByteSize GetAllocatedBytesForCurrentThread() => GC.GetAllocatedBytesForCurrentThread().Bytes();
        public ByteSize GetTotalMemory_WITH_ForceFullCollection() => GC.GetTotalMemory(forceFullCollection: true).Bytes();
        public ByteSize GetTotalMemory_WITHOUT_ForceFullCollection() => GC.GetTotalMemory(forceFullCollection: false).Bytes();
        public ByteSize GetTotalAllocatedBytes_WITH_Precise() => GC.GetTotalAllocatedBytes(precise: true).Bytes();
        public ByteSize GetTotalAllocatedBytes_WITHOUT_Precise() => GC.GetTotalAllocatedBytes(precise: false).Bytes();

        public void CollectMyGenerationByForced(bool blocking = false, bool compacting = false) =>
            GC.Collect(GetMyGeneration(), GCCollectionMode.Forced, blocking, compacting);
        public void CollectMyGenerationByOptimizedMode(bool blocking = false, bool compacting = false) =>
            GC.Collect(GetMyGeneration(), GCCollectionMode.Optimized, blocking, compacting);

        public void CollectAllGenerationsByForcedMode(bool blocking = false, bool compacting = false) =>
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, blocking, compacting);
        public void CollectAllGenerationsByOptimizedMode(bool blocking = false, bool compacting = false) =>
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Optimized, blocking, compacting);

        public GCNotificationStatus WaitForFullGCApproach(int millisecondsTimeout = 0) =>
            GC.WaitForFullGCApproach(millisecondsTimeout);
        public GCNotificationStatus WaitForFullGCComplete(int millisecondsTimeout = 0) =>
            GC.WaitForFullGCComplete(millisecondsTimeout);
        public void WaitForPendingFinalizers() => GC.WaitForPendingFinalizers();
        #endregion

        #region InteriorLogging
        public void EnableInteriorLogger() => IsInteriorLoggerEnabled = true;
        public void DisableIntriorLogger() => IsInteriorLoggerEnabled = false;
        public bool IsInteriorLoggerEnabled { get; private set; }

        public void FlushInteriorLogger([CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0)
        {
            InteriorLogs.Clear();

            bool _lastInteriorLoggerState = IsInteriorLoggerEnabled;
            EnableInteriorLogger();
            LogInteriorInformation("Flushed",
                callerName, callerFilePath, callerLineNumber);
            if (!_lastInteriorLoggerState)
                DisableIntriorLogger();
        }
        internal ConcurrentQueue<ITelemetryObject> InteriorLogs { get; private set; }
        public Queue<ITelemetryObject> GetInteriorLogs() => new(InteriorLogs.ToArray());
        public void LogInterior(ITelemetryObject telemetryObject)
        {
            if (IsInteriorLoggerEnabled)
                InteriorLogs.Enqueue(telemetryObject);
        }

        public void LogInteriorInformation(string message,
            [CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) =>
            LogInterior(new TelemetryObject(DateTime.Now, LogEventLevel.Information, message,
                new CallerInformation(callerName, callerFilePath, callerLineNumber)));
        public void LogInteriorInformation(string message, List<ITelemetryParameters> parameters,
           [CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) =>
           LogInterior(new TelemetryObject(DateTime.Now, LogEventLevel.Information, message,
               new CallerInformation(callerName, callerFilePath, callerLineNumber),
               parameters));
        public void LogInteriorInformation(string message, ICallerInformation callerInformation) =>
            LogInterior(new TelemetryObject(DateTime.Now, LogEventLevel.Information, message,
                callerInformation));
        public void LogInteriorInformation(string message, ICallerInformation callerInformation, List<ITelemetryParameters> parameters) =>
            LogInterior(new TelemetryObject(DateTime.Now, LogEventLevel.Information, message,
                callerInformation,
                parameters));

        public void LogInteriorWarning(string message,
            [CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) =>
            LogInterior(new TelemetryObject(DateTime.Now, LogEventLevel.Warning, message,
                new CallerInformation(callerName, callerFilePath, callerLineNumber)));
        public void LogInteriorWarning(string message, Exception exception,
            [CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) =>
            LogInterior(new TelemetryObject(DateTime.Now, LogEventLevel.Warning, message,
                new CallerInformation(callerName, callerFilePath, callerLineNumber), exception));
        public void LogInteriorWarning(string message, List<ITelemetryParameters> parameters,
            [CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) =>
            LogInterior(new TelemetryObject(DateTime.Now, LogEventLevel.Warning, message,
                new CallerInformation(callerName, callerFilePath, callerLineNumber),
                parameters));
        public void LogInteriorWarning(string message, List<ITelemetryParameters> parameters, Exception exception,
            [CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) =>
            LogInterior(new TelemetryObject(DateTime.Now, LogEventLevel.Warning, message,
                new CallerInformation(callerName, callerFilePath, callerLineNumber), exception,
                parameters));

        public void LogInteriorError(string message, Exception exception,
            [CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) =>
            LogInterior(new TelemetryObject(DateTime.Now, LogEventLevel.Error, message,
                new CallerInformation(callerName, callerFilePath, callerLineNumber), exception));
        public void LogInteriorError(string message, List<ITelemetryParameters> parameters, Exception exception,
            [CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) =>
            LogInterior(new TelemetryObject(DateTime.Now, LogEventLevel.Error, message,
                new CallerInformation(callerName, callerFilePath, callerLineNumber), exception,
                parameters));

        public void LogInteriorFatal(string message, Exception exception,
           [CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) =>
           LogInterior(new TelemetryObject(DateTime.Now, LogEventLevel.Fatal, message,
               new CallerInformation(callerName, callerFilePath, callerLineNumber), exception));
        public void LogInteriorFatal(string message, List<ITelemetryParameters> parameters, Exception exception,
            [CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0) =>
            LogInterior(new TelemetryObject(DateTime.Now, LogEventLevel.Fatal, message,
                new CallerInformation(callerName, callerFilePath, callerLineNumber), exception,
                parameters));
        #endregion
    }
}
