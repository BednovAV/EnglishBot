using EnglishBot.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishBot.Models.Commands
{
    public class TranslateRuToEnCommand : Command
    {
        public override string Name { get; } = "/translaterutoen";

        public override void Execute(BotUser user)
        {
            user.DialogStatus = Status.TranslateRuToEn;
        }
    }
}