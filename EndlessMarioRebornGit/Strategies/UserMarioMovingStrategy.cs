using EndlessMarioRebornGit.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit.Strategies
{
    class UserMarioMovingStrategy : Strategy
    {
        public UserMarioMovingStrategy()
        {

        }

        public void LeftArrowClicked()
        {
            AddToCommands(new MoveLeftCommand());
        }

        public void RightArrowClicked()
        {
            AddToCommands(new MoveRightCommand());
        }

        public void SpaceClicked()
        {
            AddToCommands(new JumpCommand());
        }
    }
}
