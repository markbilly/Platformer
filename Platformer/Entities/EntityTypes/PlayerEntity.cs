using Microsoft.Xna.Framework;
using Platformer.Entities.Components;
using Platformer.Graphics.Components;
using Platformer.Input.Components;
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
            AddComponent(new SpriteGraphicsComponent("test/walk"));
            AddComponent(new AnimateComponent(this, AnimationParameters.Default()));
            AddComponent(new ApplyForceComponent());
            AddComponent(new CollisionComponent());
            AddComponent(new RigidBodyComponent(this));
            AddComponent(new PlayerInputComponent());
            AddComponent(new HumanoidStateComponent());
            AddComponent(new HumanoidAnimationComponent(this));

            GetComponent<RigidBodyComponent>()
                .SetEntityTypeExclusions(new HashSet<Type>
                {
                    typeof(GuardEntity)
                });
        }
    }
}
