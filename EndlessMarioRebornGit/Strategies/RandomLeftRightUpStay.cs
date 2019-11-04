using EndlessMarioRebornGit.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit.Strategies
{
    class RandomLeftRightUpStay : Strategy
    {
        private Random rnd;
        private int prevCount;

        public RandomLeftRightUpStay()
        {
            rnd = new Random();
            prevCount = 0;
        }

        /// <summary>
        /// Should be invoked every round
        /// </summary>
        public override List<Command> GetCommands()
        {
            prevCount--;
            if (prevCount < 0)
            {
                double rndDub = rnd.NextDouble();
                if (rndDub < 0.3)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        AddToCommands(new MoveLeftCommand());
                    }
                }
                else if (rndDub < 0.6)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        AddToCommands(new MoveRightCommand());
                    }
                }
                rndDub = rnd.NextDouble();
                if (rndDub < 15f/50f)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        AddToCommands(new MoveUpCommand());
                    }
                }
                prevCount = 15;
            }
            return base.GetCommands();
        }
    }
}
