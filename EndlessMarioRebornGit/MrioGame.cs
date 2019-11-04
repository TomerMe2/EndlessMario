using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using EndlessMarioRebornGit.Strategies;
using EndlessMarioRebornGit.Monsters;
using EndlessMarioRebornGit.StillObjects;
using EndlessMarioRebornGit.MenuItems;
using EndlessMarioRebornGit.Weapons;
using EndlessMarioRebornGit.ItemsThumbnails;
using System.Linq;

namespace EndlessMarioRebornGit
{

    public class MrioGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Mario mrio;
        private UserMarioMovingStrategy mrioStrategy;
        private Background bckgrnd;
        private List<GameObject> allNotMenuObjects;
        private List<GameObject> allMenuObjects;
        private List<ItemCell> mrioInventoryCells;
        private List<GameObject> chestStrip;
        private ItemCell[] chestWpnsCells;
        private BlackScreen blkScrn;
        private bool isInPause;
        private bool isEscpWasPressed;
        private bool isBwasPressed;
        private bool isEnterWasPressed;
        private SpriteFont fntForPauseOrDeath;
        private GameObjsCreator crtr;

        int count = 0;

        public MrioGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            allNotMenuObjects = new List<GameObject>();
            allMenuObjects = new List<GameObject>();
            mrioInventoryCells = new List<ItemCell>();
            chestWpnsCells = new ItemCell[Chest.ITEMS_NUM_IN_CHEST];
            isInPause = false;
            isEscpWasPressed = false;
            isBwasPressed = false;
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
            allMenuObjects.Add(blkScrn);
            bckgrnd = new Background(Content.Load<Texture2D>("background"), Content.Load<Texture2D>("background"), GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, allNotMenuObjects);
            List<Texture2D> mrioFacingRightTextures = new List<Texture2D>();
            List<Texture2D> mrioFacingLeftTextures = new List<Texture2D>();
            foreach (string assetName in Mario.texturesNameFacingRight)
            {
                mrioFacingRightTextures.Add(Content.Load<Texture2D>(assetName));
            }
            foreach (string assetName in Mario.texturesNameFacingLeft)
            {
                mrioFacingLeftTextures.Add(Content.Load<Texture2D>(assetName));
            }
            Floor flr = new Floor(bckgrnd.GameWidth, bckgrnd.GameHeight);
            Texture2D hrtTexture = Content.Load<Texture2D>(Heart.textureName);
            List<Heart> hrts = new List<Heart>();
            hrts.Add(new Heart(hrtTexture, 10));
            hrts.Add(new Heart(hrtTexture, hrts[0].Right + 5));
            hrts.Add(new Heart(hrtTexture, hrts[1].Right + 5));
            mrioStrategy = new UserMarioMovingStrategy();
            mrio = new Mario(mrioFacingRightTextures, mrioFacingLeftTextures, flr, mrioStrategy, hrts);
            allNotMenuObjects.Add(flr);

            Pipe pip = new Pipe(Content.Load<Texture2D>(Pipe.textureName), 800, 0.7f);
            allNotMenuObjects.Add(pip);

            foreach (Heart hrt in hrts)
            {
                allNotMenuObjects.Add(hrt);
            }

            crtr = new GameObjsCreator(pip, flr, this, mrio);
            HugeCannonBomb cnnBmbHuge = crtr.CreateHugeCannonBomb();
            if (cnnBmbHuge != null)
            {
                allNotMenuObjects.Add(cnnBmbHuge);
            }
            List<Texture2D> gmbaFacingRightTextures = new List<Texture2D>();
            List<Texture2D> gmbaFacingLeftTextures = new List<Texture2D>();
            foreach (string assetName in Goomba.texturesNameFacingRight)
            {
                gmbaFacingRightTextures.Add(Content.Load<Texture2D>(assetName));
            }
            foreach (string assetName in Goomba.texturesNameFacingLeft)
            {
                gmbaFacingLeftTextures.Add(Content.Load<Texture2D>(assetName));
            }
            Texture2D gmbaDeadTxtr = Content.Load<Texture2D>(Goomba.deadTextureNm);
            Texture2D gmbaDeadTxtrFlipped = Content.Load<Texture2D>(Goomba.deadTextureFlippedNm);
            Goomba gmba = new Goomba(gmbaFacingRightTextures, gmbaFacingLeftTextures, flr, 500, new RandomLeftRightStay(), gmbaDeadTxtr, gmbaDeadTxtrFlipped, mrio.Points);
            allNotMenuObjects.Add(mrio);
            allNotMenuObjects.Add(gmba);
            List<GameObject> invntryStrip = crtr.CreateInventoryStrip(bckgrnd.GameWidth);
            for (int i = 1; i < invntryStrip.Count; i++)
            {
                mrioInventoryCells.Add(invntryStrip[i] as ItemCell);
            }
            allMenuObjects.AddRange(invntryStrip);
            chestStrip = crtr.CreateChestInvStrip(bckgrnd.GameWidth, bckgrnd.GameHeight);
            for (int i = 1; i < chestStrip.Count; i++)
            {
                chestWpnsCells[i - 1] = chestStrip[i] as ItemCell;
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        private void PauseOrUnpause()
        {
            isInPause = !isInPause;
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
                List<ItemThumbnail> inventoryThumbnails = GetListOfInventoryThumbnails();
                for (int i = 0; i < mrioInventoryCells.Count; i++)
                {
                    mrioInventoryCells[i].HoldingThumbnail = inventoryThumbnails[i];   //update the inventory thumbnails on screen
                }
                List<ItemThumbnail> chestThumbnail = GetListOfChestThumbnails();
                if (chestThumbnail.Count > 0)
                {
                    for (int i = 0; i < chestWpnsCells.Length; i++)
                    {
                        chestWpnsCells[i].HoldingThumbnail = chestThumbnail[i];
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.B))
                {
                    isBwasPressed = true;
                }
                else if (isBwasPressed && Keyboard.GetState().IsKeyUp(Keys.B))
                {
                    isBwasPressed = false;
                    mrioStrategy.Bclicked();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    isEnterWasPressed = true;
                }
                else if (isEnterWasPressed && Keyboard.GetState().IsKeyUp(Keys.Enter))
                {
                    isEnterWasPressed = false;
                    mrioStrategy.EnterClicked();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    mrioStrategy.RightArrowClicked();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    mrioStrategy.LeftArrowClicked();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    mrioStrategy.SpaceClicked();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D1))
                {
                    mrioStrategy.NumClicked(1);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D2))
                {
                    mrioStrategy.NumClicked(2);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D3))
                {
                    mrioStrategy.NumClicked(3);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D4))
                {
                    mrioStrategy.NumClicked(4);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D5))
                {
                    mrioStrategy.NumClicked(5);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D6))
                {
                    mrioStrategy.NumClicked(6);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    mrioStrategy.Sclicked();
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Q))
                {
                    mrioStrategy.CharClicked('Q');
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    mrioStrategy.CharClicked('W');
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.E))
                {
                    mrioStrategy.CharClicked('E');
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    mrioStrategy.CharClicked('R');
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.T))
                {
                    mrioStrategy.CharClicked('T');
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Y))
                {
                    mrioStrategy.CharClicked('Y');
                }
                GameObject newObj = crtr.Create();
                if (newObj != null)
                {
                    allNotMenuObjects.Add(newObj);
                }
                for (int i = 0; i < allNotMenuObjects.Count; i++)
                {
                    GameObject gmObj = allNotMenuObjects[i];
                    if (gmObj is GreenTurtle && (gmObj as GreenTurtle).IsDead)
                    {
                        //Replace the turtle with it's shield
                        gmObj = crtr.CreateShield(gmObj as GreenTurtle);
                        allNotMenuObjects[i] = gmObj;
                    }
                    if (gmObj is MovingObj)
                    {
                        ((MovingObj)gmObj).UpdateFrameStart();
                    }
                }
                //Remove the objects that need to be removed
                for (int i = 0; i < allNotMenuObjects.Count; i++)
                {
                    GameObject curr = allNotMenuObjects[i];
                    if (curr.IsNeedDisposal)
                    {
                        //At this case, we will remove the object from the list
                        allNotMenuObjects.RemoveAt(i);
                        i--;
                    }
                }
                //Update mario inventory choice
                SelectCorrectCell(mrio.CurrCellIndxOfChosenWpn(), mrioInventoryCells);
                //Update chest inventory choice if needed
                Chest chst = mrio.ChestToDisplay;
                if (chst != null)
                {
                    SelectCorrectCell(chst.SelectedWpnIndx, chestWpnsCells);
                }
                HandleAllCollusions();
                foreach (GameObject gmObj in allNotMenuObjects)
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
                //Add the projectile that mario shot this round - if he shot one
                if (mrio.ShotThisFrame != null)
                {
                    allNotMenuObjects.Add(mrio.ShotThisFrame);
                }
                //TODO: FIND OUT WHY IS IT HERE. I THINK I SHOULD REMOVE THIS CODE!
                List<ItemThumbnail> invntryThmbNails = mrio.WpnsInventory.Select(wpn => wpn == null ? null : wpn.GetThumbnail()).ToList();
                for (int i = 0; i < mrioInventoryCells.Count; i++)
                {
                    mrioInventoryCells[i].HoldingThumbnail = invntryThmbNails[i];
                }
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
            List<GameObject> allObjs = new List<GameObject>(allNotMenuObjects);
            allObjs.AddRange(allMenuObjects);
            foreach (GameObject obj in allObjs)
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
                //if it's mario, we should draw it's weapon to
                //it's here bacuse the weapon should be almost in the same depth as mrio (it should be behind him)
                if (obj is Mario && mrio.CurrWeapon() != null)
                {
                    Weapon wpn = mrio.CurrWeapon();
                    spriteBatch.Draw(wpn.CurrentTexture, wpn.Loc, null, Color.White, 0f, new Vector2(0, 0), wpn.Scale, SpriteEffects.None, 0f);
                }
                spriteBatch.Draw(obj.CurrentTexture, obj.Loc, null, Color.White, 0f, new Vector2(0, 0), obj.Scale, SpriteEffects.None, 0f);
            }
            //draws the thumbnails inside the inventory cells up in the screen
            foreach (ItemCell cl in mrioInventoryCells)
            {
                if (cl.HoldingThumbnail != null)
                {
                    spriteBatch.Draw(cl.HoldingThumbnail.CurrentTexture, cl.HoldingThumbnail.Loc, null, Color.White, 0f, new Vector2(0, 0), cl.HoldingThumbnail.Scale, SpriteEffects.None, 0f);
                }
            }
            if (!isInPause && !mrio.HasLost && mrio.ChestToDisplay != null)
            {
                foreach (GameObject chestInvObj in chestStrip)
                {
                    spriteBatch.Draw(chestInvObj.CurrentTexture, chestInvObj.Loc, null, Color.White, 0f, new Vector2(0, 0), chestInvObj.Scale, SpriteEffects.None, 0f);
                }
                foreach (ItemCell cl in chestWpnsCells)
                {
                    if (cl.HoldingThumbnail != null)
                    {
                        spriteBatch.Draw(cl.HoldingThumbnail.CurrentTexture, cl.HoldingThumbnail.Loc, null, Color.White, 0f, new Vector2(0, 0), cl.HoldingThumbnail.Scale, SpriteEffects.None, 0f);
                    }
                }
            }
            string mrioPointsToDisplay = mrio.Points > 1000000 ? (long)mrio.Points / 1000000 + "M" :
                mrio.Points > 10000 ? (long)mrio.Points / 1000 + "K" :
                (long)mrio.Points + "";
            spriteBatch.DrawString(fntForPauseOrDeath, "POINTS " + mrioPointsToDisplay, new Vector2(hrt.Left, hrt.Bottom), Color.White);
            if (isInPause)
            {
                if (mrio.HasLost)
                {
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
            foreach (GameObject checkOn in allNotMenuObjects)
            {
                if (!(checkOn is MovingObj))
                {
                    continue;
                }
                foreach (GameObject other in allNotMenuObjects)
                {
                    if (!checkOn.Equals(other))
                    {
                        other.Collusion((MovingObj)checkOn);
                    }
                }
            }
        }

        private List<ItemThumbnail> GetListOfInventoryThumbnails()
        {
            return mrio.WpnsInventory.Select(wpn => wpn == null ? null : wpn.GetThumbnail()).ToList();
        }

        private List<ItemThumbnail> GetListOfChestThumbnails()
        {
            if (mrio.ChestToDisplay != null)
            {
                return mrio.ChestToDisplay.Wpns.Select(wpn => wpn == null ? null : wpn.GetThumbnail()).ToList();
            }
            return new List<ItemThumbnail>();
        }

        private void SelectCorrectCell(int cellIndx, IList<ItemCell> cells)
        {
            cells[cellIndx].Select();
            for (int i = 0; i < cells.Count; i++)
            {
                if (i != cellIndx)
                {
                    cells[i].UnSelect();
                }
            }
        }

        public float GetGameWindowWidth()
        {
            return bckgrnd.GameWidth;
        }

        /// <summary>
        /// Return true iff there are objects in the given rectangle
        /// </summary>
        public bool ObjectsInRect(FloatRectangle checkOn)
        {
            foreach (GameObject inGame in allNotMenuObjects)
            {
                FloatRectangle recForObj = new FloatRectangle(inGame.Left, inGame.Right, inGame.Top, inGame.Bottom);
                if (recForObj.Intersects(checkOn))
                {
                    return true;
                }
            }
            return false;
        }



    }
}