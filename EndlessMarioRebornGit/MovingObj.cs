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
        private List<Texture2D> texturesFacingRight;
        private List<Texture2D> texturesFacingLeft;
        protected bool isWalking;
        protected bool isFlipped;
        protected float accelerationX;
        protected float speedX;
        protected float accelerationY;
        protected float speedY;
        private float maxSpeed;
        private float jumpingPower;
        private float walkingPower;
        
        public MovingObj(List<Texture2D> texturesFacingRight, List<Texture2D> texturesFacingLeft, Vector2 startLoc, float scale, bool isCollideAble, float walkingPower, 
            float jumpingPower, float maxSpeed) : base(startLoc, texturesFacingRight.ElementAt(0), scale, isCollideAble)
        {
            //Texture Laws:
            //First will come the statnding texture
            //Then, the walking textures
            //Then, the jumping texture
            this.texturesFacingRight = texturesFacingRight;
            this.texturesFacingLeft = texturesFacingLeft;
            isWalking = false;
            isFlipped = false;
            accelerationX = 0;
            accelerationY = 0;
            speedX = 0;
            speedY = 0;
            this.maxSpeed = maxSpeed;
            this.jumpingPower = jumpingPower;
            this.walkingPower = walkingPower;
        }
        
        /// <summary>
        /// Initiates jump 
        /// </summary>
        public virtual void Jump()
        {
            if (speedY == 0)   //if it's not jumping alraedy
            {
                accelerationY = Physics.GRAVITY;
                speedY = - jumpingPower + accelerationY;
                //Updating the textures
                if (isFlipped)
                {
                    currentTexture = texturesFacingLeft.ElementAt(texturesFacingLeft.Count - 1); 
                }
                else
                {
                    currentTexture = texturesFacingRight.ElementAt(texturesFacingLeft.Count - 1);
                }
            }
        }

        /// <summary>
        /// Initiates fall
        /// </summary>
        public void Fall()
        {
            if (speedY == 0)   //if it's not falling already
            {
                accelerationY = Physics.GRAVITY;
                speedY = accelerationY;
            }
        }

        public virtual void Walk(Direction dir, float speed)
        {

        }

        /// <summary>
        /// Flip the object
        /// Keeps the same texture, but makes it flipped
        /// </summary>
        protected void FlipTexture()
        {
            if (isFlipped) //Facing left
            {
                int currIndex = texturesFacingLeft.FindIndex(new Predicate<Texture2D>(currentTexture.Equals)); //Finds the index of the current texture
                currentTexture = texturesFacingRight.ElementAt(currIndex); //make the flip!
            }
            else //Facing right
            {
                int currIndex = texturesFacingRight.FindIndex(new Predicate<Texture2D>(currentTexture.Equals)); //Finds the index of the current texture
                currentTexture = texturesFacingLeft.ElementAt(currIndex); //make the flip!
            }
            isFlipped = !isFlipped; //flip the flag
        }
    }
}
