using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EndlessMarioRebornGit
{
    class GameObject
    {
        protected Vector2 loc;
        protected Texture2D currentTexture;
        protected float scale;
        protected bool isCollideAble;

        public GameObject(Vector2 loc, Texture2D texture, float scale, bool isCollideAble)
        {
            this.loc = loc;
            this.currentTexture = texture;
            this.scale = scale;
            this.isCollideAble = isCollideAble;
        }

        public GameObject(Vector2 loc, float scale, bool isCollideAble)
        {
            this.loc = loc;
            this.scale = scale;
            this.isCollideAble = isCollideAble;
        }

        virtual public Vector2 Loc
        {
            get { return loc; }
            set { loc = value; }
        }

        public void MoveOnX(float howMuch)
        {
            this.loc.X = this.loc.X + howMuch;
        }

        virtual public Texture2D CurrentTexture
        {
            get { return this.currentTexture; }
            set { this.currentTexture = value; }
        }

        public float Scale
        {
            get { return this.scale; }
        }

        public bool IsCollideAble
        {
            get { return this.isCollideAble; }
        }
    }
}
