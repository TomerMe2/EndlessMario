using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit.Weapons
{
    abstract class Weapon : GameObject
    {

        //TODO: CHANGE TEXTURES WHILE ATTACK, SET FRAME COUNT

        private Mario mrio;
        private bool isAttacking;
        private int framesSinceStartedAttack;

        public Weapon(Texture2D texture, Mario mrio) :
            base(new Vector2(mrio.Right, mrio.Bottom - (mrio.Bottom - mrio.Top)/2), texture, 1, true)
        {
            isAttacking = false;
        }

        public void Attack()
        {
            isAttacking = true;
        }
    }
}
