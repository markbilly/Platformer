using Microsoft.Xna.Framework;
using Platformer.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Factories
{
    public class TestEntityFactory : IEntityFactory
    {
        public Entity Build()
        {
            var testPositionComponent = new PositionComponent(new Vector2(10, 10));
            var testMovementComponent = new MovementComponent(testPositionComponent);
            var testSpriteComponent = new SpriteComponent(testPositionComponent, new Point(16, 32), "test/walk");
            var testAnimateComponent = new AnimateComponent(testSpriteComponent, 0, 8, 4);

            testMovementComponent.Velocity = new Vector2(0, 0);

            return new Entity
            {
                testPositionComponent,
                testMovementComponent,
                testSpriteComponent,
                testAnimateComponent
            };
        }
    }
}
