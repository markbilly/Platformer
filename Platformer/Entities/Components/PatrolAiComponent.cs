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
        private RigidBodyComponent _rigidBodyComponent;

        private bool _patrolling;
        private int _patrolSpeed;
        private int _patrolDirection;

        public PatrolAiComponent(RigidBodyComponent rigidBodyComponent)
        {
            _rigidBodyComponent = rigidBodyComponent;
        }

        public void StartPatrol(Entity entity)
        {
            _patrolling = true;
            _patrolSpeed = 1;
            _patrolDirection = 1;
        }

        public void Update(Entity entity)
        {
            var collision = _rigidBodyComponent.GetLatestCollision();
            if (collision.X < collision.Y) // only care about x-direction collisions
            {
                var collisionDirection = collision.X > 0 ? 1 : -1;
                _patrolDirection = collisionDirection * -1;
            }

            if (_patrolling)
            {
                entity.Velocity = new Vector2(_patrolDirection * _patrolSpeed, entity.Velocity.Y);
            }
        }
    }
}
