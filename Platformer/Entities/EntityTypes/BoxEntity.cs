using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Platformer.Entities.Components;

namespace Platformer.Entities.EntityTypes
{
    public class BoxEntity : Entity
    {
        private static readonly Point BOX_SIZE = new Point(16, 16);

        public BoxEntity() : base(BOX_SIZE)
        {
            var spriteComponent = new SpriteGraphicsComponent("test/box");
            var collisionComponent = new CollisionComponent();

            AddComponent(spriteComponent);
            AddComponent(new ApplyForceComponent());
            AddComponent(collisionComponent);
            AddComponent(new RigidBodyComponent(collisionComponent));
        }
    }
}
