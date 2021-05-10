using EnglishBot.Models;
using EnglishBot.Models.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnglishBot.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            string result = string.Empty;
            using (BotDbContext db = new BotDbContext())
            {
                var users = db.Users;

                foreach (var item in users)
                {
                    result += $"{item.Id} {item.Chat} {item.Name} {item.DialogStatus} <br>";
                }
            }

            result += VoiceTest.LastVoiceMessage;
            return result;
        }

        
    }
}