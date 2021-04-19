using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishBot.Models.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract void Execute(User user);

        public virtual bool Contains(string command)
        {
            return Name == command;
        }
    }
}