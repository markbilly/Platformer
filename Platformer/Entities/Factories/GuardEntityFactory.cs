﻿using Microsoft.Xna.Framework;
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
            var testSpriteComponent = new SpriteComponent("test/walk");

            var testAnimateComponent = new AnimateComponent(testSpriteComponent, new AnimationParameters
            {
                StartFrame = 0,
                EndFrame = 8,
                FramesPerSecond = 4,
            });

            var testMovementComponent = new MovementComponent(testAnimateComponent, 1);
            var testRigidBodyComponent = new RigidBodyComponent();
            var testPatrolAiComponent = new PatrolAiComponent(testMovementComponent, testRigidBodyComponent);

            return new Entity(
                new Vector2(10, 10),
                new Point(16, 32),
                testMovementComponent,
                testSpriteComponent,
                testAnimateComponent,
                testRigidBodyComponent,
                testPatrolAiComponent);
        }
    }
}
