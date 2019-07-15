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
        bool isDead;
        public Monster(List<Texture2D> texturesFacingRight, List<Texture2D> texturesFacingLeft, Vector2 startLoc, float scale, bool isCollideAble, float walkingPower,
          float jumpingPower, float maxSpeed, Floor flr, Strategy strtgy) : base(texturesFacingRight, texturesFacingLeft, startLoc, scale, isCollideAble,
              walkingPower, jumpingPower, maxSpeed, flr, strtgy)
        {
            isDead = false;
        }

        public bool IsDead
        {
            get { return isDead; }
        }

        protected override void HandleCollusion(Mario other, List<Direction> dirs)
        {
            if (dirs.Contains(Direction.Down) && other.Loc.Y + other.CurrentTexture.Height*Scale < Loc.Y + CurrentTexture.Height*Scale)
            {
                HitMnstr(other);
            }
            else
            {
                other.HitMrio(this);
            }
        }

        /// <summary>
        /// This monster is being hit by mario.
        /// </summary>
        public void HitMnstr(Mario mrio)
        {
            Die();
        }

        protected void Die()
        {
            isDead = true;
        }
    }
}
