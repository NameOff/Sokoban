using System.IO;
using Newtonsoft.Json;

namespace Sokoban.Infrastructure
{
    public static class JsonHelper
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        };

        public static void Serialize(object obj, string fileName)
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(obj, Settings));
        }

        public static T Deserialize<T>(string fileName)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName), Settings);
        }
    }
}