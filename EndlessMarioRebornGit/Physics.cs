using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit
{
    public enum Direction
    {
        Left,
        Right,
        Down,
        Up,
        Rand,
        None
    }
    static class Physics
    {
        public const float GRAVITY = 1;
        public const float FRICTION = 0.4f;
        public const int FLOOR_LOC = 427;
    }
}
