﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using EndlessMarioRebornGit.Strategies;
using EndlessMarioRebornGit.Monsters;

namespace EndlessMarioRebornGit
{
    class Mario : MovingObj
    {
        public static Vector2 MARIOSTARTLOC = new Vector2(350, Physics.FLOOR_LOC); //TRUE START LOC
        //public static Vector2 MARIOSTARTLOC = new Vector2(250, Physics.FLOOR_LOC);
        private const float MARIOJUMPOWER = 17;
        private const float MARIOMAXNORMALSPEEDX = 4;
        private const float MARIOACCELERATIONX = 1;
        private bool isMarioSuper = false;
        public static string[] texturesNameFacingRight = { "MarioStand", "MarioWalk1", "MarioWalk2", "MarioWalk3", "MarioJump" };
        public static string[] texturesNameFacingLeft = {"MarioStandFlip", "MarioWalk1Flip", "MarioWalk2Flip", "MarioWalk3Flip", "MarioJumpFlip" };

        public Mario(List<Texture2D> texturesFacingRight, List<Texture2D> texturesFacingLeft, Floor flr, UserMarioMovingStrategy strtgy) : base(texturesFacingRight, texturesFacingLeft, 
            new Vector2(MARIOSTARTLOC.X, MARIOSTARTLOC.Y - texturesFacingLeft.ElementAt(0).Height*0.6f), 0.6f, true, MARIOACCELERATIONX, MARIOJUMPOWER, MARIOMAXNORMALSPEEDX, flr, strtgy)
        {

        }

        protected override void UpdateSpeedEndOfFrame()
        {
            loc.Y = loc.Y + speedY;
        }

        protected override void HandleCollusion(Monster other, List<Direction> dirs) {
            if (dirs.Contains(Direction.Up) && other.Loc.Y + other.CurrentTexture.Height*other.Scale > Loc.Y + CurrentTexture.Height*Scale)     //TODO: dirs is acting weird while only "Up
            {
                other.HitMnstr(this);
            }
            else
            {
                HitMrio(other);
            }
        }

        /// <summary>
        /// Mario is being hit by that monster
        /// </summary>
        public void HitMrio(Monster mnstr)
        {
            //TODO: MAKE STUFF HAPPEN HERE
        }
    }
}
