using Microsoft.Xna.Framework;
using Platformer.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.EntityTypes
{
    public class PlayerEntity : Entity
    {
        private static readonly Point PLAYER_SIZE = new Point(16, 32);

        public PlayerEntity() : base(PLAYER_SIZE)
        {
            var spriteComponent = new SpriteGraphicsComponent("test/walk");

            var animateComponent = new AnimateComponent(spriteComponent, new AnimationParameters
            {
                StartFrame = 0,
                EndFrame = 8,
                FramesPerSecond = 4,
            });

            var movementAnimationComponent = new MovementAnimationComponent(animateComponent);
            var collisionComponent = new CollisionComponent();

            var rigidBodyComponent = new RigidBodyComponent(collisionComponent);
            rigidBodyComponent.SetEntityTypeExclusions(new HashSet<Type>
            {
                typeof(GuardEntity)
            });

            var applyForceComponent = new ApplyForceComponent();

            AddComponent(movementAnimationComponent);
            AddComponent(spriteComponent);
            AddComponent(animateComponent);
            AddComponent(applyForceComponent);
            AddComponent(collisionComponent);
            AddComponent(rigidBodyComponent);
            AddComponent(new MovementStateComponent());
        }
    }
}
