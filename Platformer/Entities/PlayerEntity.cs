using Microsoft.Xna.Framework;
using Platformer.Animation.Components;
using Platformer.Characters.Components;
using Platformer.Core;
using Platformer.Graphics.Components;
using Platformer.Input.Components;
using Platformer.Physics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities
{
    public class PlayerEntity : Entity
    {
        private static readonly Point PLAYER_SIZE = new Point(16, 32);

        public PlayerEntity() : base(PLAYER_SIZE)
        {
            AddComponent<SpriteGraphicsComponent>();
            AddComponent<AnimateComponent>();
            AddComponent<ApplyForceComponent>();
            AddComponent<CollisionComponent>();
            AddComponent<RigidBodyComponent>();
            AddComponent<PlayerInputComponent>();
            AddComponent<HumanoidStateComponent>();
            AddComponent<HumanoidAnimationComponent>();

            // TODO: Do not parameterise component classes - use inheritance
            GetGraphicsComponent<SpriteGraphicsComponent>().Spritesheet = "test/walk";
            GetComponent<RigidBodyComponent>().SetEntityTypeExclusions(new HashSet<Type> { typeof(GuardEntity) });
        }
    }
}
