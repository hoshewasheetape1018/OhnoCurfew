using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace curfew
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Debug debug;



        // Window settings
        public int windowWidth;
        public int windowHeight;

        // Assets
        SpriteFont spriteFont;
        Texture2D titleBgtest;
        Song song;


        // Player
        Player player;
        Texture2D heroTexture;

        //Player animations
        Texture2D playerIdle;
        Texture2D playerWalk;

        //Gametiles
        GameTiles[] tiles = new GameTiles[1];

        //BG
        Texture2D backgroundTexture;
        Rectangle backgroundDisplay;
        Color backgroundColor;

        // Enemies
        Texture2D enemyTexture;
        Enemy enemy;
        List<Enemy> enemies = new List<Enemy>();

        Character charatest;

        // Scene manager
        Scene scene;
        Color screenColor = Color.CornflowerBlue;
        KeyboardState prevKeyState;
        KeyboardState currentKeyState;

        //Test tile
        Texture2D pixel;
        Rectangle groundRect;

        //Camera
        Matrix camPos;
        private Vector2 cameraTarget; 
        private Vector2 cameraPosition; 
        private float cameraLerpSpeed = 0.055f;
        private const int MAP_WIDTH = 3072;
        private const int MAP_HEIGHT = 864;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            windowWidth = 1536;
            windowHeight = 864;
            _graphics.PreferredBackBufferWidth = windowWidth;
            _graphics.PreferredBackBufferHeight = windowHeight;
        }

        protected override void Initialize()
        {
            debug = new Debug(windowWidth, windowHeight);
            groundRect = new Rectangle(0, windowHeight - 100, windowWidth, 100);
            camPos = Matrix.Identity;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load player
            playerIdle = Content.Load<Texture2D>("idle");
            playerWalk = Content.Load<Texture2D>("HeroKnight");
  
            player = new Player(400, 0, "idle", playerIdle, 4);
            player.setStateTexture(
                playerIdle, playerWalk, playerIdle, playerIdle, playerIdle, playerIdle, playerIdle
            );
            Console.WriteLine("Player created at: " + player.xpos + ", " + player.ypos);


            // Load enemies
            enemyTexture = Content.Load<Texture2D>("HeroKnight");
       

            enemies.Add(new Enemy(20, 0, "idle", playerIdle, 4, enemies));
            enemies.Add(new Enemy(950, 0, "idle", playerIdle, 4, enemies));
            enemies.Add(new Enemy(130, 0, "idle", playerIdle, 4, enemies));
            enemies.Add(new Enemy(900, 0, "idle", playerIdle,4, enemies));


            foreach (Enemy lenemy in enemies)
            {
                lenemy.setStateTexture(
                   playerIdle, playerWalk, playerIdle, playerIdle, playerIdle, playerIdle, playerIdle
               );
            }



            //Load bg
            backgroundTexture = Content.Load<Texture2D>("levelmap");
            backgroundDisplay = new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height);
            backgroundColor = Color.White;

            // Load scene
            scene = new Scene(player, "title", Exit, Content, _spriteBatch, windowWidth, windowHeight);
            titleBgtest = Content.Load<Texture2D>("titlescreenplaceholder");
            spriteFont = Content.Load<SpriteFont>("gameFont");
            song = Content.Load<Song>("8bit");

            //MediaPlayer.Play(song);
            scene.SetAsset(titleBgtest, spriteFont);


            // load tile ?
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.Red });
            tiles[0] = new GameTiles(pixel, groundRect, new Rectangle(0, 0, 0, 0), Color.Brown);

        }

        protected override void Update(GameTime gameTime)
        {

            KeyboardState key = Keyboard.GetState();
            prevKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
       
            if(scene.CurrentScene == "game") {

            Camera();
                player.Update(tiles, Keyboard.GetState(), enemies);
                foreach (var enemy in enemies)
                {
                    // intersect AND player is not in iFrames
                    if (enemy.collisionBox.Intersects(player.collisionBox) && player.iFrames <= 0)
                    {
                        bool fromLeft = player.xpos > enemy.xpos; 
                        player.TakeDamage(1, fromLeft);

                    }
                }


            }

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(tiles, Keyboard.GetState(), enemies);

                if (enemies[i].isDead)
                {
                    enemies.RemoveAt(i);
                }
            }


            //debug.playerInfo(player);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(screenColor);
            scene.drawSelectScene(camPos, tiles[0], backgroundTexture, backgroundDisplay, backgroundColor);
            _spriteBatch.Draw(tiles[0].tilesTexture, tiles[0].tilesDisplay, backgroundDisplay, backgroundColor);
            player.Draw(_spriteBatch);
            player.DrawHitbox(_spriteBatch, pixel);
            player.DrawCollisionBox(_spriteBatch, pixel); // optional for debug only

            foreach (var enemy in enemies)
            {
                enemy.Draw(_spriteBatch);
                enemy.DrawCollisionBox(_spriteBatch, pixel); // optional for debug only
            }


            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void Camera()
        {

            int halfScreenW = windowWidth / 2;
            int halfScreenH = windowHeight / 2;

            // Calculate the target position based on player's center
            cameraTarget = new Vector2(
                player.xpos + player.charaWidth / 2 - halfScreenW,
                player.ypos + player.charaHeight / 2 - halfScreenH
            );

            // Clamp bounds
            cameraTarget.X = MathHelper.Clamp(cameraTarget.X, 0, MAP_WIDTH - windowWidth);
            cameraTarget.Y = MathHelper.Clamp(cameraTarget.Y, 0, MAP_HEIGHT - windowHeight);

            // Lerp
            cameraPosition = Vector2.Lerp(cameraPosition, cameraTarget, cameraLerpSpeed);

            // Create camera transform
            camPos = Matrix.CreateTranslation(new Vector3(-cameraPosition, 0));


        }

    }
}
