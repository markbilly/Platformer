using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Platformer.Entities.Components;
using Microsoft.Xna.Framework;

namespace Platformer.Entities.Commands
{
    public class MoveStopCommand : IEntityCommand
    {
        public void Execute(Entity entity)
        {
            entity.Velocity = new Vector2(0, entity.Velocity.Y);
        }
    }
}
