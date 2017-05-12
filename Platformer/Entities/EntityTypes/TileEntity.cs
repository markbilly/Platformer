using Microsoft.Xna.Framework;
using Platformer.Entities.Components;
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
            var spriteComponent = new SpriteGraphicsComponent("test/grass");
            var collisionComponent = new AABBCollisionComponent();

            AddComponent(spriteComponent);
            AddComponent(collisionComponent);
            AddComponent(new RigidBodyComponent(collisionComponent));
        }
    }
}
