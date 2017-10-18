﻿using Microsoft.Xna.Framework.Input;
using Platformer.Core;
using Platformer.Input.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Input.Components
{
    public class PlayerInputComponent : IComponent
    {
        private PlayerMovementHandler _inputHandler;
        private KeyboardState _previousKeyboardState;

        public PlayerInputComponent()
        {
            _inputHandler = new PlayerMovementHandler();
        }

        public ComponentType Type
        {
            get { return ComponentType.Input; }
        }

        public void Update(Entity entity)
        {
            var currentKeyboardState = Keyboard.GetState();

            var commands = _inputHandler.HandleInput(_previousKeyboardState, currentKeyboardState);
            foreach (var command in commands)
            {
                command.Execute(entity);
            }

            _previousKeyboardState = currentKeyboardState;
        }
    }
}
