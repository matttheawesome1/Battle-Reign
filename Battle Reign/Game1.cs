using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Battle_Reign {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Camera2D camera;
        GameMouse mouse;

        SceneManager sm;

        int fps = 0, tempFPS = 0;

        float elapsed = 0f;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 768;

            graphics.SynchronizeWithVerticalRetrace = false;
            
            Window.IsBorderless = true;
            Window.Title = "Civive";

            IsFixedTimeStep = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            camera = new Camera2D(GraphicsDevice.Viewport);
            mouse = new GameMouse(Content);

            GameObject.Initialize(Content, graphics, camera, mouse);

            sm = new SceneManager(this);
            sm.AddScene(new SceneMainMenu());

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            //if (Keyboard.GetState().IsKeyDown(Keys.Down)) camera.Zoom -= .5f;
            //if (Keyboard.GetState().IsKeyDown(Keys.Up)) camera.Zoom += .5f;

            sm.Update(gameTime);

            mouse.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.DimGray);
            var viewMatrix = camera.GetViewMatrix();

            if (elapsed > 1f) {
                elapsed = 0f;
                fps = tempFPS;
                tempFPS = 0;
            }

            elapsed += (float) gameTime.ElapsedGameTime.TotalSeconds;
            tempFPS++;

            //spriteBatch.Begin(transformMatrix: viewMatrix);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, transformMatrix: viewMatrix);
            
            sm.Draw(spriteBatch);

            spriteBatch.DrawString(GameObject.FontMedium, fps.ToString(), new Vector2(camera.Position.X + graphics.PreferredBackBufferWidth - 100, camera.Position.Y + graphics.PreferredBackBufferHeight - 35), Color.Red);

            mouse.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}