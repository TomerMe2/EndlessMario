using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndlessMarioRebornGit
{
    public class FloatRectangle
    {
        float Left { get; set; }
        float Right { get; set; }
        float Top { get; set; }
        float Bottom { get; set; }

        public FloatRectangle(float left, float right, float top, float bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public bool Intersects(FloatRectangle anotherRect)
        {
            return (Left < anotherRect.Right && Right > anotherRect.Left && Top < anotherRect.Bottom && Bottom > anotherRect.Top);
        }
    }
}
