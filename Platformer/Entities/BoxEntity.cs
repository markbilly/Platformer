using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Platformer.Core;
using Platformer.Graphics.Components;
using Platformer.Graphics.EntityRenderers;
using Platformer.Physics.Components;

namespace Platformer.Entities
{
    public class BoxEntity : Entity
    {
        private static readonly Point BOX_SIZE = new Point(16, 16);

        public BoxEntity() : base(BOX_SIZE)
        {
            AddComponent<ApplyForceComponent>();
            AddComponent<CollisionComponent>();
            AddComponent<RigidBodyComponent>();
            AddComponent<SpriteComponent>();

            var spriteComponent = GetComponent<SpriteComponent>();
            spriteComponent.Spritesheet = "test/box";

            AddRenderer(new SpriteEntityRenderer(spriteComponent));
        }
    }
}
