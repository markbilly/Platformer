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
            var testSpriteComponent = new SpriteComponent("test/walk");
            var testRigidBodyComponent = new RigidBodyComponent();

            return new Entity(
                new Vector2(10, 10),
                new Point(16, 16),
                testSpriteComponent,
                testRigidBodyComponent);
        }
    }
}
