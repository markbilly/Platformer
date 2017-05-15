using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Entities.Components
{
    public class HumanoidStateComponent : IComponent
    {
        private Vector2 _previousVelocity;

        public bool IsJumping { get; private set; }
        public bool IsWalkingRight { get; private set; }
        public bool IsWalkingLeft { get; private set; }
        public bool IsIdleRight { get; private set; }
        public bool IsIdleLeft { get; private set; }

        public int Order { get { return 50; } }

        public void Update(Entity entity)
        {
            IsJumping = Math.Abs(entity.Velocity.Y) > 1;

            IsWalkingRight = entity.Velocity.X > 0;
            IsWalkingLeft = entity.Velocity.X < 0;

            IsIdleRight = entity.Velocity.X == 0 && _previousVelocity.X > 0;
            IsIdleLeft = entity.Velocity.X == 0 && _previousVelocity.X < 0;

            if (IsIdleLeft || IsIdleRight)
            {
                var t = 1;
            }

            _previousVelocity = entity.Velocity;
        }
    }
}
