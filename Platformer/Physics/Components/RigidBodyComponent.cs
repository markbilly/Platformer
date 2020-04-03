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
    public class RigidBodyComponent : IPhysicsComponent
    {
        private readonly VelocityComponent _velocityComponent;
        private readonly PositionComponent _positionComponent;

        public RigidBodyComponent(CollisionComponent collisionComponent, PositionComponent positionComponent, VelocityComponent velocityComponent)
        {
            _velocityComponent = velocityComponent;
            _positionComponent = positionComponent;
            CollisionComponent = collisionComponent;
        }

        public CollisionComponent CollisionComponent { get; }

        public void Update()
        {
            var resolutions = new List<CollisionResolution>();

            // resolve all collisions
            var collisions = CollisionComponent.GetCollisions();
            for (var i = collisions.Count() - 1; i >= 0; i--)
            {
                var collision = collisions.ElementAt(i);
                var resolution = CalculateResolution(collision);

                // only resolve one collision in each direction
                if (!resolutions.Any(x => x.VelocityNormalised == resolution.VelocityNormalised))
                {
                    resolutions.Add(resolution);
                    resolution.Apply(_positionComponent, _velocityComponent);
                }
            }
        }

        private CollisionResolution CalculateResolution(Collision collision)
        {
            // ignore collisions between player and non player
            if (collision.CollisionProfile == CollisionProfiles.NonPlayerCharacter || collision.CollisionProfile == CollisionProfiles.PlayerCharacter)
            {
                if (CollisionComponent.CollisionProfile == CollisionProfiles.NonPlayerCharacter || CollisionComponent.CollisionProfile == CollisionProfiles.PlayerCharacter)
                {
                    return DoNothingCollisionResolution();
                }
            }

            // do not clip if entity is not moving
            if (_velocityComponent.Velocity == Vector2.Zero)
            {
                return DoNothingCollisionResolution();
            }

            // work out possible new x and y positions after clipping
            var newPositionX = _positionComponent.Position.X - collision.Vector.X;
            var newPositionY = _positionComponent.Position.Y - collision.Vector.Y;

            // work out whether collision is vertical or horizontal
            // do this by checking the collision vector
            var absoluteCollisionX = Math.Abs(collision.Vector.X);
            var absoluteCollisionY = Math.Abs(collision.Vector.Y);
            if (absoluteCollisionX > absoluteCollisionY)
            {
                // if y penetration is smallest "fix" y
                return new CollisionResolution(
                    new Vector2(_positionComponent.Position.X, newPositionY),
                    new Vector2(_velocityComponent.Velocity.X, 0));
            }
            else if (absoluteCollisionY > absoluteCollisionX)
            {
                // if x penetration is smallest "fix" x
                return new CollisionResolution(
                    new Vector2(newPositionX, _positionComponent.Position.Y),
                    new Vector2(0, _velocityComponent.Velocity.Y));
            }
            else
            {
                // if penetration is equal "fix" x and y
                return new CollisionResolution(
                    new Vector2(newPositionX, newPositionY),
                    new Vector2(0, 0));
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

            public void Apply(PositionComponent positionComponent, VelocityComponent velocityComponent)
            {
                positionComponent.Position = Position;
                velocityComponent.Velocity = Velocity;
            }
        }

        private CollisionResolution DoNothingCollisionResolution()
        {
            return new CollisionResolution(_positionComponent.Position, _velocityComponent.Velocity);
        }
    }
}
