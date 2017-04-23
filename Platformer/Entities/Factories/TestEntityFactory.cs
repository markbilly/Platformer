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
            var testSpriteComponent = new SpriteComponent(testPositionComponent, new Point(68, 70), "test/bit");

            testMovementComponent.Velocity = new Vector2(1, 1);

            return new Entity
            {
                testPositionComponent,
                testMovementComponent,
                testSpriteComponent
            };
        }
    }
}
