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

        protected virtual void HandleCollusion(GameObject other, Direction dir) { }

        /// <summary>
        /// Returns the direction of other realative to this if there's a collusion. Returns Direction.None otherwise
        /// Prefers left/right collusions over top/bottom collusions
        /// </summary>
        public Direction Collusion(MovingObj other)
        {
            if (this is Floor)
            {
                string deb = "debug";
            }
            //TODO: override it in MovingObj so this obj can have speed to!
            //other is checking if he is colliding me
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
            //if they are on the same left/right area
            if ((otherRight <= this.Right && otherLeft >= this.Left) || (this.Right <= otherRight && this.Left >= otherLeft) ||
                (otherLeft <= this.Left && otherRight >= this.Left) || (otherRight >= this.Right && otherLeft <= this.Right))
            {
                if (otherTop <= this.Bottom && otherTop >= this.Top)
                {
                    other.HandleCollusion((dynamic)this, Direction.Down);
                    return Direction.Down;
                }
                if (otherBottom >= this.Top && otherBottom <= this.Bottom)
                {
                    if (this is Pipe)
                    {
                        hasMeetPipe = true;
                    }
                    //BUG: WHEN JUMPING AND COLLIDING PIPE FROM RIGHT IT'S COMING HERE. NEED TO CHECK WHY, CUS IT SHOULD GO TO Direction.Right!
                    other.HandleCollusion((dynamic)this, Direction.Up);
                    return Direction.Up;
                }
            }

            if (this is Pipe && hasMeetPipe)
            {
                string deb = "kajshdkjas";
            }
            return Direction.None;

        }
    }
}
