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
        public AlwaysLeftStrategy()
        {

        }

        public override List<Command> GetCommands()
        {
            return new List<Command> { new MoveLeftCommand()};
        }

    }
}
