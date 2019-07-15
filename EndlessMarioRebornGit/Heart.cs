using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit
{
    class Heart : GameObject
    {
        public static string textureName = "Heart";
        private const float START_SCALE = 0.4f;
        private const float START_Y_LOC = 10;

        public Heart(Texture2D texture, float locX) :
            base(new Vector2(locX, START_Y_LOC), texture, START_SCALE, false)
        {

        }

        /// <summary>
        /// Invoked when this heart need to disappear from the screen
        /// </summary>
        public void PrepareForDisposal()
        {
            isNeedDisposal = true;
            needToBeDraw = false;
        }
    }
}
