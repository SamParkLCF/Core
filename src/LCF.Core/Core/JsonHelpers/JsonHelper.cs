
using LCF.Core.JsonHelpers;

using Newtonsoft.Json;

namespace LCF.Core
{
    internal static class JsonHelper
    {
        public static string SerializeObject(object @object) =>
            JsonConvert.SerializeObject(@object, new DefaultJsonSerializerSettings());
        public static string SerializeObject(object @object, JsonSerializerSettings serializerSettings) =>
            JsonConvert.SerializeObject(@object, serializerSettings);
    }
}
