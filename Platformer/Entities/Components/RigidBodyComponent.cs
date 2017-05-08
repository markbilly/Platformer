using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Components
{
    public class RigidBodyComponent : IComponent
    {
        private Vector2 _collision;

        public RigidBodyComponent()
        {
            _collision = Vector2.Zero;
        }

        public IEnumerable<Entity> NearbyEntities { get; set; }

        public void AddCollision(Vector2 collision)
        {
            _collision = collision;
        }

        public Vector2 GetCollision()
        {
            var collision = _collision;

            _collision = Vector2.Zero;

            return collision;
        }

        public void Update(Entity entity)
        {
            if (NearbyEntities == null)
            {
                throw new InvalidOperationException("NearbyEntities has not been set");
            }

            foreach (var nearbyEntity in NearbyEntities)
            {
                if (entity != nearbyEntity)
                {
                    var thisEntityBounds = new Rectangle(entity.Position.ToPoint(), entity.Size);
                    var nearbyEntityBounds = new Rectangle(nearbyEntity.Position.ToPoint(), nearbyEntity.Size);
                    var nearbyEntityRigidBodyComponent = nearbyEntity.GetComponent<RigidBodyComponent>();

                    if (thisEntityBounds.Intersects(nearbyEntityBounds))
                    {
                        ResolveCollision(thisEntityBounds, nearbyEntityBounds, nearbyEntityRigidBodyComponent);
                    }
                }
            }
        }

        private void ResolveCollision(Rectangle thisEntityBounds, Rectangle nearbyEntityBounds, RigidBodyComponent nearbyEntityRigidBodyComponent)
        {
            Debug.WriteLine($"collision detected: X1 {thisEntityBounds.X}, X2: {nearbyEntityBounds.X}");

            var collisionX = nearbyEntityBounds.X - thisEntityBounds.X;
            if (collisionX != 0)
            {
                collisionX = collisionX > 0 ? 1 : -1;
            }

            Debug.WriteLine($"collision detected: collisionX value is {collisionX}");

            AddCollision(new Vector2(collisionX, 0));
            nearbyEntityRigidBodyComponent.AddCollision(new Vector2(-collisionX, 0));
        }
    }
}
