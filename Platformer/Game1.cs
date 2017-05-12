using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Entities;
using Platformer.Entities.Components;
using Platformer.Entities.EntityTypes;
using Platformer.Input;
using Platformer.Scenes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Platformer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _debugFont;

        private Scene _testScene;
        private Entity _playerEntity;

        private KeyboardState _previousKeyboardState;
        private IEnumerable<IInputHandler> _inputHandlers;

        private double _timeSinceLastUpdate;
        private const int DEBUG_PAUSE_PER_FRAME = 0; // use zero value to disable debug pause

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
            var box1 = new BoxEntity();
            var guard = new GuardEntity();
            var box2 = new BoxEntity();

            box1.Position = new Vector2(100, 100);
            guard.Position = new Vector2(200, 100);
            box2.Position = new Vector2(390, 100);

            _playerEntity = new PlayerEntity();
            _playerEntity.Position = new Vector2(130, 100);

            _testScene = new Scene();
            _testScene.AddEntity(box1);
            _testScene.AddEntity(guard);
            _testScene.AddEntity(box2);
            _testScene.AddEntity(_playerEntity);

            for (var i = 0; i < 30; i++)
            {
                var tile = new TileEntity();
                tile.Position = new Vector2(i * 16, 200);
                _testScene.AddEntity(tile);
            }

            guard.GetComponent<PatrolAIComponent>().StartPatrol(guard);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _testScene.Load(Content, _spriteBatch, GraphicsDevice);
            _debugFont = Content.Load<SpriteFont>("fonts/debug");
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
            _timeSinceLastUpdate += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (_timeSinceLastUpdate >= DEBUG_PAUSE_PER_FRAME)
            {
                _timeSinceLastUpdate = 0;

                HandleInputs();

                _testScene.Update();

                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            var frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;

            GraphicsDevice.Clear(Color.Gray);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _testScene.Draw();

#if DEBUG
            _spriteBatch.DrawString(_debugFont, frameRate.ToString(), Vector2.Zero, Color.Red);
#endif

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
