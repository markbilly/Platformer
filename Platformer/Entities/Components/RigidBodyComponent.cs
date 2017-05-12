using Microsoft.Xna.Framework;
using Platformer.Utilities;
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
        private IList<Entity> _nearbyEntities;
        private IList<Vector2> _collisions;

        public RigidBodyComponent()
        {
            _collisions = new List<Vector2>();
        }

        public Vector2 GetCollision(Func<Vector2, bool> predicate)
        {
            for (var i = 0; i < _collisions.Count; i++)
            {
                if (predicate(_collisions[i]))
                {
                    // return collision value and remove from list
                    var collision = _collisions[i];
                    _collisions.RemoveAt(i);
                    return collision;
                }
            }

            return Vector2.Zero;
        }

        public void AddCollision(Vector2 collision)
        {
            _collisions.Add(collision);
        }

        public void SetNearbyEntities(IList<Entity> entities)
        {
            _nearbyEntities = entities;
        }

        public void Update(Entity entity)
        {
            // clear collisions as they were all resolved last update
            _collisions.Clear();
             
            // detect and add new collisions for this update
            foreach (var nearbyEntity in _nearbyEntities)
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
                    }
                }
            }
        }

        private Vector2 GetCollisionVector(RectangleF thisEntityBounds, RectangleF nearbyEntityBounds)
        {
            var entityWidthOffset = (thisEntityBounds.X > nearbyEntityBounds.X ? -1 : 1) * thisEntityBounds.Width;
            var entityHeightOffset = (thisEntityBounds.Y > nearbyEntityBounds.Y ? -1 : 1) * thisEntityBounds.Height;

            var collisionX = thisEntityBounds.X + entityWidthOffset - nearbyEntityBounds.X;
            var collisionY = thisEntityBounds.Y + entityHeightOffset - nearbyEntityBounds.Y;

            return new Vector2(collisionX, collisionY);
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
            // do this by checking the collision vector
            var absoluteCollisionX = Math.Abs(collision.X);
            var absoluteCollisionY = Math.Abs(collision.Y);
            if (absoluteCollisionX > absoluteCollisionY)
            {
                // if y penetration is smallest "fix" y
                entity.Position = new Vector2(entity.Position.X, newPositionY);
            }
            else if (absoluteCollisionY > absoluteCollisionX)
            {
                // if x penetration is smallest "fix" x
                entity.Position = new Vector2(newPositionX, entity.Position.Y);
            }
            else
            {
                // if penetration is equal "fix" x and y
                entity.Position = new Vector2(newPositionX, newPositionY);
            }
        }
    }
}
