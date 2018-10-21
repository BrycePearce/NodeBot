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
            //    foreach (var channel in Context.Client.Guilds)
            //    {
            //        foreach (var user in channel.Users)
            //        {
            //            await Context.Channel.SendMessageAsync(user.Username);
            //        }
            //    }
        }

        /*
         * https://i.imgur.com/RSNDxvl.png
         * Although this wants a SocketUser, it's actually sending a subclass of SocketGuildUser (among other things)
         * todo: move this out into its own non-modules file/folder.
         * https://discord.foxbot.me/docs/api/Discord.WebSocket.SocketUser.html
         */
        public async Task AnnounceJoinedUser(SocketGuildUser user, SocketUser userAgain) //Welcomes the new user
        {
            //var channel = client.GetChannel(/*/TextChannelID/*/) as SocketTextChannel; // Gets the channel to send the message in
            //await channel.SendMessageAsync($"Welcome {user.mention} to {channel.Guild.Name}"); //Welcomes the new user
        }
    }
}
