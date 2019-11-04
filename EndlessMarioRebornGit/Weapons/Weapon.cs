using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EndlessMarioRebornGit.ItemsThumbnails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit.Weapons
{
    abstract class Weapon : GameObject
    {
        // private List<Projectile> projectiles;
        protected Texture2D txtrFacingLeft;
        protected Texture2D txtrFacingRight;

        protected Mario mrio;

        public Weapon(Texture2D txtrFacingRight, Texture2D txtrFacingLeft, Mario mrio) :
            base(new Vector2(mrio.Right, mrio.Bottom - (mrio.Bottom - mrio.Top)/2), txtrFacingRight, 1, true)
        {
            //projectiles = new List<Projectile>();
            this.txtrFacingLeft = txtrFacingLeft;
            this.txtrFacingRight = txtrFacingRight;
            this.mrio = mrio;
        }

        /// <summary>
        /// Should be called every turn, after mario moves. Implemented for pistol here - because it's quite general
        /// </summary>
        public abstract void UpdateEndOfFrame();

        public abstract ItemThumbnail GetThumbnail();

    }
}
