using EndlessMarioRebornGit.ItemsThumbnails;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit.Weapons
{
    class BigPistol : RangedWeapon
    {
        public static string textureNameFacingRight = @"Weapons\BigPistol\BigPistolFacingRight";
        public static string textureNameFacingLeft = @"Weapons\BigPistol\BigPistolFacingLeft";
        public const float SHOOTING_POWER = 30f;
        public const int FRAMES_BEFORE_NEXT_SHOT = 50;

        public BigPistol(Texture2D txtOfWpnFacingRight, Texture2D txtrOfWpnFacingLeft, Mario mrio, Floor flr,
            Texture2D txtrOfProjectileFacingRight, Texture2D txtrOfProjecileFacingLeft) : base(txtOfWpnFacingRight, txtrOfWpnFacingLeft, mrio, flr, SHOOTING_POWER,
                FRAMES_BEFORE_NEXT_SHOT, txtrOfProjectileFacingRight, txtrOfProjecileFacingLeft)
        {

        }

        public override Projectile Shoot()
        {
            if (framesFromLastShot > framesBetweenShots)
            {
                framesFromLastShot = 0;
                return new PistolBullet(txtrOfProjectileFacingRight, txtrOfProjecileFacingLeft, this, flr, mrio);
            }
            return null;
        }

        public override void UpdateEndOfFrame()
        {
            int txtrNum = mrio.GetTextureNum();
            if (mrio.IsFacingRight())
            {
                currentTexture = txtrFacingRight;
                Loc = txtrNum == 0 ? new Vector2(mrio.Left + (mrio.Right - mrio.Left) / 1.4f, mrio.Top + (mrio.Bottom - mrio.Top) / 2f) :
                    txtrNum == 1 ? new Vector2(mrio.Left + (mrio.Right - mrio.Left) / 2.25f, mrio.Top + (mrio.Bottom - mrio.Top) / 2f) :
                    txtrNum == 2 ? new Vector2(mrio.Left + (mrio.Right - mrio.Left) / 2.75f, mrio.Top + (mrio.Bottom - mrio.Top) / 2f) :
                    txtrNum == 3 ? new Vector2(mrio.Left + (mrio.Right - mrio.Left) / 1.75f, mrio.Top + (mrio.Bottom - mrio.Top) / 2f) :
                    new Vector2(mrio.Left + (mrio.Right - mrio.Left) / 1.17f, mrio.Top + (mrio.Bottom - mrio.Top) / 2.2f);
            }
            else
            {
                currentTexture = txtrFacingLeft;
                Loc = txtrNum == 0 ? new Vector2(mrio.Right - (mrio.Right - mrio.Left) / 1.4f - txtrFacingRight.Width, mrio.Top + (mrio.Bottom - mrio.Top) / 2f) :
                    txtrNum == 1 ? new Vector2(mrio.Right - (mrio.Right - mrio.Left) / 2.25f - txtrFacingRight.Width, mrio.Top + (mrio.Bottom - mrio.Top) / 2f) :
                    txtrNum == 2 ? new Vector2(mrio.Right - (mrio.Right - mrio.Left) / 2.75f - txtrFacingRight.Width, mrio.Top + (mrio.Bottom - mrio.Top) / 2f) :
                    txtrNum == 3 ? new Vector2(mrio.Right - (mrio.Right - mrio.Left) / 1.75f - txtrFacingRight.Width, mrio.Top + (mrio.Bottom - mrio.Top) / 2f) :
                    new Vector2(mrio.Right - (mrio.Right - mrio.Left) / 1.17f - txtrFacingRight.Width, mrio.Top + (mrio.Bottom - mrio.Top) / 2.2f);
            }
            base.UpdateEndOfFrame();
        }

        public override ItemThumbnail GetThumbnail()
        {
            return new BigPistolThumbnail(txtrFacingRight);
        }
    }
}
