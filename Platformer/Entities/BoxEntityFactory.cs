using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Platformer.Core;
using Platformer.Graphics;
using Platformer.Graphics.Components;
using Platformer.Physics;
using Platformer.Physics.Components;

namespace Platformer.Entities
{
    public class BoxEntityFactory : EntityFactory
    {
        private static readonly Point BOX_SIZE = new Point(16, 16);

        public BoxEntityFactory() : base(new[]
            {
                typeof(SizeComponent),
                typeof(PositionComponent),
                typeof(VelocityComponent),
                typeof(ApplyForceComponent),
                typeof(CollisionComponent),
                typeof(RigidBodyComponent),
                typeof(SpriteComponent)
            })
        { }

        public override Entity Build()
        {
            var entity = base.Build();

            entity.GetComponent<SpriteComponent>().Spritesheet = "test/box";
            entity.GetComponent<SizeComponent>().Size = BOX_SIZE;
            entity.GetComponent<CollisionComponent>().CollisionProfile = CollisionProfiles.StaticSceneElement;

            return entity;
        }
    }
}
