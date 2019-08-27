using Microsoft.Xna.Framework;
using Platformer.Core;
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
    public class TileEntity : Entity
    {
        private static readonly Point TILE_SIZE = new Point(16, 16);

        public TileEntity()
        {
            AddComponent<SizeComponent>();
            AddComponent<CollisionComponent>();
            AddComponent<RigidBodyComponent>();
            AddComponent<SpriteComponent>();

            GetComponent<SpriteComponent>().Spritesheet = "test/grass";
            GetComponent<SizeComponent>().Size = TILE_SIZE;
            GetComponent<CollisionComponent>().EntityType = this.GetType();
        }
    }
}
