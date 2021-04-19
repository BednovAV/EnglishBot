﻿using EnglishBot.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;

namespace EnglishBot.Models
{
    public class Bot
    {
        private static List<Command> commandsList;
        public static IReadOnlyList<Command> Commands { get => commandsList.AsReadOnly(); }


        public static IUserLogic UserLogic;

        
        private static TelegramBotClient _client;

        public static async Task<TelegramBotClient> Get()
        {
            if (_client != null)
            {
                return _client;
            }

            // user logic initialization
            UserLogic = new UserLogic();

            // initialization of commands
            commandsList = new List<Command>();
            commandsList.Add(new RenameCommand());
            commandsList.Add(new TranslateRuToEnCommand());
            commandsList.Add(new TranslateEnToRuCommand());

            // client initialization
            _client = new TelegramBotClient(AppSettings.Key);

            // web hook initialization
            var hook = string.Format(AppSettings.URL, "api/message/update");
            await _client.SetWebhookAsync(hook);

            return _client;
        }
    }
}