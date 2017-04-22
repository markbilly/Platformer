using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platformer.Entities;
using Platformer.Scenes;
using System.Linq;

namespace Platformer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        Scene _testScene;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            var testPositionComponent = new PositionComponent(new Vector2(10, 10));
            var testMovementComponent = new MovementComponent(testPositionComponent);
            var testSpriteComponent = new SpriteComponent(testPositionComponent, new Point(10, 10), "test/bit");
            
            testMovementComponent.Velocity = new Vector2(1, 1);

            var testEntity = new Entity
            {
                testPositionComponent,
                testMovementComponent,
                testSpriteComponent
            };

            _testScene = new Scene();
            _testScene.Entities.Add(testEntity);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (var entity in _testScene.Entities)
            {
                var spriteComponent = entity.GetComponent<SpriteComponent>();

                if (spriteComponent != null)
                {
                    spriteComponent.SpriteBatch = _spriteBatch;
                    spriteComponent.Load(Content);
                }
            }
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            foreach (var entity in _testScene.Entities)
            {
                entity.Update();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach (var entity in _testScene.Entities)
            {
                entity.Draw();
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
