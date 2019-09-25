using EndlessMarioRebornGit.ItemsThumbnails;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit.StillObjects
{
    class ItemCell : GameObject
    {
        public const float SCALE = 1;

        private int cellNum;
        private bool isSelected;
        private ItemThumbnail holdingThumbnail;
        private Texture2D textureNotSelected;
        private Texture2D textureSelected;


        private static string[] textureNormalNames = { @"ItemCells\Cell1", @"ItemCells\Cell2", @"ItemCells\Cell3", @"ItemCells\Cell4", @"ItemCells\Cell5", @"ItemCells\Cell6"};
        private static string[] textureChosenNames = { @"ItemCells\Cell1Chosen", @"ItemCells\Cell2Chosen", @"ItemCells\Cell3Chosen", @"ItemCells\Cell4Chosen",
            @"ItemCells\Cell5Chosen", @"ItemCells\Cell6Chosen" };

        public ItemCell(Texture2D textureNotSelected, Texture2D textureSelected, float locX, float locY, int cellNum) :
            base(new Vector2(locX, locY), textureNotSelected, SCALE, false)
        {
            this.cellNum = cellNum;
            holdingThumbnail = null;
            this.textureNotSelected = textureNotSelected;
            this.textureSelected = textureSelected;
        }

        public void Select()
        {
            isSelected = true;
            CurrentTexture = textureSelected;
        }

        public void UnSelect()
        {
            isSelected = false;
            CurrentTexture = textureNotSelected;
        }

        public static int GetNumOfCells()
        {
            return textureNormalNames.Length;
        }

        /// <summary>
        /// index is from 1 to GetNumOfCells() included
        /// </summary>
        public static string GetCellTextureNormalName(int index)
        {
            return textureNormalNames[index - 1];
        }

        /// <summary>
        /// index is from 1 to GetNumOfCells() included
        /// </summary>
        public static string GetCellTextureSelectedName(int index)
        {
            return textureChosenNames[index - 1];
        }

        public ItemThumbnail HoldingThumbnail
        {
            get { return holdingThumbnail; }
            set
            {
                holdingThumbnail = value;
                if (holdingThumbnail != null)
                {
                    holdingThumbnail.Loc = new Vector2(this.Left, this.Top + (this.Bottom - this.Top)/holdingThumbnail.ImageDivisorLeftRight);
                }
            }
        }

    }
}
