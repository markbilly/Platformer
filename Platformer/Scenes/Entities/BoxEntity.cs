using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Platformer.Core;
using Platformer.Graphics.Components;
using Platformer.Physics.Components;

namespace Platformer.Scenes.Entities
{
    public class BoxEntity : Entity
    {
        private static readonly Point BOX_SIZE = new Point(16, 16);

        public BoxEntity() : base(BOX_SIZE)
        {
            AddComponent(new SpriteGraphicsComponent("test/box"));
            AddComponent(new ApplyForceComponent());
            AddComponent(new CollisionComponent());
            AddComponent(new RigidBodyComponent());
        }
    }
}
