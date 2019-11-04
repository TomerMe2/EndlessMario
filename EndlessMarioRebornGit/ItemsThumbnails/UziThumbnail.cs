using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit.ItemsThumbnails
{
    class UziThumbnail : ItemThumbnail
    {
        private const float IMAGE_DIVISOR_LEFT_RIGHT = 3.5f;
        private const float SCALE = 0.75f;

        public UziThumbnail(Texture2D texture) : base(texture, SCALE, IMAGE_DIVISOR_LEFT_RIGHT)
        {
        }
    }
}
