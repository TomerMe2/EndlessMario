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

        public ItemThumbnail(Vector2 loc, Texture2D texture, float scale, bool isCollideAble) : base(loc, texture, scale, isCollideAble) { }

        public abstract GameObject CreateItemOfThisThumbnail();
    }
}
