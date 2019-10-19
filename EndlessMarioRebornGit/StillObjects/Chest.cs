using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndlessMarioRebornGit.Weapons;
namespace EndlessMarioRebornGit.StillObjects
{
    class Chest : GameObject
    {
        public static string textureNameClosed = @"Chest\ChestClosed";
        public static string textureNameOpen = @"Chest\ChestOpen";
        public const int ITEMS_NUM_IN_CHEST = 6;

        private Texture2D closeTxtr;
        private Texture2D openTxtr;
        private bool isOpen;
        private Weapon[] wpns;
        private int selectedWpnIndx;

        public Chest(Texture2D textureClosed, Texture2D textureOpen, float locX, float scale, Weapon[] wpns) :
            base(new Vector2(locX, Physics.FLOOR_LOC - textureClosed.Height * scale), textureClosed, scale, true)
        {
            closeTxtr = textureClosed;
            openTxtr = textureOpen;
            isOpen = false;
            this.wpns = wpns;
            selectedWpnIndx = 0;
        }

        private void Open()
        {
            CurrentTexture = openTxtr;
            isOpen = true;
        }

        private void Close()
        {
            CurrentTexture = closeTxtr;
            isOpen = false;
        }

        /// <summary>
        /// Open or Close, depends on the last state of the chest
        /// </summary>
        public void SwitchChestState()
        {
            if (isOpen)
            {
                Close();
            }
            else
            {
                Open();
            }
        }

        public Weapon ReplaceSelectWpn(Weapon toReplaceWith)
        {
            Weapon wpn = wpns[selectedWpnIndx];
            wpns[selectedWpnIndx] = toReplaceWith;
            return wpn;
        }
        

        /// <summary>
        /// Selects the weapon with the given index
        /// </summary>
        public void SelectWeapon(int weaponIndx)
        {
            if (weaponIndx >= 0 && weaponIndx < wpns.Length)
            {
                selectedWpnIndx = weaponIndx;
            }
        }

        public int SelectedWpnIndx
        {
            get { return selectedWpnIndx; }
        }

        public Weapon[] Wpns
        {
            get { return wpns; }
        }
    }
}
