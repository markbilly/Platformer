using Microsoft.Xna.Framework;
using Platformer.Core;
using Platformer.Graphics.Components;
using Platformer.Physics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.GameLogic.Components
{
    public class HumanoidBehaviourComponent : IGameLogicComponent
    {
        private readonly AnimateComponent _animateComponent;
        private readonly VelocityComponent _velocityComponent;
        private readonly ApplyForceComponent _applyForceComponent;

        private Vector2 _previousVelocity;

        public HumanoidBehaviourComponent(AnimateComponent animateComponent, VelocityComponent velocityComponent, ApplyForceComponent applyForceComponent)
        {
            _animateComponent = animateComponent;
            _velocityComponent = velocityComponent;
            _applyForceComponent = applyForceComponent;
        }

        public bool IsJumping { get; private set; }
        public bool IsWalkingRight { get; private set; }
        public bool IsWalkingLeft { get; private set; }
        public bool IsIdleRight { get; private set; }
        public bool IsIdleLeft { get; private set; }

        public void StartMoveRight()
        {
            _velocityComponent.Velocity = new Vector2(1, _velocityComponent.Velocity.Y);
        }

        public void StartMoveLeft()
        {
            _velocityComponent.Velocity = new Vector2(-1, _velocityComponent.Velocity.Y);
        }

        public void StopMoveHorizontal()
        {
            _velocityComponent.Velocity = new Vector2(0, _velocityComponent.Velocity.Y);
        }

        public void StartJump()
        {
            if (!IsJumping)
            {
                _applyForceComponent.ApplyForce("jump", new Vector2(0, -150f), 2);
            }
        }

        public void Update()
        {
            // Update state
            IsJumping = Math.Abs(_velocityComponent.Velocity.Y) > 1;

            IsWalkingRight = _velocityComponent.Velocity.X > 0;
            IsWalkingLeft = _velocityComponent.Velocity.X < 0;

            IsIdleRight = _velocityComponent.Velocity.X == 0 && _previousVelocity.X > 0;
            IsIdleLeft = _velocityComponent.Velocity.X == 0 && _previousVelocity.X < 0;

            _previousVelocity = _velocityComponent.Velocity;

            //Set animation
            if (IsWalkingRight)
            {
                _animateComponent.SetAnimation(Animations.MoveRight);
            }

            if (IsWalkingLeft)
            {
                _animateComponent.SetAnimation(Animations.MoveLeft);
            }

            if (IsIdleRight)
            {
                _animateComponent.SetAnimation(Animations.IdleRight);
            }

            if (IsIdleLeft)
            {
                _animateComponent.SetAnimation(Animations.IdleLeft);
            }
        }
    }
}
