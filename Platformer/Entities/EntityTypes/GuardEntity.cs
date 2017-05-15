using Microsoft.Xna.Framework;
using Platformer.AI.Components;
using Platformer.Entities.Components;
using Platformer.Graphics.Components;
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
            AddComponent(new SpriteGraphicsComponent("test/walk"));
            AddComponent(new AnimateComponent(AnimationParameters.Default()));
            AddComponent(new ApplyForceComponent());
            AddComponent(new CollisionComponent());
            AddComponent(new RigidBodyComponent());
            AddComponent(new PatrolAIComponent());
            AddComponent(new HumanoidStateComponent());
            AddComponent(new HumanoidAnimationComponent());
            
            GetComponent<RigidBodyComponent>()
                .SetEntityTypeExclusions(new HashSet<Type>
                {
                    typeof(PlayerEntity)
                });
        }
    }
}
