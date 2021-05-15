using EnglishBot.Models.Commands;
using EnglishBot.Models.Logic;
using PhraseologicalService;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using TranslatorLibrary;

namespace EnglishBot.Models
{
    public class Bot
    {
        private static List<Command> commandsList;
        public static IReadOnlyList<Command> Commands  => commandsList.AsReadOnly(); 


        public static IUserLogic UserLogic;

        public static Phraseological PhraseologService { get; private set; }

        public static GoogleTranslator TranslateService { get; private set; }

        private static TelegramBotClient _client;

        public static async Task<TelegramBotClient> Get()
        {
            if (_client != null)
            {
                return _client;
            }

            // phraseological service initialization
            PhraseologService = new Phraseological();

            // Translate service initialization
            TranslateService = new GoogleTranslator();

            // user logic initialization
            UserLogic = new UserLogic();

            // initialization of commands
            commandsList = new List<Command>();
            commandsList.Add(new RenameCommand());
            commandsList.Add(new TranslateRuToEnCommand());
            commandsList.Add(new TranslateEnToRuCommand());
            commandsList.Add(new PhraseologCommand());

            // client initialization
            _client = new TelegramBotClient(AppSettings.Key);

            // web hook initialization
            var hook = string.Format(AppSettings.URL, "api/message/update");
            await _client.SetWebhookAsync(hook);

            return _client;
        }
    }
}