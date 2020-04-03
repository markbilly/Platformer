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

        private RecentVelocitySnapshot _velocitySnapshot = new RecentVelocitySnapshot();

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
            IsJumping = _velocitySnapshot.MaxY > 0.5f;

            IsWalkingRight = _velocityComponent.Velocity.X > 0;
            IsWalkingLeft = _velocityComponent.Velocity.X < 0;

            IsIdleRight = _velocityComponent.Velocity.X == 0 && _velocitySnapshot.Previous.X > 0;
            IsIdleLeft = _velocityComponent.Velocity.X == 0 && _velocitySnapshot.Previous.X < 0;

            _velocitySnapshot.Log(_velocityComponent.Velocity);

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

        private class RecentVelocitySnapshot
        {
            private readonly Vector2[] _pastVelocities = new Vector2[5];
            private int _indexOfLastVelocityLogged = -1;

            public void Log(Vector2 nextVelocity)
            {
                _pastVelocities[++_indexOfLastVelocityLogged] = nextVelocity;
                if (_indexOfLastVelocityLogged == _pastVelocities.Length - 1)
                {
                    _indexOfLastVelocityLogged = -1;
                }
            }

            public float MaxY => _pastVelocities.Max(v => Math.Abs(v.Y));

            public Vector2 Previous
            {
                get
                {
                    if (_indexOfLastVelocityLogged == -1)
                    {
                        return new Vector2();
                    }

                    return _pastVelocities[_indexOfLastVelocityLogged];
                }
            }
        }
    }
}
