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
    class PistolBullet : Projectile
    {
        public static string textureNameFacingRight = @"Weapons\Pistol\PistolBulletFacingRight";
        public static string textureNameFacingLeft = @"Weapons\Pistol\PistolBulletFacingLeft";

        public const float MASS = 0.05f;

        public PistolBullet(Texture2D textureFacingRight, Texture2D textureFacingLeft, RangedWeapon wpnThatShotThis, Floor flr, Mario mrio) : 
            base(textureFacingRight, textureFacingLeft, wpnThatShotThis, MASS, 1, flr, mrio)
        {

        }

    }
}
