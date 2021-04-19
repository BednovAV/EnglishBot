using EnglishBot.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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