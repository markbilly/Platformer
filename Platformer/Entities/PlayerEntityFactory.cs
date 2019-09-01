using Microsoft.Xna.Framework;
using Platformer.Core;
using Platformer.GameLogic.Components;
using Platformer.Graphics;
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
    public class PlayerEntityFactory : EntityFactory
    {
        private static readonly Point PLAYER_SIZE = new Point(16, 32);

        public PlayerEntityFactory() : base(new[]
            {
                typeof(SizeComponent),
                typeof(PositionComponent),
                typeof(VelocityComponent),
                typeof(AnimateComponent),
                typeof(ApplyForceComponent),
                typeof(CollisionComponent),
                typeof(RigidBodyComponent),
                typeof(PlayerInputComponent),
                typeof(HumanoidBehaviourComponent),
                typeof(SpriteComponent)
            })
        { }

        public override Entity Build()
        {
            var entity = base.Build();

            entity.GetComponent<RigidBodyComponent>().SetEntityTypeExclusions(new HashSet<Type> { typeof(GuardEntityFactory) });
            entity.GetComponent<SizeComponent>().Size = PLAYER_SIZE;
            entity.GetComponent<SpriteComponent>().Spritesheet = "test/walk";
            entity.GetComponent<CollisionComponent>().EntityType = this.GetType();

            return entity;
        }
    }
}
