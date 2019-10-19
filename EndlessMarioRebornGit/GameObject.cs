using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndlessMarioRebornGit.Monsters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EndlessMarioRebornGit.StillObjects;
using EndlessMarioRebornGit.Weapons;

namespace EndlessMarioRebornGit
{
    abstract class GameObject
    {
        protected Vector2 loc;
        protected Texture2D currentTexture;
        protected float scale;
        protected float angleInRads;  //in radians
        protected bool isCollideAble;
        protected bool isNeedDisposal;
        protected bool needToBeDraw;

        private bool hasMeetPipe = false;

        public GameObject(Vector2 loc, Texture2D texture, float scale, bool isCollideAble)
        {
            this.loc = loc;
            this.currentTexture = texture;
            this.scale = scale;
            angleInRads = 0;
            this.isCollideAble = isCollideAble;
            isNeedDisposal = false;
            needToBeDraw = true; 
        }

        public GameObject(Vector2 loc, float scale, bool isCollideAble)
        {
            this.loc = loc;
            this.scale = scale;
            this.isCollideAble = isCollideAble;
        }

        #region GettersSetters
        virtual public Vector2 Loc
        {
            get { return loc; }
            set { loc = value; }
        }

        virtual public float AngleInRads
        {
            get { return angleInRads; }
            protected set { angleInRads = value; }
        }

        public bool NeedToBeDraw
        {
            get { return needToBeDraw; }
        }

        public virtual bool IsNeedDisposal
        {
            get { return isNeedDisposal; }
            private set { isNeedDisposal = value; }
        }

        virtual public Texture2D CurrentTexture
        {
            get { return this.currentTexture; }
            set { this.currentTexture = value; }
        }

        virtual public float Scale
        {
            get { return this.scale; }
        }

        virtual public bool IsCollideAble
        {
            get { return this.isCollideAble; }
        }

        virtual public float Top
        {
            get { return Loc.Y; }
        }

        virtual public float Bottom
        {
            get { return Loc.Y + currentTexture.Bounds.Height * scale; }
        }

        virtual public float Left
        {
            get { return Loc.X; }
        }

        virtual public float Right
        {
            get { return (float)(Math.Round(Loc.X + currentTexture.Bounds.Width * scale)); }
        }

        #endregion

        public virtual void MoveOnX(float howMuch)
        {
            this.loc.X = this.loc.X + howMuch;
        }

        /// <summary>
        /// Adds howMuchInRads to the current angle
        /// IF ANGLE IS NOT 0, ALL PHYSICS ENGINE GOES TO HELL
        /// </summary>
        public virtual void Rotate(float howMuchInRads)
        {
            angleInRads = angleInRads + howMuchInRads;
            //normalize it
            while (angleInRads > 2*Math.PI)
            {
                angleInRads = angleInRads - (float)(2 * Math.PI);
            }
            while (angleInRads < -2 * Math.PI)
            {
                angleInRads = angleInRads + (float)(2 * Math.PI);
            }
        }

        protected virtual void HandleCollusion(GameObject other, List<Direction> dirs) { }

        protected virtual void HandleCollusion(Pipe other, List<Direction> dirs) { }

        protected virtual void HandleCollusion(Floor other, List<Direction> dirs)
        {
            //Direction is always Up
        }

        protected virtual void HandleCollusion(Mario other, List<Direction> dirs) { }

        protected virtual void HandleCollusion(Monster other, List<Direction> dirs) { }

        protected virtual void HandleCollusion(CannonBomb other, List<Direction> dirs) { }

        protected virtual void HandleCollusion(Weapon other, List<Direction> dirs) { }


        private bool CheckCollusionRight(float otherLeft)
        {
            return (otherLeft <= this.Right && otherLeft >= this.Left);
        }

        private bool CheckCollusionLeft(float otherRight)
        {
            return (otherRight >= this.Left && otherRight <= this.Right);
        }

        private bool CheckCollusionUp(float otherBottom)
        {
            return (otherBottom >= this.Top && otherBottom <= this.Bottom);
        }

        private bool CheckCollusionBottom(float otherTop)
        {
            return (otherTop <= this.Bottom && otherTop >= this.Top);
        }


        /// <summary>
        /// Returns the directions of the collusion of other with this and invokes handle collusion
        /// </summary>
        protected List<Direction> ProtectedCollusion(MovingObj other, float thisLeft, float thisRight, float thisTop, float thisBottom)
        {
            float otherTop = other.Top + other.SpeedY;
            float otherBottom = other.Bottom + other.SpeedY;
            float otherLeft = other.Left + other.SpeedX;
            float otherRight = other.Right + other.SpeedX;
            List<Direction> dirs = new List<Direction>();
            if (!(thisLeft <= otherRight && thisRight >= other.Left && thisBottom >= otherTop && thisTop <= otherBottom))
            {
                //there's no collusion
                return dirs;
            }
            if (this is Floor)
            {
                if (otherBottom >= thisTop)
                {
                    dirs.Add(Direction.Up);
                    other.HandleCollusion((dynamic)this, dirs);
                }
                return dirs;
            }
            //other is checking if he is colliding me
            //check collusion from right, speed must be negative cus other must go right
            if (other.SpeedX < 0 && thisRight > otherLeft && thisLeft <= otherLeft)
            {
                //it's in X range of collusion, check Y
                if (thisTop <= otherTop && thisBottom >= otherBottom)
                {
                    //this is Bigger than other (other is inside of this Y)
                    dirs.Add(Direction.Right);
                }
                else if (otherTop <= thisTop && otherBottom >= thisBottom)
                {
                    //other is bigger than this
                    dirs.Add(Direction.Right);
                }
                else if (!(this is Floor) && otherTop <= thisTop && otherBottom >= thisTop)
                {
                    //other is in limbo
                    dirs.Add(Direction.Right);
                }
            }
            if (!dirs.Contains(Direction.Right))
            {
                //check from left, speed must be positive cus other must go left
                if (other.SpeedX > 0 && thisLeft <= otherRight && thisRight >= otherRight)
                {
                    //it's in X range of collusion, check Y
                    if (thisTop <= otherTop && thisBottom >= otherBottom)
                    {
                        //this is Bigger than other (other is inside of this Y)
                        dirs.Add(Direction.Left);
                    }
                    else if (otherTop <= thisTop && otherBottom >= thisBottom)
                    {
                        //other is bigger than this
                        dirs.Add(Direction.Left);
                    }
                    else if (!(this is Floor) && otherTop <= thisTop && otherBottom >= thisTop)
                    {
                        //other is in limbo
                        dirs.Add(Direction.Left);
                    }
                }
            }
            if (thisTop <= otherBottom && thisBottom >= otherTop)
            {
                //in Y range. check if X is in range
                if (thisLeft <= otherLeft && thisRight >= otherRight)  //this is bigger than other
                {
                    dirs.Add(Direction.Up);
                }
                else if (otherLeft <= thisLeft && otherRight >= thisRight)  //other is bigger than this
                {
                    dirs.Add(Direction.Up);
                }
                else if ((other.PrevSarfuce == null || this.Equals(other.PrevSarfuce)) && thisLeft >= otherLeft && thisLeft <= otherRight)  //other partially standing on this, other is on right side of this
                {
                    dirs.Add(Direction.Up);
                }
                else if ((other.PrevSarfuce == null || this.Equals(other.PrevSarfuce)) && thisRight <= otherRight && thisRight >= otherLeft)   //other partially standing on this, other is on left side of this
                {
                    dirs.Add(Direction.Up);
                }
            }
            if (!dirs.Contains(Direction.Up))
            {
                if (thisBottom >= otherTop && thisTop <= otherBottom)
                {
                    //Y is in range. Check if X is in range
                    if (thisLeft <= otherLeft && thisRight >= otherRight)  //this is bigger than other
                    {
                        dirs.Add(Direction.Down);
                    }
                    else if (otherLeft <= thisLeft && otherRight >= thisRight)  //other is bigger than this
                    {
                        dirs.Add(Direction.Down);
                    }
                }
            }
            if (dirs.Contains(Direction.Up) && (dirs.Contains(Direction.Left) || dirs.Contains(Direction.Right)))
            {
                if (dirs.Contains(Direction.Left))
                {
                    if (other.Bottom > thisTop)
                    {
                        //flip em
                        dirs[0] = Direction.Up;
                        dirs[1] = Direction.Left;
                    }
                }
                if (dirs.Contains(Direction.Right))
                {
                    if (other.Bottom > thisTop)
                    {
                        //flip em
                        dirs[0] = Direction.Up;
                        dirs[1] = Direction.Right;
                    }
                }
            }
            //Checking for a possibilty that "other" was way too fast, and just passed "this" in 1 turn
            if (dirs.Count == 0)
            {
                if (otherBottom <= thisBottom && otherTop >= thisTop)   //if it's in cullosion range in terms of Y axis
                {
                    if (other.SpeedX > 0 && thisLeft >= other.Right && thisRight <= otherLeft)     //if other passed this in the turn (the speed has to be positive)
                    {
                        dirs = new List<Direction> { Direction.Left };
                    }
                    else if (other.SpeedX < 0 && thisRight <= other.Left && thisLeft >= otherRight)
                    {
                        dirs = new List<Direction> { Direction.Right };
                    }
                }
            }
            other.HandleCollusion((dynamic)this, dirs);
            return dirs;
        }

        //protected virtual bool PixelCollusion(MovingObj other)
        //{
        //    Color[] bitsThis = new Color[currentTexture.Width * currentTexture.Height];
        //    Color[] bitsOther = new Color[other.currentTexture.Width * other.currentTexture.Height];
        //    Color[] bitsThisScaled = new Color[RoundUp(currentTexture.Width * currentTexture.Height * scale)];
        //    Color[] bitsOtherScaled = new Color[RoundUp(other.currentTexture.Width * other.currentTexture.Height * other.scale)];
        //}

        //private Color[] ScaledClrs(Texture2D txtr, float scale)
        //{
        //    //NOT WORKING. FK.
        //    Color[] clrs = new Color[txtr.Width * txtr.Height];
        //    int colsInScaled = RoundUp(txtr.Width * scale);
        //    int rowsInScaled = RoundUp(txtr.Height * scale);
        //    Color[] scaled = new Color[colsInScaled * rowsInScaled];
        //    for (int row = 0; row < txtr.Height; row++)
        //    {
        //        int rowStartNonTransperent = -1;
        //        int rowEndNonTransperent = -1;
        //        for (int col = 0; col < txtr.Width; col++)
        //        {
        //            //clrs[row * width + column] how to get to the current cell
        //            if (rowStartNonTransperent == -1 && clrs[row * txtr.Width + col] != Color.Transparent)
        //            {
        //                rowStartNonTransperent = col;
        //            }
        //            if (rowStartNonTransperent != -1 && clrs[row * txtr.Width + col] == Color.Transparent)
        //            {
        //                rowEndNonTransperent = col;
        //            }
        //        }
        //        for (int currColInScaled = 0; currColInScaled < colsInScaled; currColInScaled++)
        //        {
        //            scaled[]
        //        }
        //    }
        //}

        //private int RoundUp(float num)
        //{
        //    bool a = num - (int)num > 0;
        //    int rounded = Convert.ToInt32(num);
        //    return (num - rounded) > 0 ? rounded + 1 : rounded;
        //}

        /// <summary>
        /// Returns the direction of other realative to this if there's a collusion. Returns Direction.None otherwise
        /// Prefers left/right collusions over up/bottom collusions
        /// </summary>
        public virtual List<Direction> Collusion(MovingObj other)
        {
            return ProtectedCollusion(other, this.Left, this.Right, this.Top, this.Bottom);
        }
    }
}
