using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit.ItemsThumbnails
{
    abstract class ItemThumbnail : GameObject
    {
        protected float imageDivisorLeftRight;

        //After creation, the main should re-locate the thumbnail in it's place
        public ItemThumbnail(Texture2D texture, float scale, float imageDivisorLeftRight) : base(new Vector2(0, 0), texture, scale, false)
        {
            this.imageDivisorLeftRight = imageDivisorLeftRight;
        }

        public float ImageDivisorLeftRight
        {
            get { return imageDivisorLeftRight; }
        }
    }
}
