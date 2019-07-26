using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit.Commands
{
    class InventorySwitchCommand : Command
    {
        private int numOfItem;

        public InventorySwitchCommand(int numOfItem)
        {
            this.numOfItem = numOfItem;
        }

        public int NumOfItem
        {
            get { return numOfItem; }
            private set { numOfItem = value; }
        }
    }
}
