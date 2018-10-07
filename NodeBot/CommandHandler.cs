using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace NodeBot
{
    class CommandHandler
    {
        DiscordSocketClient _client;
        CommandService _service;

        public async Task InitializeAsync(DiscordSocketClient client)
        {
            _client = client; // discord client
            _service = new CommandService(); // discord service
            await _service.AddModulesAsync(Assembly.GetEntryAssembly()); // adds modules asynchronously 
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage; // convert? to SocketUserMessage. Because that's what SocketCommandContext needs

            if (msg == null) return;

            var context = new SocketCommandContext(_client, msg);
            int messagePosition = 0;
            if (msg.HasStringPrefix(Config.bot.cmdPrefix, ref messagePosition) || msg.HasMentionPrefix(_client.CurrentUser, ref messagePosition)) // if message has our prefix, at position 0. Or if the bot (current user) has been mentioned e.g. @NodeBot at position 0
            {
                // execute the command
                var result = await _service.ExecuteAsync(context, messagePosition);

                // if something went wrong, write out the error
                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    Console.Write(result.ErrorReason);
                }
            }
        }
    }
}
