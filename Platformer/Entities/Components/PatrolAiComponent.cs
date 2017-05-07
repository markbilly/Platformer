using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Components
{
    public class PatrolAiComponent : IComponent
    {
        private RigidBodyComponent _rigidBodyComponent;
        private MovementComponent _movementComponent;

        public PatrolAiComponent(
            MovementComponent movementComponent,
            RigidBodyComponent rigidBodyComponent)
        {
            _movementComponent = movementComponent;
            _rigidBodyComponent = rigidBodyComponent;
        }

        public void StartPatrol()
        {
            _movementComponent.StartMove();
        }

        public void Update()
        {
            if (_rigidBodyComponent.Collision != Vector2.Zero)
            {
                _movementComponent.Velocity = new Vector2(_rigidBodyComponent.Collision.X * -1, 0);
            }
        }
    }
}
