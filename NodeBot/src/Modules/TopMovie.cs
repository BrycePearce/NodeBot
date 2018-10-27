using Discord;
using Discord.Commands;
using Jurassic.Library;
using Newtonsoft.Json;
using NodeBot.src.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace NodeBot.src.Modules
{
    // **********
    // Disclaimer:
    // The following is a full plate of Barilla with tomato sauce, and dusted Parmesan on top.
    // **********

    public class TopMovie : ModuleBase<SocketCommandContext>
    {
        [Command("movies")]
        [Alias("top10", "top")]
        public async Task GetTop10([Optional][Remainder] string message) // [Remainder] gives us the message after the command try = "" again
        {
            if (message != "day" && message != "month" && message != "week" && message != null)
            {
                await Context.Channel.SendMessageAsync("Invalid Params.", false);
                return;
            }
            if (message == null) message = "week";
            var cookieContainer = new CookieContainer();
            var baseAddress = new Uri(Config.movieConfig.movieSiteLog);

            SomeMovieSiteObject result = new SomeMovieSiteObject();
            result = CallTopList(cookieContainer, 0);


            var des = DoStuff(result, message);
            if (message == "day") message = "daily";
            var embed = new EmbedBuilder();
            embed.Title = "Latest " + message + "ly movies some Movie Tracker";
            embed.Description = des;
            await Context.Channel.SendMessageAsync("", false, embed);
        }

        private string DoStuff(SomeMovieSiteObject result, string message)
        {
            string thing = "";

            // top 10 daily
            if (message == "day")
            {
                for (var i = 0; i <= 9; i += 1)
                {
                    thing += result.Movies[i].Title + "\n";
                }
            }
            else if (message == "month")
            {
                for (var i = 10; i <= 19; i += 1)
                {
                    thing += result.Movies[i].Title + "\n";
                }
            }
            // week
            else
            {
                for (var i = 10; i <= 19; i += 1)
                {
                    thing += result.Movies[i].Title + "\n";
                }
            }

            // top 10 weekly
            return thing;
        }

        private SomeMovieSiteObject UsefulTextExtractor(string text)
        {
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(text);
            SomeMovieSiteObject results = new SomeMovieSiteObject
            {
                Movies = new List<Movie>()
            };

            foreach (var item in htmlDoc.DocumentNode.Descendants().Where(n => n.Name == "script"))
            {
                if (item.InnerText.Contains("coverViewJsonData") && item.InnerText.Length > 200) // gets rid of lines that have variable declarations, etc..
                {
                    // the coverViewJsonData is an array, so rather than looping through it and returning an index in evalute, parse out the index and replace it with a regular variable
                    string output = item.InnerText.Substring(item.InnerText.IndexOf("="));
                    output = "coverViewJsonData " + output;
                    var engine = new Jurassic.ScriptEngine();
                    var result = engine.Evaluate("(function() { " + output + " return coverViewJsonData; })()");
                    var json = JSONObject.Stringify(engine, result);
                    dynamic tmp = JsonConvert.DeserializeObject(json);

                    foreach (var movie in tmp.Movies)
                    {
                        results.Movies.Add(new Movie()
                        {
                            Title = movie.Title.ToString(),
                            Cover = movie.Cover.ToString()
                        });
                    }
                }
            }
            return results;
        }

        private HttpResponseMessage Login(CookieContainer cookieContainer)
        {
            var baseAddress = new Uri(Config.movieConfig.movieSiteLog);
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", Config.movieConfig.movieUser),
                    new KeyValuePair<string, string>("password", Config.movieConfig.moviePass),
                    new KeyValuePair<string, string>("passkey", Config.movieConfig.movieKey),
                    //new KeyValuePair<string, string>("keeplogged", "1"),
                    new KeyValuePair<string, string>("login", "Login"),
                });
                var messagey = new HttpRequestMessage(HttpMethod.Post, "/test");
                cookieContainer.Add(baseAddress, new Cookie("CookieName", "cookie_value"));
                HttpResponseMessage response = client.PostAsync(baseAddress, content).Result;
                //response.EnsureSuccessStatusCode();

                IEnumerable<Cookie> responseCookies = cookieContainer.GetCookies(baseAddress).Cast<Cookie>();
                foreach (Cookie cookie in responseCookies)
                {
                    if (cookie.Name == "session")
                    {
                        Config.movieConfig.movieCookieName = cookie.Name;
                        Config.movieConfig.movieCookieValue = cookie.Value;
                    }
                }
                return response;
            }
        }

        private SomeMovieSiteObject CallTopList(CookieContainer cookieContainer, int tries)
        {
            string thing;
            using (WebClient client2 = new WebClient())
            {
                string uri = Config.movieConfig.movieSiteTop;
                SomeMovieSiteObject stuff;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                SomeMovieSiteObject jsonRes = new SomeMovieSiteObject();
                string cookiething = Config.movieConfig.movieCookieName + "=" + Config.movieConfig.movieCookieValue;
                request.Headers.Add(HttpRequestHeader.Cookie, cookiething);
                using (HttpWebResponse response2 = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response2.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    thing = reader.ReadToEnd();
                    if (thing.Contains("login.php"))
                    {
                        // get new cookies
                        HttpResponseMessage loginRes = Login(cookieContainer);
                        stuff = CallTopList2(cookieContainer, tries);
                        return stuff;
                    }
                    else
                    {
                        jsonRes = UsefulTextExtractor(thing);
                        tries = 0;
                        return jsonRes;
                    }
                }
            }
        }

        private SomeMovieSiteObject CallTopList2(CookieContainer cookieContainer, int tries)
        {
            string thing;
            using (WebClient client2 = new WebClient())
            {
                string uri = Config.movieConfig.movieSiteTop;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                SomeMovieSiteObject jsonRes = new SomeMovieSiteObject();
                string cookiething = Config.movieConfig.movieCookieName + "=" + Config.movieConfig.movieCookieValue;
                request.Headers.Add(HttpRequestHeader.Cookie, cookiething);
                using (HttpWebResponse response2 = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response2.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    thing = reader.ReadToEnd();
                    var jsonRes2 = UsefulTextExtractor(thing);
                    return jsonRes2;
                }
            }
        }
    }
}