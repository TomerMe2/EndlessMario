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
        Right,
        Rand
    }
    abstract class MovingObj : GameObject
    {
        private const int FRAMES_BEFORE_CHANGING_TEXTURE = 6;
        private List<Texture2D> texturesFacingRight;
        private List<Texture2D> texturesFacingLeft;
        protected bool isWalking;
        protected bool isWalkingPrevFrame;
        protected bool isFlipped;
        protected bool isJumping;
        protected float accelerationX;
        protected float speedX;
        protected float accelerationY;
        protected float speedY;
        private float maxSpeed;
        private float jumpingPower;
        private float walkingPower;
        private int changingTextureCounter;

        private int jumpingCounter;
        //TODO: HANDLE CHANGING DIRECTIONS FOR THE TEXTURES DURING JUMPING
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
            isWalkingPrevFrame = false;
            isFlipped = false;
            isJumping = false;
            accelerationX = 0;
            accelerationY = 0;
            speedX = 0;
            speedY = 0;
            this.maxSpeed = maxSpeed;
            this.jumpingPower = jumpingPower;
            this.walkingPower = walkingPower;
            changingTextureCounter = 0;

            jumpingCounter = 0;
        }

        public void Update()
        {
            if (isWalking)
            {
                HandleSpeedChangesInWalking();
                if (!isJumping)  //if it's not jumping or falling
                {
                    HandleTextureChangesInWalking();
                }   
            }
            else
            {
                if (!isJumping)
                {
                    HandleIdleTexture(); //make the texture idle
                }
                if (speedX != 0)   //the object need to slow down because of friction
                {
                    HandleNotWalkingSpeed();
                }
            }
            if (isJumping)  //Object is falling or jumping
            {
                //Need to handle speed changes in jumping and know where to land
                HandleSpeedChangesInJumpOrFall();
            }
            isWalkingPrevFrame = isWalking;
            isWalking = false;   //prepare it for the next frame
            loc.X = loc.X + speedX;
            loc.Y = loc.Y + speedY;
        }

        private void HandleSpeedChangesInJumpOrFall()
        {
            speedY = speedY + accelerationY;
            //THE PROBLEM IS HERE, CUS HE WONT JUMP AGAIN
            if (loc.Y > Physics.FLOOR_LOC && isJumping)  //impact with the floor
            {
                loc.Y = Physics.FLOOR_LOC;
                speedY = 0;
                accelerationY = 0;
                isJumping = false;
            }
        }

        /// <summary>
        /// Changes the speed of the object during walking
        /// </summary>
        private void HandleSpeedChangesInWalking()
        {
            if ((isFlipped && speedX > -maxSpeed) ||(!isFlipped && speedX < maxSpeed))
            {
                speedX = speedX + accelerationX;
                if (isFlipped)  //facing left
                {
                    if (speedX < -maxSpeed)
                    {
                        speedX = -maxSpeed;
                    }
                }
                else    //facing right
                {
                    if (speedX > maxSpeed)
                    {
                        speedX = maxSpeed;
                    }
                }
            }
        }

        private void HandleNotWalkingSpeed()
        {
            if (isFlipped)
            {
                accelerationX = Physics.FRICTION;
                speedX = speedX + accelerationX;
                if (speedX > 0)  //if he is starting to go left
                {
                    speedX = 0;
                    accelerationX = 0;
                }
            }
            else
            {
                accelerationX = -Physics.FRICTION;
                speedX = speedX + accelerationX;
                if (speedX < 0)  //if he is starting to go right
                {
                    speedX = 0;
                    accelerationX = 0;
                }
            }
        }

        /// <summary>
        /// Changes the texture of the object during walking
        /// </summary>
        private void HandleTextureChangesInWalking()
        {
            int currIndex = 0;
            if (isFlipped)
            {
                currIndex = texturesFacingLeft.FindIndex(new Predicate<Texture2D>(currentTexture.Equals)); //Finds the index of the current texture
            }
            else
            {
                currIndex = texturesFacingRight.FindIndex(new Predicate<Texture2D>(currentTexture.Equals)); //Finds the index of the current texture
            }
            if (currIndex == texturesFacingLeft.Count - 1 && !isJumping)  //it's the jumping texture, and he is not jumping
            {
                if (isFlipped)
                {
                    currentTexture = texturesFacingLeft.ElementAt(1);
                }
                else
                {
                    currentTexture = texturesFacingRight.ElementAt(1);
                }
                changingTextureCounter = 0;
            }
            if (changingTextureCounter == FRAMES_BEFORE_CHANGING_TEXTURE)
            {
                //It's time to change a texture!
                changingTextureCounter = 0;
                if (currIndex == texturesFacingLeft.Count - 2)   //if it's the last walking texture
                {
                    //Will go to the first walking texture
                    if (isFlipped)
                    {
                        currentTexture = texturesFacingLeft.ElementAt(1);
                    }
                    else
                    {
                        currentTexture = texturesFacingRight.ElementAt(1);
                    }
                }
                else
                {
                    //Updates the texture to the next one
                    if (isFlipped)
                    {
                        currentTexture = texturesFacingLeft.ElementAt(currIndex + 1);
                    }
                    else
                    {
                        currentTexture = texturesFacingRight.ElementAt(currIndex + 1);
                    }
                }
            }
            else
            {
                changingTextureCounter++;
            }
        }

        /// <summary>
        /// Sets the current texture as the idle texture
        /// </summary>
        private void HandleIdleTexture()
        {
            if (isFlipped)
            {
                currentTexture = texturesFacingLeft.ElementAt(0);
            }
            else
            {
                currentTexture = texturesFacingRight.ElementAt(0);
            }
        }
        
        /// <summary>
        /// Initiates jump 
        /// </summary>
        public virtual void Jump()
        {
            if (!isJumping)   //if it's not jumping alraedy
            {
                jumpingCounter++;
                if (jumpingCounter > 1)
                {
                    string s = "debug";
                }
                isJumping = true;
                accelerationY = Physics.GRAVITY;
                speedY = - jumpingPower + accelerationY;
                UpdateTextureToJumpingFalling();   //Updating the textures
            }
        }

        /// <summary>
        /// Initiates fall
        /// </summary>
        public void Fall()
        {
            if (speedY == 0)   //if it's not falling already
            {
                isJumping = true;
                accelerationY = Physics.GRAVITY;
                speedY = accelerationY;
                UpdateTextureToJumpingFalling();    //Updating the textures
            }
        }

        /// <summary>
        /// update the current texture to the jumping texture in the correct facing direction
        /// </summary>
        private void UpdateTextureToJumpingFalling()
        {
            if (isFlipped)
            {
                currentTexture = texturesFacingLeft.ElementAt(texturesFacingLeft.Count - 1);
            }
            else
            {
                currentTexture = texturesFacingRight.ElementAt(texturesFacingLeft.Count - 1);
            }
        }

        public virtual void Walk(Direction dir)
        {
            bool isChangingDir = (dir == Direction.Left && !isFlipped) || (dir == Direction.Right && isFlipped);
            if ((!isWalkingPrevFrame) || (isWalkingPrevFrame && isChangingDir))
            {
                if (!isWalking)
                {
                    changingTextureCounter = FRAMES_BEFORE_CHANGING_TEXTURE;
                }
                if (dir == Direction.Right)  //not flipped
                {
                    accelerationX = walkingPower - Physics.FRICTION;
                    isFlipped = false;     //because the obj is facing right
                }
                else   //flipped
                {
                    accelerationX = -walkingPower + Physics.FRICTION;
                    isFlipped = true;       //because the obj is facing left
                }
            }
            else  //need to continue the walk
            {
                //For now, I need this part to do nothing
            }
            isWalking = true;
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
