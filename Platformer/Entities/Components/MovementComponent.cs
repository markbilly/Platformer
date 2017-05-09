using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Components
{
    public class MovementComponent : IComponent
    {
        private AnimateComponent _animateComponent;

        private int _movementSpeed;
        private int _previousSpeed;

        public MovementComponent(
            AnimateComponent animateComponent,
            int movementSpeed)
        {
            _animateComponent = animateComponent;
            _movementSpeed = movementSpeed;
        }

        public Vector2 Velocity { get; set; }

        public void StartMove()
        {
            Velocity = new Vector2(_movementSpeed, 0);
        }

        public void StopMove()
        {
            Velocity = Vector2.Zero;
        }

        public void Update(Entity entity)
        {
            entity.Position += Velocity;

            if (Velocity.X > 0)
            {
                _animateComponent.SetAnimation(Animations.WalkRight);
            }
            else if (Velocity.X < 0)
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

            _previousSpeed = (int)Velocity.X;
        }
    }
}
