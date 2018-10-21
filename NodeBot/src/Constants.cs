using System.Collections.Generic;

namespace NodeBot.src
{
    public class Constants
    {
        public static Dictionary<string, object> weatherEmojis = new Dictionary<string, object>
        {
            { "clear-night", "🌙" },
            { "rain", "☔️" },
            { "snow", "❄️" },
            { "sleet", "❄️" },
            { "wind", "💨" },
            { "fog", "🌫"},
            { "cloudy", "☁️"},
            { "partly-cloudy-day", "⛅️"},
            { "partly-cloudy-night", "☁️"},
            { "thunderstorm", "⛈"},
            { "tornado", "🌪"},
        };

        public static string statusOnline = "online";
        public static string statusOffline = "offline";
    }
}
