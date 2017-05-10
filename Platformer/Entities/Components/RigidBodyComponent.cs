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
            ClipEntity(entity, collision);

            entity.Velocity = new Vector2(
                collision.X != 0 ? 0 : entity.Velocity.X,
                collision.Y != 0 ? 0 : entity.Velocity.Y);
        }

        private static void ClipEntity(Entity entity, Vector2 collision)
        {
            // do not clip if entity is not moving
            if (entity.Velocity == Vector2.Zero)
            {
                return;
            }

            // work out possible new x and y positions after clipping
            var newPositionX = entity.Position.X - collision.X;
            var newPositionY = entity.Position.Y - collision.Y;

            // work out whether collision is vertical or horizontal
            // do this by checking x and y speed
            var speedX = Math.Abs(entity.Velocity.X);
            var speedY = Math.Abs(entity.Velocity.Y);

            if (speedY > speedX)
            {
                // if speed in y direction is more "fix" y
                entity.Position = new Vector2(entity.Position.X, newPositionY);
            }
            else if (speedX > speedY)
            {
                // if speed in x direction is more "fix" x
                entity.Position = new Vector2(newPositionX, entity.Position.Y);
            }
            else
            {
                // if speeds equal "fix" x and y
                entity.Position = new Vector2(newPositionX, newPositionY);
            }
        }

        private Vector2 GetCollisionVector(Rectangle thisEntityBounds, Rectangle nearbyEntityBounds)
        {
            var collisionX = nearbyEntityBounds.X - thisEntityBounds.X;
            var collisionY = nearbyEntityBounds.Y - thisEntityBounds.Y;

            return new Vector2(collisionX, collisionY);
        }
    }
}
