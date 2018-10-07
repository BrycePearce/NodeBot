using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace NodeBot
{
    class Program
    {
        DiscordSocketClient _client;
        CommandHandler _handler;
        static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult(); // tell main to start asynchronously

        public async Task StartAsync()
        {
            // make sure we have a token
            if (Config.bot.token == "" || Config.bot.token == null) return;

            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose // verbose logging for debugging
            });
            // subscribe to a log event
            _client.Log += Log;
   
            // Log in and start the bot
            await _client.LoginAsync(TokenType.Bot, Config.bot.token);
            await _client.StartAsync();

            _handler = new CommandHandler();
            await _handler.InitializeAsync(_client);
            await Task.Delay(-1); // wait until the operation ends

        }

        private async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message);
        }
    }
}
