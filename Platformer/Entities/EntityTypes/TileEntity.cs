using Microsoft.Xna.Framework;
using Platformer.Entities.Components;
using Platformer.Graphics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.EntityTypes
{
    public class TileEntity : Entity
    {
        private static readonly Point TILE_SIZE = new Point(16, 16);

        public TileEntity() : base(TILE_SIZE)
        {
            AddComponent(new SpriteGraphicsComponent("test/grass"));
            AddComponent(new CollisionComponent());
            AddComponent(new RigidBodyComponent(this));
        }
    }
}
