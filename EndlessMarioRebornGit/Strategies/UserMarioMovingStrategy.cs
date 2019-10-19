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

        public void Bclicked()
        {
            AddToCommands(new ChestSwitchCommand());
        }

        public void Sclicked()
        {
            AddToCommands(new ShootCommand());
        }

        //TODO: MAKE IT SAFE WHILE ATTACKING
        public void NumClicked(int num)
        {
            if (num >= 1 && num <= 6)
            {
                AddToCommands(new InventorySwitchCommand(num));
            }
        }

        public void CharClicked(char chr)
        {
            switch (chr)
            {
                case 'Q':
                    AddToCommands(new ChestCellSwitchCommand(1));
                    break;
                case 'W':
                    AddToCommands(new ChestCellSwitchCommand(2));
                    break;
                case 'E':
                    AddToCommands(new ChestCellSwitchCommand(3));
                    break;
                case 'R':
                    AddToCommands(new ChestCellSwitchCommand(4));
                    break;
                case 'T':
                    AddToCommands(new ChestCellSwitchCommand(5));
                    break;
                case 'Y':
                    AddToCommands(new ChestCellSwitchCommand(6));
                    break;
            }            
        }

        public void EnterClicked()
        {
            AddToCommands(new SwitchInventoryAndChestCommand());
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
