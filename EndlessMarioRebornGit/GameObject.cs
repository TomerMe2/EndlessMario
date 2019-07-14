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

        protected virtual void HandleCollusion(Pipe other, List<Direction> dirs) { }

        protected virtual void HandleCollusion(Floor other, List<Direction> dirs)
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


        protected virtual void HandleCollusion(GameObject other, List<Direction> dirs) { }

        /// <summary>
        /// Returns the direction of other realative to this if there's a collusion. Returns Direction.None otherwise
        /// Prefers left/right collusions over up/bottom collusions
        /// </summary>
        public List<Direction> Collusion(MovingObj other)
        {
            //TODO: override it in MovingObj so this obj can have speed to!
            //other is checking if he is colliding me
            float otherTop = other.Top + other.SpeedY;
            float otherBottom = other.Bottom + other.SpeedY;
            float otherLeft = other.Left + other.SpeedX;
            float otherRight = other.Right + other.SpeedX;
            List<Direction> dirs = new List<Direction>();
            if (!(this.Left <= otherRight && this.Right >= other.Left && this.Bottom >= otherTop && this.Top <= otherBottom))
            {
                //there's no collusion
                return dirs;
            }
            //TODO: override it in MovingObj so this obj can have speed to!
            //other is checking if he is colliding me
            //check collusion from right
            if (this.Right > otherLeft && this.Left <= otherLeft)
            {
                //it's in X range of collusion, check Y
                if (this.Top <= otherTop && this.Bottom >= otherBottom)
                {
                    //this is Bigger than other (other is inside of this Y)
                    dirs.Add(Direction.Right);
                }
                else if (otherTop <= this.Top && otherBottom >= this.Bottom)
                {
                    //other is bigger than this
                    dirs.Add(Direction.Right);
                }
                else if (!(this is Floor) && otherTop <= this.Top && otherBottom >= this.Top)
                {
                    //other is in limbo
                    dirs.Add(Direction.Right);
                }
            }
            if (!dirs.Contains(Direction.Right))
            {
                //check from left
                if (this.Left <= otherRight && this.Right >= otherRight)
                {
                    //it's in X range of collusion, check Y
                    if (this.Top <= otherTop && this.Bottom >= otherBottom)
                    {
                        //this is Bigger than other (other is inside of this Y)
                        dirs.Add(Direction.Left);
                    }
                    else if (otherTop <= this.Top && otherBottom >= this.Bottom)
                    {
                        //other is bigger than this
                        dirs.Add(Direction.Left);
                    }
                    else if (!(this is Floor) && otherTop <= this.Top && otherBottom >= this.Top)
                    {
                        //other is in limbo
                        dirs.Add(Direction.Left);
                    }
                }
            }
            if (this.Top <= otherBottom && this.Bottom >= otherTop)
            {
                //in Y range. check if X is in range
                if (this.Left <= otherLeft && this.Right >= otherRight)  //this is bigger than other
                {
                    dirs.Add(Direction.Up);
                }
                else if (otherLeft <= this.Left && otherRight >= this.Right)  //other is bigger than this
                {
                    dirs.Add(Direction.Up);
                }
                else if ((other.PrevSarfuce == null || this.Equals(other.PrevSarfuce)) && this.Left >= otherLeft && this.Left <= otherRight)  //other partially standing on this, other is on right side of this
                {
                    dirs.Add(Direction.Up);
                }
                else if ((other.PrevSarfuce == null || this.Equals(other.PrevSarfuce)) && this.Right <= otherRight && this.Right >= otherLeft)   //other partially standing on this, other is on left side of this
                {
                    dirs.Add(Direction.Up);
                }
            }
            if (!dirs.Contains(Direction.Up))
            {
                if (this.Bottom >= otherTop && this.Top <= otherBottom)
                {
                    //Y is in range. Check if X is in range
                    if (this.Left <= otherLeft && this.Right >= otherRight)  //this is bigger than other
                    {
                        dirs.Add(Direction.Down);
                    }
                    else if (otherLeft <= this.Left && otherRight >= this.Right)  //other is bigger than this
                    {
                        dirs.Add(Direction.Down);
                    }
                }
            }
            if (dirs.Contains(Direction.Up) && (dirs.Contains(Direction.Left) || dirs.Contains(Direction.Right)))
            {
                if (dirs.Contains(Direction.Left))
                {
                    if (other.Bottom > this.Top)
                    {
                        //flip em
                        dirs[0] = Direction.Up;
                        dirs[1] = Direction.Left;
                    }
                }
                if (dirs.Contains(Direction.Right))
                {
                    if (other.Bottom > this.Top)
                    {
                        //flip em
                        dirs[0] = Direction.Up;
                        dirs[1] = Direction.Right;
                    }
                }
            }
            other.HandleCollusion((dynamic)this, dirs);
            return dirs;
        }
    }
}
