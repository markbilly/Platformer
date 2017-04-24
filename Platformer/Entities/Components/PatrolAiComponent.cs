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
        private PositionComponent _positionComponent;
        private MovementComponent _movementComponent;

        public PatrolAiComponent(
            PositionComponent positionComponent,
            MovementComponent movementComponent)
        {
            _positionComponent = positionComponent;
            _movementComponent = movementComponent;
        }

        public void StartPatrol()
        {
            _movementComponent.StartMove();
        }

        public void Draw()
        {
            return;
        }

        public void Update()
        {
            if (_positionComponent.Position.X > Constants.Game.Width ||
                _positionComponent.Position.X < 0)
            {
                _movementComponent.ChangeDirection();
            }
        }
    }
}
