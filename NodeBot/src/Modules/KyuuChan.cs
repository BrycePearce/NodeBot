using Discord.Commands;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text.RegularExpressions;
using System;

namespace NodeBot.src.Modules
{
    public class KyuuChan : ModuleBase<SocketCommandContext>
    {
        [Command("kyuu")]
        [Alias("k")]
        public async Task GetKyuu([Remainder][Optional] string message) // [Remainder] gives us the message after the command try = "" again
        {
            if (message == null)
            {
                message = GetLatestKyuuChapterNumber();
            }
            else if (message.ToLower() == "rando" || message.ToLower() == "random" || message.ToLower() == "?" || message.ToLower() == "r")
            {
                var latest = int.Parse(GetLatestKyuuChapterNumber());
                Random r = new Random();
                message = r.Next(1, latest).ToString();
            }

            // get the page
            var web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument document = null;
            try
            {
                document = web.Load($"https://helveticascans.com/r/read/wonder-cat-kyuu-chan/en/0/{message}/page/1");
            }
            catch
            {
                await Context.Channel.SendMessageAsync("kyuute-chan not found :(");
                return;
            }
            var page = document.DocumentNode;

            string kyuuteLink = "";
            // loop through all div tags with item css class
            foreach (var item in page.QuerySelectorAll("img"))
            {
                foreach (var imageTag in item.Attributes)
                {
                    if (imageTag.Value.Contains("wonder-cat-kyuu-chan"))
                    {
                        kyuuteLink = imageTag.Value;
                    }

                }
            }

            if (kyuuteLink.Length > 0)
            {
                await Context.Channel.SendMessageAsync(kyuuteLink);
            }
        }

        private string GetLatestKyuuChapterNumber()
        {
            //get the page
            var web = new HtmlWeb();
            var document = web.Load("https://helveticascans.com/r/series/wonder-cat-kyuu-chan/");
            var page = document.DocumentNode;

            var thingy = page.QuerySelector("div.element");
            var rawTitleElements = thingy.QuerySelector("div.title").QuerySelector("a").InnerText.Split(' ');

            // remove pesky elements like :, so we can extract latest numerical chapter without any baggage
            var cleanedTitleElements = rawTitleElements.Select(x => Regex.Replace(x, "[^0-9]", "")).ToArray();

            var latestChapter = cleanedTitleElements.FirstOrDefault(x => IsDigitsOnly(x) && x.Length > 0);
            // find the one that is all numbers
            return latestChapter;
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
