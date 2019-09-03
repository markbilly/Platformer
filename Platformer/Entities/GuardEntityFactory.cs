using Microsoft.Xna.Framework;
using Platformer.Core;
using Platformer.GameLogic.Components;
using Platformer.Graphics;
using Platformer.Graphics.Components;
using Platformer.Physics;
using Platformer.Physics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities
{
    public class GuardEntityFactory : EntityFactory
    {
        private static readonly Point GUARD_SIZE = new Point(16, 32);

        public GuardEntityFactory() : base(new[]
            {
                typeof(SizeComponent),
                typeof(PositionComponent),
                typeof(VelocityComponent),
                typeof(AnimateComponent),
                typeof(ApplyForceComponent),
                typeof(CollisionComponent),
                typeof(RigidBodyComponent),
                typeof(PatrolBehaviourComponent),
                typeof(HumanoidBehaviourComponent),
                typeof(SpriteComponent)
            })
        { }

        public override Entity Build()
        {
            var entity = base.Build();

            entity.GetComponent<SpriteComponent>().Spritesheet = "test/walk";
            entity.GetComponent<SizeComponent>().Size = GUARD_SIZE;
            entity.GetComponent<CollisionComponent>().CollisionProfile = CollisionProfiles.NonPlayer;

            return entity;
        }
    }
}
