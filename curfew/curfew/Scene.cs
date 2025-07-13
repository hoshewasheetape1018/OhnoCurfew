using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace curfew
{
    internal class Scene
    {
        private Action exitCallback;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ContentManager Content;

        string currentScene;
        Color screenColor;
        private Texture2D titleBg;
        private int windowHeight;
        private int windowWidth;
        SpriteFont spriteFont;

        Player player;
        MouseState previousMouseState;
        Rectangle startButton = new Rectangle(180, 420, 250, 40);
        Rectangle exitButton = new Rectangle(180, 510, 250, 40);

        bool hasCheckpointSave = false;

        public Scene(Player player, string currentScene, Action exitCallback, ContentManager Content, SpriteBatch spriteBatch, int windowWidth, int windowHeight)
        {
            this.player = player;
            this.currentScene = currentScene;
            this.exitCallback = exitCallback;
            this.Content = Content;
            this._spriteBatch = spriteBatch;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
        }

        public string CurrentScene { get => currentScene; }

        public void selectScene()
        {
            switch (currentScene)
            {
                case ("title"):
                    titleScreen();
                    break;

                case ("game"):
                    startGame();
                    break;

                case ("settings"):
                    settingsScreen();
                    break;

                case ("exit"):
                    exitCallback?.Invoke();
                    break;

                default:
                    break;
            }
        }

        protected void titleScreen()
        {
            // placeholder for switching logic
        }

        protected void settingsScreen()
        {
            Console.WriteLine("Settings Screen");
        }

        protected void startGame()
        {
            Console.WriteLine("In game");
        }

        public void drawSelectScene(Player player, GameTiles tiles, Texture2D backgroundTexture, Rectangle backgroundRectangle, Color backgroundColor)
        {
            switch (currentScene)
            {
                case ("title"):
                    drawTitleScreen();
                    break;

                case ("game"):
                    drawgameScreen(player, tiles, backgroundTexture, backgroundRectangle, backgroundColor);
                    break;

                case ("settings"):
                    drawSettingsScreen();
                    break;

                default:
                    break;
            }
        }

        public void SetAsset(Texture2D titleBackground, SpriteFont font)
        {
            titleBg = titleBackground;
            spriteFont = font;
        }

        protected void drawTitleScreen()
        {
            screenColor = Color.Red;
            MouseState mouse = Mouse.GetState();
            Point mousePoint = new Point(mouse.X, mouse.Y);

            _spriteBatch.Draw(titleBg, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);

            Color startColor = startButton.Contains(mousePoint) ? Color.Blue : Color.White;
            Color exitColor = exitButton.Contains(mousePoint) ? Color.Blue : Color.White;

            _spriteBatch.DrawString(spriteFont, "START GAME", new Vector2(startButton.X, startButton.Y), startColor);
            _spriteBatch.DrawString(spriteFont, "EXIT", new Vector2(exitButton.X, exitButton.Y), exitColor);

            if (mouse.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                if (startButton.Contains(mousePoint))
                    currentScene = "game";
                else if (exitButton.Contains(mousePoint))
                    currentScene = "exit";

                selectScene();
            }

            previousMouseState = mouse;
        }

        protected void drawSettingsScreen()
        {
            screenColor = Color.Yellow;
            _spriteBatch.Draw(titleBg, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
            _spriteBatch.DrawString(spriteFont, "SETTINGS", new Vector2(180, 350), Color.White);
        }

        protected void drawgameScreen(Player player, GameTiles tiles, Texture2D backgroundDisplay, Rectangle backgroundRectangle, Color backgroundColor)
        {
            screenColor = Color.Magenta;
            // _spriteBatch.Draw(backgroundDisplay, backgroundRectangle, backgroundColor);
            _spriteBatch.Draw(player.charaTexture, new Vector2(player.xpos, player.ypos), player.sourceRectangle, Color.White);
        }

        public Color getColor()
        {
            return screenColor;
        }

    }
}
