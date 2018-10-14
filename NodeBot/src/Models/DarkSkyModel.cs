using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeBot.src.Models
{
    public class Currently
    {
        public double time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
        public double nearestStormDistance { get; set; }
        public double nearestStormBearing { get; set; }
        public double precipdoubleensity { get; set; }
        public double precipProbability { get; set; }
        public double temperature { get; set; }
        public double apparentTemperature { get; set; }
        public double dewPodouble { get; set; }
        public double humidity { get; set; }
        public double pressure { get; set; }
        public double windSpeed { get; set; }
        public double windGust { get; set; }
        public double windBearing { get; set; }
        public double cloudCover { get; set; }
        public double uvIndex { get; set; }
        public double visibility { get; set; }
        public double ozone { get; set; }
    }

    public class Datum
    {
        public double time { get; set; }
        public double precipdoubleensity { get; set; }
        public double precipProbability { get; set; }
    }

    public class Minutely
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public List<Datum> data { get; set; }
    }

    public class Datum2
    {
        public double time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
        public double precipdoubleensity { get; set; }
        public double precipProbability { get; set; }
        public double temperature { get; set; }
        public double apparentTemperature { get; set; }
        public double dewPodouble { get; set; }
        public double humidity { get; set; }
        public double pressure { get; set; }
        public double windSpeed { get; set; }
        public double windGust { get; set; }
        public double windBearing { get; set; }
        public double cloudCover { get; set; }
        public double uvIndex { get; set; }
        public double visibility { get; set; }
        public double ozone { get; set; }
        public string precipType { get; set; }
    }

    public class Hourly
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public List<Datum2> data { get; set; }
    }

    public class Datum3
    {
        public double time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
        public double sunriseTime { get; set; }
        public double sunsetTime { get; set; }
        public double moonPhase { get; set; }
        public double precipdoubleensity { get; set; }
        public double precipdoubleensityMax { get; set; }
        public double precipdoubleensityMaxTime { get; set; }
        public double precipProbability { get; set; }
        public string precipType { get; set; }
        public double temperatureHigh { get; set; }
        public double temperatureHighTime { get; set; }
        public double temperatureLow { get; set; }
        public double temperatureLowTime { get; set; }
        public double apparentTemperatureHigh { get; set; }
        public double apparentTemperatureHighTime { get; set; }
        public double apparentTemperatureLow { get; set; }
        public double apparentTemperatureLowTime { get; set; }
        public double dewPodouble { get; set; }
        public double humidity { get; set; }
        public double pressure { get; set; }
        public double windSpeed { get; set; }
        public double windGust { get; set; }
        public double windGustTime { get; set; }
        public double windBearing { get; set; }
        public double cloudCover { get; set; }
        public double uvIndex { get; set; }
        public double uvIndexTime { get; set; }
        public double visibility { get; set; }
        public double ozone { get; set; }
        public double temperatureMin { get; set; }
        public double temperatureMdoubleime { get; set; }
        public double temperatureMax { get; set; }
        public double temperatureMaxTime { get; set; }
        public double apparentTemperatureMin { get; set; }
        public double apparentTemperatureMdoubleime { get; set; }
        public double apparentTemperatureMax { get; set; }
        public double apparentTemperatureMaxTime { get; set; }
    }

    public class Daily
    {
        public string summary { get; set; }
        public string icon { get; set; }
        public List<Datum3> data { get; set; }
    }


    public class DarkSkyResponse
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string timezone { get; set; }
        public Currently currently { get; set; }
        public Minutely minutely { get; set; }
        public Hourly hourly { get; set; }
        public Daily daily { get; set; }
        public double offset { get; set; }
    }
}
