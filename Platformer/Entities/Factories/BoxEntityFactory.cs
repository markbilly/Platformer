using Microsoft.Xna.Framework;
using Platformer.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Factories
{
    public class BoxEntityFactory : IEntityFactory
    {
        public Entity Build()
        {
            var bounds = new Point(16, 16);

            var testPositionComponent = new PositionComponent(new Vector2(10, 10));
            var testSpriteComponent = new SpriteComponent(testPositionComponent, bounds, "test/walk");
            var testRigidBodyComponent = new RigidBodyComponent(testPositionComponent, bounds);

            return new Entity
            {
                testPositionComponent,
                testSpriteComponent,
                testRigidBodyComponent,
            };
        }
    }
}
