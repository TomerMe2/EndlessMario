using EndlessMarioRebornGit.Monsters;
using EndlessMarioRebornGit.Strategies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit
{
    class GreenTurtleShield : MovingObj
    {
        private const float SHIELD_JUMPING_POWER = 0;   //since shield can't jump
        private const float SHIELD_MAX_SPEED = 15f;
        private const float SHIELD_ACCELERATION_X = 15f;
        private const float SHIELD_SCALE = 1f;
        public static string textureNameFacingRight = @"GreenTurtle\GreenTurtleShell";
        public static string textureNameFacingLeft = @"GreenTurtle\GreenTurtleShellFlip";

        private OnceLeftRightStrategy shieldStrtgy;

        public GreenTurtleShield(List<Texture2D> texturesFacingRight, List<Texture2D> texturesFacingLeft, Floor flr, float xLoc, OnceLeftRightStrategy strtgy, GreenTurtle trtl) :
            base(texturesFacingRight, texturesFacingLeft, new Vector2(xLoc, Physics.FLOOR_LOC - texturesFacingLeft.ElementAt(0).Height * SHIELD_SCALE), SHIELD_SCALE, true,
                SHIELD_ACCELERATION_X, 0, SHIELD_MAX_SPEED, flr, strtgy)
        {
            shieldStrtgy = strtgy;
            if (!trtl.isFacingRight)
            {
                CurrentTexture = texturesFacingLeft[0];   //flip it! because the turtle was flipped when he was killed
            }
        }

        protected override void HandleCollusion(Mario other, List<Direction> dirs)
        {
            base.HandleCollusion(other, dirs);
            if (other.SpeedY > 0)
            {
                float shieldMiddleX = Left + (Right - Left) / 2;
                float mrioMiddleX = other.Left + (other.Right - other.Left) / 2;
                if (mrioMiddleX > shieldMiddleX)
                {
                    shieldStrtgy.GoLeft();
                }
                else
                {
                    shieldStrtgy.GoRight();
                }
            }
        }
    }
}
