using Microsoft.Xna.Framework;
using Platformer.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.EntityTypes
{
    public class GuardEntity : Entity
    {
        private static readonly Point GUARD_SIZE = new Point(16, 32);

        public GuardEntity() : base(GUARD_SIZE)
        {
            var spriteComponent = new SpriteGraphicsComponent("test/walk");

            var animateComponent = new AnimateComponent(spriteComponent, new AnimationParameters
            {
                StartFrame = 0,
                EndFrame = 8,
                FramesPerSecond = 4,
            });

            var movementComponent = new MovementAnimationComponent(animateComponent);
            var collisionComponent = new CollisionComponent();

            var rigidBodyComponent = new RigidBodyComponent(collisionComponent);
            rigidBodyComponent.SetEntityTypeExclusions(new HashSet<Type>
            {
                typeof(PlayerEntity)
            });

            var patrolAiComponent = new PatrolAIComponent(collisionComponent);

            AddComponent(movementComponent);
            AddComponent(spriteComponent);
            AddComponent(animateComponent);
            AddComponent(new ApplyForceComponent());
            AddComponent(collisionComponent);
            AddComponent(rigidBodyComponent);
            AddComponent(patrolAiComponent);
        }
    }
}
