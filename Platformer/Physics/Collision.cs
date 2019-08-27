using Microsoft.Xna.Framework;
using Platformer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Physics
{
    public struct Collision
    {
        public Collision(Type entityType, Vector2 vector)
        {
            EntityType = entityType;
            Vector = vector;
        }

        public Type EntityType;
        public Vector2 Vector;
    }
}
