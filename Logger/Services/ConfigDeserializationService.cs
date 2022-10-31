using Logging.FileConfig;
using Newtonsoft.Json;

namespace Logging.Services
{
    public class ConfigDeserializationService
    {
        public static Config FilePathSerialization()
        {
            var configFile = File.ReadAllText("config.json");
            var config = JsonConvert.DeserializeObject<Config>(configFile);
            return config;
        }
    }
}
