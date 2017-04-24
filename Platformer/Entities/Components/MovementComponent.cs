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
        private PositionComponent _positionComponent;
        private AnimateComponent _animateComponent;
        private SpriteComponent _spriteComponent;

        private int _movementSpeed;

        public MovementComponent(
            PositionComponent positionComponent,
            AnimateComponent animateComponent,
            SpriteComponent spriteComponent,
            int movementSpeed)
        {
            _positionComponent = positionComponent;
            _animateComponent = animateComponent;
            _spriteComponent = spriteComponent;
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

        public void Draw()
        {
            return;
        }

        public void Update()
        {
            _positionComponent.Position += Velocity;

            if (Velocity.X == 0)
            {
                _animateComponent.Pause();
                return;
            }

            _spriteComponent.SpritesheetRow = Velocity.X > 0
                ? Constants.Animation.WalkRight
                : Constants.Animation.WalkLeft;

            _animateComponent.Play();
        }
    }
}
