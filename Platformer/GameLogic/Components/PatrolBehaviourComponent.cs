using Microsoft.Xna.Framework;
using Platformer.Entities;
using Platformer.Physics;
using Platformer.Physics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.GameLogic.Components
{
    public class PatrolBehaviourComponent : IGameLogicComponent
    {
        private readonly CollisionComponent _collisionComponent;
        private readonly VelocityComponent _velocityComponent;

        private bool _patrolling;
        private int _patrolSpeed;
        private int _patrolDirection;

        public PatrolBehaviourComponent(CollisionComponent collisionComponent, VelocityComponent velocityComponent)
        {
            _collisionComponent = collisionComponent;
            _velocityComponent = velocityComponent;
        }

        public void StartPatrolling()
        {
            _patrolling = true;
            _patrolSpeed = 1;
            _patrolDirection = 1;
        }

        public void Update()
        {
            var xCollision = _collisionComponent.GetCollision(IsRelevantCollision);

            if (xCollision.HasValue)
            {
                var collisionDirection = xCollision.Value.Vector.X > 0 ? 1 : -1;
                _patrolDirection = collisionDirection * -1;
            }

            if (_patrolling)
            {
                _velocityComponent.Velocity = new Vector2(_patrolDirection * _patrolSpeed, _velocityComponent.Velocity.Y);
            }
        }

        private bool IsRelevantCollision(Collision collision)
        {
            // Note: we only care about x-direction collision with non-player entities
            return Math.Abs(collision.Vector.X) < Math.Abs(collision.Vector.Y) && collision.EntityType != typeof(PlayerEntity);
        }
    }
}
