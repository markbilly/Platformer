using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Components
{
    public class RigidBodyComponent : IComponent
    {
        private PositionComponent _positionComponent;
        private MovementComponent _movementComponent;

        public RigidBodyComponent(
            PositionComponent positionComponent,
            MovementComponent movementComponent)
        {
            _positionComponent = positionComponent;
            _movementComponent = movementComponent;
        }

        public Vector2 Collision { get; set; }

        public void Draw()
        {
            return;
        }

        public void Update()
        {
            // Entity is solid
            if (Collision != Vector2.Zero)
            {
                _movementComponent.StopMove();
            }
        }
    }
}
