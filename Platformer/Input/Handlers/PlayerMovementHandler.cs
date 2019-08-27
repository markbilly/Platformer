using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Platformer.Core;
using Platformer.GameLogic.Components;

namespace Platformer.Input.Handlers
{
    public class PlayerMovementHandler : IInputHandler
    {
        private readonly HumanoidBehaviourComponent _humanoidBehaviourComponent;

        public PlayerMovementHandler(HumanoidBehaviourComponent humanoidBehaviourComponent)
        {
            _humanoidBehaviourComponent = humanoidBehaviourComponent;
        }

        public void HandleInput(KeyboardState previousKeyboardState, KeyboardState currentKeyboardState)
        {
            if (previousKeyboardState.IsKeyUp(Keys.Space) && currentKeyboardState.IsKeyDown(Keys.Space))
            {
                _humanoidBehaviourComponent.StartJump();
            }

            if (currentKeyboardState.IsKeyUp(Keys.Right) && currentKeyboardState.IsKeyUp(Keys.Left))
            {
                _humanoidBehaviourComponent.StopMoveHorizontal();
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                _humanoidBehaviourComponent.StartMoveRight();
            }

            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                _humanoidBehaviourComponent.StartMoveLeft();
            }
        }
    }
}
