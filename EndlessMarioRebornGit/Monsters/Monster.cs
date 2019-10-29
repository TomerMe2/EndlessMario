using EndlessMarioRebornGit.Strategies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit.Monsters
{
    class Monster : MovingObj
    {
        private bool isDead;
        private int framesFromDeath;
        private const int MAX_FRAMES_BEFORE_DISPOSAL = 20;

        private Texture2D deadTexture;
        private Texture2D deadTextureFlipped;

        public Monster(List<Texture2D> texturesFacingRight, List<Texture2D> texturesFacingLeft, Vector2 startLoc, float scale, bool isCollideAble, float walkingPower,
          float jumpingPower, float maxSpeed, Floor flr, Strategy strtgy, Texture2D deadTexture, Texture2D deadTextureFlipped) : base(texturesFacingRight, texturesFacingLeft, startLoc, scale, isCollideAble,
              walkingPower, jumpingPower, maxSpeed, flr, strtgy)
        {
            this.deadTexture = deadTexture;
            this.deadTextureFlipped = deadTextureFlipped;
            isDead = false;
            framesFromDeath = -1;
        }

        public bool IsDead
        {
            get { return isDead; }
        }


        protected override void HandleCollusion(Mario other, List<Direction> dirs)
        {
            base.HandleCollusion(other, dirs);
            if (!isDead)
            {
                if (other.SpeedY > 0 && dirs.Contains(Direction.Down) && other.Loc.Y + other.CurrentTexture.Height * Scale < Loc.Y + CurrentTexture.Height * Scale)
                {
                    HitMnstr(other);
                }
                else
                {
                    other.HitMrio(this);
                }
            }
        }

        protected override void HandleCollusion(GreenTurtleShield other, List<Direction> dirs)
        {
            base.HandleCollusion(other, dirs);
            if (other.SpeedX != 0)
            {
                HitMnstr(null);
            }
        }

        /// <summary>
        /// This monster is being hit by mario.
        /// mrio can be null
        /// </summary>
        public void HitMnstr(Mario mrio)
        {
            Die();
            if (mrio != null)
            {
                mrio.AddPointsFromKillingMonster();
            }
        }

        protected virtual void Die()
        {
            if (!isDead)
            {
                framesFromDeath = 0;
                isDead = true;
                if (deadTexture != null && deadTextureFlipped != null)
                {
                    currentTexture = isFlipped ? deadTextureFlipped : deadTexture;
                }
                Loc = new Vector2(Loc.X, Physics.FLOOR_LOC - currentTexture.Height * scale);
            }
        }

        public override void UpdateFrameStart()
        {
            if (!isDead)
            {
                base.UpdateFrameStart();
            }
            else
            {
                framesFromDeath++;
                if (framesFromDeath > MAX_FRAMES_BEFORE_DISPOSAL)
                {
                    isNeedDisposal = true;
                }
            }
        }

        public override void UpdateFrameEnd()
        {
            if (!isDead)
            {
                base.UpdateFrameEnd();
            }
        }
    }
}
