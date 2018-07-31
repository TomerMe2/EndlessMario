using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessMarioRebornGit
{
    public enum Direction
    {
        Left,
        Right
    }
    abstract class MovingObj : GameObject
    {
        private List<Texture2D> textures;
        protected bool isWalking;
        protected bool isFlipped;
        protected float accelerationX;
        protected float speedX;
        protected float accelerationY;
        protected float speedY;

        public MovingObj(List<Texture2D> textures, Vector2 startLoc, float scale, bool isCollideAble)
            : base(startLoc, textures.ElementAt(0), scale, isCollideAble)
        {
            this.textures = textures;
            isWalking = false;
            isFlipped = false;
        }
        
        public virtual void Jump()
        {

        }

        public virtual void Walk(Direction dir, float speed)
        {

        }
    }
}
