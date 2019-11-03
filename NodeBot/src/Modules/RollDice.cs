using Discord.Commands;
using RollGen;
using RollGen.IoC;
using System.Threading.Tasks;

namespace NodeBot.src.Modules
{
    public class RollDice : ModuleBase<SocketCommandContext>
    {
        [Command("roll")]
        [Alias("r", "d")]
        public async Task RollD20([Remainder] string message) // [Remainder] gives us the message after the command try = "" again
        {
            var roll = RollGen(message);
            await ReplyAsync($":: Total {roll.AsSum()} / {roll.AsPotentialMaximum()} [{roll.AsPotentialAverage()}%] :: Results [{string.Join(", ", roll.AsIndividualRolls())}] ::");
        }

        private PartialRoll RollGen(string diceRoll)
        {
            var myDice = DiceFactory.Create();
            return myDice.Roll(diceRoll);
        }
    }
}
