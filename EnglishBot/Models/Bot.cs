using EnglishBot.Models.Commands;
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
        private static TelegramBotClient _client;

        public static IUserLogic UserLogic;

        private static List<Command> commandsList;
        public static IReadOnlyList<Command> Commands { get => commandsList.AsReadOnly(); }

        public static async Task<TelegramBotClient> Get()
        {
            if (_client != null)
            {
                return _client;
            }

            UserLogic = new UserLogic();

            commandsList = new List<Command>();
            commandsList.Add(new RenameCommand());

            _client = new TelegramBotClient(AppSettings.Key);

            var hook = string.Format(AppSettings.URL, "api/message/update");
            await _client.SetWebhookAsync(hook);

            return _client;
        }
    }
}