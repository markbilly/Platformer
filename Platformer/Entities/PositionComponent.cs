using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities
{
    public class PositionComponent : IComponent
    {
        public PositionComponent(Vector2 position)
        {
            Position = position;
        }

        public Vector2 Position { get; set; }

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
