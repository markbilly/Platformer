using Microsoft.Xna.Framework.Input;
using Platformer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Input
{
    public interface IInputHandler
    {
        void HandleInput(KeyboardState previousKeyboardState, KeyboardState currentKeyboardState);
    }
}
