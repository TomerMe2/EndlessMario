using EndlessMarioRebornGit.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit.Strategies
{
    class AlwaysLeftStrategy : Strategy
    {

        public override List<Command> GetCommands()
        {
            List<Command> toRet = new List<Command>();
            toRet.Add(new MoveLeftCommand());
            return toRet;
        }

    }
}
