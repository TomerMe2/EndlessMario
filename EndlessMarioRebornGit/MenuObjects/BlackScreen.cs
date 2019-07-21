using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit.MenuObjects
{
    class BlackScreen : GameObject
    {
        public static string textureName = "BlackScreen";
        private const float START_SCALE = 1;

        public BlackScreen(Texture2D texture) :
            base(new Vector2(0, 0), texture, START_SCALE, false)
        {

        }

    }
}
