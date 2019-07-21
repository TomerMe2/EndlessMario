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
        List<Command> cmndsLst;
        public UserMarioMovingStrategy()
        {
            cmndsLst = new List<Command>();
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

        public void ChestSwitchClicked()
        {
            AddToCommands(new ChestSwitchCommand());
        }

        /// <summary>
        /// This Stategy uses only one list, and not a queue. That is because commands need to be in real time.
        /// </summary>
        /// <returns></returns>
        public override List<Command> GetCommands()
        {
            List<Command> toRet = new List<Command>(cmndsLst);
            cmndsLst.Clear();
            return toRet;
        }

        protected override void AddToCommands(Command cmnd)
        {
            cmndsLst.Add(cmnd);
        }
    }
}
