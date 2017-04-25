using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Components
{
    public class RigidBodyComponent : IComponent
    {
        private PositionComponent _positionComponent;

        public RigidBodyComponent(
            PositionComponent positionComponent,
            Point boundingBoxSize)
        {
            _positionComponent = positionComponent;
            BoundingBoxSize = boundingBoxSize;
        }

        public Vector2 Collision { get; set; }
        public Point BoundingBoxSize { get; private set; }

        public void Draw()
        {
            return;
        }

        public void Update()
        {
            return;
        }
    }
}
