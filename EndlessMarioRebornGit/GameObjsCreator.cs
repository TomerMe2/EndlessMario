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
            //TODO: think on a function that says when to create an object (0% on 0, 100% on 300, growing in pow of 2 at least)
            if (lastX - 200 > startPipe.Left)
            {
                //goes from 0.25 to 0, it's smaller the more progress the player have
                double progressFee = startPipe.Left < 0 ? 0.25 - (mrio.Points / 100000) : 0.25;
                progressFee = progressFee < 0 ? 0 : progressFee;
                //mrio.AddPointsFromDistance();
                if (progressFee < 0.10)
                {
                    bool deb = true;
                }
                double randDbl = rndm.NextDouble();
                lastX = startPipe.Left;
                return randDbl < 0.3 + progressFee ? CreateGoomba() :
                    randDbl < 0.6 ? (GameObject)CreateCannonBomb() :
                    CreatePipe();               
            }
            return null;
        }

        private Pipe CreatePipe()
        {
            float randFlt = (float)(rndm.NextDouble() * (0.45) + 0.5);
            int randInt = rndm.Next(950, 1050);
            return new Pipe(gm.Content.Load<Texture2D>(Pipe.textureName), Mario.MARIOSTARTLOC.X + randInt, randFlt);
        }

        private Goomba CreateGoomba()
        {
            Texture2D gmbaDeadTxtr = gm.Content.Load<Texture2D>(@"Goomba\" + Goomba.deadTextureNm);
            Texture2D gmbaDeadTxtrFlipped = gm.Content.Load<Texture2D>(@"Goomba\" + Goomba.deadTextureFlippedNm);
            int randInt = rndm.Next(950, 1050);
            return new Goomba(GetTexturesForList(Goomba.texturesNameFacingRight, @"Goomba\"),
                GetTexturesForList(Goomba.texturesNameFacingLeft, @"Goomba\"), flr, randInt, new RandomLeftRightStay(),
                gmbaDeadTxtr, gmbaDeadTxtrFlipped, 0);
        }

        private CannonBomb CreateCannonBomb()
        {
            Texture2D cnnDeadTxtr = gm.Content.Load<Texture2D>(CannonBomb.deadTextureNm);
            Texture2D cnnDeadTxtrFlipped = gm.Content.Load<Texture2D>(CannonBomb.deadTextureFlippedNm);
            int randInt;
            if (rndm.NextDouble() < 0.7)
            {
                randInt = rndm.Next(0, 10);
            }
            else
            {
                randInt = rndm.Next(150, 250);
            }
            return new CannonBomb(GetTexturesForList(CannonBomb.texturesNameFacingRight, ""),
                GetTexturesForList(CannonBomb.texturesNameFacingLeft, ""), flr, 1000, randInt, cnnDeadTxtr,
                cnnDeadTxtrFlipped, mrio.Points);
        }


        private List<Texture2D> GetTexturesForList(string[] assetsName, string prefix)
        {
            List<Texture2D> toRet = new List<Texture2D>();
            foreach (string assetName in assetsName)
            {
                toRet.Add(gm.Content.Load<Texture2D>(prefix + assetName));
            }
            return toRet;
        }
    }
}
