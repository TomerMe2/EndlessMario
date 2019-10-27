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
    class Uzi : RangedWeapon
    {
        public static string textureNameFacingRight = @"Weapons\Uzi\UziFacingRight";
        public static string textureNameFacingLeft = @"Weapons\Uzi\UziFacingLeft";
        public const float SHOOTING_POWER = 35f;
        public const int FRAMES_BEFORE_NEXT_SHOT = 10;

        private bool hasShotPrevTurn;
        private bool hasShotThisTurn;

        public Uzi(Texture2D txtOfWpnFacingRight, Texture2D txtrOfWpnFacingLeft, Mario mrio, Floor flr,
            Texture2D txtrOfProjectileFacingRight, Texture2D txtrOfProjecileFacingLeft) : base(txtOfWpnFacingRight, txtrOfWpnFacingLeft, mrio, flr, SHOOTING_POWER,
                FRAMES_BEFORE_NEXT_SHOT, txtrOfProjectileFacingRight, txtrOfProjecileFacingLeft)
        {
            hasShotPrevTurn = false;
            hasShotThisTurn = false;
        }

        public override Projectile Shoot()
        {
            hasShotThisTurn = true;
            if (framesFromLastShot >= framesBetweenShots)
            {
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
                Loc = txtrNum == 0 ? new Vector2(mrio.Left + (mrio.Right - mrio.Left) / 1.55f, mrio.Top + (mrio.Bottom - mrio.Top) / 2f) :
                    txtrNum == 1 ? new Vector2(mrio.Left + (mrio.Right - mrio.Left) / 2.40f, mrio.Top + (mrio.Bottom - mrio.Top) / 2f) :
                    txtrNum == 2 ? new Vector2(mrio.Left + (mrio.Right - mrio.Left) / 2.90f, mrio.Top + (mrio.Bottom - mrio.Top) / 2f) :
                    txtrNum == 3 ? new Vector2(mrio.Left + (mrio.Right - mrio.Left) / 1.90f, mrio.Top + (mrio.Bottom - mrio.Top) / 2f) :
                    new Vector2(mrio.Left + (mrio.Right - mrio.Left) / 1.32f, mrio.Top + (mrio.Bottom - mrio.Top) / 2.2f);
            }
            else
            {
                currentTexture = txtrFacingLeft;
                Loc = txtrNum == 0 ? new Vector2(mrio.Right - (mrio.Right - mrio.Left) / 1.55f - txtrFacingRight.Width, mrio.Top + (mrio.Bottom - mrio.Top) / 2f) :
                    txtrNum == 1 ? new Vector2(mrio.Right - (mrio.Right - mrio.Left) / 2.40f - txtrFacingRight.Width, mrio.Top + (mrio.Bottom - mrio.Top) / 2f) :
                    txtrNum == 2 ? new Vector2(mrio.Right - (mrio.Right - mrio.Left) / 2.90f - txtrFacingRight.Width, mrio.Top + (mrio.Bottom - mrio.Top) / 2f) :
                    txtrNum == 3 ? new Vector2(mrio.Right - (mrio.Right - mrio.Left) / 1.90f - txtrFacingRight.Width, mrio.Top + (mrio.Bottom - mrio.Top) / 2f) :
                    new Vector2(mrio.Right - (mrio.Right - mrio.Left) / 1.32f - txtrFacingRight.Width, mrio.Top + (mrio.Bottom - mrio.Top) / 2.2f);
            }
            base.UpdateEndOfFrame();
        }

        public override ItemThumbnail GetThumbnail()
        {
            return new UziThumbnail(txtrFacingRight);
        }
    }
}
