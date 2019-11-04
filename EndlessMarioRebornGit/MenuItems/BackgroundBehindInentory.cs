using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit.MenuItems
{
    class BackgroundBehindInentory : GameObject
    {
        public static string textureName = "BehindInventoryCells";
        public const float SCALE = 0.6f;

        public BackgroundBehindInentory(Texture2D texture, float xLoc, float yLoc) :
            base(new Vector2(xLoc, yLoc), texture, SCALE, false)
        {

        }

    }
}
