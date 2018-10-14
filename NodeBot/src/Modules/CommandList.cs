using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeBot.src.Modules
{
    public class CommandList : ModuleBase<SocketCommandContext>
    {
        [Command("commands")]
        public async Task ListCommands()
        {
            await Context.Channel.SendMessageAsync("Current commands list:\n" + ".weather Some Location\n" + ".weather set Your Location");
        }
    }
}
