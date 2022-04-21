using Newtonsoft.Json;
using STJsonSerializer = System.Text.Json.JsonSerializer;

namespace TimeTracker.Core.Shared.Utils
{
    public static class AppJsonConverter
    {
        private static JsonSerializerSettings SerializerOptions => new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            ObjectCreationHandling = ObjectCreationHandling.Auto,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public static string Serialize<T>(T data, bool internals = false) where T : class
        {
            if (internals) return JsonConvert.SerializeObject(data, SerializerOptions);

            return STJsonSerializer.Serialize(data);
        }

        public static T? DeSerialize<T>(string data, bool internals = false) where T : class
        {
            if (internals) return JsonConvert.DeserializeObject<T>(data, SerializerOptions);

            return STJsonSerializer.Deserialize<T>(data);
        }
    }
}