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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            scene = new Scene("title", Exit);
            screenColor = Color.CornflowerBlue;
            Console.WriteLine("Color: " + screenColor.ToString());
            // TODO: Initialize stages, Initialize to titleScreen
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            scene.selectScene();

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
