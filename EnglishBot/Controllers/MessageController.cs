using EnglishBot.Models;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Telegram.Bot.Types;


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

            await userLogic.ReceiveMessageAsync(message, client);

            return Ok();
        }
    }
}