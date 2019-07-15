using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using EndlessMarioRebornGit.Strategies;
using EndlessMarioRebornGit.Monsters;

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
        UserMarioMovingStrategy mrioStrategy;
        Background bckgrnd;
        StartingFlag strtFlg;
        List<GameObject> allObjects;
        BlackScreen blkScrn;
        bool isInPause;
        bool isEscpWasPressed;
        SpriteFont fntForPauseOrDeath;
        private GameObjsCreator crtr;

        int count = 0;

        public MrioGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            allObjects = new List<GameObject>();
            isInPause = false;
            isEscpWasPressed = false;
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
            fntForPauseOrDeath = Content.Load<SpriteFont>("FntForPauseOrDeath");
            blkScrn = new BlackScreen(Content.Load<Texture2D>(BlackScreen.textureName));
            blkScrn.Hide();
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
            Floor flr = new Floor(bckgrnd.GameWidth, bckgrnd.GameHeight);
            Texture2D hrtTexture = Content.Load<Texture2D>(Heart.textureName);
            List<Heart> hrts = new List<Heart>();
            hrts.Add(new Heart(hrtTexture, 10));
            hrts.Add(new Heart(hrtTexture, hrts[0].Right + 5));
            hrts.Add(new Heart(hrtTexture, hrts[1].Right + 5));
            mrioStrategy = new UserMarioMovingStrategy();
            mrio = new Mario(mrioFacingRightTextures, mrioFacingLeftTextures, flr, mrioStrategy, hrts);
            allObjects.Add(mrio);
            allObjects.Add(flr);
            
            Pipe pip = new Pipe(Content.Load<Texture2D>(Pipe.textureName), 800, 0.7f);
            allObjects.Add(pip);

            foreach (Heart hrt in hrts)
            {
                allObjects.Add(hrt);
            }
            List<Texture2D> gmbaFacingRightTextures = new List<Texture2D>();
            List<Texture2D> gmbaFacingLeftTextures = new List<Texture2D>();
            foreach (string assetName in Goomba.texturesNameFacingRight)
            {
                gmbaFacingRightTextures.Add(Content.Load<Texture2D>(@"Goomba\" + assetName));
            }
            foreach (string assetName in Goomba.texturesNameFacingLeft)
            {
                gmbaFacingLeftTextures.Add(Content.Load<Texture2D>(@"Goomba\" + assetName));
            }
            Texture2D gmbaDeadTxtr = Content.Load<Texture2D>(@"Goomba\" + Goomba.deadTextureNm);
            Texture2D gmbaDeadTxtrFlipped = Content.Load<Texture2D>(@"Goomba\" + Goomba.deadTextureFlippedNm);
            Goomba gmba = new Goomba(gmbaFacingRightTextures, gmbaFacingLeftTextures, flr, 500, new RandomLeftRightStay(), gmbaDeadTxtr, gmbaDeadTxtrFlipped, mrio.Points);
            allObjects.Add(gmba);

            crtr = new GameObjsCreator(pip, flr, this, mrio);
            allObjects.Add(blkScrn);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void PauseOrUnpause()
        {
            isInPause = !isInPause;
            int indxOfBlackScreen = -1;
            for (int i = 0; i < allObjects.Count; i++)
            {
                if (allObjects[i] is BlackScreen)
                {
                    indxOfBlackScreen = i;
                    break;
                }
            }
            allObjects[indxOfBlackScreen] = allObjects[allObjects.Count - 1];
            allObjects[allObjects.Count - 1] = blkScrn;
            blkScrn.HideOrShow();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            count++;
            if (mrio.HasLost && isInPause && Keyboard.GetState().IsKeyDown(Keys.C))
            {
                Program.PrepareForRestart();
                Exit();
            }
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) && !mrio.HasLost)
            {
                isEscpWasPressed = true;
            }
            if (isEscpWasPressed && Keyboard.GetState().IsKeyUp(Keys.Escape) && !mrio.HasLost)
            {
                isEscpWasPressed = false;
                PauseOrUnpause();
            }
            if (!isInPause)
            {
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
                GameObject newObj = crtr.Create();
                if (newObj != null)
                {
                    allObjects.Add(newObj);
                }
                foreach (GameObject gmObj in allObjects)
                {
                    if (gmObj is MovingObj)
                    {
                        ((MovingObj)gmObj).UpdateFrameStart();
                    }
                }
                for (int i = 0; i < allObjects.Count; i++)
                {
                    GameObject curr = allObjects[i];
                    if (curr.IsNeedDisposal)
                    {
                        //At this case, we will remove the object from the list
                        allObjects.RemoveAt(i);
                        i--;
                    }
                }
                HandleAllCollusions();
                foreach (GameObject gmObj in allObjects)
                {
                    if (gmObj is MovingObj)
                    {
                        ((MovingObj)gmObj).UpdateFrameEnd();
                    }
                }
                if (mrio.HasLost)
                {
                    PauseOrUnpause();
                }
                bckgrnd.BackgroundUpate(mrio.SpeedX);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Heart hrt = null;
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            Rectangle[] bkToDraw = bckgrnd.BackgroundLocToDraw();
            spriteBatch.Draw(bckgrnd.BackgroundTextures[0], bkToDraw[0], Color.White);
            spriteBatch.Draw(bckgrnd.BackgroundTextures[1], bkToDraw[1], Color.White);
            if (count > 1000)
            {
                bool deb = true;
            }
            foreach (GameObject obj in allObjects)
            {
                if (hrt == null && obj is Heart)
                {
                    hrt = (Heart)obj;
                }
                if (obj is Floor)
                {
                    continue;
                }
                if (!obj.NeedToBeDraw)
                {
                    continue;
                }
                spriteBatch.Draw(obj.CurrentTexture, obj.Loc, null, Color.White, 0f, new Vector2(0, 0), obj.Scale, SpriteEffects.None, 0f);
                //spriteBatch.Draw(obj.CurrentTexture, new Rectangle((int)obj.Loc.X, (int)obj.Loc.Y, obj.CurrentTexture.Width, obj.CurrentTexture.Height), Color.White, 0f, null, obj.Scale, SpriteEffects.None, 0);
            }
            spriteBatch.DrawString(fntForPauseOrDeath, "POINTS " + (long)mrio.Points, new Vector2(hrt.Left, hrt.Bottom), Color.White);
            //spriteBatch.Draw(mrio.CurrentTexture, new Rectangle((int)mrio.Loc.X, (int)mrio.Loc.Y, mrio.CurrentTexture.Width, mrio.CurrentTexture.Height), Color.White);
            if (isInPause)
            {
                if (mrio.HasLost)
                {
                    //spriteBatch.DrawString(fntForPauseOrDeath, "POINTS: " + mrio.Points, new Vector2(mrio.Left - mrio.CurrentTexture.Width / 3.5f, mrio.Top - 150), Color.White);
                    spriteBatch.DrawString(fntForPauseOrDeath, "YOU LOST", new Vector2(mrio.Left - mrio.CurrentTexture.Width / 3.5f, mrio.Top - 120), Color.White);
                    spriteBatch.DrawString(fntForPauseOrDeath, "PRESS C TO RESTART", new Vector2(mrio.Left - mrio.CurrentTexture.Width, mrio.Top - 90), Color.White);
                }
                else
                {
                    spriteBatch.DrawString(fntForPauseOrDeath, "PAUSE", new Vector2(mrio.Left - mrio.CurrentTexture.Width / 4, mrio.Top - 120), Color.White);
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void HandleAllCollusions()
        {
            foreach (GameObject checkOn in allObjects)
            {
                if(!(checkOn is MovingObj))
                {
                    continue;
                }
                foreach (GameObject other in allObjects)
                {
                    if (!checkOn.Equals(other))
                    {
                        other.Collusion((MovingObj)checkOn);
                    }
                }
            }
        }
    }
}
