using Microsoft.Xna.Framework;
using Platformer.Entities.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Commands
{
    public class JumpCommand : IEntityCommand
    {
        public void Execute(Entity entity)
        {
            if (!entity.GetComponent<MovementStateComponent>().IsJumping)
            {
                entity.GetComponent<ApplyForceComponent>().ApplyForce("jump", new Vector2(0, -150f), 2);
            }
        }
    }
}
