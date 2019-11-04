using EndlessMarioRebornGit.Commands;
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
    class FlyingEnemy : Monster
    {

        private const float JUMPING_POWER = 0;   //since can't jump, he is flyinggggggggggg
        private const float MAX_SPEED = 2;
        private const float ACCELERATION_X = 0.5f;
        private const float SCALE = 1f;
        private const float MAX_SPEED_Y = 1;
        public static string textureNameFacingRight = @"FlyingEnemy\FlyingEnemy";
        public static string textureNameFacingLeft = @"FlyingEnemy\FlyingEnemyFlip";
        public static string deadTextureNm = @"FlyingEnemy\DeadFlyingEnemy";
        public static string deadTextureFlippedNm = @"FlyingEnemy\DeadFlyingEnemyFlip";

        public FlyingEnemy(List<Texture2D> texturesFacingRight, List<Texture2D> texturesFacingLeft, Floor flr, float xLoc, Strategy strtgy, Texture2D deadTxtr, Texture2D deadTxtrFlip, double mrioPointsAtCreation) :
            base(texturesFacingRight, texturesFacingLeft, new Vector2(xLoc, Physics.FLOOR_LOC - texturesFacingLeft.ElementAt(0).Height * SCALE), SCALE, true,
                ACCELERATION_X + (float)mrioPointsAtCreation / 100000, 0, MAX_SPEED + (float)mrioPointsAtCreation / 100000, flr, strtgy, deadTxtr, deadTxtrFlip)
        {
        }

        protected override void HandleCommand(MoveUpCommand mvUpCmnd)
        {
            base.HandleCommand(mvUpCmnd);
            speedY = - MAX_SPEED_Y - Physics.GRAVITY;
        }

        protected override void HandleSpeedYEndOfFrameStart()
        {
            //do nothing
        }

        protected override void HandleSpeedStartOfFrameStart()
        {
            speedY = 0;
        }
    }
}
