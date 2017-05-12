using Microsoft.Xna.Framework;
using Platformer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Utilities
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
