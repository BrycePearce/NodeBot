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
        public static MovieConfig movieConfig;

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
                movieConfig = JsonConvert.DeserializeObject<MovieConfig>(json);
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

    public struct MovieConfig
    {
        public string movieSiteLog;
        public string movieSiteTop;
        public string movieUser;
        public string moviePass;
        public string movieKey;
        public string movieCookieName;
        public string movieCookieValue;
    
    }
}