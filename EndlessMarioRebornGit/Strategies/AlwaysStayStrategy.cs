using EndlessMarioRebornGit.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit.Strategies
{
    class AlwaysStayStrategy : Strategy
    {
        public AlwaysStayStrategy()
        {

        }

        public override List<Command> GetCommands()
        {
            return new List<Command>();
        }
    }
}
