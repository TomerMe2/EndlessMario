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
    class GreenTurtle : Monster
    {
        private const float TRTL_JUMPING_POWER = 0;   //since Goomba can't jump
        private const float TRTL_MAX_SPEED = 2;
        private const float TRTL_ACCELERATION_X = 0.5f;
        private const float TRTL_SCALE = 1f;
        public static string[] texturesNameFacingRight = { @"GreenTurtle\GreenTurtleStand", @"GreenTurtle\GreenTurtleWalk1",
            @"GreenTurtle\GreenTurtleWalk2", @"GreenTurtle\GreenTurtleWalk3" };
        public static string[] texturesNameFacingLeft = { @"GreenTurtle\GreenTurtleStandFlip", @"GreenTurtle\GreenTurtleWalk1Flip",
            @"GreenTurtle\GreenTurtleWalk2Flip", @"GreenTurtle\GreenTurtleWalk3Flip" };

        public GreenTurtle(List<Texture2D> texturesFacingRight, List<Texture2D> texturesFacingLeft, Floor flr, float xLoc, Strategy strtgy, double mrioPointsAtCreation) :
            base(texturesFacingRight, texturesFacingLeft, new Vector2(xLoc, Physics.FLOOR_LOC - texturesFacingLeft.ElementAt(0).Height * TRTL_SCALE), TRTL_SCALE, true,
                TRTL_ACCELERATION_X + (float)mrioPointsAtCreation / 100000, 0, TRTL_MAX_SPEED + (float)mrioPointsAtCreation / 100000, flr, strtgy, null, null)
        {
            
        }
    }
}
