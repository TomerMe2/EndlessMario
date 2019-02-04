using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessMarioRebornGit
{
    class Pipe : GameObject
    {
        public static string textureName = "GreenPipe";

        public Pipe(Texture2D texture, float locX, float scale) :
            base(new Vector2(locX, Physics.FLOOR_LOC - texture.Height*scale), texture, scale, true)
        {

        }
    }
}
