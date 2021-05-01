using EnglishBot.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace EnglishBot.Controllers
{
    public class MessageController : ApiController
    {

        [Route(@"api/message/update")]
        public async Task<OkResult> Update([FromBody] Update update)
        {
            var client = await Bot.Get();

            var message = update.Message;

            var userLogic = Bot.UserLogic;

            switch (message.Type)
            {
                case MessageType.Text:
                    await userLogic.ReceiveTextMessageAsync(message, client);
                    break;
                case MessageType.Audio:
                    await userLogic.ReceiveVoiceMessageAsync(message, client);
                    break;
                default:
                    break;
            }

            return Ok();
        }
    }
}