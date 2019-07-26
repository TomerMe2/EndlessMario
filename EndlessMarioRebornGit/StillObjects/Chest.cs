using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndlessMarioRebornGit.Weapons;
namespace EndlessMarioRebornGit.StillObjects
{
    class Chest : GameObject
    {
        public static string textureNameClosed = @"Chest\ChestClosed";
        public static string textureNameOpen = @"Chest\ChestOpen";
        public const int ITEMS_NUM_IN_CHEST = 6;

        private Texture2D closeTxtr;
        private Texture2D openTxtr;
        private bool isOpen;
        private ItemCell[] cells;

        public Chest(Texture2D textureClosed, Texture2D textureOpen, float locX, float scale, ItemCell[] cells) :
            base(new Vector2(locX, Physics.FLOOR_LOC - textureClosed.Height * scale), textureClosed, scale, true)
        {
            closeTxtr = textureClosed;
            openTxtr = textureOpen;
            isOpen = false;
            this.cells = cells;
        }

        private void Open()
        {
            CurrentTexture = openTxtr;
            isOpen = true;
        }

        private void Close()
        {
            CurrentTexture = closeTxtr;
            isOpen = false;
        }

        /// <summary>
        /// Open or Close, depends on the last state of the chest
        /// </summary>
        public void SwitchChestState()
        {
            if (isOpen)
            {
                Close();
            }
            else
            {
                Open();
            }
        }


    }
}
