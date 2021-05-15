using EnglishBot.Models.DbModels;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EnglishBot.Models.Logic
{
    public class UserLogic : IUserLogic
    {
        public async Task ReceiveTextMessageAsync(Message message, TelegramBotClient client)
        {
            
            using(BotDbContext db = new BotDbContext())
            {
                BotUser user = db.Users
                    .FirstOrDefault(u => u.Chat == message.Chat.Id);

                if (user is null)
                {
                    user = new BotUser
                    {
                        Chat = message.Chat.Id,
                        Name = message.From.FirstName,
                        DialogStatus = Status.NewUser
                    };

                    db.Users.Add(user);
                }

                if (message.Text.First() == '/')
                {
                    Bot.Commands
                        .FirstOrDefault(command => command.Contains(message.Text))
                        ?.Execute(user);
                }

                switch (user.DialogStatus)
                {
                    case Status.NewUser:
                    case Status.WaitingNewUser:
                        await NewUserAsync(message, client, user);
                        break;

                    case Status.TranslateEnToRu:
                    case Status.WaitingTranslteEnToRu:
                    case Status.TranslateRuToEn:
                    case Status.WaitingTranslteRuToEn:
                        await TranslateAsync(message, client, user);
                        break;

                    case Status.GetPhraseologiсal:
                        await PhraseologicalAsync(message, client, user);
                        break;

                    case Status.GetWord:
                    case Status.WaitingGetWord:
                        await CheckWordAsync(message, client, user);
                        break;

                    case Status.other:
                        await OtherAsync(message, client, user);
                        break;
                }

                await db.SaveChangesAsync();
            }
        }

        private async Task CheckWordAsync(Message message, TelegramBotClient client, BotUser user)
        {
            if (user.DialogStatus == Status.GetWord)
            {
                var word = Bot.WordsService.Get();
                user.WaitingWord = word;

                await client.SendTextMessageAsync(
                                       chatId: user.Chat,
                                       text: "Я скажу тебе слово, а ты мне его перевод.",
                                       replyToMessageId: message.MessageId);

                await client.SendTextMessageAsync(
                                       chatId: user.Chat,
                                       text: word);

                user.DialogStatus = Status.WaitingGetWord;
            }
            else
            {
                var waitingWord = Bot.TranslateService.TranslateEnToRu(user.WaitingWord);

                string responce = message.Text.Trim().ToLower() == waitingWord.Trim().ToLower()?
                                  "Все верно!"
                                  : $"Правильный ответ - {waitingWord} :(";

                await client.SendTextMessageAsync(
                                       chatId: user.Chat,
                                       text: responce,
                                       replyToMessageId: message.MessageId);

                user.DialogStatus = Status.other;
            }
        }

        public Task ReceiveVoiceMessageAsync(Message message, TelegramBotClient client)
        {
            return Task.CompletedTask;
        }

        private async Task PhraseologicalAsync(Message message, TelegramBotClient client, BotUser user)
        {
            await client.SendTextMessageAsync(
                                       chatId: user.Chat,
                                       text: Bot.PhraseologService.Get());

            user.DialogStatus = Status.other;
        }

        private async Task TranslateAsync(Message message, TelegramBotClient client, BotUser user)
        {
            if (user.DialogStatus == Status.TranslateEnToRu || 
                user.DialogStatus == Status.TranslateRuToEn)
            {
                await client.SendTextMessageAsync(
                                chatId: user.Chat,
                                text: "Введите слово или предложение");

                user.DialogStatus = user.DialogStatus == Status.TranslateEnToRu ?
                                    Status.WaitingTranslteEnToRu:
                                    Status.WaitingTranslteRuToEn;
            }
            else
            {
                var translatedText = user.DialogStatus == Status.WaitingTranslteEnToRu?
                                     Bot.TranslateService.TranslateEnToRu(message.Text)
                                     : Bot.TranslateService.TranslateRuToEn(message.Text);

                await client.SendTextMessageAsync(
                                chatId: user.Chat,
                                text: translatedText,
                                replyToMessageId: message.MessageId);

                user.DialogStatus = Status.other;
            }
        }

        private async Task OtherAsync(Message message, TelegramBotClient client, BotUser user) 
            => await client.SendTextMessageAsync(
                            chatId: user.Chat,
                            text: $"Привет, {user.Name}",
                            replyToMessageId: message.MessageId);

        private async Task NewUserAsync(Message message, TelegramBotClient client, BotUser user)
        {
            if (user.DialogStatus == Status.NewUser)
            {
                await client.SendTextMessageAsync(
                                chatId : user.Chat,
                                text : "Как я могу к вам обращаться?");

                user.DialogStatus = Status.WaitingNewUser;
            }
            else
            {
                user.Name = message.Text;
                await client.SendTextMessageAsync(
                                chatId : user.Chat,
                                text : "Хорошо, запомнил.");

                user.DialogStatus = Status.other;
            }
        }
    }
}