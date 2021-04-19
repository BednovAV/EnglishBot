using EnglishBot.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using TranslatorLibrary;

namespace EnglishBot.Models
{
    public class UserLogic : IUserLogic
    {

        public async Task ReceiveMessageAsync(Message message, TelegramBotClient client)
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

                    case Status.other:
                        await OtherAsync(message, client, user);
                        break;
                }

                await db.SaveChangesAsync();
            }

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
                Language from;
                Language to;

                if (user.DialogStatus == Status.WaitingTranslteEnToRu)
                {
                    from = Language.en;
                    to = Language.ru;
                }
                else
                {
                    from = Language.ru;
                    to = Language.en;
                }

                var translatedText = GoogleTranslator.Translate(message.Text, from, to);

                await client.SendTextMessageAsync(
                                chatId: user.Chat,
                                text: translatedText,
                                replyToMessageId: message.MessageId);

                user.DialogStatus = Status.other;
            }
        }

        private async Task OtherAsync(Message message, TelegramBotClient client, BotUser user)
        {
            await client.SendTextMessageAsync(
                            chatId : user.Chat,
                            text : $"Привет, {user.Name}",
                            replyToMessageId : message.MessageId);
        }

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