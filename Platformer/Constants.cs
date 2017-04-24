using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    // TODO: Find a nicer way doing this
    static class Constants
    {
        public static class Animation
        {
            public static int WalkRight { get { return 0; } }
            public static int WalkLeft { get { return 1; } }
        }

        public static class Game
        {
            public static int Scale { get { return 3; } }
            public static int Width { get { return 480; } }
            public static int Height { get { return 279; } }
        }
    }
}
