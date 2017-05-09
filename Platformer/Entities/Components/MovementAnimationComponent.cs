using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Components
{
    public class MovementAnimationComponent : IComponent
    {
        private AnimateComponent _animateComponent;

        private int _previousSpeed;

        public MovementAnimationComponent(AnimateComponent animateComponent)
        {
            _animateComponent = animateComponent;
        }

        public void Update(Entity entity)
        {
            if (entity.Velocity.X > 0)
            {
                _animateComponent.SetAnimation(Animations.WalkRight);
            }
            else if (entity.Velocity.X < 0)
            {
                _animateComponent.SetAnimation(Animations.WalkLeft);
            }
            else
            {
                if (_previousSpeed != 0)
                {
                    _animateComponent.SetAnimation(_previousSpeed > 0 ? Animations.IdleRight : Animations.IdleLeft);
                }
            }

            _previousSpeed = (int)entity.Velocity.X;
        }
    }
}
