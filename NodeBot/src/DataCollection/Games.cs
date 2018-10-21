using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeBot.src.DataCollection
{
    public class Games
    {
        /*
         * https://i.imgur.com/RSNDxvl.png
         * Although this wants a SocketUser, it's actually sending a subclass of SocketGuildUser (among other things)
         * todo: move this out into its own non-modules file/folder.
         * https://discord.foxbot.me/docs/api/Discord.WebSocket.SocketUser.html
         */
        public async Task AnnounceJoinedUser(SocketGuildUser user, SocketUser userAgain)
        {
            //var channel = client.GetChannel(user.Guild.Id.ToString()) as SocketTextChannel; // Gets the channel to send the message in
            //await channel.SendMessageAsync($"Welcome {user.mention} to {channel.Guild.Name}");
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