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
    public class PlayerEntity : Entity
    {
        private static readonly Point PLAYER_SIZE = new Point(16, 32);

        public PlayerEntity()
        {
            AddComponent<SizeComponent>();
            AddComponent<AnimateComponent>();
            AddComponent<ApplyForceComponent>();
            AddComponent<CollisionComponent>();
            AddComponent<RigidBodyComponent>();
            AddComponent<PlayerInputComponent>();
            AddComponent<HumanoidBehaviourComponent>();
            AddComponent<SpriteComponent>();


            GetComponent<RigidBodyComponent>().SetEntityTypeExclusions(new HashSet<Type> { typeof(GuardEntity) });
            GetComponent<SizeComponent>().Size = PLAYER_SIZE;
            GetComponent<SpriteComponent>().Spritesheet = "test/walk";
            GetComponent<CollisionComponent>().EntityType = this.GetType();
        }
    }
}
