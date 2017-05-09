using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Platformer.Entities;
using Platformer.Entities.Commands;

namespace Platformer.Input.Handlers
{
    public class PlayerMovementHandler : IInputHandler
    {
        public IEntityCommand HandleInput(KeyboardState previousKeyboardState, KeyboardState currentKeyboardState)
        {
            if (previousKeyboardState.IsKeyUp(Keys.Space) && currentKeyboardState.IsKeyDown(Keys.Space))
            {
                return new JumpCommand();
            }

            if (currentKeyboardState.IsKeyUp(Keys.Right) && currentKeyboardState.IsKeyUp(Keys.Left))
            {
                return new MoveStopCommand();
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                return new MoveRightCommand();
            }

            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                return new MoveLeftCommand();
            }

            return null;
        }
    }
}
