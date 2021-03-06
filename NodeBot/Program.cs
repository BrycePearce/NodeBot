﻿using Discord;
using Discord.WebSocket;
using NodeBot.src.DataCollection;
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
            Games statusChange = new Games();
            // make sure we have a token
            if (String.IsNullOrEmpty(Config.bot.token)) return;

            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose // verbose logging for debugging
            });
            // set bot status
            //await _client.SetGameAsync("Roboholics-Anonymous", "https://www.twitch.tv/cocoari", StreamType.Twitch);
            await _client.SetGameAsync(".commands");

            _client.GuildMemberUpdated += statusChange.CollectGamesBeingPlayed;


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