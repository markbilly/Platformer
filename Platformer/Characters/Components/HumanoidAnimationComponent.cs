using Microsoft.Xna.Framework;
using Platformer.Characters.Components;
using Platformer.Core;
using Platformer.Graphics.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Characters.Components
{
    public class HumanoidAnimationComponent : Component
    {
        private readonly AnimateComponent _animateComponent;
        private readonly HumanoidStateComponent _humanoidStateComponent;

        public HumanoidAnimationComponent(AnimateComponent animateComponent, HumanoidStateComponent humanoidStateComponent) 
            : base(ComponentType.Graphics)
        {
            _animateComponent = animateComponent;
            _humanoidStateComponent = humanoidStateComponent;
        }

        public override void Update(Entity entity)
        {
            if (_humanoidStateComponent.IsWalkingRight)
            {
                _animateComponent.SetAnimation(Animations.MoveRight);
            }

            if (_humanoidStateComponent.IsWalkingLeft)
            {
                _animateComponent.SetAnimation(Animations.MoveLeft);
            }

            if (_humanoidStateComponent.IsIdleRight)
            {
                _animateComponent.SetAnimation(Animations.IdleRight);
            }

            if (_humanoidStateComponent.IsIdleLeft)
            {
                _animateComponent.SetAnimation(Animations.IdleLeft);
            }
        }
    }
}
