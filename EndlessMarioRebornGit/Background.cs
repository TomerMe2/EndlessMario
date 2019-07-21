using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndlessMarioRebornGit.MenuObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessMarioRebornGit
{
    class Background
    {
        public const string textureName = "background";
        protected Texture2D[] bkgrndImages;
        protected float[] LocX;
        protected float[] LocY;
        protected int gameWidth;
        protected int gameHeight;
        protected bool flagIsMovingRight = true;
        List<GameObject> allObjectsButMario;
        /// <summary>
        /// This method will create a background
        /// </summary>
        /// <param name="bkgrndImage">The image of the background</param>
        /// <param name="bkgrndImage2">Same image</param>
        /// <param name="gameWidth">Game width</param>
        /// <param name="gameHeight">Game height</param>
        public Background(Texture2D bkgrndImage, Texture2D bkgrndImage2, int gameWidth, int gameHeight, List<GameObject> allObjectsButMario)
        {
            this.bkgrndImages = new Texture2D[2] { bkgrndImage, bkgrndImage2 };
            this.LocX = new float[2] { 0, gameWidth };
            this.LocY = new float[2] { 0, 0 };
            this.gameHeight = gameHeight;
            this.gameWidth = gameWidth;
            this.allObjectsButMario = allObjectsButMario;
        }

        /// <summary>
        /// This method will return two Rectangles ready to enter to the draw functions, in order to draw the backgrounds.
        /// </summary>
        /// <returns></returns>
        public Rectangle[] BackgroundLocToDraw()
        {
            Rectangle[] toReturn = new Rectangle[2];
            toReturn[0] = new Rectangle((int)this.LocX[0], (int)this.LocY[0], this.gameWidth, this.gameHeight);
            toReturn[1] = new Rectangle((int)this.LocX[1], (int)this.LocY[1], this.gameWidth, this.gameHeight);
            return toReturn;
        }

        /// <summary>
        /// USE ONLY IN UPDATE MAIN METHOD. This method will update the background and will let mario walk.
        /// </summary>
        /// <param name="marioSpeed"></param>
        public void BackgroundUpate(float marioSpeed)
        {
            foreach (GameObject obj in this.allObjectsButMario)
            {
                if (!(obj is Mario) && !(obj is Heart) && !(obj is BlackScreen))
                {
                    obj.MoveOnX(-marioSpeed);
                }
            }
            this.LocX[0] -= marioSpeed;
            this.LocX[1] -= marioSpeed;
            //Moving backwards
            if (marioSpeed < 0)
            {
                if (this.LocX[0] > this.gameWidth)
                {
                    this.LocX[0] = this.LocX[1] - this.gameWidth;
                }
                if (this.LocX[1] > this.gameWidth)
                {
                    this.LocX[1] = this.LocX[0] - this.gameWidth;
                }
            }
            //Moving forward
            if (marioSpeed > 0)
            {
                if (this.LocX[0] < -this.gameWidth)
                {
                    this.LocX[0] = this.LocX[1] + this.gameWidth;
                }
                if (this.LocX[1] < -this.gameWidth)
                {
                    this.LocX[1] = this.LocX[0] + this.gameWidth;
                }
            }
        }

        public Texture2D[] BackgroundTextures
        {
            get { return this.bkgrndImages; }
        }

        public int GameWidth
        {
            get { return gameWidth; }
        }

        public int GameHeight
        {
            get { return gameHeight; }
        }
    }
}
