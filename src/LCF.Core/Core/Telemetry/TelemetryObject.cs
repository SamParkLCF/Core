using System;
using System.Collections.Generic;

using Serilog.Events;

namespace LCF.Core
{
    public class TelemetryObject : ITelemetryObject
    {
        public TelemetryObject(DateTime dateTime, LogEventLevel eventLevel, string message,
            ICallerInformation callerInformation,
            params ITelemetryParameters[] parameters)
        {
            DateTime = dateTime;
            LogEventLevel = eventLevel;
            Message = message;
            Parameters = new List<ITelemetryParameters>(parameters);
            CallerInformation = callerInformation;
        }
        public TelemetryObject(DateTime dateTime, LogEventLevel eventLevel, string message,
            ICallerInformation callerInformation,
            Exception exception,
            params ITelemetryParameters[] parameters)
        {
            DateTime = dateTime;
            LogEventLevel = eventLevel;
            Message = message;
            Exception = exception;
            Parameters = new List<ITelemetryParameters>(parameters);
            CallerInformation = callerInformation;
        }
        public TelemetryObject(DateTime dateTime, LogEventLevel eventLevel, string message,
           ICallerInformation callerInformation,
           List<ITelemetryParameters> parameters)
        {
            DateTime = dateTime;
            LogEventLevel = eventLevel;
            Message = message;
            Parameters = new List<ITelemetryParameters>(parameters);
            CallerInformation = callerInformation;
        }
        public TelemetryObject(DateTime dateTime, LogEventLevel eventLevel, string message,
            ICallerInformation callerInformation,
            Exception exception,
            List<ITelemetryParameters> parameters)
        {
            DateTime = dateTime;
            LogEventLevel = eventLevel;
            Message = message;
            Exception = exception;
            Parameters = new List<ITelemetryParameters>(parameters);
            CallerInformation = callerInformation;
        }

        public DateTime DateTime { get; protected set; }
        public LogEventLevel LogEventLevel { get; protected set; }
        public string Message { get; protected set; }
        public Exception Exception { get; protected set; }
        public List<ITelemetryParameters> Parameters { get; }
        public ICallerInformation CallerInformation { get; protected set; }

        public override string ToString() => JsonHelper.SerializeObject(this);
    }
}
