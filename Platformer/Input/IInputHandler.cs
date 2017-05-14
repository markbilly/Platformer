using Microsoft.Xna.Framework.Input;
using Platformer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Input
{
    public interface IInputHandler
    {
        IEnumerable<IEntityCommand> HandleInput(KeyboardState previousKeyboardState, KeyboardState currentKeyboardState);
    }
}
