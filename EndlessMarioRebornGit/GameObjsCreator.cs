using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessMarioRebornGit
{
    static class GameObjsCreator
    {
        private static float lastX = 0;
        private static Random rndm = new Random();
        public static GameObject Create(Pipe startPipe, MrioGame gm)
        {
            if (lastX - 200 > startPipe.Left)
            {
                float randFlt = (float)(rndm.NextDouble() * (0.45) + 0.5);
                lastX = startPipe.Left;
                int randInt = rndm.Next(950, 1050);
                return new Pipe(gm.Content.Load<Texture2D>(Pipe.textureName), Mario.MARIOSTARTLOC.X + randInt, randFlt);
            }
            return null;
        }
    }
}
