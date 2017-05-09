using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Components
{
    public class MovementStateComponent : IComponent
    {
        public bool IsJumping { get; private set; }

        public void Update(Entity entity)
        {
            if (entity.Velocity.Y != 0)
            {
                IsJumping = true;
            }
        }
    }
}
