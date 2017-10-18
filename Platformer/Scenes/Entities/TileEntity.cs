using Microsoft.Xna.Framework;
using Platformer.Core;
using Platformer.Graphics.Components;
using Platformer.Physics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Scenes.Entities
{
    public class TileEntity : Entity
    {
        private static readonly Point TILE_SIZE = new Point(16, 16);

        public TileEntity() : base(TILE_SIZE)
        {
            AddComponent(new SpriteGraphicsComponent("test/grass"));
            AddComponent(new CollisionComponent());
            AddComponent(new RigidBodyComponent());
        }
    }
}
