using EnglishBot.Models.DbModels;

namespace EnglishBot.Models.Commands
{
    public class TranslateEnToRuCommand : Command
    {
        public override string Name { get; } = "/translateentoru";

        public override void Execute(BotUser user)
        {
            user.DialogStatus = Status.TranslateEnToRu;
        }
    }
}