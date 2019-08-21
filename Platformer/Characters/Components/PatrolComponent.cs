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
    public class PatrolComponent : Component
    {
        private readonly CollisionComponent _collisionComponent;

        private bool _patrolling;
        private int _patrolSpeed;
        private int _patrolDirection;

        public PatrolComponent(CollisionComponent collisionComponent) : base(ComponentType.Input)
        {
            _collisionComponent = collisionComponent;
        }

        public void StartPatrol(Entity entity)
        {
            _patrolling = true;
            _patrolSpeed = 1;
            _patrolDirection = 1;
        }

        public override void Update(Entity entity)
        {
            var xCollision = _collisionComponent.GetCollision(IsRelevantCollision);

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

        private bool IsRelevantCollision(Collision collision)
        {
            // Note: we only care about x-direction collision with non-player entities
            return Math.Abs(collision.Vector.X) < Math.Abs(collision.Vector.Y) && collision.EntityType != typeof(PlayerEntity);
        }
    }
}
