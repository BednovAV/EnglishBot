using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace EnglishBot.Models
{
    public interface IUserLogic
    {
        Task ReceiveMessageAsync(Message message, TelegramBotClient client);
    }
}
