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
                    var thisEntityBounds = new RectangleF(entity.Position, entity.Size);
                    var nearbyEntityBounds = new RectangleF(nearbyEntity.Position, nearbyEntity.Size);

                    if (RectangleF.Intersects(thisEntityBounds, nearbyEntityBounds))
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
                //entity.Position = new Vector2(newPositionX, entity.Position.Y);
            }
            else
            {
                // if speeds equal "fix" x and y
                entity.Position = new Vector2(newPositionX, newPositionY);
            }
        }

        private Vector2 GetCollisionVector(RectangleF thisEntityBounds, RectangleF nearbyEntityBounds)
        {
            var collisionX = thisEntityBounds.X + thisEntityBounds.Width - nearbyEntityBounds.X;
            var collisionY = thisEntityBounds.Y + thisEntityBounds.Height - nearbyEntityBounds.Y;

            return new Vector2(collisionX, collisionY);
        }

        private struct RectangleF
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
}
