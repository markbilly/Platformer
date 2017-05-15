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
        private HumanoidStateComponent _stateComponent;
        private ApplyForceComponent _forceComponent;
        
        public void Execute(Entity entity)
        {
            GatherDependencies(entity);

            if (!_stateComponent.IsJumping)
            {
                _forceComponent.ApplyForce("jump", new Vector2(0, -150f), 2);
            }
        }

        private void GatherDependencies(Entity entity)
        {
            if (_stateComponent == null)
            {
                _stateComponent = entity.GetComponent<HumanoidStateComponent>();
            }

            if (_forceComponent == null)
            {
                _forceComponent = entity.GetComponent<ApplyForceComponent>();
            }
        }
    }
}
