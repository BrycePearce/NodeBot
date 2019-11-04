using Discord.Commands;
using RollGen;
using RollGen.IoC;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NodeBot.src.Modules
{
    public class RollDice : ModuleBase<SocketCommandContext>
    {
        [Command("roll")]
        [Alias("r", "d")]
        public async Task RollD20([Remainder] string message) // [Remainder] gives us the message after the command try = "" again
        {
            var dice = RollGen(message);
            var roll = dice.AsIndividualRolls();
            int[] array = new int[] { 18, 17, 17, 16 }; // 68 / 80, 85%
            double thing = (double)array.Sum() / (double)80 * 100;
            double percentage = Math.Round((double)roll.Sum() / (double)dice.AsPotentialMaximum() * 100, 0);
            await ReplyAsync($":: Total {roll.Sum()} / {dice.AsPotentialMaximum()} [{percentage}%] :: Results [{string.Join(", ", roll)}] ::");
        }

        private PartialRoll RollGen(string diceRoll)
        {
            var myDice = DiceFactory.Create();
            return myDice.Roll(diceRoll);
        }
    }
}
