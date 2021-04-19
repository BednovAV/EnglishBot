using EnglishBot.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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