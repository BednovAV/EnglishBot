using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EnglishBot.Models
{
    public enum Status
    {
        NewUser,
        WaitingNewUser,
        other
    }

    public class User
    {
        public string Name { get; private set; }

        public long ChatId { get; }

        public Status DialogStatus { get; set; }

        public User(string name, long chatId)
        {
            Name = name;
            ChatId = chatId;
            DialogStatus = Status.NewUser;
        }

        public async Task ReceiveMessageAsync(Message message, TelegramBotClient client)
        {
            if (message.Text.First() == '/')
            {
                foreach (var command in Bot.Commands)
                {
                    if (command.Contains(message.Text))
                    {
                        command.Execute(this);
                        break;
                    }
                }
            }
            switch(DialogStatus)
            {
                case Status.NewUser:
                case Status.WaitingNewUser:
                    await NewUserAsync(message, client);
                    break;
                case Status.other:
                    await OtherAsync(client);
                    break;
            }
        }

        private async Task OtherAsync(TelegramBotClient client)
        {
            await client.SendTextMessageAsync(ChatId, $"Привет, {Name}");
        }

        private async Task NewUserAsync(Message message, TelegramBotClient client)
        {
            if (DialogStatus == Status.NewUser)
            {
                await client.SendTextMessageAsync(ChatId, "Как я могу к вам обращаться?");
                DialogStatus = Status.WaitingNewUser;
            }
            else
            {
                Name = message.Text;
                await client.SendTextMessageAsync(ChatId, "Хорошо, запомнил.");
                DialogStatus = Status.other;
            }
        }
    }
}