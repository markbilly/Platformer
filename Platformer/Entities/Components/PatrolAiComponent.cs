using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Components
{
    public class PatrolAiComponent : IComponent
    {
        private AABBCollisionComponent _collisionComponent;

        private bool _patrolling;
        private int _patrolSpeed;
        private int _patrolDirection;

        public PatrolAiComponent(AABBCollisionComponent collisionComponent)
        {
            _collisionComponent = collisionComponent;
        }

        public void StartPatrol(Entity entity)
        {
            _patrolling = true;
            _patrolSpeed = 1;
            _patrolDirection = 1;
        }

        public void Update(Entity entity)
        {
            // only care about x-direction collision
            var xCollision = _collisionComponent.GetCollision(c => Math.Abs(c.Vector.X) < Math.Abs(c.Vector.Y));

            if (xCollision.HasValue)
            {
                var collisionDirection = xCollision.Value.Vector.X > 0 ? 1 : -1;
                _patrolDirection = collisionDirection * -1;
            }

            if (_patrolling)
            {
                entity.Velocity = new Vector2(_patrolDirection * _patrolSpeed, entity.Velocity.Y);
            }
        }
    }
}
