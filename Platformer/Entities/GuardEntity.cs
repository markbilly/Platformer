using Microsoft.Xna.Framework;
using Platformer.Characters.Components;
using Platformer.Core;
using Platformer.Graphics.Components;
using Platformer.Graphics.EntityRenderers;
using Platformer.Physics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities
{
    public class GuardEntity : Entity
    {
        private static readonly Point GUARD_SIZE = new Point(16, 32);

        public GuardEntity() : base(GUARD_SIZE)
        {
            AddComponent<AnimateComponent>();
            AddComponent<ApplyForceComponent>();
            AddComponent<CollisionComponent>();
            AddComponent<RigidBodyComponent>();
            AddComponent<PatrolComponent>();
            AddComponent<HumanoidStateComponent>();
            AddComponent<HumanoidAnimationComponent>();
            AddComponent<SpriteComponent>();

            // TODO: Do not parameterise component classes - use inheritance
            GetComponent<RigidBodyComponent>().SetEntityTypeExclusions(new HashSet<Type> { typeof(PlayerEntity) });

            var spriteComponent = GetComponent<SpriteComponent>();
            spriteComponent.Spritesheet = "test/walk";

            AddRenderer(new SpriteEntityRenderer(spriteComponent));
        }
    }
}
