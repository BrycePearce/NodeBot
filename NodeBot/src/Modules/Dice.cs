using Dice;
using Dice.AST;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NodeBot.src.Modules
{
    public class Dice : ModuleBase<SocketCommandContext>
    {
        [Command("roll")]
        [Alias("r", "d")]
        public async Task WeatherInformation([Remainder] string message) // [Remainder] gives us the message after the command try = "" again
        {
            RollResult roll = Roller.Roll(message);
            List<decimal> results = new List<decimal>();
            RollNode rollNode = (RollNode)roll.RollRoot;
            foreach (var die in roll.Values)
            {
                if (die.DieType.ToString() == "Normal")
                {
                    results.Add(die.Value);
                }
            }
            var totalSides = roll.NumRolls * rollNode.NumSides.Value;
            decimal rawPercent = roll.Value / totalSides;
            int percent = (int)((rawPercent - (int)rawPercent) * 100);
            await ReplyAsync(":: Total " + roll.Value + " / " + totalSides + " [" + percent + "%]" + " :: " + "Results [" + String.Join(", ", results) + "] ::");
        }
    }
}
