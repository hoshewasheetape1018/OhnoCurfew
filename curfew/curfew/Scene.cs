using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        string currentScene;
        Color screenColor;

        public Scene(string currentScene, Action exitCallback)
        {
            this.currentScene = currentScene;
            this.exitCallback = exitCallback;
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

        protected void drawTitleScreen()
        {
            // Draw function of TitleScreen here
            screenColor = Color.Red;

        }


        protected void drawSettingsScreen()
        {
            // Draw function of TitleScreen here
            screenColor = Color.Yellow;

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
