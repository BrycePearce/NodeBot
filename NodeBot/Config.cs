using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeBot
{
    class Config
    {
        private const string configFolder = "Resources";
        private const string configFile = "config.json";
        public static BotConfig bot;
        public static WeatherConfig weatherTokens;
        // constructor
        static Config()
        {
            if (!Directory.Exists(configFolder)) Directory.CreateDirectory(configFolder);

            if (!File.Exists(configFolder + "/" + configFile))
            {
                // if the config does not exist, create and setup the configuration file
                bot = new BotConfig();
                string json = JsonConvert.SerializeObject(bot, Formatting.Indented);
                File.WriteAllText(configFolder + "/" + configFile, json);
            }
            else
            {
                // if it does exist, set json config values to bot
                string json = File.ReadAllText(configFolder + "/" + configFile);
                bot = JsonConvert.DeserializeObject<BotConfig>(json);
                weatherTokens = JsonConvert.DeserializeObject<WeatherConfig>(json);
            }
        }
    }

    public struct BotConfig
    {
        public string token;
        public string cmdPrefix;
    }

    public struct WeatherConfig
    {
        public string googleGeoToken;
        public string darkSkyToken;
    }
}