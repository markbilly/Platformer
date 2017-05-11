using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Utilities
{
    public struct RectangleF
    {
        public RectangleF(Vector2 position, Point size)
        {
            X = position.X;
            Y = position.Y;
            Width = size.X;
            Height = size.Y;
        }

        public float X;
        public float Y;
        public int Width;
        public int Height;

        public static bool Intersects(RectangleF rectangle1, RectangleF rectangle2)
        {
            return
                rectangle1.X < rectangle2.X + rectangle2.Width &&
                rectangle1.X + rectangle1.Width > rectangle2.X &&
                rectangle1.Y < rectangle2.Y + rectangle2.Height &&
                rectangle1.Height + rectangle1.Y > rectangle2.Y;
        }
    }
}
