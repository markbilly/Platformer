using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Platformer.Core;
using Platformer.Characters.Commands;

namespace Platformer.Input.Handlers
{
    public class PlayerMovementHandler : IInputHandler
    {
        public IEnumerable<IEntityCommand> HandleInput(KeyboardState previousKeyboardState, KeyboardState currentKeyboardState)
        {
            if (previousKeyboardState.IsKeyUp(Keys.Space) && currentKeyboardState.IsKeyDown(Keys.Space))
            {
                return new[] { new JumpCommand() };
            }

            if (currentKeyboardState.IsKeyUp(Keys.Right) && currentKeyboardState.IsKeyUp(Keys.Left))
            {
                return new[] { new MoveStopCommand() };
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                return new[] { new MoveRightCommand() };
            }

            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                return new[] { new MoveLeftCommand() };
            }

            return null;
        }
    }
}
