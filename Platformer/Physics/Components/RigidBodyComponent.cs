using Microsoft.Xna.Framework;
using Platformer.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Physics.Components
{
    public class RigidBodyComponent : IComponent
    {
        private CollisionComponent _collisionComponent;
        private HashSet<Type> _entityTypeExclusions;

        public RigidBodyComponent()
        {
            _entityTypeExclusions = new HashSet<Type>();
        }

        public ComponentType Type
        {
            get { return ComponentType.Physics; }
        }

        public void SetEntityTypeExclusions(HashSet<Type> entityTypes)
        {
            _entityTypeExclusions = entityTypes;
        }

        public void Update(Entity entity)
        {
            GatherDependencies(entity);

            var resolutions = new List<CollisionResolution>();

            // resolve all collisions
            var collisions = _collisionComponent.GetCollisions();
            for (var i = collisions.Count() - 1; i <= 0; i--)
            {
                var collision = collisions.ElementAt(i);
                var resolution = CalculateResolution(entity, collision);

                // only resolve one collision in each direction
                if (!resolutions.Any(x => x.VelocityNormalised == resolution.VelocityNormalised))
                {
                    resolutions.Add(resolution);
                    resolution.ApplyToEntity(entity);
                }
            }
        }

        private CollisionResolution CalculateResolution(Entity entity, Collision collision)
        {
            // do not react to entity types that are excluded
            if (_entityTypeExclusions.Contains(collision.EntityType))
            {
                return new CollisionResolution(entity.Position, entity.Velocity);
            }

            // do not clip if entity is not moving
            if (entity.Velocity == Vector2.Zero)
            {
                return new CollisionResolution(entity.Position, entity.Velocity);
            }

            // work out possible new x and y positions after clipping
            var newPositionX = entity.Position.X - collision.Vector.X;
            var newPositionY = entity.Position.Y - collision.Vector.Y;

            // work out whether collision is vertical or horizontal
            // do this by checking the collision vector
            var absoluteCollisionX = Math.Abs(collision.Vector.X);
            var absoluteCollisionY = Math.Abs(collision.Vector.Y);
            if (absoluteCollisionX > absoluteCollisionY)
            {
                // if y penetration is smallest "fix" y
                return new CollisionResolution(
                    new Vector2(entity.Position.X, newPositionY),
                    new Vector2(entity.Velocity.X, 0));
            }
            else if (absoluteCollisionY > absoluteCollisionX)
            {
                // if x penetration is smallest "fix" x
                return new CollisionResolution(
                    new Vector2(newPositionX, entity.Position.Y),
                    new Vector2(0, entity.Velocity.Y));
            }
            else
            {
                // if penetration is equal "fix" x and y
                return new CollisionResolution(
                    new Vector2(newPositionX, newPositionY),
                    new Vector2(0, 0));
            }
        }

        private void GatherDependencies(Entity entity)
        {
            if (_collisionComponent == null)
            {
                _collisionComponent = entity.GetComponent<CollisionComponent>();
            }
        }

        private class CollisionResolution
        {
            public CollisionResolution(Vector2 position, Vector2 velocity)
            {
                Position = position;
                Velocity = velocity;

                VelocityNormalised = new Vector2(Velocity.X, Velocity.Y);
                VelocityNormalised.Normalize();
            }

            public Vector2 Position { get; set; }
            public Vector2 Velocity { get; set; }
            public Vector2 VelocityNormalised { get; set; }

            public void ApplyToEntity(Entity entity)
            {
                entity.Position = Position;
                entity.Velocity = Velocity;
            }
        }
    }
}
