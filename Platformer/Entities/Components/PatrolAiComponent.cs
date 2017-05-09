using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Components
{
    public class PatrolAiComponent : IComponent
    {
        private RigidBodyComponent _rigidBodyComponent;

        public PatrolAiComponent(RigidBodyComponent rigidBodyComponent)
        {
            _rigidBodyComponent = rigidBodyComponent;
        }

        public void StartPatrol(Entity entity)
        {
            entity.Velocity = new Vector2(1, 0);
        }

        public void Update(Entity entity)
        {
            var collision = _rigidBodyComponent.GetLatestCollision();
            if (collision.X != 0)
            {
                entity.Velocity = new Vector2(collision.X * -1, entity.Velocity.Y);
            }
        }
    }
}
