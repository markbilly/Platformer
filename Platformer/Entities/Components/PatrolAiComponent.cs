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
        private SpriteComponent _spriteComponent;

        public PatrolAiComponent(
            PositionComponent positionComponent,
            MovementComponent movementComponent,
            SpriteComponent spriteCompoonent)
        {
            _positionComponent = positionComponent;
            _movementComponent = movementComponent;
            _spriteComponent = spriteCompoonent;
        }

        public void Draw()
        {
            return;
        }

        public void Update()
        {
            // too far right
            if (_positionComponent.Position.X > Constants.GameWidth)
            {
                // walk left
                _movementComponent.Velocity = new Vector2(_movementComponent.Velocity.X * -1, _movementComponent.Velocity.Y);
                _spriteComponent.SpritesheetRow = 1;
            }

            // too far left
            if (_positionComponent.Position.X < 0)
            {
                // walk right
                _movementComponent.Velocity = new Vector2(_movementComponent.Velocity.X * -1, _movementComponent.Velocity.Y);
                _spriteComponent.SpritesheetRow = 0;
            }
        }
    }
}
