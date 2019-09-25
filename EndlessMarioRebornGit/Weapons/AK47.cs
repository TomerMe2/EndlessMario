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
    class AK47 : RangedWeapon
    {
        public static string textureNameFacingRight = @"AK47\AK47FacingRight";
        public static string textureNameFacingLeft = @"AK47\AK47FacingLeft";
        public const float SHOOTING_POWER = 28f;
        public const int FRAMES_BEFORE_NEXT_SHOT = 60;
        public const int MAX_BULLETS_PER_BURST = 3;

        private const int FRAMES_BETWEEN_BULLETS_IN_BURST = 3;
        private int bulletsShotThisBurst;
        private bool hasShotPrevTurn;
        private bool hasShotThisTurn;

        public AK47(Texture2D txtOfWpnFacingRight, Texture2D txtrOfWpnFacingLeft, Mario mrio, Floor flr,
            Texture2D txtrOfProjectileFacingRight, Texture2D txtrOfProjecileFacingLeft) : base(txtOfWpnFacingRight, txtrOfWpnFacingLeft, mrio, flr, SHOOTING_POWER,
                FRAMES_BEFORE_NEXT_SHOT, txtrOfProjectileFacingRight, txtrOfProjecileFacingLeft)
        {
            bulletsShotThisBurst = 0;
            hasShotPrevTurn = false;
            hasShotThisTurn = false;
        }

        public override Projectile Shoot()
        {
            hasShotThisTurn = true;
            if (framesFromLastShot % FRAMES_BETWEEN_BULLETS_IN_BURST == 0 && bulletsShotThisBurst < MAX_BULLETS_PER_BURST)
            {
                bulletsShotThisBurst++;
                return new PistolBullet(txtrOfProjectileFacingRight, txtrOfProjecileFacingLeft, this, flr, mrio);
            }
            if (framesFromLastShot > framesBetweenShots)
            {
                bulletsShotThisBurst = 1;
                framesFromLastShot = 0;
                return new PistolBullet(txtrOfProjectileFacingRight, txtrOfProjecileFacingLeft, this, flr, mrio);
            }
            return null;
        }

        public override void UpdateEndOfFrame()
        {
            hasShotPrevTurn = hasShotThisTurn;
            hasShotThisTurn = false;

            int txtrNum = mrio.GetTextureNum();
            if (mrio.IsFacingRight())
            {
                currentTexture = txtrFacingRight;
                Loc = txtrNum == 0 ? new Vector2(mrio.Left + (mrio.Right - mrio.Left) / 2.05f, mrio.Top + (mrio.Bottom - mrio.Top) / 1.95f) :
                    txtrNum == 1 ? new Vector2(mrio.Left + (mrio.Right - mrio.Left) / 2.4f, mrio.Top + (mrio.Bottom - mrio.Top) / 1.95f) :
                    txtrNum == 2 ? new Vector2(mrio.Left + (mrio.Right - mrio.Left) / 2.95f, mrio.Top + (mrio.Bottom - mrio.Top) / 1.95f) :
                    txtrNum == 3 ? new Vector2(mrio.Left + (mrio.Right - mrio.Left) / 2.2f, mrio.Top + (mrio.Bottom - mrio.Top) / 1.95f) :
                    new Vector2(mrio.Left + (mrio.Right - mrio.Left) / 1.95f, mrio.Top + (mrio.Bottom - mrio.Top) / 2.2f);
            }
            else
            {
                currentTexture = txtrFacingLeft;
                Loc = txtrNum == 0 ? new Vector2(mrio.Right - (mrio.Right - mrio.Left) / 2.05f - txtrFacingRight.Width, mrio.Top + (mrio.Bottom - mrio.Top) / 1.95f) :
                    txtrNum == 1 ? new Vector2(mrio.Right - (mrio.Right - mrio.Left) / 2.4f - txtrFacingRight.Width, mrio.Top + (mrio.Bottom - mrio.Top) / 1.95f) :
                    txtrNum == 2 ? new Vector2(mrio.Right - (mrio.Right - mrio.Left) / 2.95f - txtrFacingRight.Width, mrio.Top + (mrio.Bottom - mrio.Top) / 1.95f) :
                    txtrNum == 3 ? new Vector2(mrio.Right - (mrio.Right - mrio.Left) / 2.2f - txtrFacingRight.Width, mrio.Top + (mrio.Bottom - mrio.Top) / 1.95f) :
                    new Vector2(mrio.Right - (mrio.Right - mrio.Left) / 1.95f - txtrFacingRight.Width, mrio.Top + (mrio.Bottom - mrio.Top) / 2.2f);
            }
            base.UpdateEndOfFrame();
        }

        public override ItemThumbnail GetThumbnail()
        {
            return new AK47Thumbnail(txtrFacingRight);
        }
    }
}
