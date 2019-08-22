using Microsoft.Xna.Framework;
using Platformer.Core;
using Platformer.Graphics.Components;
using Platformer.Graphics.EntityRenderers;
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

        public TileEntity() : base(TILE_SIZE)
        {
            AddComponent<CollisionComponent>();
            AddComponent<RigidBodyComponent>();
            AddComponent<SpriteComponent>();

            var spriteComponent = GetComponent<SpriteComponent>();
            spriteComponent.Spritesheet = "test/grass";

            AddRenderer(new SpriteEntityRenderer(spriteComponent));
        }
    }
}
