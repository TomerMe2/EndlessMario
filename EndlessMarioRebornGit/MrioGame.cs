using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace EndlessMarioRebornGit
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MrioGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Mario mrio;
        Background bckgrnd;
        StartingFlag strtFlg;
        List<GameObject> lstObjsToDraw;
        List<GameObject> allObjects;

        //BUGS:
        //Collusion is not very great, problems while trying to jump on platforms and from platroms
        public MrioGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            allObjects = new List<GameObject>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            bckgrnd = new Background(Content.Load<Texture2D>("background"), Content.Load<Texture2D>("background"), GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, allObjects);
            List<Texture2D> mrioFacingRightTextures = new List<Texture2D>();
            List<Texture2D> mrioFacingLeftTextures = new List<Texture2D>();
            foreach (string assetName in Mario.texturesNameFacingRight)
            {
                mrioFacingRightTextures.Add(Content.Load<Texture2D>(@"Mario\" + assetName));
            }
            foreach (string assetName in Mario.texturesNameFacingLeft)
            {
                mrioFacingLeftTextures.Add(Content.Load<Texture2D>(@"Mario\" + assetName));
            }
            mrio = new Mario(mrioFacingRightTextures, mrioFacingLeftTextures);
            allObjects.Add(mrio);
            Floor flr = new Floor(bckgrnd.GameWidth, bckgrnd.GameHeight);
            allObjects.Add(flr);
            Pipe pip = new Pipe(Content.Load<Texture2D>(Pipe.textureName), 100, 0.6f);
            allObjects.Add(pip);
            lstObjsToDraw = new List<GameObject>();
            //Adds the objects in the ToDrawList to the allObjects list
            foreach (GameObject obj in lstObjsToDraw)
            {
                allObjects.Add(obj);
            }

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                mrio.Walk(Direction.Right);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                mrio.Walk(Direction.Left);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                mrio.Jump();
            }
            mrio.UpdateFrameStart();
            HandleAllCollusions();
            mrio.UpdateFrameEnd();
            bckgrnd.BackgroundUpate(mrio.SpeedX);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            Rectangle[] bkToDraw = bckgrnd.BackgroundLocToDraw();
            spriteBatch.Draw(bckgrnd.BackgroundTextures[0], bkToDraw[0], Color.White);
            spriteBatch.Draw(bckgrnd.BackgroundTextures[1], bkToDraw[1], Color.White);
            foreach (GameObject obj in allObjects)
            {
                if (obj is Floor)
                {
                    continue;
                }
                spriteBatch.Draw(obj.CurrentTexture, obj.Loc, null, Color.White, 0f, new Vector2(0, 0), obj.Scale, SpriteEffects.None, 0f);
                //spriteBatch.Draw(obj.CurrentTexture, new Rectangle((int)obj.Loc.X, (int)obj.Loc.Y, obj.CurrentTexture.Width, obj.CurrentTexture.Height), Color.White, 0f, null, obj.Scale, SpriteEffects.None, 0);
            }
            //spriteBatch.Draw(mrio.CurrentTexture, new Rectangle((int)mrio.Loc.X, (int)mrio.Loc.Y, mrio.CurrentTexture.Width, mrio.CurrentTexture.Height), Color.White);
            // TODO: Add your drawing code here

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void HandleAllCollusions()
        {
            //Now only works for mario
            MovingObj checkOn = mrio;
            foreach (GameObject other in allObjects)
            {
                if (!checkOn.Equals(other))
                {
                    other.Collusion(checkOn);
                }
            }
        }

        //private GameObject isCollidingX(MovingObj obj)
        //{
        //    float objLeft = obj.Loc.X;
        //    float objRight = obj.Loc.X + obj.CurrentTexture.Width;
        //    float toCheckVsLeft = 0;
        //    float toCheckVsRight = 0;
        //    foreach (GameObject toCheckVs in allObjects)
        //    {
        //        if (!obj.Equals(toCheckVs) && toCheckVs.IsCollideAble)
        //        {
        //            toCheckVsLeft = toCheckVs.Loc.X;
        //            toCheckVsRight = toCheckVs.Loc.X + obj.CurrentTexture.Width;
        //            //FOR A REASON, WORKS FOR GOING LEFT TOO
        //            //BUGGGGGGGGGGGGGGGG
        //            //Moving right and colliding with that obj
        //            if ((obj.AccelerationX > 0) && (objRight > toCheckVsLeft && objLeft < toCheckVsRight))
        //            {
        //                return toCheckVs;
        //            }
        //            //Moving left and colliding with that obj
        //            if ((obj.AccelerationX < 0) && (objLeft < toCheckVsRight && objRight > toCheckVsLeft))
        //            {
        //                return toCheckVs;
        //            }
        //        }
        //    }
        //    return null;
        //}




    }
}
