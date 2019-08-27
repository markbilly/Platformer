using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Platformer.Core;
using Platformer.GameLogic;
using Platformer.Graphics;
using Platformer.Input;
using Platformer.Physics;
using Platformer.Physics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer.Core
{
    public class Scene
    {
        private readonly GraphicsSystem _graphicsSystem;
        private readonly PhysicsSystem _physicsSystem;
        private readonly InputSystem _inputSystem;
        private readonly GameLogicSystem _gameLogicSystem;

        public Scene()
        {
            _graphicsSystem = new GraphicsSystem();
            _physicsSystem = new PhysicsSystem();
            _inputSystem = new InputSystem();
            _gameLogicSystem = new GameLogicSystem();
        }

        public void Load(ContentManager contentManager, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            _graphicsSystem.Load(contentManager, spriteBatch, graphicsDevice);
        }

        public void AddEntity(Entity entity)
        {
            // TODO: Replace with entity factory and only pass json definition or something

            foreach (var component in entity.Components)
            {
                if (component is IGraphicsComponent graphicsComponent)
                {
                    _graphicsSystem.RegisterComponent(graphicsComponent);
                }

                if (component is IInputComponent inputComponent)
                {
                    _inputSystem.RegisterComponent(inputComponent);
                }

                if (component is IPhysicsComponent physicsComponent)
                {
                    _physicsSystem.RegisterComponent(physicsComponent);
                }

                if (component is IGameLogicComponent gameLogicComponent)
                {
                    _gameLogicSystem.RegisterComponent(gameLogicComponent);
                }
            }
        }

        public void Update()
        {
            _physicsSystem.Update();
            _inputSystem.Update();
            _gameLogicSystem.Update();
        }

        public void Draw()
        {
            _graphicsSystem.Update();
        }
    }
}
