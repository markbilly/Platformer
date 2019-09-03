using Microsoft.Xna.Framework;
using Platformer.Core;
using Platformer.Graphics;
using Platformer.Graphics.Components;
using Platformer.Physics;
using Platformer.Physics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities
{
    public class TileEntityFactory : EntityFactory
    {
        private static readonly Point TILE_SIZE = new Point(16, 16);

        public TileEntityFactory() : base(new[]
            {
                typeof(SizeComponent),
                typeof(PositionComponent),
                typeof(VelocityComponent),
                typeof(CollisionComponent),
                typeof(RigidBodyComponent),
                typeof(SpriteComponent)
            })
        { }

        public override Entity Build()
        {
            var entity = base.Build();

            entity.GetComponent<SpriteComponent>().Spritesheet = "test/grass";
            entity.GetComponent<SizeComponent>().Size = TILE_SIZE;
            entity.GetComponent<CollisionComponent>().CollisionProfile = CollisionProfiles.StaticSceneElement;

            return entity;
        }
    }
}
