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
            var spriteComponent = new SpriteComponent("test/walk");

            var animateComponent = new AnimateComponent(spriteComponent, new AnimationParameters
            {
                StartFrame = 0,
                EndFrame = 8,
                FramesPerSecond = 4,
            });

            var movementComponent = new MovementAnimationComponent(animateComponent);

            return new Entity(
                new Point(16, 32),
                movementComponent,
                spriteComponent,
                animateComponent,
                new MovementStateComponent(),
                new ForceComponent(),
                new RigidBodyComponent());
        }
    }
}
