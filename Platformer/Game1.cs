﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Entities;
using Platformer.Entities.Components;
using Platformer.Entities.Factories;
using Platformer.Input;
using Platformer.Scenes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Platformer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Scene _testScene;
        private Entity _playerEntity;

        private KeyboardState _previousKeyboardState;
        private IEnumerable<IInputHandler> _inputHandlers;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Constants.Game.Width * Constants.Game.Scale;
            _graphics.PreferredBackBufferHeight = Constants.Game.Height * Constants.Game.Scale;

            Content.RootDirectory = "Content";

            _inputHandlers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IInputHandler).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => (IInputHandler)Activator.CreateInstance(x));
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            var guardEntityFactory = new GuardEntityFactory();
            var boxEntityFactory = new BoxEntityFactory();

            var box1 = boxEntityFactory.Build();
            var guard = guardEntityFactory.Build();
            var box2 = boxEntityFactory.Build();

            box1.GetComponent<PositionComponent>().Position = new Vector2(10, 26);
            guard.GetComponent<PositionComponent>().Position = new Vector2(200, 10);
            box2.GetComponent<PositionComponent>().Position = new Vector2(390, 26);

            _playerEntity = new PlayerEntityFactory().Build();

            _testScene = new Scene();
            _testScene.Entities.Add(box1);
            _testScene.Entities.Add(guard);
            _testScene.Entities.Add(box2);
            _testScene.Entities.Add(_playerEntity);

            guard.GetComponent<PatrolAiComponent>().StartPatrol();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _testScene.Load(Content, _spriteBatch);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            HandleInputs();

            _testScene.HandleCollisions();
            _testScene.UpdateEntites();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            foreach (var entity in _testScene.Entities)
            {
                entity.Draw();
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void HandleInputs()
        {
            var currentKeyboardState = Keyboard.GetState();

            if (currentKeyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            foreach (var handler in _inputHandlers)
            {
                var command = handler.HandleInput(_previousKeyboardState, currentKeyboardState);
                if (command != null)
                {
                    command.Execute(_playerEntity);
                }
            }

            _previousKeyboardState = currentKeyboardState;
        }
    }
}
