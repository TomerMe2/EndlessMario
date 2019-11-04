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
    class HugeCannonBomb : Monster
    {
        private const float CANNON_JUMP_POWER = 0;   //since CannonBomb can't jump
        private const float HCANNON_MAX_SPEED = 0.5f;
        private const float CANNON_ACCELERATION_X = 0.5f;
        private const float CANNON_SCALE = 1;
        public static string[] texturesNameFacingRight = { "HugeCannonBomb", "HugeCannonBomb", "HugeCannonBomb", "HugeCannonBomb" };  //can't go left
        public static string[] texturesNameFacingLeft = { "HugeCannonBomb", "HugeCannonBomb", "HugeCannonBomb", "HugeCannonBomb" };
        public static string deadTextureNm = "HugeCannonBomb";  //can't be dead
        public static string deadTextureFlippedNm = "HugeCannonBomb";

        public HugeCannonBomb(List<Texture2D> texturesFacingRight, List<Texture2D> texturesFacingLeft, Floor flr, float xLoc, Texture2D deadTxtr, Texture2D deadTxtrFlip, double mrioPointsAtCreation) :
            base(texturesFacingRight, texturesFacingLeft, new Vector2(xLoc, Physics.FLOOR_LOC - texturesFacingLeft.ElementAt(0).Height * CANNON_SCALE), CANNON_SCALE, true,
                CANNON_ACCELERATION_X, 0, HCANNON_MAX_SPEED, flr, new AlwaysRightStrategy(), deadTxtr, deadTxtrFlip)
        {

        }

        protected override void Fall()
        {
            //do nothing, can't fall
        }

        protected override void HandleCollusion(Mario other, List<Direction> dirs)
        {
            base.HandleCollusion(other, dirs);
            //CannonBomb can't die, thus always hitting mario upon cullosion.
            other.Kill(this);
        }


        protected override void CollusionWithHardObj(GameObject other, List<Direction> dirs)
        {
            //do nothing, shall pass any obstacle
        }

        protected override void Die()
        {
            //do nothing, can't die
        }

        //We are doing this because the picture of HugeCannonBomb is quite huge
        public override float Right => base.Right - CurrentTexture.Width / 9f;
    }
}