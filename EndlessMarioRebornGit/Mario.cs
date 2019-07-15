using System;
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
        private const float JUMP_POWER_AFTER_HIT_ENEMY = 6;
        private const float MARIOMAXNORMALSPEEDX = 4;
        private const float MARIOACCELERATIONX = 1;
        private const int MAX_TURNS_COUNT_FROM_HIT = 97;
        private const int MAX_TURNS_COUNT_TO_FLICKER = 4;
        private const int FRAMES_OFF_DAMAGE_GRACE = 10;
        private bool isHit;
        private int turnsCountFromHit;
        private bool isMarioSuper = false;
        private double points;
        private List<Heart> hrtsLst;
        private bool hasLost;
        public static string[] texturesNameFacingRight = { "MarioStand", "MarioWalk1", "MarioWalk2", "MarioWalk3", "MarioJump" };
        public static string[] texturesNameFacingLeft = {"MarioStandFlip", "MarioWalk1Flip", "MarioWalk2Flip", "MarioWalk3Flip", "MarioJumpFlip" };

        public Mario(List<Texture2D> texturesFacingRight, List<Texture2D> texturesFacingLeft, Floor flr, UserMarioMovingStrategy strtgy, List<Heart> hrtsLst) : base(texturesFacingRight, texturesFacingLeft, 
            new Vector2(MARIOSTARTLOC.X, MARIOSTARTLOC.Y - texturesFacingLeft.ElementAt(0).Height*0.6f), 0.6f, true, MARIOACCELERATIONX, MARIOJUMPOWER, MARIOMAXNORMALSPEEDX, flr, strtgy)
        {
            this.hrtsLst = hrtsLst.ToList();
            isHit = false;
            turnsCountFromHit = -1;
            hasLost = false;
            points = 0;
        }

        public override void UpdateFrameStart()
        {
            if (turnsCountFromHit >= 0)
            {
                turnsCountFromHit--;
                if (turnsCountFromHit % 10 == 0)
                {
                    needToBeDraw = !needToBeDraw;
                }
                if (turnsCountFromHit < FRAMES_OFF_DAMAGE_GRACE)
                {
                    needToBeDraw = true;
                }
            }
            base.UpdateFrameStart();
        }

        protected override void UpdateSpeedEndOfFrame()
        {
            loc.Y = loc.Y + speedY;
        }

        protected override void HandleCollusion(Monster other, List<Direction> dirs) {
            if (!other.IsDead)
            {
                if ((dirs.Count == 1 && dirs[0] == Direction.Up || dirs.Count == 2 && dirs[1] == Direction.Up) && other.Loc.Y + other.CurrentTexture.Height * other.Scale > Loc.Y + CurrentTexture.Height * Scale)
                {
                    other.HitMnstr(this);
                    JumpProtected(JUMP_POWER_AFTER_HIT_ENEMY);   //Mario should jump after hitting a monster
                }
                else
                {
                    HitMrio(other);
                }
            }
            // {UP, LEFT} not good
            // {LEFT, UP} is good
            // {UP} is good
        }

        /// <summary>
        /// Mario is being hit by that monster
        /// </summary>
        public void HitMrio(Monster mnstr)
        {
            if (turnsCountFromHit < 0)
            {
                isHit = true;
                turnsCountFromHit = MAX_TURNS_COUNT_FROM_HIT;
                hrtsLst[hrtsLst.Count - 1].PrepareForDisposal();
                hrtsLst.RemoveAt(hrtsLst.Count - 1);
                if (hrtsLst.Count == 0)
                {
                    hasLost = true;
                }
            }
        }

        public void AddPointsFromDistance()
        {
            points = points == 0 ? 2 : (points * 1.05);
        }

        public void AddPointsFromKillingMonster()
        {
            points = points == 0 ? 2 : (points * 1.08);
        }

        public double Points
        {
            get { return points; }
        }
        public bool HasLost
        {
            get { return hasLost; }
        }
    }
}
