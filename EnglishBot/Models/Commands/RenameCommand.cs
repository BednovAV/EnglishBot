using EnglishBot.Models.DbModels;

namespace EnglishBot.Models.Commands
{
    public class RenameCommand : Command
    {
        public override string Name { get; } = "/rename";

        public override void Execute(BotUser user)
        {
            user.DialogStatus = Status.NewUser;
        }
    }
}