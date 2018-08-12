using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EndlessMarioRebornGit
{
    class Mario : MovingObj
    {
        //public static Vector2 MARIOSTARTLOC = new Vector2(350, 352); TRUE START LOC
        public static Vector2 MARIOSTARTLOC = new Vector2(100, 200);
        private const float MARIOJUMPOWER = 17;
        private const float MARIOMAXNORMALSPEEDX = 4;
        private const float MARIOACCELERATIONX = 1;
        private bool isMarioSuper = false;
        public static string[] texturesNameFacingRight = { "MarioStand", "MarioWalk1", "MarioWalk2", "MarioWalk3", "MarioJump" };
        public static string[] texturesNameFacingLeft = {"MarioStandFlip", "MarioWalk1Flip", "MarioWalk2Flip", "MarioWalk3Flip", "MarioJumpFlip" };

        public Mario(List<Texture2D> texturesFacingRight, List<Texture2D> texturesFacingLeft) : base(texturesFacingRight, texturesFacingLeft, MARIOSTARTLOC, 1, true, MARIOACCELERATIONX,
            MARIOJUMPOWER, MARIOMAXNORMALSPEEDX)
        {

        }

        //public virtual void Walk(Direction dir)
        //{
        //    if (dir == Direction.Left)
        //    {

        //    }
        //    if (dir == Direction.Right)
        //    {

        //    }
        //}

        //public virtual void Jump()
        //{

        //}


    }
}
