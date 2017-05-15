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
        private CollisionComponent _collisionComponent;
        private HashSet<Type> _entityTypeExclusions;

        public RigidBodyComponent(Entity entity)
        {
            _collisionComponent = entity.GetComponent<CollisionComponent>();
            _entityTypeExclusions = new HashSet<Type>();
        }

        public int Order { get { return 30; } }

        public void SetEntityTypeExclusions(HashSet<Type> entityTypes)
        {
            _entityTypeExclusions = entityTypes;
        }

        public void Update(Entity entity)
        {
            // resolve all collisions
            foreach (var collision in _collisionComponent.GetCollisions())
            {
                ResolveCollision(entity, collision);
            }
        }

        private void ResolveCollision(Entity entity, Collision collision)
        {
            // do not react to entity types that are excluded
            if (_entityTypeExclusions.Contains(collision.EntityType))
            {
                return;
            }

            ClipEntity(entity, collision.Vector);

            entity.Velocity = new Vector2(
                collision.Vector.X != 0 ? 0 : entity.Velocity.X,
                collision.Vector.Y != 0 ? 0 : entity.Velocity.Y);
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
