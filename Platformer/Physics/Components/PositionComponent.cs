using Microsoft.Xna.Framework;
using Platformer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Physics.Components
{
    public class PositionComponent : IPhysicsComponent
    {
        public Vector2 Position { get; set; }

        public void Update() { }
    }
}
