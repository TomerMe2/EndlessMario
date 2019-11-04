using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit.ItemsThumbnails
{
    class AK47Thumbnail : ItemThumbnail
    {
        private const float IMAGE_DIVISOR_LEFT_RIGHT = 3;
        private const float SCALE = 0.6f;

        public AK47Thumbnail(Texture2D texture) : base(texture, SCALE, IMAGE_DIVISOR_LEFT_RIGHT)
        {
        }
    }
}
