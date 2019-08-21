using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Platformer.Core;
using Platformer.Graphics.Components;
using Platformer.Graphics.GraphicsComponents;
using Platformer.Physics.Components;

namespace Platformer.Entities
{
    public class BoxEntity : Entity
    {
        private static readonly Point BOX_SIZE = new Point(16, 16);

        public BoxEntity() : base(BOX_SIZE)
        {
            AddComponent<SpriteGraphicsComponent>();
            AddComponent<ApplyForceComponent>();
            AddComponent<CollisionComponent>();
            AddComponent<RigidBodyComponent>();

            // TODO: Do not parameterise component classes - use inheritance
            GetGraphicsComponent<SpriteGraphicsComponent>().Spritesheet = "test/box";
        }
    }
}
