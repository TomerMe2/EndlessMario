using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EndlessMarioRebornGit.Strategies;
using EndlessMarioRebornGit.Commands;
using EndlessMarioRebornGit.StillObjects;
using EndlessMarioRebornGit.Monsters;
using EndlessMarioRebornGit.Weapons;

namespace EndlessMarioRebornGit
{

    //Each round, firstly the speeds must update to the current round, and then the Update method is being called.
    //If this ends up colliding with another obj, it's speed will handled accordinly in the Update method.
    abstract class MovingObj : GameObject
    {
        private const int FRAMES_BEFORE_CHANGING_TEXTURE = 6;
        protected List<Texture2D> texturesFacingRight;
        protected List<Texture2D> texturesFacingLeft;
        protected bool isWalking;
        protected bool isWalkingPrevFrame;
        protected bool isFlipped;
        protected bool isJumping;
        protected bool isOnSarfuce;
        protected float accelerationX;
        protected float speedX;
        protected float accelerationY;
        protected float speedY;
        private float maxSpeed;
        private float jumpingPower;
        private float walkingPower;
        private int changingTextureCounter;
        private List<GameObject> collidedWithPrevTurn;
        protected List<GameObject> collidesWithNow;
        private GameObject sarfuce;
        private GameObject prevSarfuce;
        private Strategy strtgy;

        public MovingObj(List<Texture2D> texturesFacingRight, List<Texture2D> texturesFacingLeft, Vector2 startLoc, float scale, bool isCollideAble, float walkingPower,
            float jumpingPower, float maxSpeed, Floor flr, Strategy strtgy) : base(startLoc, texturesFacingRight.ElementAt(0), scale, isCollideAble)
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
            isOnSarfuce = false;
            accelerationX = 0;
            accelerationY = 0;
            speedX = 0;
            speedY = 0;
            this.maxSpeed = maxSpeed;
            this.jumpingPower = jumpingPower;
            this.walkingPower = walkingPower;
            changingTextureCounter = 0;
            sarfuce = flr;
            prevSarfuce = flr;
            collidedWithPrevTurn = new List<GameObject>();
            collidesWithNow = new List<GameObject>();
            this.strtgy = strtgy;
        }

        public virtual void UpdateFrameStart()
        {
            //first, handle move strategy
            HandleSpeedStartOfFrameStart();
            List<Command> cmnds = strtgy.GetCommands();
            foreach (Command cmnd in cmnds)
            {
                HandleCommand(cmnd);
            }
            prevSarfuce = sarfuce;
            sarfuce = null;
            collidedWithPrevTurn = collidesWithNow;
            collidesWithNow = new List<GameObject>();
            if (isWalking)
            {
                HandleSpeedChangesInWalking();
            }
            else
            {
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
            else
            {
                HandleSpeedYEndOfFrameStart();
            }

        }

        protected virtual void HandleSpeedYEndOfFrameStart()
        {
            speedY = 0;
        }

        protected virtual void HandleSpeedStartOfFrameStart()
        {
            // do nothing
        }

        public virtual void UpdateFrameEnd()
        { 
            if (isWalking)
            {
                if (!isJumping)
                {
                    float legsYLoc = Bottom;
                    HandleTextureChangesInWalking();
                    //In order to keep the legs on the ground when the new texture is with different height
                    float newYLoc = Top + (legsYLoc - Bottom);
                    Loc = new Vector2(Left, newYLoc);

                }
                else
                {
                    HandleTextureFlipJumpingAndWalking();
                }
            }
            else
            {
                if (!isJumping)
                {
                    HandleIdleTexture(); //make the texture idle
                }
            }
            if (!isOnSarfuce)
            {
                Fall();
            }
            isWalkingPrevFrame = isWalking;
            //prepare it for the next frame
            isWalking = false;
            isOnSarfuce = false;
            UpdateSpeedEndOfFrame();
        }

        protected virtual void UpdateSpeedEndOfFrame()
        {
            loc.X = loc.X + speedX;
            loc.Y = loc.Y + speedY;
        }

        private void HandleTextureFlipJumpingAndWalking()
        {
            if (accelerationX > 0)
            {
                if (!currentTexture.Equals(texturesFacingRight.ElementAt(texturesFacingRight.Count - 1)))  //if it's not the correct texture
                {
                    currentTexture = texturesFacingRight.ElementAt(texturesFacingRight.Count - 1);
                }
            }
            if (accelerationX < 0)
            {
                if (!currentTexture.Equals(texturesFacingLeft.ElementAt(texturesFacingLeft.Count - 1)))  //if it's not the correct texture
                {
                    currentTexture = texturesFacingLeft.ElementAt(texturesFacingLeft.Count - 1);
                }
            }
        }

        private void HandleSpeedChangesInJumpOrFall()
        {
            speedY = speedY + accelerationY;
        }

        /// <summary>
        /// Changes the speed of the object during walking
        /// </summary>
        private void HandleSpeedChangesInWalking()
        {
            if ((isFlipped && speedX > -maxSpeed) || (!isFlipped && speedX < maxSpeed))
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

        protected virtual void HandleNotWalkingSpeed()
        {
            HandleNotWalkingSpeedMechanics(Physics.FRICTION);   //default for most moving objects
        }

        /// <summary>
        /// This method is the rule of handling non walking speed mechanics. It requires friction because some object (such as projectiles) have other friction.
        /// </summary>
        protected void HandleNotWalkingSpeedMechanics(float friction)
        {
            if (isFlipped)
            {
                accelerationX = friction;
                speedX = speedX + accelerationX;
                if (speedX > 0)  //if he is starting to go left
                {
                    speedX = 0;
                    accelerationX = 0;
                }
            }
            else
            {
                accelerationX = -friction;
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
            currentTexture = isFlipped ? texturesFacingLeft.ElementAt(0) : texturesFacingRight.ElementAt(0);
        }

        /// <summary>
        /// Initiates jump 
        /// </summary>
        public virtual void Jump()
        {

            if (!isJumping)   //if it's not jumping alraedy
            {
                JumpProtected(jumpingPower, Physics.GRAVITY);
            }
        }

        protected virtual void JumpProtected(float jumpingPower, float gravity)
        {
            isJumping = true;
            accelerationY = gravity;
            speedY = -jumpingPower + accelerationY;
            UpdateTextureToJumpingFalling();   //Updating the textures
        }

        /// <summary>
        /// Initiates fall
        /// </summary>
        protected virtual void Fall()
        {
            FallMechanism(Physics.GRAVITY);
        }

        /// <summary>
        /// This method rules the mechanism for fall, in order to deal with different gravities.
        /// </summary>
        protected virtual void FallMechanism(float gravity)
        {
            if (speedY == 0)   //if it's not falling already
            {
                isJumping = true;
                accelerationY = gravity;
                speedY = accelerationY;
                UpdateTextureToJumpingFalling();    //Updating the textures
            }
        }

        /// <summary>
        /// update the current texture to the jumping texture in the correct facing direction
        /// </summary>
        private void UpdateTextureToJumpingFalling()
        {
            currentTexture = isFlipped ? texturesFacingLeft.ElementAt(texturesFacingLeft.Count - 1) : texturesFacingRight.ElementAt(texturesFacingLeft.Count - 1);
        }

        /// <summary>
        /// Switches between different kinds of commands 
        /// </summary>
        /// <param name="cmnd"></param>
        protected void HandleCommand(Command cmnd)
        {
            HandleCommand((dynamic) cmnd);
        }


        protected void HandleCommand(MoveLeftCommand lftCmnd)
        {
            Walk(Direction.Left);
        }

        protected void HandleCommand(MoveRightCommand rightCmnd)
        {
            Walk(Direction.Right);
        }

        protected void HandleCommand(JumpCommand jmpCmnd)
        {
            Jump();
        }

        protected virtual void HandleCommand(ChestSwitchCommand chstSwtchCmnd)
        {

        }

        protected virtual void HandleCommand(ChestCellSwitchCommand chstCellSwtchCmnd)
        {

        }

        protected virtual void HandleCommand(SwitchInventoryAndChestCommand swtchInvAndChstCmnd)
        {

        }

        protected virtual void HandleCommand(InventorySwitchCommand chstSwtchCmnd)
        {

        }

        protected virtual void HandleCommand(ShootCommand shootCmnd)
        {

        }

        protected virtual void HandleCommand(MoveUpCommand mvUpCmnd)
        {

        }

        public virtual void Walk(Direction dir)
        {
            WalkMechanics(dir, Physics.FRICTION);    //this is the default for most moving objects
        }

        /// <summary>
        /// This method is the rule of walking mechanics. It requires friction because some object (such as projectiles) have other friction.
        /// </summary>
        protected void WalkMechanics(Direction dir, float friction)
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
                    accelerationX = walkingPower - friction;
                    isFlipped = false;     //because the obj is facing right
                }
                else   //flipped
                {
                    accelerationX = -walkingPower + friction;
                    isFlipped = true;       //because the obj is facing left
                }
            }
            else  //need to continue the walk
            {
                //For now, I need this part to do nothing
            }
            isWalking = true;
        }

        protected virtual void CollusionWithHardObj(GameObject other, List<Direction> dirs)
        {
            CollusionWithHardObjMechanism(other, dirs, Physics.GRAVITY);
        }


        /// <summary>
        /// Rules the mechanism for coullsion with hard object.
        /// </summary>
        protected virtual void CollusionWithHardObjMechanism(GameObject other, List<Direction> dirs, float gravity)
        {
            if (dirs.Count == 0)
            {
                return;
            }
            if (isJumping)
            {
                string deb = "deb";
            }
            if (dirs.Contains(Direction.Up) && dirs.Contains(Direction.Left))
            {
                string deb = "deb";
            }
            if (dirs.Count > 1 && dirs[0] != Direction.Up && dirs.Contains(Direction.Up))
            {
                sarfuce = other;
            }
            else if (dirs.Contains(Direction.Up) && dirs.Count < 2)
            {
                //contains only Up actually
                sarfuce = other;
            }
            else if (dirs.Contains(Direction.Left) || dirs.Contains(Direction.Right))
            {
                //speedX = 0;
                if (dirs.Contains(Direction.Left))
                {
                    if (isJumping)
                    {
                        accelerationX = 0;
                    }
                    if (speedX > 0)
                    {
                        speedX = other.Left - this.Right;
                    }
                    else
                    {
                        loc.X = other.Left - (this.Right - this.Left);
                    }
                }
                else
                {
                    if (isJumping)
                    {
                        accelerationX = 0;
                    }
                    if (speedX < 0)
                    {
                        speedX = other.Right - this.Left;
                    }
                    else
                    {
                        loc.X = other.Right;
                    }
                }
                accelerationX = 0;
                isWalking = false;
            }
            if ((dirs.Contains(Direction.Up) && dirs.Count < 2) || (dirs[0] != Direction.Up && dirs.Contains(Direction.Up)))
            {
                isOnSarfuce = true;
                speedY = other.Top - this.Bottom;
                accelerationY = 0;
                isJumping = false;
            }
            if (dirs.Contains(Direction.Down))
            {
                speedY = other.Bottom - this.Top - gravity;    //so it will be only other.Bottom - this.Top after we will add GRAVITY to it
                accelerationY = gravity;
            }
        }

        public override List<Direction> Collusion(MovingObj other)
        {
            return ProtectedCollusion(other, Left + speedX, Right + speedX, Top + SpeedY, Bottom + SpeedY);
        }

        protected override void HandleCollusion(Pipe other, List<Direction> dirs)
        {
            collidesWithNow.Add(other);
            CollusionWithHardObj(other, dirs);
        }

        protected override void HandleCollusion(Floor other, List<Direction> dirs)
        {
            //Direction is always Up
            collidesWithNow.Add(other);
            CollusionWithHardObj(other, dirs);
        }

        protected override void HandleCollusion(GameObject other, List<Direction> dirs)
        {
            collidesWithNow.Add(other);
        }

        protected override void HandleCollusion(Mario other, List<Direction> dirs)
        {
            collidesWithNow.Add(other);
        }

        protected override void HandleCollusion(Monster other, List<Direction> dirs)
        {
            collidesWithNow.Add(other);
        }

        protected override void HandleCollusion(CannonBomb other, List<Direction> dirs)
        {
            collidesWithNow.Add(other);
        }

        protected override void HandleCollusion(GreenTurtleShield other, List<Direction> dirs)
        {
            collidesWithNow.Add(other);
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

        public bool HasCollidedWithPrevTurn(GameObject other)
        {
            return (collidedWithPrevTurn.Contains(other));
        }
        public float AccelerationX
        {
            get {return accelerationX; }
        }

        public float SpeedX
        {
            get { return speedX; }
        }

        public float SpeedY
        {
            get { return speedY; }
        }

        public GameObject Sarfuce
        {
            get { return sarfuce; }
        }

        public GameObject PrevSarfuce
        {
            get { return prevSarfuce; }
        }

        public bool isFacingRight
        {
            get { return !isFlipped; }
        }
    }
}
