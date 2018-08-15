using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace EndlessMarioRebornGit
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Mario mrio;
        Background bckgrnd;
        //Pipe pip;
        StartingFlag strtFlg;
        List<GameObject> lstObjsToDraw;
        List<GameObject> allObjectsButMario;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            allObjectsButMario = new List<GameObject>();
            //bckgrnd = new Background(Content.Load<Texture2D>("background"), Content.Load<Texture2D>("background"), GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, allObjectsButMario);
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
            lstObjsToDraw = new List<GameObject>();
            //Adds the objects in the ToDrawList to the allObjects list
            foreach (GameObject obj in lstObjsToDraw)
            {
                allObjectsButMario.Add(obj);
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
            mrio.Update();
            // TODO: Add your update logic here

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

            spriteBatch.Draw(mrio.CurrentTexture, new Rectangle((int)mrio.Loc.X, (int)mrio.Loc.Y, mrio.CurrentTexture.Width, mrio.CurrentTexture.Height), Color.White);
            // TODO: Add your drawing code here

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private GameObject isCollidingX(GameObject obj)
        {
            float objLeft = obj.Loc.X;
            float objRight = obj.Loc.X + obj.CurrentTexture.Width;
            float toCheckVsLeft = 0;
            float toCheckVsRight = 0;
            if (!obj.Equals(mrio))
            {
                //TODO: IMPLEMENT THIS PART
                //Check collide vs mario
            }
            foreach (GameObject toCheckVs in allObjectsButMario)
            {
                if (!obj.Equals(toCheckVs))
                {
                    toCheckVsLeft = toCheckVs.Loc.X;
                    toCheckVsRight = toCheckVs.Loc.X + obj.CurrentTexture.Width;
                    //Moving right and colliding with that obj
                    if (objRight > toCheckVsLeft && objLeft < toCheckVsRight)
                    {
                        return toCheckVs;
                    }
                    //Moving left and colliding with that obj
                    if (objLeft < toCheckVsRight && objRight > toCheckVsLeft)
                    {
                        return toCheckVs;
                    }
                }
            }
            return null;
        }
    }
}
