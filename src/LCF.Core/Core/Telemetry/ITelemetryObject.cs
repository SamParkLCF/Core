
using System;
using System.Collections.Generic;

using Serilog.Events;

namespace LCF.Core
{
    public interface ITelemetryObject
    {
        DateTime DateTime { get; }
        LogEventLevel LogEventLevel { get; }
        string Message { get; }
        Exception Exception { get; }
        List<ITelemetryParameters> Parameters { get; }
        ICallerInformation CallerInformation { get; }
    }
}
