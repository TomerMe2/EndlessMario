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
using EndlessMarioRebornGit.Commands;
using EndlessMarioRebornGit.StillObjects;
using EndlessMarioRebornGit.Weapons;

namespace EndlessMarioRebornGit
{
    class Mario : MovingObj
    {
        public const int INVENTORY_SIZE = 6;
        public static Vector2 MARIOSTARTLOC = new Vector2(350, Physics.FLOOR_LOC); //TRUE START LOC
        //public static Vector2 MARIOSTARTLOC = new Vector2(250, Physics.FLOOR_LOC);
        private const float MARIOJUMPOWER = 17;
        private const float JUMP_POWER_AFTER_HIT_ENEMY = 6;
        private const float MARIOMAXNORMALSPEEDX = 4;
        private const float MARIOACCELERATIONX = 1;
        private const int MAX_TURNS_COUNT_FROM_HIT = 97;
        private const int MAX_TURNS_COUNT_TO_FLICKER = 4;
        private const int FRAMES_OFF_DAMAGE_GRACE = 10;
        private int turnsCountFromHit;
        private double points;
        private List<Heart> hrtsLst;
        private bool hasLost;
        private float distanceAcc;
        private float lastXtoRaisePoints;
        private Weapon[] wpnsInventory;
        private int currWpnInd;
        private Chest focousedChest;
        private Projectile shotThisFrame;
        

        public static string[] texturesNameFacingRight = { @"Mario\MarioStand", @"Mario\MarioWalk1", @"Mario\MarioWalk2", @"Mario\MarioWalk3", @"Mario\MarioJump" };
        public static string[] texturesNameFacingLeft = { @"Mario\MarioStandFlip", @"Mario\MarioWalk1Flip", @"Mario\MarioWalk2Flip", @"Mario\MarioWalk3Flip", @"Mario\MarioJumpFlip" };

        public Mario(List<Texture2D> texturesFacingRight, List<Texture2D> texturesFacingLeft, Floor flr, UserMarioMovingStrategy strtgy, List<Heart> hrtsLst) : base(texturesFacingRight, texturesFacingLeft, 
            new Vector2(MARIOSTARTLOC.X, MARIOSTARTLOC.Y - texturesFacingLeft.ElementAt(0).Height*0.6f), 0.6f, true, MARIOACCELERATIONX, MARIOJUMPOWER, MARIOMAXNORMALSPEEDX, flr, strtgy)
        {
            this.hrtsLst = hrtsLst.ToList();
            turnsCountFromHit = -1;
            hasLost = false;
            points = 0;
            distanceAcc = 0;
            lastXtoRaisePoints = 0;
            wpnsInventory = new Weapon[INVENTORY_SIZE];
            currWpnInd = 0;
            focousedChest = null;
            shotThisFrame = null;
        }

        public override void UpdateFrameStart()
        {
            distanceAcc += speedX;
            if (distanceAcc > lastXtoRaisePoints + 200)
            {
                AddPointsFromDistance();
                lastXtoRaisePoints = distanceAcc;
            }
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
            shotThisFrame = null;
            base.UpdateFrameStart();
        }


        protected override void UpdateSpeedEndOfFrame()
        {
            loc.Y = loc.Y + speedY;
            if (CurrWeapon() != null)
            {
                CurrWeapon().UpdateEndOfFrame();
            }
            if (focousedChest != null && !collidesWithNow.Contains(focousedChest))
            {
                focousedChest.SwitchChestState();    //close it
                focousedChest = null;
            }
        }

        protected override void HandleCollusion(CannonBomb other, List<Direction> dirs)
        {
            base.HandleCollusion(other, dirs);
            HitMrio(other);
        }

        protected override void HandleCollusion(Monster other, List<Direction> dirs) {
            base.HandleCollusion(other, dirs);
            if (!other.IsDead)
            {
                if (SpeedY > 0
                    && (dirs.Count == 1 && dirs[0] == Direction.Up || dirs.Count == 2 && dirs[1] == Direction.Up)
                    && other.Loc.Y + other.CurrentTexture.Height * other.Scale > Loc.Y + CurrentTexture.Height * Scale)
                {
                    other.HitMnstr(this);
                    JumpProtected(JUMP_POWER_AFTER_HIT_ENEMY, Physics.GRAVITY);   //Mario should jump after hitting a monster
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
                turnsCountFromHit = MAX_TURNS_COUNT_FROM_HIT;
                hrtsLst[hrtsLst.Count - 1].PrepareForDisposal();
                hrtsLst.RemoveAt(hrtsLst.Count - 1);
                if (hrtsLst.Count == 0)
                {
                    hasLost = true;
                }
            }
        }

        protected override void HandleCommand(ShootCommand shootCmnd)
        {
            Weapon wpn = CurrWeapon();
            if (wpn != null && wpn is RangedWeapon)
            {
                shotThisFrame = (wpn as RangedWeapon).Shoot();
            }
        }

        protected override void HandleCommand(ChestSwitchCommand chstSwtchCmnd)
        {
            if (focousedChest == null)  //there's no focused chest
            {
                foreach (GameObject obj in collidesWithNow)
                {
                    if (obj is Chest)
                    {
                        focousedChest = obj as Chest;
                        focousedChest.SwitchChestState();   //open it
                        break;
                    }
                }
            }
            else   //there's a focused chest
            {
                focousedChest.SwitchChestState();   //close it
                focousedChest = null;
            }
        }

        protected override void HandleCommand(ChestCellSwitchCommand chstCellSwtchCmnd)
        {
            if (focousedChest != null)
            {
                focousedChest.SelectWeapon(chstCellSwtchCmnd.NumOfItem - 1);
            }
        }

        protected override void HandleCommand(SwitchInventoryAndChestCommand swtchInvAndChstCmnd)
        {
            if (focousedChest != null)
            {
                Weapon currInv = wpnsInventory[currWpnInd];
                wpnsInventory[currWpnInd] = focousedChest.ReplaceSelectWpn(currInv);
            }
        }

        protected override void HandleCommand(InventorySwitchCommand invSwtchCmnd)
        {
            currWpnInd = invSwtchCmnd.NumOfItem - 1;
        }

        

        /// <summary>
        /// CAN RETURN NULL!
        /// </summary>
        public Weapon CurrWeapon()
        {
            return wpnsInventory[currWpnInd];
        }

        /// <summary>
        /// RETURNS A NUMBER BETWEEN 0 AND 5
        /// </summary>
        public int CurrCellIndxOfChosenWpn()
        {
            return currWpnInd;
        }

        //ONLY FOR DEBUG. ITS BRAKING THE ENCAPSULATION!
        public void AddWeaponToInv(Weapon wpn, int indx)
        {
            wpnsInventory[indx] = wpn;
        }


        /// <summary>
        /// Mario is killed instantly by that monster
        /// </summary>
        public void Kill(Monster mnstr)
        {
            foreach (Heart hrt in hrtsLst)
            {
                hrt.PrepareForDisposal();
            }
            hrtsLst.Clear();
            hasLost = true;
        }

        public Chest ChestToDisplay
        {
            get{ return focousedChest; }
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

        public Projectile ShotThisFrame
        {
            get { return shotThisFrame; }
        }

        public List<Weapon> WpnsInventory
        {
            get { return new List<Weapon>(wpnsInventory); }
        }

        public int GetTextureNum()
        {
            if (!isFlipped)
            {
                for (int i = 0; i < texturesFacingRight.Count; i++)
                {
                    if (currentTexture == texturesFacingRight[i])
                    {
                        return i;
                    }
                }
            }
            else
            {
                for (int i = 0; i < texturesFacingLeft.Count; i++)
                {
                    if (currentTexture == texturesFacingLeft[i])
                    {
                        return i;
                    }
                }
            }
            return 0; //never happends
        }

        public bool IsFacingRight()
        {
            return !isFlipped;
        }
    }
}
