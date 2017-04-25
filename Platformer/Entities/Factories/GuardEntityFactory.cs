using Microsoft.Xna.Framework;
using Platformer.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Factories
{
    public class GuardEntityFactory : IEntityFactory
    {
        public Entity Build()
        {
            var bounds = new Point(16, 32);

            var testPositionComponent = new PositionComponent(new Vector2(10, 10));
            var testSpriteComponent = new SpriteComponent(testPositionComponent, bounds, "test/walk");

            var testAnimateComponent = new AnimateComponent(testSpriteComponent, new AnimationParameters
            {
                StartFrame = 0,
                EndFrame = 8,
                SpritesheetRow = 0,
                FramesPerSecond = 4,
            });

            var testMovementComponent = new MovementComponent(testPositionComponent, testAnimateComponent, testSpriteComponent, 1);
            var testRigidBodyComponent = new RigidBodyComponent(testPositionComponent, bounds);
            var testPatrolAiComponent = new PatrolAiComponent(testMovementComponent, testRigidBodyComponent);

            return new Entity
            {
                testPositionComponent,
                testMovementComponent,
                testSpriteComponent,
                testAnimateComponent,
                testRigidBodyComponent,
                testPatrolAiComponent
            };
        }
    }
}
