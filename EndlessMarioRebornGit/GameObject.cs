using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessMarioRebornGit
{
    abstract class GameObject
    {
        protected Vector2 loc;
        protected Texture2D currentTexture;
        protected float scale;
        protected bool isCollideAble;

        private bool hasMeetPipe = false;

        public GameObject(Vector2 loc, Texture2D texture, float scale, bool isCollideAble)
        {
            this.loc = loc;
            this.currentTexture = texture;
            this.scale = scale;
            this.isCollideAble = isCollideAble;
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
            get { return Loc.Y + currentTexture.Bounds.Height*scale; }
        }

        virtual public float Left
        {
            get { return Loc.X; }
        }

        virtual public float Right
        {
            get { return Loc.X + currentTexture.Bounds.Width*scale; }
        }

        #endregion

        public virtual void MoveOnX(float howMuch)
        {
            this.loc.X = this.loc.X + howMuch;
        }

        protected virtual void HandleCollusion(Pipe other, Direction dir) { }

        protected virtual void HandleCollusion(Floor other, Direction dir)
        {
            //Direction is always Up
        }

        private bool CheckCollusionRight(float otherLeft)
        {
            if (otherLeft <= this.Right && otherLeft >= this.Left)
            {
                return true;
            }
            return false;
        }

        private bool CheckCollusionLeft(float otherRight)
        {
            if (otherRight >= this.Left && otherRight <= this.Right)
            {
                return true;
            }
            return false;
        }

        private bool CheckCollusionUp(float otherBottom)
        {
            if (otherBottom >= this.Top && otherBottom <= this.Bottom)
            {
                return true;
            }
            return false;
        }

        private bool CheckCollusionBottom(float otherTop)
        {
            if (otherTop <= this.Bottom && otherTop >= this.Top)
            {
                return true;
            }
            return false;
        }


        protected virtual void HandleCollusion(GameObject other, Direction dir) { }

        /// <summary>
        /// Returns the direction of other realative to this if there's a collusion. Returns Direction.None otherwise
        /// Prefers left/right collusions over up/bottom collusions
        /// </summary>
        public Direction Collusion(MovingObj other)
        {
            //if (RectA.X1 < RectB.X2 && RectA.X2 > RectB.X1 && RectA.Y1 < RectB.Y2 && RectA.Y2 > RectB.Y1) 
            if (Math.Abs(other.SpeedX) > 6)
            {
                string debug = "nini";
            }
            if (this is Floor && hasMeetPipe)
            {
                string debug = "ninini";
            }
            //TODO: override it in MovingObj so this obj can have speed to!
            //other is checking if he is colliding me
            float otherTop = other.Top + other.SpeedY;
            float otherBottom = other.Bottom + other.SpeedY;
            float otherLeft = other.Left + other.SpeedX;
            float otherRight = other.Right + other.SpeedX;
            Rectangle rc1, rc2;
            if (!(this.Left <= otherRight && this.Right >= other.Left && this.Bottom >= otherTop && this.Top <= otherBottom))
            {
                //there's no collusion
                return Direction.None;
            }
            if (this is Pipe)
            {
                string deb = "debug";
            }
            Direction dir = Direction.None;
            if (CheckCollusionLeft(otherRight) && CheckCollusionRight(otherLeft))
            {
                //it's up or down
                if (CheckCollusionUp(otherBottom))
                {
                    dir = Direction.Up;
                }
                if (CheckCollusionBottom(otherTop))
                {
                    dir = Direction.Down;
                }
            }
            if (dir == Direction.None)
            //if (CheckCollusionUp(otherBottom) && CheckCollusionBottom(otherTop))
            {
                if (CheckCollusionLeft(otherRight))
                {
                    dir = Direction.Left;
                }
                if (CheckCollusionRight(otherLeft))
                {
                    dir = Direction.Right;
                }
            }
          

            //if (CheckCollusionLeft(otherLeft))
            //{
            //    dir = Direction.Left;
            //}
            //if (CheckCollusionRight(otherRight))
            //{
            //    dir = Direction.Right;
            //}
            //if (CheckCollusionUp(otherBottom))
            //{
            //    dir = Direction.Up;
            //}
            //if (CheckCollusionBottom(otherTop))
            //{
            //    dir = Direction.Down;
            //}
            if (this is Pipe)
            {
                string debug = "nini";
            }
            other.HandleCollusion((dynamic)this, dir);
            if (Math.Abs(other.SpeedY) > 0)
            {
                string deb = "deb";
            }
            if (Math.Abs(other.SpeedX) > 6)
            {
                string debug = "nini";
            }
            return dir;
            ////if they are on the same height level
            //if ((otherTop <= this.Top && otherBottom >= this.Bottom) || (this.Top <= otherTop && this.Bottom >= otherBottom) ||
            //    (otherBottom <= this.Bottom && otherTop >= this.Bottom) || (otherTop >= this.Top && otherTop <= this.Bottom) ||
            //  //  otherBottom >= this.Top && otherTop <= this.Top && otherBottom <= this.Bottom)
            //   //(otherBottom <= this.Bottom && otherBottom >= this.Top && otherTop <= this.Top))
            //    //(otherBottom <= this.Bottom && otherTop >= this.Bottom) || (otherTop >= this.Top && otherBottom <= this.Top))
            //    //(otherBottom <= this.Bottom && otherTop <= this.Top && otherBottom >= this.Top))
            //     //|| (other.PrevSarfuce != null && !(this.Equals(other.PrevSarfuce)) && (otherBottom < this.Bottom && otherBottom > this.Top))
            //    // || (other.PrevSarfuce == null && otherBottom < this.Bottom && otherBottom > this.Top))
            //    //TODO: FIX THIS IF. THE LAST ROW WAS THE BEST ADDITION, BUT STILL BUGGY.
            //    //MAYBE I SHOULD ADD A REFERENCE TO THE SARFUCE AND CHECK IF ITS NOT THE SRAFUCE ONLY
            //   // (otherBottom < this.Bottom && otherBottom > this.Top))
            //{
            //    if (otherRight >= this.Left && otherRight <= this.Right)
            //    {
            //        other.HandleCollusion((dynamic)this, Direction.Left);
            //        return Direction.Left;
            //    }
            //    if (otherLeft <= this.Right && otherLeft >= this.Left)
            //    {
            //        other.HandleCollusion((dynamic)this, Direction.Right);
            //        return Direction.Right;
            //    }
            //}
            ////if they are on the same left/right area
            //if ((otherRight <= this.Right && otherLeft >= this.Left) || (this.Right <= otherRight && this.Left >= otherLeft) ||
            //    (otherLeft <= this.Left && otherRight >= this.Left) || (otherRight >= this.Right && otherLeft <= this.Right))
            //{
            //    if (otherTop <= this.Bottom && otherTop >= this.Top && CheckCollusionLeftRight(other) == Direction.None)
            //    {
            //        other.HandleCollusion((dynamic)this, Direction.Down);
            //        return Direction.Down;
            //    }
            //    if (otherBottom >= this.Top && otherBottom <= this.Bottom && CheckCollusionLeftRight(other) == Direction.None)
            //    {
            //        if (this is Pipe)
            //        {
            //            hasMeetPipe = true;
            //        }
            //        //BUG: WHEN JUMPING AND COLLIDING PIPE FROM RIGHT IT'S COMING HERE. NEED TO CHECK WHY, CUS IT SHOULD GO TO Direction.Right!
            //        other.HandleCollusion((dynamic)this, Direction.Up);
            //        return Direction.Up;
            //    }
            //}
            return Direction.None;
        }

        private Direction CheckCollusionLeftRight(MovingObj other)
        {
            float otherTop = other.Top + other.SpeedY;
            float otherBottom = other.Bottom + other.SpeedY;
            float otherLeft = other.Left + other.SpeedX;
            float otherRight = other.Right + other.SpeedX;
            //if they are on the same height level
            if ((otherTop <= this.Top && otherBottom >= this.Bottom) || (this.Top <= otherTop && this.Bottom >= otherBottom))
            //(otherBottom <= this.Bottom && otherBottom >= this.Top && otherTop <= this.Top))
            //(otherBottom <= this.Bottom && otherTop >= this.Bottom) || (otherTop >= this.Top && otherBottom <= this.Top))
            //(otherBottom <= this.Bottom && otherTop <= this.Top && otherBottom >= this.Top))
            // || (!(this is Floor) && (otherTop <= this.Top && otherBottom >= this.Top) || (otherBottom >= this.Bottom && otherTop <= this.Bottom)))
            //TODO: FIX THIS IF. THE LAST ROW WAS THE BEST ADDITION, BUT STILL BUGGY.
            //MAYBE I SHOULD ADD A REFERENCE TO THE SARFUCE AND CHECK IF ITS NOT THE SRAFUCE ONLY
            // (otherBottom < this.Bottom && otherBottom > this.Top))
            {
                if (otherRight >= this.Left && otherRight <= this.Right)
                {
                    other.HandleCollusion((dynamic)this, Direction.Left);
                    return Direction.Left;
                }
                if (otherLeft <= this.Right && otherLeft >= this.Left)
                {
                    other.HandleCollusion((dynamic)this, Direction.Right);
                    return Direction.Right;
                }
            }
            return Direction.None;
        }
    }
}
