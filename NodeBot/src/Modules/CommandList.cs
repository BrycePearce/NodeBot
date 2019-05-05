using Discord.Commands;
using System.Threading.Tasks;

namespace NodeBot.src.Modules
{
    public class CommandList : ModuleBase<SocketCommandContext>
    {
        [Command("commands")]
        public async Task ListCommands()
        {
            await Context.Channel.SendMessageAsync("Current commands list:\n" + ".weather Some Location\n" + ".weather set Your Location\n" + 
                ".listgames (lists games being played in current channel)\n" +
                ".movies (day,week,month)\n" +
                ".roll (.r .d) e.g. .r 4d20 rolls dice and prints out results\n" +
                ".dog / .cat prints out a given animal (maybe)"
                );
        }
    }
}
