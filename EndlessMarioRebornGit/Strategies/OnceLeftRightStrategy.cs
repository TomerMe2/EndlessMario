using EndlessMarioRebornGit.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit.Strategies
{
    class OnceLeftRightStrategy : Strategy
    {
        Command cmnd;

        public OnceLeftRightStrategy()
        {
            cmnd = null;
        }

        public override List<Command> GetCommands()
        {
            List<Command> toRet = new List<Command>();
            if (cmnd != null)
            {
                toRet.Add(cmnd);
            }
            cmnd = null;
            return toRet;
        }

        public void GoLeft()
        {
            cmnd = new MoveLeftCommand();
        }

        public void GoRight()
        {
            cmnd = new MoveRightCommand();
        }
    }
}
