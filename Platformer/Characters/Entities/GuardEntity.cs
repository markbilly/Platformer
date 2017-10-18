using Microsoft.Xna.Framework;
using Platformer.Animation.Components;
using Platformer.Characters.Components;
using Platformer.Core;
using Platformer.Graphics.Components;
using Platformer.Physics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Characters.Entities
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
            AddComponent(new PatrolComponent());
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
