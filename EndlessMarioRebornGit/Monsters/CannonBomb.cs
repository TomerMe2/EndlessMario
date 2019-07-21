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
    class CannonBomb : Monster
    {
        private const float CANNON_JUMP_POWER = 0;   //since CannonBomb can't jump
        private const float CANNON_MAX_SPEED = 4;
        private const float CANNON_ACCELERATION_X = 4;
        private const float CANNON_SCALE = 0.6f;
        public static string[] texturesNameFacingRight = { "CannonBomb", "CannonBomb", "CannonBomb", "CannonBomb" };
        public static string[] texturesNameFacingLeft = { "CannonBomb", "CannonBomb", "CannonBomb", "CannonBomb" };  //can't go right
        public static string deadTextureNm = "CannonBomb";  //can't be dead
        public static string deadTextureFlippedNm = "CannonBomb";

        private float startingXloc;

        public CannonBomb(List<Texture2D> texturesFacingRight, List<Texture2D> texturesFacingLeft, Floor flr, float xLoc, float randSpaceFromFloor, Texture2D deadTxtr, Texture2D deadTxtrFlip, double mrioPointsAtCreation) :
            base(texturesFacingRight, texturesFacingLeft, new Vector2(xLoc, Physics.FLOOR_LOC - texturesFacingLeft.ElementAt(0).Height * CANNON_SCALE - randSpaceFromFloor), CANNON_SCALE, true,
                CANNON_ACCELERATION_X + (float)mrioPointsAtCreation / 100000, 0, CANNON_MAX_SPEED + (float)mrioPointsAtCreation / 100000, flr, new AlwaysLeftStrategy(), deadTxtr, deadTxtrFlip)
        {
            startingXloc = xLoc;
        }

        public override void UpdateFrameEnd()
        {
            if (Loc.X < startingXloc - 1500)
            {
                isNeedDisposal = true;
                needToBeDraw = false;
            }
            base.UpdateFrameEnd();
        }

        protected override void Fall()
        {
            //do nothing, can't fall
        }

        protected override void HandleCollusion(Mario other, List<Direction> dirs)
        {
            base.HandleCollusion(other, dirs);
            //CannonBomb can't die, thus always hitting mario upon cullosion.
            other.HitMrio(this);
        }


        protected override void CollusionWithHardObj(GameObject other, List<Direction> dirs)
        {
            base.HandleCollusion(other, dirs);
            //do nothing, shall pass any obstacle
        }

        protected override void Die()
        {
            //do nothing, can't die
        }

    }
}
