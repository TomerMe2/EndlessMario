using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessMarioRebornGit
{
    class Floor : GameObject
    {
        private float gameHeight;
        private float gameWidth;

        public Floor(int gameWidth, int gameHeight) : base(new Vector2(0, Physics.FLOOR_LOC), 1, true)
        {
            this.gameHeight = gameHeight;
            //this.gameWidth = gameWidth;
            this.gameWidth = float.MaxValue;
        }

        public override float Bottom
        {
            get { return gameHeight; }
        }

        public override float Right
        {
            get { return gameWidth; }
        }

        public override void MoveOnX(float howMuch)
        {
            //do nothing
        }
    }
}
