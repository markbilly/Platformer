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

        public HumanoidAnimationComponent(Entity entity)
        {
            _animateComponent = entity.GetComponent<AnimateComponent>();
            _stateComponent = entity.GetComponent<HumanoidStateComponent>();            
        }

        public int Order { get { return 60; } }

        public void Update(Entity entity)
        {
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
    }
}
