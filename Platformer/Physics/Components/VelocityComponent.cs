using Microsoft.Xna.Framework;
using Platformer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Physics.Components
{
    public class VelocityComponent : IPhysicsComponent
    {
        private readonly PositionComponent _positionComponent;

        public VelocityComponent(PositionComponent positionComponent)
        {
            _positionComponent = positionComponent;
        }

        public Vector2 Velocity { get; set; }

        public void Update()
        {
            _positionComponent.Position += Velocity;
        }
    }
}
