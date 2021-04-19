using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EnglishBot.Models.DbModels
{
    public class BotDbContext : DbContext
    {
        public BotDbContext() : base("DbConnection") { }

        public DbSet<BotUser> Users { get; set; }
    }
}