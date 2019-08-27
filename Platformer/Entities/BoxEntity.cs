using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Platformer.Core;
using Platformer.Graphics;
using Platformer.Graphics.Components;
using Platformer.Physics.Components;

namespace Platformer.Entities
{
    public class BoxEntity : Entity
    {
        private static readonly Point BOX_SIZE = new Point(16, 16);

        public BoxEntity()
        {
            AddComponent<SizeComponent>();
            AddComponent<PositionComponent>();
            AddComponent<VelocityComponent>();
            AddComponent<ApplyForceComponent>();
            AddComponent<CollisionComponent>();
            AddComponent<RigidBodyComponent>();
            AddComponent<SpriteComponent>();

            GetComponent<SpriteComponent>().Spritesheet = "test/box";
            GetComponent<SizeComponent>().Size = BOX_SIZE;
            GetComponent<CollisionComponent>().EntityType = this.GetType();
        }
    }
}
