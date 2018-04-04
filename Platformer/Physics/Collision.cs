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
        public Collision(Entity entity, Vector2 vector)
        {
            EntityType = entity.GetType();
            Vector = vector;
        }

        public Type EntityType;
        public Vector2 Vector;
    }
}
