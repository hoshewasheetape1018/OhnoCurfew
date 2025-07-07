using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;

using System;


namespace curfew
{
    internal class Scene
    {
        // make functions for Title screen -> void function?
        // switch case for choice
        // if no save file exists then "Start Game" else "Continue Game"

        // START GAME - Go to stage 1 or CONTINUE GAME - Go to checkpoint
        // SETTINGS - Open Settings (Mute audio, Go to fullscreen, back to Main Title)
        // EXIT - Exit the game

        private Action exitCallback;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private ContentManager Content;

        string currentScene;
        Color screenColor;
        private Texture2D titleBg;
        private int windowHeight;
        private int windowWidth;
        public Scene(string currentScene, Action exitCallback, ContentManager Content, SpriteBatch spriteBatch, int windowWidth, int windowHeight)
        {
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
            Console.WriteLine("Title Screen");
            Console.WriteLine("Enter scene: title game settings exit");
            currentScene = Console.ReadLine();
            selectScene();

        }

        protected void settingsScreen()
        {
            Console.WriteLine("Settings Screen");
        }
        protected void startGame()
        {
            Console.WriteLine("In game");
        }



        // DRAW FUNCTIONS -- Called in Game1.cs Draw
        public void drawSelectScene()
        {
            switch (currentScene)
            {
                case ("title"):
                    drawTitleScreen();
                    break;

                case ("game"):
                    drawgameScreen();
                    break;

                case ("settings"):
                    drawSettingsScreen();
                    break;

                default:
                    break;
            }

        }
        public void SetAsset(Texture2D titleBackground)
        {
            titleBg = titleBackground;
        }
        protected void drawTitleScreen()
        {
            // Draw function of TitleScreen here
            screenColor = Color.Red;
            // _spriteBatch.Draw(titleBg, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);

        }


        protected void drawSettingsScreen()
        {
            // Draw function of TitleScreen here
            screenColor = Color.Yellow;

            //for testing only lol will remove when Title screen gets a proper UI.
            _spriteBatch.Draw(titleBg, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);

        }

        protected void drawgameScreen()
        {
            // Draw function of TitleScreen here
            screenColor = Color.Magenta;

        }

        public Color getColor()
        {
            return screenColor;
        }


    }
}
