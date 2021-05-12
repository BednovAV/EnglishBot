using EnglishBot.Models.DbModels;

namespace EnglishBot.Models.Commands
{
    public class PhraseologCommand : Command
    {
        public override string Name { get; } = "/phraseological";

        public override void Execute(BotUser user)
        {
            user.DialogStatus = Status.GetPhraseologiсal;
        }
    }
}