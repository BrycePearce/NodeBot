using Discord.Commands;
using Newtonsoft.Json;
using NodeBot.src.Models;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NodeBot.src.Modules
{
    public class Weather : ModuleBase<SocketCommandContext>
    {
        private static readonly HttpClient client = new HttpClient(); // I think adding this here might be bad practice. Not sure. todo: findout
        [Command("weather")]
        public async Task WeatherInformation([Remainder] string message) // [Remainder] gives us the whole message as one long string
        {

            // Call google to geocode given address, store lat lng for darksky api
            string geocodeUrl = "https://maps.googleapis.com/maps/api/geocode/json?address=" + message + "&key=" + Config.weatherTokens.googleGeoToken; // todo: move key to json

            var geoResponse = await client.GetAsync(geocodeUrl);
            var geoResponseString = await geoResponse.Content.ReadAsStringAsync();

            GeocodeResponse geoFormattedResponse = JsonConvert.DeserializeObject<GeocodeResponse>(geoResponseString);

            var result = geoFormattedResponse.results;
            double lat = result.FirstOrDefault(x => x.geometry.location != null)?.geometry.location.lat ?? 0;
            double lng = result.FirstOrDefault(x => x.geometry.location != null)?.geometry.location.lng ?? 0;

            // Call DarkSky api with lat lng
            string darkSkyRequestUrl = "https://api.darksky.net/forecast/" + Config.weatherTokens.darkSkyToken + "/" + lat + "," + lng;

            var darkResponse = await client.GetAsync(darkSkyRequestUrl);
            var darkResponseString = await darkResponse.Content.ReadAsStringAsync();

            //DarkSkyResponse darkSkyResponse = JsonConvert.DeserializeObject<DarkSkyResponse>(darkResponseString); // not working, can't figure out why. todo: fix
            dynamic deserializedDarkResponse = JsonConvert.DeserializeObject(darkResponseString);
            await Context.Channel.SendMessageAsync("The temperature in " + message + " is currently: " + deserializedDarkResponse.currently.temperature + "F");
        }
    }
}