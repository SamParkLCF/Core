using System;

using Newtonsoft.Json;

using Serilog.Events;

namespace LCF.Core.JsonHelpers
{
    public class LogEventLevelJsonConverter : JsonConverter<LogEventLevel>
    {
        public override LogEventLevel ReadJson(JsonReader reader, Type objectType, LogEventLevel existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var enumString = (string)reader.Value;

            return Enum.Parse<LogEventLevel>(enumString, true);
        }

        public override void WriteJson(JsonWriter writer, LogEventLevel value, JsonSerializer serializer)
        {
            switch (value)
            {
                case LogEventLevel.Information:
                    writer.WriteValue(nameof(LogEventLevel.Information));
                    break;
                case LogEventLevel.Debug:
                    writer.WriteValue(nameof(LogEventLevel.Debug));
                    break;
                case LogEventLevel.Warning:
                    writer.WriteValue(nameof(LogEventLevel.Warning));
                    break;
                case LogEventLevel.Error:
                    writer.WriteValue(nameof(LogEventLevel.Error));
                    break;
                case LogEventLevel.Verbose:
                    writer.WriteValue(nameof(LogEventLevel.Verbose));
                    break;
                case LogEventLevel.Fatal:
                    writer.WriteValue(nameof(LogEventLevel.Fatal));
                    break;
            }
        }
    }
}
