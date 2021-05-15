using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishBot.Models.DbModels
{
    public class BotUser
    {
        public int Id { get; set; }

        public long Chat { get; set; }

        public string Name { get; set; }

        public string WaitingWord { get; set; }

        public Status DialogStatus { get; set; }
    }
}