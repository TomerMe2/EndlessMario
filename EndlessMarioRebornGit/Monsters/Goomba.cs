using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndlessMarioRebornGit.Strategies;
using EndlessMarioRebornGit.Monsters;

namespace EndlessMarioRebornGit.Monsters
{
    class Goomba : Monster
    {
        //public static Vector2 MARIOSTARTLOC = new Vector2(350, Physics.FLOOR_LOC); //TRUE START LOC

        private const float GOMBA_JUMPING_POWER = 0;   //since Goomba can't jump
        private const float GOMBA_MAX_SPEED = 2;
        private const float GOMBA_ACCELERATION_X = 0.5f;
        private const float GOMBA_SCALE = 2f;
        public static string[] texturesNameFacingRight = { "GoombaStand", "GoombaWalk1", "GoombaWalk2", "GoombaWalk1"};
        public static string[] texturesNameFacingLeft = { "GoombaStandFlip", "GoombaWalk1Flip", "GoombaWalk2Flip", "GoombaWalk1Flip"};

        public Goomba(List<Texture2D> texturesFacingRight, List<Texture2D> texturesFacingLeft, Floor flr, float xLoc, Strategy strtgy) : base(texturesFacingRight, texturesFacingLeft,
            new Vector2(xLoc, Physics.FLOOR_LOC - texturesFacingLeft.ElementAt(0).Height * GOMBA_SCALE), GOMBA_SCALE, true, GOMBA_ACCELERATION_X, 0, GOMBA_MAX_SPEED, flr, strtgy)
        {

        }
    }
}
