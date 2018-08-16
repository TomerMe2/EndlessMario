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
        public static string textureName = "GreenPipe.png";

        public Pipe(Texture2D texture, Vector2 loc, float scale) : base(loc, texture, scale, true)
        {

        } 
    }
}
