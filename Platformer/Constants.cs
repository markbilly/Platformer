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

        public static int GameScale { get { return 3; } }
        public static int GameWidth { get { return 480; } }
        public static int GameHeight { get { return 279; } }
    }
}
