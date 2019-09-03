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
        public Collision(CollisionProfiles collisionProfile, Vector2 vector)
        {
            CollisionProfile = collisionProfile;
            Vector = vector;
        }

        public CollisionProfiles CollisionProfile;
        public Vector2 Vector;
    }
}
