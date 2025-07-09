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

        // Collision
        Texture2D collisionmapTexture;
        Rectangle collisionmapDisplay;
        Color collisionmapColor;

        //BG
        Texture2D backgroundTexture;
        Rectangle backgroundDisplay;
        Color backgroundColor;

        // Enemies
        Texture2D enemyTexture;
        List<Enemy> enemies;

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

            // Load textures
            heroTexture = Content.Load<Texture2D>("HeroKnight");
            enemyTexture = Content.Load<Texture2D>("HeroKnight"); // reuse for now
            backgroundTexture = Content.Load<Texture2D>("levelmap");
            collisionmapTexture = Content.Load<Texture2D>("levelmapcollisionreal");
            collisionmapTexture = Content.Load<Texture2D>("levelmapcollisionreal");
            collisionmapDisplay = new Rectangle(0, 0, windowWidth, windowHeight);
            collisionmapColor = Color.White;

            // Load player
            int heroWidth = heroTexture.Width / 10;
            int heroHeight = heroTexture.Height / 9;
            Rectangle heroStartRect = new Rectangle(25, 650, heroWidth, heroHeight);
            player = new Player();
            player.LoadContent(heroTexture, collisionmapTexture, heroStartRect, collisionmapDisplay);

            //Load bg
            backgroundDisplay = new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height);
            backgroundColor = Color.White;


            // Load enemies
            enemies = new List<Enemy>
            {
                new Enemy(enemyTexture, collisionmapTexture, new Rectangle(300, 650, 64, 64), collisionmapDisplay),
                new Enemy(enemyTexture, collisionmapTexture, new Rectangle(600, 650, 64, 64), collisionmapDisplay),
                // Add more if needed
            };

            // Load scene
            scene = new Scene("title", Exit, Content, _spriteBatch, windowWidth, windowHeight);
            titleBgtest = Content.Load<Texture2D>("titlescreenplaceholder");
            spriteFont = Content.Load<SpriteFont>("gameFont");
            song = Content.Load<Song>("8bit");

            MediaPlayer.Play(song);
            scene.SetAsset(titleBgtest, spriteFont);
            scene.selectScene();
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState key = Keyboard.GetState();

            // Update player
            player.Update(key, gameTime, enemies);

            // Update enemies
            foreach (var enemy in enemies)
            {
                enemy.Update(gameTime);

                if (!player.IsHit && enemy.CheckCollision(player.displayRectangle) && !enemy.IsDead)
                {
                    player.TakeDamage(enemy.GetBounds().Center.ToVector2());
                }
            }

            // Update camera
            cameraOrigin = new Vector2(windowWidth / 2f, windowHeight / 1f);
            Vector2 targetPos = new Vector2(player.displayRectangle.X + player.displayRectangle.Width / 2, player.displayRectangle.Y + player.displayRectangle.Height);

            float cameraX = MathHelper.Clamp(targetPos.X - cameraOrigin.X, 0, MAP_WIDTH - windowWidth);
            float cameraY = MathHelper.Clamp(targetPos.Y - cameraOrigin.Y, 0, MAP_HEIGHT - windowHeight);

            cameraPosition = new Vector2(cameraX, cameraY);
            cameraMatrix = Matrix.CreateTranslation(new Vector3(-cameraPosition, 0f));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(screenColor);

            _spriteBatch.Begin(transformMatrix: cameraMatrix);
            scene.drawSelectScene(player, collisionmapTexture, backgroundTexture, collisionmapDisplay, backgroundDisplay, collisionmapColor, backgroundColor, enemies);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
