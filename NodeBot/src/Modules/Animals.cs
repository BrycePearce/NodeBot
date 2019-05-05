using Dice;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using NodeBot.src.Models;
using System;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NodeBot.src.Modules
{
    public class Animals : ModuleBase<SocketCommandContext>
    {
        public static Random RandomGen = new Random(); // cannot use this in method, since the random seed is based on computer clock.

        [Command("doggo")]
        [Alias("dog", "pup", "woofer", "cat", "kitter", "meow")]
        public async Task GetAnimal()
        {
            string message = Context.Message.Content;
            string[] doggoCommands = new string[] { "dog", "pup", "doggo", "woofer" };
            bool isRequestingDoggo = doggoCommands.Any(message.Contains) ? true : false;
            bool shouldReturnAskedAnimal = MeetsPercentage(90);
            bool shouldReturnDog = ShouldReturnDog(isRequestingDoggo, shouldReturnAskedAnimal);

            string url = GenerateAnimalUrl(shouldReturnDog);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Animal result = new Animal();
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string responseString = reader.ReadToEnd();
                if (shouldReturnDog)
                {
                    result.Dog = JsonConvert.DeserializeObject<Dog>(responseString);
                }
                else
                {
                    Cat[] Cats = JsonConvert.DeserializeObject<Cat[]>(responseString);
                    result.Cat = new Cat
                    {
                        Url = Cats[0].Url
                    };
                }
            }

            var embed = new EmbedBuilder();
            embed.WithImageUrl(shouldReturnDog ? result.Dog.Message : result.Cat.Url);

            bool isGivingDogOpposite = isRequestingDoggo && !shouldReturnDog;
            bool isGivingCatOpposite = !isRequestingDoggo && shouldReturnDog;

            string outputString = "";

            if (isGivingDogOpposite) { outputString = "Have a cat instead"; }
            else if (isGivingCatOpposite) { outputString = "Have a dog instead"; }

            await Context.Channel.SendMessageAsync(outputString, false, embed);
        }

        private bool MeetsPercentage(int percentChance)
        {
            RollResult roll = Roller.Roll("1d100");

            return percentChance > roll.Value;
        }

        private bool ShouldReturnDog(bool isRequestingDoggo, bool shouldReturnAskedAnimal)
        {
            return (isRequestingDoggo && shouldReturnAskedAnimal) || (!isRequestingDoggo && !shouldReturnAskedAnimal);
        }

        private string GenerateAnimalUrl(bool shouldReturnDogUrl)
        {
            string[] dogBreeds = new string[] { "Samoyed", "Shiba", "Husky" };
            return shouldReturnDogUrl ? $"https://dog.ceo/api/breed/{dogBreeds[new Random().Next(0, dogBreeds.Length)]}/images/random" : "https://api.thecatapi.com/v1/images/search";
        }
    }
}
