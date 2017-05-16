using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Components
{
    public class HumanoidAnimationComponent : IComponent
    {
        private AnimateComponent _animateComponent;
        private HumanoidStateComponent _stateComponent;

        public void Update(Entity entity)
        {
            GatherDependencies(entity);

            if (_stateComponent.IsWalkingRight)
            {
                _animateComponent.SetAnimation(Animations.WalkRight);
            }

            if (_stateComponent.IsWalkingLeft)
            {
                _animateComponent.SetAnimation(Animations.WalkLeft);
            }

            if (_stateComponent.IsIdleRight)
            {
                _animateComponent.SetAnimation(Animations.IdleRight);
            }

            if (_stateComponent.IsIdleLeft)
            {
                _animateComponent.SetAnimation(Animations.IdleLeft);
            }
        }

        private void GatherDependencies(Entity entity)
        {
            if (_animateComponent == null)
            {
                _animateComponent = entity.GetComponent<AnimateComponent>();
            }

            if (_stateComponent == null)
            {
                _stateComponent = entity.GetComponent<HumanoidStateComponent>();
            }
        }
    }
}
