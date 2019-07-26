using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndlessMarioRebornGit.Monsters;
using EndlessMarioRebornGit.Strategies;
using Microsoft.Xna.Framework.Graphics;
using EndlessMarioRebornGit.StillObjects;
using EndlessMarioRebornGit.MenuItems;

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
            double shouldIbuildRnd = rndm.NextDouble();
            double functionOutput = (lastX - startPipe.Left) / (1500 - mrio.Points / 50);
            functionOutput = lastX - startPipe.Left < 140 ? 0 : functionOutput;
            if (shouldIbuildRnd < functionOutput)
            {
                //if (lastX - 200 > startPipe.Left)
                //goes from 0.25 to 0, it's smaller the more progress the player have
                double progressFee = startPipe.Left < 0 ? 0.25 - (mrio.Points / 100000) : 0.25;
                progressFee = progressFee < 0 ? 0 : progressFee;
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
            Texture2D gmbaDeadTxtr = gm.Content.Load<Texture2D>(Goomba.deadTextureNm);
            Texture2D gmbaDeadTxtrFlipped = gm.Content.Load<Texture2D>(Goomba.deadTextureFlippedNm);
            int randInt = rndm.Next(950, 1050);
            return new Goomba(GetTexturesForList(Goomba.texturesNameFacingRight),
                GetTexturesForList(Goomba.texturesNameFacingLeft), flr, randInt, new RandomLeftRightStay(),
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
            return new CannonBomb(GetTexturesForList(CannonBomb.texturesNameFacingRight),
                GetTexturesForList(CannonBomb.texturesNameFacingLeft), flr, 1000, randInt, cnnDeadTxtr,
                cnnDeadTxtrFlipped, mrio.Points);
        }

        public HugeCannonBomb CreateHugeCannonBomb()
        {
            Texture2D cnnDeadTxtr = gm.Content.Load<Texture2D>(HugeCannonBomb.deadTextureNm);
            Texture2D cnnDeadTxtrFlipped = gm.Content.Load<Texture2D>(HugeCannonBomb.deadTextureFlippedNm);
            return new HugeCannonBomb(GetTexturesForList(HugeCannonBomb.texturesNameFacingRight),
                GetTexturesForList(HugeCannonBomb.texturesNameFacingLeft), flr, -500, cnnDeadTxtr,
                cnnDeadTxtrFlipped, mrio.Points);
        }

        //TODO: CHECK THIS
        public List<GameObject> CreateInventoryStrip(float gameWindowWidth)
        {
            List<GameObject> toRet = new List<GameObject>();
            Texture2D txtrForBkrnd = gm.Content.Load<Texture2D>(BackgroundBehindInentory.textureName);
            BackgroundBehindInentory bkrnd = new BackgroundBehindInentory(txtrForBkrnd, gm.GetGameWindowWidth() / 2 - txtrForBkrnd.Width*BackgroundBehindInentory.SCALE / 2);
            toRet.Add(bkrnd);
            float breaksBetweenCells = 0;
            float yLoc = 0;
            for (int i = 1; i <= ItemCell.GetNumOfCells(); i++)
            {
                Texture2D txtrOfCellSelected = gm.Content.Load<Texture2D>(ItemCell.GetCellTextureSelectedName(i));
                Texture2D txtOfCellNotSelected = gm.Content.Load<Texture2D>(ItemCell.GetCellTextureNormalName(i));
                if (breaksBetweenCells == 0)
                {
                    float sumOfCellsWidth = ItemCell.GetNumOfCells() * txtrOfCellSelected.Width;
                    breaksBetweenCells = (txtrForBkrnd.Width*BackgroundBehindInentory.SCALE - sumOfCellsWidth) / (ItemCell.GetNumOfCells() + 1);
                    yLoc = bkrnd.Top + ((bkrnd.Bottom - bkrnd.Top) - txtOfCellNotSelected.Height * ItemCell.SCALE) / 2;
                }
                float xLoc = bkrnd.Left + breaksBetweenCells * i + (i - 1) * txtrOfCellSelected.Width;
                ItemCell cl = new ItemCell(txtOfCellNotSelected, txtrOfCellSelected, xLoc, yLoc, i);
                toRet.Add(cl);
                if (i == 1)
                {
                    cl.Select();
                }
            }
            return toRet;
        }


        private List<Texture2D> GetTexturesForList(string[] assetsName)
        {
            List<Texture2D> toRet = new List<Texture2D>();
            foreach (string assetName in assetsName)
            {
                toRet.Add(gm.Content.Load<Texture2D>(assetName));
            }
            return toRet;
        }

    }
}