using Microsoft.Xna.Framework;
using Platformer.Characters.Components;
using Platformer.Core;
using Platformer.Physics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.EntityCommands
{
    public class JumpEntityCommand : IEntityCommand
    {
        public void Execute(Entity entity)
        {
            if (!entity.GetComponent<HumanoidStateComponent>().IsJumping)
            {
                entity.GetComponent<ApplyForceComponent>().ApplyForce("jump", new Vector2(0, -150f), 2);
            }
        }
    }
}
