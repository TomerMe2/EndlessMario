using EndlessMarioRebornGit.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit.Strategies
{
    class AlwaysRightStrategy : Strategy
    {
        public AlwaysRightStrategy()
        {

        }

        public override List<Command> GetCommands()
        {
            List<Command> toRet = new List<Command>();
            toRet.Add(new MoveRightCommand());
            return toRet;
        }
    }
}
