﻿using Microsoft.Xna.Framework;
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

        private const float GOMBA_JUMPING_POWER = 0;   //since Goomba can't jump
        private const float GOMBA_MAX_SPEED = 2;
        private const float GOMBA_ACCELERATION_X = 0.5f;
        private const float GOMBA_SCALE = 2f;
        public static string[] texturesNameFacingRight = { @"Goomba\GoombaStand", @"Goomba\GoombaWalk1", @"Goomba\GoombaWalk2", @"Goomba\GoombaWalk1" };
        public static string[] texturesNameFacingLeft = { @"Goomba\GoombaStandFlip", @"Goomba\GoombaWalk1Flip", @"Goomba\GoombaWalk2Flip", @"Goomba\GoombaWalk1Flip" };
        public static string deadTextureNm = @"Goomba\GoombaSquashed";
        public static string deadTextureFlippedNm = @"Goomba\GoombaSquashedFlip";

        public Goomba(List<Texture2D> texturesFacingRight, List<Texture2D> texturesFacingLeft, Floor flr, float xLoc, Strategy strtgy, Texture2D deadTxtr, Texture2D deadTxtrFlip, double mrioPointsAtCreation) :
            base(texturesFacingRight, texturesFacingLeft, new Vector2(xLoc, Physics.FLOOR_LOC - texturesFacingLeft.ElementAt(0).Height * GOMBA_SCALE), GOMBA_SCALE, true,
                GOMBA_ACCELERATION_X + (float)mrioPointsAtCreation / 100000, 0, GOMBA_MAX_SPEED + (float)mrioPointsAtCreation/100000, flr, strtgy, deadTxtr, deadTxtrFlip)
        {

        }
    }
}
