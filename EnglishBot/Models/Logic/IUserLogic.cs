using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EnglishBot.Models.Logic
{
    public interface IUserLogic
    {
        Task ReceiveTextMessageAsync(Message message, TelegramBotClient client);
        Task ReceiveVoiceMessageAsync(Message message, TelegramBotClient client);
    }
}
