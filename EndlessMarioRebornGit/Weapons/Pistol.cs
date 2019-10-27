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
    class Pistol : RangedWeapon
    {
        public static string textureNameFacingRight = @"Weapons\Pistol\PistolFacingRight";
        public static string textureNameFacingLeft = @"Weapons\Pistol\PistolFacingLeft";
        public const float SHOOTING_POWER = 20f;
        public const int FRAMES_BEFORE_NEXT_SHOT = 50;

        public Pistol(Texture2D txtOfWpnFacingRight, Texture2D txtrOfWpnFacingLeft, Mario mrio, Floor flr,
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
                Loc = txtrNum == 0 ? new Vector2(mrio.Left + (mrio.Right - mrio.Left) / 1.45f, mrio.Top + (mrio.Bottom - mrio.Top) / 1.67f) :
                    txtrNum == 1 ? new Vector2(mrio.Left + (mrio.Right - mrio.Left) / 2.3f, mrio.Top + (mrio.Bottom - mrio.Top) / 1.67f) :
                    txtrNum == 2 ? new Vector2(mrio.Left + (mrio.Right - mrio.Left) / 2.8f, mrio.Top + (mrio.Bottom - mrio.Top) / 1.67f) :
                    txtrNum == 3 ? new Vector2(mrio.Left + (mrio.Right - mrio.Left) / 1.8f, mrio.Top + (mrio.Bottom - mrio.Top) / 1.67f) :
                    new Vector2(mrio.Left + (mrio.Right - mrio.Left) / 1.23f, mrio.Top + (mrio.Bottom - mrio.Top) / 1.9f);
            }
            else
            {
                currentTexture = txtrFacingLeft;
                Loc = txtrNum == 0 ? new Vector2(mrio.Right - (mrio.Right - mrio.Left) / 1.45f - txtrFacingRight.Width, mrio.Top + (mrio.Bottom - mrio.Top) / 1.67f) :
                    txtrNum == 1 ? new Vector2(mrio.Right - (mrio.Right - mrio.Left) / 2.3f - txtrFacingRight.Width, mrio.Top + (mrio.Bottom - mrio.Top) / 1.67f) :
                    txtrNum == 2 ? new Vector2(mrio.Right - (mrio.Right - mrio.Left) / 2.8f - txtrFacingRight.Width, mrio.Top + (mrio.Bottom - mrio.Top) / 1.67f) :
                    txtrNum == 3 ? new Vector2(mrio.Right - (mrio.Right - mrio.Left) / 1.8f - txtrFacingRight.Width, mrio.Top + (mrio.Bottom - mrio.Top) / 1.67f) :
                    new Vector2(mrio.Right - (mrio.Right - mrio.Left) / 1.23f - txtrFacingRight.Width, mrio.Top + (mrio.Bottom - mrio.Top) / 1.9f);
            }
            base.UpdateEndOfFrame();
        }

        public override ItemThumbnail GetThumbnail()
        {
            return new PistolThumbnail(txtrFacingRight);
        }
    }
}
