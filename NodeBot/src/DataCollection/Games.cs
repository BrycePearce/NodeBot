using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace NodeBot.src.DataCollection
{
    public class Games : ModuleBase<SocketCommandContext>
    {
        /*
         * https://i.imgur.com/RSNDxvl.png
         * Although this wants a SocketUser, it's actually sending a subclass of SocketGuildUser (among other things)
         * todo: move this out into its own non-modules file/folder.
         * https://discord.foxbot.me/docs/api/Discord.WebSocket.SocketUser.html
         */
        public async Task CollectGamesBeingPlayed(SocketGuildUser user, SocketUser userAgain)
        {
            string strGuildId = user.Guild.Id.ToString();
            ulong ulongId = Convert.ToUInt64(strGuildId);
            var channel = user.Guild.GetChannel(ulongId) as SocketTextChannel;
            //await channel.SendMessageAsync("a user has changed status!");
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