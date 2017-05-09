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

        public Vector2 GetLatestCollision()
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

                    if (thisEntityBounds.Intersects(nearbyEntityBounds))
                    {
                        var collision = GetCollisionVector(thisEntityBounds, nearbyEntityBounds);
                        ResolveCollision(entity, collision);

                        AddCollision(collision);
                        nearbyEntity.GetComponent<RigidBodyComponent>().AddCollision(-collision);
                    }
                }
            }
        }

        private void ResolveCollision(Entity entity, Vector2 collision)
        {
            entity.Velocity = new Vector2(
                collision.X != 0 ? 0 : entity.Velocity.X,
                collision.Y != 0 ? 0 : entity.Velocity.Y);
        }

        private Vector2 GetCollisionVector(Rectangle thisEntityBounds, Rectangle nearbyEntityBounds)
        {
            var collisionX = nearbyEntityBounds.X - thisEntityBounds.X;
            if (collisionX != 0)
            {
                collisionX = collisionX > 0 ? 1 : -1;
            }

            var collisionY = nearbyEntityBounds.Y - thisEntityBounds.Y;
            if (collisionY != 0)
            {
                collisionY = collisionY > 0 ? 1 : -1;
            }

            return new Vector2(collisionX, collisionY);
        }
    }
}
