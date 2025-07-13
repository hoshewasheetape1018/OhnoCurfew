using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace curfew
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Camera
        private Matrix cameraMatrix;
        private Vector2 cameraPosition;
        private Vector2 cameraOrigin;
        private const int MAP_WIDTH = 3072;
        private const int MAP_HEIGHT = 864;

        // Window settings
        float preciseScale = 1.25f;
        public int windowWidth;
        public int windowHeight;

        // Assets
        SpriteFont spriteFont;
        Texture2D titleBgtest;
        Song song;

        // Player
        Player player;
        Texture2D heroTexture;

        //Gametiles
        GameTiles tiles;

        //BG
        Texture2D backgroundTexture;
        Rectangle backgroundDisplay;
        Color backgroundColor;

        // Enemies
        Texture2D enemyTexture;


        // Scene manager
        Scene scene;
        Color screenColor = Color.CornflowerBlue;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            windowWidth = (int)Math.Ceiling(1920f / preciseScale);
            windowHeight = (int)Math.Ceiling(1080f / preciseScale);
            _graphics.PreferredBackBufferWidth = windowWidth;
            _graphics.PreferredBackBufferHeight = windowHeight;
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);



            // Load player
            heroTexture = Content.Load<Texture2D>("HeroKnight");
            enemyTexture = Content.Load<Texture2D>("HeroKnight"); // reuse for now
            backgroundTexture = Content.Load<Texture2D>("levelmap");
            player = new Player(200, 400, heroTexture, windowWidth, windowHeight);


            //Load bg
            backgroundDisplay = new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height);
            backgroundColor = Color.White;

            // Load scene
            scene = new Scene(player, "title", Exit, Content, _spriteBatch, windowWidth, windowHeight);
            titleBgtest = Content.Load<Texture2D>("titlescreenplaceholder");
            spriteFont = Content.Load<SpriteFont>("gameFont");
            song = Content.Load<Song>("8bit");

            //MediaPlayer.Play(song);
            scene.SetAsset(titleBgtest, spriteFont);
            scene.selectScene();

            // Debug
            Debug(player);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState key = Keyboard.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(screenColor);
            _spriteBatch.Begin();
            scene.drawSelectScene(player, tiles, backgroundTexture, backgroundDisplay, backgroundColor);
            _spriteBatch.End();

            base.Draw(gameTime);
        }


        void Debug(Player player)
        {
            Console.WriteLine("Window width: " + windowWidth + " Window Height: " + windowHeight + " Xpos: " + player.xpos + " Ypos: " + player.ypos);

            Console.WriteLine("Player height: " + player.charaHeight + " Player width: " + player.charaWidth);

            if (player.xpos < windowWidth)
            {
                Console.WriteLine("Player Inbounds X position");
            }
            else
            {
                Console.WriteLine("Player Out of bounds x position");
            }
            if (player.ypos < windowHeight)
            {
                Console.WriteLine("Player Inbounds Y position");
            }
            else
            {
                Console.WriteLine("Player Out of bounds Y position");
            }

        }
    }
}
