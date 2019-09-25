using EndlessMarioRebornGit.Strategies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit.Weapons
{
    abstract class RangedWeapon : Weapon
    {
        protected float shootingPower;
        protected int framesBetweenShots;     //how much frames need to pass before shooting the next bullet
        protected int framesFromLastShot;

        protected Texture2D txtrOfProjecileFacingLeft;
        protected Texture2D txtrOfProjectileFacingRight;
        protected Floor flr;

        public RangedWeapon(Texture2D txtOfWpnFacingRight, Texture2D txtrOfWpnFacingLeft, Mario mrio, Floor flr, float shootingPower, int framesBeforeNextShot, 
            Texture2D txtrOfProjectileFacingRight, Texture2D txtrOfProjecileFacingLeft) : base(txtOfWpnFacingRight, txtrOfWpnFacingLeft, mrio)
        {
            this.shootingPower = shootingPower;
            this.framesBetweenShots = framesBeforeNextShot;
            this.txtrOfProjecileFacingLeft = txtrOfProjecileFacingLeft;
            this.txtrOfProjectileFacingRight = txtrOfProjectileFacingRight;
            this.flr = flr;
            framesFromLastShot = framesBetweenShots + 1;
        }

        public override void UpdateEndOfFrame()
        {
            if (framesFromLastShot <= framesBetweenShots)
            {
                framesFromLastShot++;
            }
        }


        /// <summary>
        /// Shoot! CAN RETURN NULL IF CAN'T SHOOT YET!
        /// </summary>
        /// <returns></returns>
        public abstract Projectile Shoot();

        public float ShootingPower
        {
            get { return shootingPower; }
        }
    }
}
