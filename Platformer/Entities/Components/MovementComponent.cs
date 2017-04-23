using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Components
{
    public class MovementComponent : IComponent
    {
        private PositionComponent _positionComponent;

        public MovementComponent(PositionComponent positionComponent)
        {
            _positionComponent = positionComponent;
        }

        public Vector2 Velocity { get; set; }

        public void Draw()
        {
            return;
        }

        public void Update()
        {
            _positionComponent.Position += Velocity;
        }
    }
}
