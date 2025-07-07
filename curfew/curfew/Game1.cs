using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace curfew
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Scene scene;
        Color screenColor;
        public Texture2D titleBgtest;

        float preciseScale;
        public int windowWidth;
        public int windowHeight;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // my laptop screen is small. change to 1 if ure monitor is okay with 1920 x 1080
            preciseScale = 1.5f;
            windowWidth = (int)Math.Ceiling(1920f / preciseScale);
            windowHeight = (int)Math.Ceiling(1080f / preciseScale);


            _graphics.PreferredBackBufferWidth = windowWidth;
            _graphics.PreferredBackBufferHeight = windowHeight;
        }

        protected override void Initialize()
        {


            screenColor = Color.CornflowerBlue;
            // TODO: Initialize stages, Initialize to titleScreen
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            scene = new Scene("title", Exit, Content, _spriteBatch, windowWidth, windowHeight);
            scene.selectScene();
            titleBgtest = Content.Load<Texture2D>("titlescreenplaceholder");
            scene.SetAsset(titleBgtest); // pass it over


            // TODO: use this.Content to load your game content here
            // no idea
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            // no idea

            screenColor = scene.getColor();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            _spriteBatch.Begin();
            GraphicsDevice.Clear(screenColor);
            scene.drawSelectScene();
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
