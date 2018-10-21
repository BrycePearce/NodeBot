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
        public async Task AnnounceJoinedUser(SocketGuildUser user, SocketUser userAgain) //Welcomes the new user
        {
            //var channel = client.GetChannel(/*/TextChannelID/*/) as SocketTextChannel; // Gets the channel to send the message in
            //await channel.SendMessageAsync($"Welcome {user.mention} to {channel.Guild.Name}"); //Welcomes the new user
        }
    }
}
