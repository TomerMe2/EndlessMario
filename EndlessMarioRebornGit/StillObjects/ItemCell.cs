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

        private static Dictionary<char, string> textureNormalNames = new Dictionary<char, string>()
        {   {'1', @"ItemCells\Cell1" },
            {'2', @"ItemCells\Cell2" },
            {'3', @"ItemCells\Cell3" },
            {'4', @"ItemCells\Cell4" },
            {'5', @"ItemCells\Cell5" },
            {'6', @"ItemCells\Cell6" } };

        private static Dictionary<char, string> textureChosenNames = new Dictionary<char, string>()
        {   {'1', @"ItemCells\Cell1Chosen" },
            {'2', @"ItemCells\Cell2Chosen" },
            {'3', @"ItemCells\Cell3Chosen" },
            {'4', @"ItemCells\Cell4Chosen" },
            {'5', @"ItemCells\Cell5Chosen" },
            {'6', @"ItemCells\Cell6Chosen" } };

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

        public static char GetLastNumericCellKey()
        {
            return textureNormalNames.Keys.Where(key => Char.IsDigit(key)).OrderBy(key => key).Last();
        }

        public static char GetLastAlphabeticalCellKey()
        {
            return textureNormalNames.Keys.Where(key => Char.IsLetter(key)).OrderBy(key => key).Last();
        }
        public static string GetCellTextureNormalName(char key)
        {
            return textureNormalNames[key];
        }

        public static string GetCellTextureSelectedName(char key)
        {
            return textureChosenNames[key];
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
