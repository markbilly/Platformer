using Microsoft.Xna.Framework;
using Platformer.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Factories
{
    public class PlayerEntityFactory : IEntityFactory
    {
        public Entity Build()
        {
            var positionComponent = new PositionComponent(new Vector2(10, 60));
            var spriteComponent = new SpriteComponent(positionComponent, new Point(16, 32), "test/walk");

            var animateComponent = new AnimateComponent(spriteComponent, new AnimationParameters
            {
                StartFrame = 0,
                EndFrame = 8,
                SpritesheetRow = 0,
                FramesPerSecond = 4,
            });

            var movementComponent = new MovementComponent(positionComponent, animateComponent, spriteComponent, 1);

            return new Entity
            {
                positionComponent,
                movementComponent,
                spriteComponent,
                animateComponent
            };
        }
    }
}
