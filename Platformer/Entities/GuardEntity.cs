using Microsoft.Xna.Framework;
using Platformer.Core;
using Platformer.GameLogic.Components;
using Platformer.Graphics;
using Platformer.Graphics.Components;
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

        public GuardEntity()
        {
            AddComponent<SizeComponent>();
            AddComponent<AnimateComponent>();
            AddComponent<ApplyForceComponent>();
            AddComponent<CollisionComponent>();
            AddComponent<RigidBodyComponent>();
            AddComponent<PatrolBehaviourComponent>();
            AddComponent<HumanoidBehaviourComponent>();
            AddComponent<SpriteComponent>();

            GetComponent<RigidBodyComponent>().SetEntityTypeExclusions(new HashSet<Type> { typeof(PlayerEntity) });
            GetComponent<SpriteComponent>().Spritesheet = "test/walk";
            GetComponent<SizeComponent>().Size = GUARD_SIZE;
            GetComponent<CollisionComponent>().EntityType = this.GetType();
        }
    }
}
