using EnglishBot.Models.DbModels;

namespace EnglishBot.Models.Commands
{
    public class GetWordCommand : Command
    {
        public override string Name { get; } = "/getword";

        public override void Execute(BotUser user)
        {
            user.DialogStatus = Status.GetWord;
        }
    }
}