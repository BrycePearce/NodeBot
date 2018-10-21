using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace NodeBot.src.Modules
{
    public class CollectGamesBeingPlayed : ModuleBase<SocketCommandContext>
    {
        [Command("listgames")]
        public async Task Print()
        {
            var users = Context.Guild.Users;
            foreach (var user in users)
            {
                // print out games played by all online non-bot users
                if (user.Status.ToString().ToLower() == Constants.statusOnline)
                {
                    if (!String.IsNullOrEmpty(user.Game.ToString()) && !user.IsBot)
                    {
                        await Context.Channel.SendMessageAsync(user.Username + " is currently playing " + user.Game.Value);
                    }
                }
            }
        }
    }
}
