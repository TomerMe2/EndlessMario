using EndlessMarioRebornGit.Monsters;
using EndlessMarioRebornGit.StillObjects;
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
    abstract class Projectile : MovingObj
    {
        private RangedWeapon wpnThatShotThis;
        private Mario mrio;
        private const float MAX_SPEED_FOR_BULLET = 999;
        private float mass;          //it's here for futher use maybe. In the futher a projectile can hurt and not only kill. It's power will be computet by mass*speed.



        //TODO: BUGS.
        //1. GO THROUGH PIPES.
        public Projectile(Texture2D textureFacingRight, Texture2D textureFacingLeft, RangedWeapon wpnThatShotThis, float mass, float scale,
            Floor flr, Mario mrio) : base(new List<Texture2D> { textureFacingRight }, new List<Texture2D> { textureFacingLeft }, LocationForNewBullet(mrio, wpnThatShotThis, textureFacingRight), scale, true,
             0, 0, MAX_SPEED_FOR_BULLET, flr, new AlwaysStayStrategy())
        {
            this.wpnThatShotThis = wpnThatShotThis;
            this.mass = mass;
            this.mrio = mrio;
            speedX = mrio.IsFacingRight() ? wpnThatShotThis.ShootingPower : -wpnThatShotThis.ShootingPower;
            currentTexture = mrio.IsFacingRight() ? textureFacingRight : textureFacingLeft;
            isFlipped = !mrio.IsFacingRight();
        }

        private static Vector2 LocationForNewBullet(Mario mrio, Weapon wpnThatShotThis, Texture2D projTxtr)
        {
            return mrio.IsFacingRight() ? new Vector2(wpnThatShotThis.Right, wpnThatShotThis.Top) : new Vector2(wpnThatShotThis.Left - projTxtr.Width, wpnThatShotThis.Top);
        }

        protected override void HandleNotWalkingSpeed()
        {
            HandleNotWalkingSpeedMechanics(Physics.PROJECTILE_FRICTION);
        }

        protected override void HandleCollusion(GameObject other, List<Direction> dirs)
        {
            base.HandleCollusion(other, dirs);
            //isNeedDisposal = true;
        }

        protected override void HandleCollusion(Monster other, List<Direction> dirs)
        {
            base.HandleCollusion(other, dirs);
            other.HitMnstr(mrio);
            isNeedDisposal = true;
        }

        protected override void HandleCollusion(Pipe other, List<Direction> dirs)
        {
            base.HandleCollusion(other, dirs);
            isNeedDisposal = true;

        }

        protected override void HandleCollusion(Floor other, List<Direction> dirs)
        {
            base.HandleCollusion(other, dirs);
            isNeedDisposal = true;
        }

        protected override void HandleCollusion(Mario other, List<Direction> dirs)
        {
            base.HandleCollusion(other, dirs);
        }

        protected override void HandleCollusion(Weapon other, List<Direction> dirs)
        {
            base.HandleCollusion(other, dirs);
        }


        protected override void HandleCollusion(CannonBomb other, List<Direction> dirs)
        {
            base.HandleCollusion(other, dirs);
            isNeedDisposal = true;
        }

        protected override void CollusionWithHardObj(GameObject other, List<Direction> dirs)
        {
            CollusionWithHardObjMechanism(other, dirs, Physics.PROJECTILE_GRAVITY);
        }

        protected override void Fall()
        {
            FallMechanism(Physics.PROJECTILE_GRAVITY);
        }
    }
}
