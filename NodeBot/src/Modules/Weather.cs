using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using NodeBot.src.Helpers;
using NodeBot.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace NodeBot.src.Modules
{
    public class Weather : ModuleBase<SocketCommandContext>
    {
        private static readonly HttpClient client = new HttpClient(); // I think adding this here might be bad practice. Not sure. todo: findout

        [Command("weather")]
        public async Task WeatherInformation([Remainder][Optional] string message) // [Remainder] gives us the message after the command try = "" again
        {

            // handle un-registered users with no query
            if (String.IsNullOrWhiteSpace(message) && !DataStorage.HasKey(Context.User.Username)) { await Context.Channel.SendMessageAsync("Set your default location with .weather set YOUR LOCATION"); return; }

            // handle registered users with no query
            if (String.IsNullOrWhiteSpace(message) && DataStorage.HasKey(Context.User.Username))
            {
                SendWeatherInfo(DataStorage.GetValueFromKey(Context.User.Username));
                return;
            }

            // handle location registry
            if (!String.IsNullOrWhiteSpace(message) && message.Substring(0, 3).ToLower() == "set")
            {
                RegisterUserLocation(message);
                return;
            }
            // otherwise, print out the result
            SendWeatherInfo(message);
        }

        private async void RegisterUserLocation(string message)
        {
            string userLocation = message.Substring(message.IndexOf("set") + "set".Length).Trim(); // get all the user the first occurance of the word "set"
            // check to see if user has existing location registered
            if (DataStorage.HasKey(Context.User.Username) && !String.IsNullOrWhiteSpace(message))
            {
                DataStorage.ReplaceKeyValue(Context.User.Username, userLocation);
                await Context.Channel.SendMessageAsync("updated location to " + userLocation);
            }
            // otherwise, register a new location for the user
            else
            {
                DataStorage.AddPairToStorage(Context.User.Username, userLocation);
                await Context.Channel.SendMessageAsync(Context.User.Username + " successfully added " + userLocation + " as their default location.");
            }
        }

        private async void SendWeatherInfo(string message)
        {
            // Call google to geocode given address, store lat lng for darksky api
            string geocodeUrl = "https://maps.googleapis.com/maps/api/geocode/json?address=" + message + "&key=" + Config.weatherTokens.googleGeoToken; // todo: move key to json

            var geoResponse = await client.GetAsync(geocodeUrl);
            var geoResponseString = await geoResponse.Content.ReadAsStringAsync();

            GeocodeResponse geoFormattedResponse = JsonConvert.DeserializeObject<GeocodeResponse>(geoResponseString);

            List<Result> result = geoFormattedResponse.results;
            double lat = result.FirstOrDefault(x => x.geometry.location != null)?.geometry.location.lat ?? 0;
            double lng = result.FirstOrDefault(x => x.geometry.location != null)?.geometry.location.lng ?? 0;
            string formattedAddress = result.FirstOrDefault(x => x != null)?.formatted_address ?? "";

            // Call DarkSky api with lat lng
            string darkSkyRequestUrl = "https://api.darksky.net/forecast/" + Config.weatherTokens.darkSkyToken + "/" + lat + "," + lng;

            var darkResponse = await client.GetAsync(darkSkyRequestUrl);
            var darkResponseString = await darkResponse.Content.ReadAsStringAsync();

            DarkSkyResponse deserializedDarkResponse = JsonConvert.DeserializeObject<DarkSkyResponse>(darkResponseString);

            //output 
            var embed = new EmbedBuilder();
            embed.Title = formattedAddress;
            embed.WithDescription("" +
                                 deserializedDarkResponse.currently.temperature + "F\n" +
                                 "Cloud Cover: " + deserializedDarkResponse.currently.cloudCover + "\n" +
                                 "Windspeed: " + deserializedDarkResponse.currently.windSpeed + "mph\n" +
                                 "Chance of Rain: " + deserializedDarkResponse.daily.data[0].precipProbability + "%\n\n" +
                                 "Forecast: " + deserializedDarkResponse.daily.summary

                );
            embed.WithColor(new Color(51, 72, 178));
            await Context.Channel.SendMessageAsync("", false, embed);

        }
    }
}