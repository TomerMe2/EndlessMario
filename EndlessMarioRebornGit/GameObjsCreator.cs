using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndlessMarioRebornGit.Monsters;
using EndlessMarioRebornGit.Strategies;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessMarioRebornGit
{
    class GameObjsCreator
    {
        private float lastX = 0;
        private Random rndm = new Random();
        private Pipe startPipe;
        private Floor flr;
        private MrioGame gm;
        private Mario mrio;

        public GameObjsCreator(Pipe startPipe, Floor flr, MrioGame gm, Mario mrio)
        {
            lastX = 0;
            rndm = new Random();
            this.startPipe = startPipe;
            this.flr = flr;
            this.gm = gm;
            this.mrio = mrio;
        }

        /// <summary>
        /// Game should invoke every frame
        /// </summary>
        /// <returns></returns>
        public GameObject Create()
        {
            if (lastX - 200 > startPipe.Left)
            {
                mrio.AddPointsFromDistance();
                double randDbl = rndm.NextDouble();
                lastX = startPipe.Left;
                if (randDbl < 0.6)
                {
                    List<Texture2D> gmbaFacingRightTextures = new List<Texture2D>();
                    List<Texture2D> gmbaFacingLeftTextures = new List<Texture2D>();
                    foreach (string assetName in Goomba.texturesNameFacingRight)
                    {
                        gmbaFacingRightTextures.Add(gm.Content.Load<Texture2D>(@"Goomba\" + assetName));
                    }
                    foreach (string assetName in Goomba.texturesNameFacingLeft)
                    {
                        gmbaFacingLeftTextures.Add(gm.Content.Load<Texture2D>(@"Goomba\" + assetName));
                    }
                    Texture2D gmbaDeadTxtr = gm.Content.Load<Texture2D>(@"Goomba\" + Goomba.deadTextureNm);
                    Texture2D gmbaDeadTxtrFlipped = gm.Content.Load<Texture2D>(@"Goomba\" + Goomba.deadTextureFlippedNm);
                    int randInt = rndm.Next(950, 1050);
                    return new Goomba(gmbaFacingRightTextures, gmbaFacingLeftTextures, flr, randInt, new RandomLeftRightStay(), gmbaDeadTxtr, gmbaDeadTxtrFlipped, 0);
                }
                else
                {
                    float randFlt = (float)(rndm.NextDouble() * (0.45) + 0.5);
                    int randInt = rndm.Next(950, 1050);
                    return new Pipe(gm.Content.Load<Texture2D>(Pipe.textureName), Mario.MARIOSTARTLOC.X + randInt, randFlt);
                }
               
            }
            return null;
        }
    }
}
