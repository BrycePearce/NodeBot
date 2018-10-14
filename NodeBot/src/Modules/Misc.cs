using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace NodeBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("echo")]
        public async Task Echo([Remainder] string message) // [Remainder] gives us the whole message as one long string
        {
            var embed = new EmbedBuilder();
            embed.WithDescription(message);
            embed.WithColor(new Color (51, 72, 178));
            embed.WithAuthor("Author: " + Context.User.Username, Context.User.GetAvatarUrl());
           await Context.Channel.SendMessageAsync("", false, embed);
        }
    }
}
