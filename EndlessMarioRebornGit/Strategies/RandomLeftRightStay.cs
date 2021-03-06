﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndlessMarioRebornGit.Commands;

namespace EndlessMarioRebornGit.Strategies
{
    class RandomLeftRightStay : Strategy
    {
        private Random rnd;
        private int prevCount;

        public RandomLeftRightStay()
        {
            rnd = new Random();
            prevCount = 0;
        }

        /// <summary>
        /// 30% chance for moving left, 30% chance for moving right, 40% chance for stay in place.
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
                    for (int i = 0; i < 15; i++)
                    {
                        AddToCommands(new MoveLeftCommand());
                    }
                    prevCount = 15;
                }
                else if (rndDub < 0.6)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        AddToCommands(new MoveRightCommand());
                    }
                    prevCount = 15;
                }
                else
                {
                    //add nothing
                    prevCount = 15;
                }
            }
            return base.GetCommands();
        }
    }
}
