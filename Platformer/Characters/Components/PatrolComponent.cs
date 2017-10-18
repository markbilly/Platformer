using Microsoft.Xna.Framework;
using Platformer.Characters.Entities;
using Platformer.Core;
using Platformer.Physics;
using Platformer.Physics.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Characters.Components
{
    public class PatrolComponent : IComponent
    {
        private CollisionComponent _collisionComponent;

        private bool _patrolling;
        private int _patrolSpeed;
        private int _patrolDirection;

        public ComponentType Type
        {
            get { return ComponentType.Input; }
        }

        public void StartPatrol(Entity entity)
        {
            _patrolling = true;
            _patrolSpeed = 1;
            _patrolDirection = 1;
        }

        public void Update(Entity entity)
        {
            GatherDependencies(entity);

            // only care about x-direction collision with non-player entities
            Func<Collision, bool> releventCollision = c => 
                Math.Abs(c.Vector.X) < Math.Abs(c.Vector.Y) && 
                c.EntityType != typeof(PlayerEntity);

            var xCollision = _collisionComponent.GetCollision(releventCollision);

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

        private void GatherDependencies(Entity entity)
        {
            if (_collisionComponent == null)
            {
                _collisionComponent = entity.GetComponent<CollisionComponent>();
            }
        }
    }
}
