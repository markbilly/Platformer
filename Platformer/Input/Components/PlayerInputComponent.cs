using Microsoft.Xna.Framework.Input;
using Platformer.Core;
using Platformer.GameLogic.Components;
using Platformer.Input.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Input.Components
{
    public class PlayerInputComponent : IInputComponent
    {
        private readonly PlayerMovementHandler _inputHandler;
        private KeyboardState _previousKeyboardState;

        public PlayerInputComponent(HumanoidBehaviourComponent humanoidBehaviourComponent)
        {
            _inputHandler = new PlayerMovementHandler(humanoidBehaviourComponent);
        }

        public void Update()
        {
            var currentKeyboardState = Keyboard.GetState();

            _inputHandler.HandleInput(_previousKeyboardState, currentKeyboardState);

            _previousKeyboardState = currentKeyboardState;
        }
    }
}
