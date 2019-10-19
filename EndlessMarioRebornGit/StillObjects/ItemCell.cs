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

        private int cellChar;
        private bool isSelected;
        private ItemThumbnail holdingThumbnail;
        private Texture2D textureNotSelected;
        private Texture2D textureSelected;

        public static readonly string[] textureNumercialNormalNames = {@"ItemCells\Cell1", @"ItemCells\Cell2", @"ItemCells\Cell3", @"ItemCells\Cell4", @"ItemCells\Cell5", @"ItemCells\Cell6"};

        public static readonly string[] textureNumericalChosenNames = { @"ItemCells\Cell1Chosen", @"ItemCells\Cell2Chosen", @"ItemCells\Cell3Chosen", @"ItemCells\Cell4Chosen",
            @"ItemCells\Cell5Chosen", @"ItemCells\Cell6Chosen" };

        public static readonly string[] textureAlphabeticalNormalName = { @"ItemCellsAlphabetical\CellQ", @"ItemCellsAlphabetical\CellW", @"ItemCellsAlphabetical\CellE",
        @"ItemCellsAlphabetical\CellR", @"ItemCellsAlphabetical\CellT", @"ItemCellsAlphabetical\CellY"};

        public static readonly string[] textureAlphabeticalChosenName = { @"ItemCellsAlphabetical\CellQChosen", @"ItemCellsAlphabetical\CellWChosen", @"ItemCellsAlphabetical\CellEChosen",
        @"ItemCellsAlphabetical\CellRChosen", @"ItemCellsAlphabetical\CellTChosen", @"ItemCellsAlphabetical\CellYChosen"};

        public ItemCell(Texture2D textureNotSelected, Texture2D textureSelected, float locX, float locY, char cellChar) :
            base(new Vector2(locX, locY), textureNotSelected, SCALE, false)
        {
            this.cellChar = cellChar;
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

        //public static string GetCellTextureNormalName(char key)
        //{
        //    return textureNormalNames[key];
        //}

        //public static string GetCellTextureSelectedName(char key)
        //{
        //    return textureChosenNames[key];
        //}

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
