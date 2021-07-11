using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LCF.Core
{
    /// <summary>
    /// Provides base features for the object that is in the base-role
    /// </summary>
    public interface IObjectBase : ICloneableBase, IAsyncCloneableBase,
        IDisposable, IAsyncDisposable,
        IObjectBornInformation, IObjectDeathInformation,
        IObjectMemory
    {
        /// <summary>
        /// Gets the Id of the object. this Is creates when object is initialized.
        /// </summary>
        Guid ObjectId { get; }

        /// <summary>
        /// Gets life time summary of the object
        /// </summary>
        /// <returns>Object life time summary</returns>
        IObjectLifeTimeSummary GetObjectLifeTimeSummary();
        /// <summary>
        /// Gets total summary of the object
        /// </summary>
        /// <returns>Object total summary</returns>
        IObjectSummary GetObjectSummary();

        /// <summary>
        /// Gets base types of the object type
        /// </summary>
        /// <param name="type">The type which will check for base types</param>
        /// <returns>Chain of base types in string</returns>
        string GetBaseTypes(Type type);

        void EnableInteriorLogger();
        void DisableIntriorLogger();
        bool IsInteriorLoggerEnabled { get; }
        /// <summary>
        /// Clears all interior logs
        /// </summary>
        void FlushInteriorLogger([CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);
        /// <summary>
        /// Gets object interior logs
        /// </summary>
        /// <returns>Queue of registered logs</returns>
        Queue<ITelemetryObject> GetInteriorLogs();
        /// <summary>
        /// Logs the interior object related action
        /// </summary>
        /// <param name="telemetryObject">Telemetry object to log</param>
        void LogInterior(ITelemetryObject telemetryObject);

        /// <summary>
        /// Logs the interior object related Info action
        /// </summary>
        /// <param name="message">Info message to log</param>
        /// <param name="callerName">Caller name of the Info log</param>
        /// <param name="callerFilePath">Caller file path of the Info log</param>
        /// <param name="callerLineNumber">Caller line number of the Info log</param>
        void LogInteriorInformation(string message,
            [CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);
        /// <summary>
        /// Logs the interior object related Info action
        /// </summary>
        /// <param name="message">Info message to log</param>
        /// <param name="parameters">Parameters related to Info log</param>
        /// <param name="callerName">Caller name of the Info log</param>
        /// <param name="callerFilePath">Caller file path of the Info log</param>
        /// <param name="callerLineNumber">Caller line number of the Info log</param>
        void LogInteriorInformation(string message, List<ITelemetryParameters> parameters,
           [CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);

        /// <summary>
        /// Logs the interior object related Warning action
        /// </summary>
        /// <param name="message">Warning message to log</param>
        /// <param name="callerName">Caller name of the Warning log</param>
        /// <param name="callerFilePath">Caller file path of the Warning log</param>
        /// <param name="callerLineNumber">Caller line number of the Warning log</param>
        void LogInteriorWarning(string message,
            [CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);
        /// <summary>
        /// Logs the interior object related Warning action
        /// </summary>
        /// <param name="message">Warning message to log</param>
        /// <param name="exception">Exception related to Warning log</param>
        /// <param name="callerName">Caller name of the Warning log</param>
        /// <param name="callerFilePath">Caller file path of the Warning log</param>
        /// <param name="callerLineNumber">Caller line number of the Warning log</param>
        void LogInteriorWarning(string message, Exception exception,
            [CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);
        /// <summary>
        /// Logs the interior object related Warning action
        /// </summary>
        /// <param name="message">Warning message to log</param>
        /// <param name="parameters">Parameters related to Warning log</param>
        /// <param name="callerName">Caller name of the Warning log</param>
        /// <param name="callerFilePath">Caller file path of the Warning log</param>
        /// <param name="callerLineNumber">Caller line number of the Warning log</param>
        void LogInteriorWarning(string message, List<ITelemetryParameters> parameters,
            [CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);
        /// <summary>
        /// Logs the interior object related Warning action
        /// </summary>
        /// <param name="message">Warning message to log</param>
        /// <param name="parameters">Parameters related to Warning log</param>
        /// <param name="exception">Exception related to Warning log</param>
        /// <param name="callerName">Caller name of the Warning log</param>
        /// <param name="callerFilePath">Caller file path of the Warning log</param>
        /// <param name="callerLineNumber">Caller line number of the Warning log</param>
        void LogInteriorWarning(string message, List<ITelemetryParameters> parameters, Exception exception,
            [CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);

        /// <summary>
        /// Logs the interior object related Error action
        /// </summary>
        /// <param name="message">Error message to log</param>
        /// <param name="exception">Exception related to Error log</param>
        /// <param name="callerName">Caller name of the Error log</param>
        /// <param name="callerFilePath">Caller file path of the Error log</param>
        /// <param name="callerLineNumber">Caller line number of the Error log</param>
        void LogInteriorError(string message, Exception exception,
            [CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);
        /// <summary>
        /// Logs the interior object related Error action
        /// </summary>
        /// <param name="message">Error message to log</param>
        /// <param name="parameters">Parameters related to Error log</param>
        /// <param name="exception">Exception related to Error log</param>
        /// <param name="callerName">Caller name of the Error log</param>
        /// <param name="callerFilePath">Caller file path of the Error log</param>
        /// <param name="callerLineNumber">Caller line number of the Error log</param>
        void LogInteriorError(string message, List<ITelemetryParameters> parameters, Exception exception,
            [CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);

        /// <summary>
        /// Logs the interior object related Fatal action
        /// </summary>
        /// <param name="message">Fatal message to log</param>
        /// <param name="exception">Exception related to Fatal log</param>
        /// <param name="callerName">Caller name of the Fatal log</param>
        /// <param name="callerFilePath">Caller file path of the Fatal log</param>
        /// <param name="callerLineNumber">Caller line number of the Fatal log</param>
        void LogInteriorFatal(string message, Exception exception,
           [CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);
        /// <summary>
        /// Logs the interior object related Fatal action
        /// </summary>
        /// <param name="message">Fatal message to log</param>
        /// <param name="parameters">Parameters related to Fatal log</param>
        /// <param name="exception">Exception related to Fatal log</param>
        /// <param name="callerName">Caller name of the Fatal log</param>
        /// <param name="callerFilePath">Caller file path of the Fatal log</param>
        /// <param name="callerLineNumber">Caller line number of the Fatal log</param>
        void LogInteriorFatal(string message, List<ITelemetryParameters> parameters, Exception exception,
            [CallerMemberName] string callerName = "", [CallerFilePath] string callerFilePath = "", [CallerLineNumber] int callerLineNumber = 0);
    }
}
