﻿using EndlessMarioRebornGit.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit.Strategies
{
    abstract class Strategy
    {
        protected Queue<List<Command>> cmnds;

        public Strategy()
        {
            cmnds = new Queue<List<Command>>();
        }

        //Returns the current commands and clear the list
        public virtual List<Command> GetCommands()
        {
            if (cmnds.Count > 0)
            {
                return cmnds.Dequeue();
            }
            return new List<Command>();
        }

        protected virtual void AddToCommands(List<Command> cmndsLst)
        {
            cmnds.Enqueue(cmndsLst);
        }

        protected virtual void AddToCommands(Command cmnd)
        {
            List<Command> lst = new List<Command>();
            lst.Add(cmnd);
            cmnds.Enqueue(lst);
        }

    }
}
