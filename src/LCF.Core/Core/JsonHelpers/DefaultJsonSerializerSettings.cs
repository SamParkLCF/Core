
using System.Collections.Generic;

using Newtonsoft.Json;

namespace LCF.Core.JsonHelpers
{
    public class DefaultJsonSerializerSettings : JsonSerializerSettings
    {
        public DefaultJsonSerializerSettings() : base()
        {
            Formatting = Formatting.Indented;
            DateTimeZoneHandling = DateTimeZoneHandling.Local;
            Converters = new List<JsonConverter>()
            {
                new LogEventLevelJsonConverter()
            };
            NullValueHandling = NullValueHandling.Ignore;
        }
    }
}
