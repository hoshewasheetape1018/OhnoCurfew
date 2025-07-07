using System.Linq;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PA2
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        GameBackground gameBackground;

        GameTiles[] tiles;
        GameLeaves[] leaves;
        GameSigns[] signs;
        GamePlants[] plant;
        GameStones[] stone;

        GameSpikes[] spike;

        Texture2D heroTexture;
        Rectangle heroDisplay, heroSource;
        Color heroColor;

        SpriteEffects heroEffect = SpriteEffects.None;

        int idleDelay = 0;
        int walkDelay = 0;
        int jumpDelay = 0;
        int fallDelay = 0;
        int hitDelay = 0;

        int heroCounter = 0;
        int heroWidth, heroHeight;

        bool isJumping = false;
        float jumpSpeed = -12f;
        float gravity = 0.55f;
        float velocityY = 0f;

        int groundY = 650;

        KeyboardState currentKeyState;
        KeyboardState prevKeyState;

        int jumpBufferTime = 6;
        int jumpBufferCounter = 0;

        bool isHit = false;

        private Random random = new Random();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferHeight = 750;
            _graphics.PreferredBackBufferWidth = 1250;
        }

        protected override void Initialize()
        {
            #region Background
            Texture2D bgTexture = Content.Load<Texture2D>("Game Background 15");
            Rectangle bgRectangle = new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height);
            Color bgColor = Color.White;

            gameBackground = new GameBackground(bgTexture, bgRectangle, bgColor);
            #endregion

            #region Platforms/Tiles Manual + Random
            // MANUAL Tiles
            int[][] manualTilePlatforms = new int[][]
            {
                new int[] { 5, 4, 4, 0, 0, 1 },
                new int[] { 0, 0 },
                new int[] { 1, 0, 0, 4, 4 },
                new int[] { 5, 5, 5 },
                new int[] { 2, 2, 2 },
                new int[] { 3, 3, 3 }
            };

            // MANUAL Positions
            Point[] manualPositions = new Point[]
            {
                new Point(0, 700),
                new Point(450, 700),
                new Point(700, 700),
                new Point(1050, 700),
                new Point(0, 400),
                new Point(1100, 100)
            };

            // RANDOM Tiles and Positions
            int[] randomTilePlatforms = new int[] { 2, 3, 4, 4, 2, 3, 4, 3, 3, 4, 2, 2, 4, 2, 2, 2, 1, 2, 1, 1 };
            Point[] randomPositions = new Point[]
            {
                new Point(300, 600),
                new Point(900, 600),
                new Point(575, 600),
                new Point(100, 500),
                new Point(700, 500),
                new Point(1100, 500),
                new Point(400, 400),
                new Point(950, 400),
                new Point(625, 300),
                new Point(150, 300),
                new Point(1100, 300),
                new Point(450, 200),
                new Point(775, 200),
                new Point(100, 200),
                new Point(0, 100),
                new Point(200, 100),
                new Point(375, 100),
                new Point(500, 100),
                new Point(1000, 100),
                new Point(675, 100)

            };

            // Load platform texture
            Texture2D tileTexture = Content.Load<Texture2D>("Platform 8");

            int tileWidth = tileTexture.Width / 6;
            int tileHeight = tileTexture.Height;
            int tileSize = 50;

            int a = 0;

            // Tile Count
            int totalManualTiles = manualTilePlatforms.Sum(row => row.Length);
            int totalRandomTiles = randomTilePlatforms.Sum();

            tiles = new GameTiles[totalManualTiles + totalRandomTiles];

            // Add Manual Tiles
            for (int row = 0; row < manualTilePlatforms.Length; row++)
            {
                int[] tileDesigns = manualTilePlatforms[row];
                int posX = manualPositions[row].X;
                int posY = manualPositions[row].Y;

                for (int i = 0; i < tileDesigns.Length; i++)
                {
                    int tileManualCount = tileDesigns[i];
                    Rectangle tileSource = new Rectangle(tileWidth * tileManualCount, 0, tileWidth, tileHeight);
                    Rectangle tileDisplay = new Rectangle(posX, posY, tileSize, tileSize);
                    Color tileColor = Color.White;

                    tiles[a++] = new GameTiles(tileTexture, tileDisplay, tileSource, tileColor);
                    posX += tileSize;
                }
            }

            // Add Random Tiles
            for (int row = 0; row < randomTilePlatforms.Length; row++)
            {
                int tileCount = randomTilePlatforms[row];
                int posX = randomPositions[row].X;
                int posY = randomPositions[row].Y;

                for (int i = 0; i < tileCount; i++)
                {
                    int tileRandomCount = random.Next(0, 4);
                    Rectangle tileSource = new Rectangle(tileWidth * tileRandomCount, 0, tileWidth, tileHeight);
                    Rectangle tileDisplay = new Rectangle(posX, posY, tileSize, tileSize);
                    Color tileColor = Color.White;

                    tiles[a++] = new GameTiles(tileTexture, tileDisplay, tileSource, tileColor);
                    posX += tileSize;
                }
            }
            #endregion

            #region Leaves
            Texture2D leavesTexture = Content.Load<Texture2D>("Leave 2");

            int leafWidth = leavesTexture.Width / 4;
            int leafHeight = leavesTexture.Height;

            int leafSize = 50;

            int[] leafPlace = new int[] { 1, 1, 1, 0, 0, 1, 1, 1 };

            Point[] leavesPositions = new Point[]
            {
                new Point(175, 650),
                new Point(200, 650),
                new Point(225, 650),

                new Point(250, 250),
                new Point(300, 250),

                new Point(1125, 50),
                new Point(1150, 50),
                new Point(1175, 50)
            };

            leaves = new GameLeaves[leavesPositions.Length];

            for (int i = 0; i < leavesPositions.Length; i++)
            {
                int leafCount = leafPlace[i];
                Rectangle leafSource = new Rectangle(leafWidth * leafCount, 0, leafWidth, leafHeight);
                Rectangle leafDisplay = new Rectangle(leavesPositions[i].X, leavesPositions[i].Y, leafSize, leafSize);

                leaves[i] = new GameLeaves(leavesTexture, leafDisplay, leafSource, Color.White);
            }
            #endregion

            #region Signs
            Texture2D signTexture = Content.Load<Texture2D>("Signs");

            int signWidth = signTexture.Width / 12;
            int signHeight = signTexture.Height;

            int signSize = 50;

            int[] signPlace = new int[] { 4, 6, 0, 11, 11, 4 };

            Point[] signPositions = new Point[]
            {
                new Point(200, 650),
                new Point(1100, 650),
                new Point(1150, 50),
                new Point(500, 350),
                new Point(375, 50),
                new Point(900, 150)
            };

            signs = new GameSigns[signPositions.Length];

            for (int i = 0; i < signPositions.Length; i++)
            {
                int signCount = signPlace[i];
                Rectangle signSource = new Rectangle(signWidth * signCount, 0, signWidth, signHeight);
                Rectangle signDisplay = new Rectangle(signPositions[i].X, signPositions[i].Y, signSize, signSize);

                signs[i] = new GameSigns(signTexture, signDisplay, signSource, Color.White);
            }
            #endregion

            #region Plants
            Texture2D plantTexture = Content.Load<Texture2D>("Plants 1");

            int plantWidth = plantTexture.Width / 4;
            int plantHeight = plantTexture.Height;

            int plantSize = 50;

            int[] plantPlace = new int[] { 2, 1, 3, 2, 3, 0, 1, 2, 1, 2 };

            Point[] plantPositions = new Point[]
            {
                new Point(0, 650),
                new Point(700, 650),
                new Point(1100, 650),
                new Point(1125, 650),
                new Point(250, 450),
                new Point(150, 450),
                new Point(1000, 550),

                new Point(150, 250),
                new Point(650, 250),
                new Point(1100, 250),
            };

            plant = new GamePlants[plantPositions.Length];

            for (int i = 0; i < plantPositions.Length; i++)
            {
                int plantCount = plantPlace[i];
                Rectangle plantSource = new Rectangle(plantWidth * plantCount, 0, plantWidth, plantHeight);
                Rectangle plantDisplay = new Rectangle(plantPositions[i].X, plantPositions[i].Y, plantSize, plantSize);

                plant[i] = new GamePlants(plantTexture, plantDisplay, plantSource, Color.White);
            }
            #endregion

            #region Stones
            Texture2D stoneTexture = Content.Load<Texture2D>("Stones 1");

            int stoneWidth = stoneTexture.Width / 4;
            int stoneHeight = stoneTexture.Height;

            int stoneSize = 50;

            int[] stonePlace = new int[] { 2, 3, 2 };

            Point[] stonePositions = new Point[]
            {
                new Point(0, 50),
                new Point(700, 250),
                new Point(600, 550)
            };

            stone = new GameStones[stonePositions.Length];

            for (int i = 0; i < stonePositions.Length; i++)
            {
                int stoneCount = stonePlace[i];
                Rectangle stoneSource = new Rectangle(stoneWidth * stoneCount, 0, stoneWidth, stoneHeight);
                Rectangle stoneDisplay = new Rectangle(stonePositions[i].X, stonePositions[i].Y, stoneSize, stoneSize);

                stone[i] = new GameStones(stoneTexture, stoneDisplay, stoneSource, Color.White);
            }
            #endregion

            #region Spikes
            Texture2D spikeTexture = Content.Load<Texture2D>("Spike 3");

            int spikeWidth = spikeTexture.Width;
            int spikeHeight = spikeTexture.Height;

            int spikeSize = 20;

            Point[] spikePositions = new Point[]
            {
                new Point(840, 182),
                new Point(820, 182),
                new Point(860, 182),

                new Point(15, 382),

                new Point(495, 382),
                new Point(475, 382),

                new Point(1045, 382),
                new Point(1065, 382),
            };

            spike = new GameSpikes[spikePositions.Length];

            for (int i = 0; i < spikePositions.Length; i++)
            {
                Rectangle spikeSource = new Rectangle(0, 0, spikeWidth, spikeHeight);
                Rectangle spikeDisplay = new Rectangle(spikePositions[i].X, spikePositions[i].Y, spikeSize, spikeSize);

                spike[i] = new GameSpikes(spikeTexture, spikeDisplay, spikeSource, Color.White);
            }
            #endregion

            #region Hero
            heroTexture = Content.Load<Texture2D>("HeroKnight");

            heroWidth = heroTexture.Width / 10;
            heroHeight = heroTexture.Height / 9;

            heroDisplay = new Rectangle(25, 650, heroWidth, heroHeight);
            heroSource = new Rectangle(0, 0, heroWidth, heroHeight);
            heroColor = Color.White;
            #endregion

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState key = Keyboard.GetState();
            prevKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();

            bool isWalkRun = false;
            bool onPlatform = false;

            #region Ground Check
            groundY = Window.ClientBounds.Height;

            // Hero Box size for Collision
            Rectangle heroBox = new Rectangle(
                heroDisplay.X + heroDisplay.Width / 5,
                heroDisplay.Y + heroDisplay.Height,
                heroDisplay.Width / 5,
                5
            );

            // Check Collision
            foreach (GameTiles tile in tiles)
            {
                Rectangle tileBox = tile.TilesDisplay;

                if (velocityY >= 0 && heroBox.Intersects(tileBox))
                {
                    groundY = tileBox.Top - heroDisplay.Height;
                    onPlatform = true;
                    break;
                }
            }
            #endregion

            #region Spike Check
            Rectangle heroHitBox = new Rectangle(
                heroDisplay.X + heroDisplay.Width / 5,
                heroDisplay.Y + heroDisplay.Height / 4,
                heroDisplay.Width / 2,
                heroDisplay.Height / 2
            );

            foreach (GameSpikes sp in spike)
            {
                if (!isHit && heroHitBox.Intersects(sp.SpikeDisplay))
                {
                    isHit = true;
                    heroCounter = 0;
                    hitDelay = 0;
                    break;
                }
            }
            #endregion

            #region Idle
            if (!isWalkRun && !isJumping && onPlatform)
            {
                idleDelay++;

                if (idleDelay > 10)
                {
                    idleDelay = 0;
                    heroCounter++;

                    if (heroCounter > 7)
                    {
                        heroCounter = 0;
                    }

                    heroSource.X = heroWidth * heroCounter;
                    heroSource.Y = heroHeight * 0;
                }
            }
            #endregion

            #region Walk/Run
            // Move RIGHT
            if (key.IsKeyDown(Keys.Right))
            {
                heroDisplay.X += 3;
                heroEffect = SpriteEffects.None;
                isWalkRun = true;
            }

            // Move LEFT
            if (key.IsKeyDown(Keys.Left))
            {
                heroDisplay.X -= 3;
                heroEffect = SpriteEffects.FlipHorizontally;
                isWalkRun = true;
            }

            // Walk/Run ANIMATION
            if (isWalkRun)
            {
                walkDelay++;

                if (walkDelay > 4)
                {
                    walkDelay = 0;
                    heroCounter++;

                    if (heroCounter > 4)
                    {
                        heroCounter = 0;
                    }

                    if (heroCounter <= 2)
                    {
                        heroSource.X = heroWidth * (7 + heroCounter);
                        heroSource.Y = heroHeight * 0;
                    }
                    else
                    {
                        heroSource.X = heroWidth * (heroCounter - 3);
                        heroSource.Y = heroHeight * 1;
                    }
                }
            }
            #endregion

            #region Jump
            // Jump BUFFER
            if (currentKeyState.IsKeyDown(Keys.Space) && prevKeyState.IsKeyUp(Keys.Space))
            {
                jumpBufferCounter = jumpBufferTime;
            }

            // Jump START
            if (jumpBufferCounter > 0 && !isJumping && onPlatform)
            {
                isJumping = true;
                velocityY = jumpSpeed;
                heroCounter = 0;
                jumpBufferCounter = 0;
                onPlatform = false;
            }

            // Jump ACTION
            if (isJumping)
            {
                velocityY += gravity;
                heroDisplay.Y += (int)velocityY;

                // Jump ANIMATION
                if (heroCounter == 0)
                {
                    heroSource.X = heroWidth * 8;
                    heroSource.Y = heroHeight * 3;
                }
                else if (heroCounter == 1)
                {
                    heroSource.X = heroWidth * 9;
                    heroSource.Y = heroHeight * 3;
                }
                else
                {
                    heroSource.X = heroWidth * 0;
                    heroSource.Y = heroHeight * 4;
                }

                jumpDelay++;
                if (jumpDelay > 8)
                {
                    jumpDelay = 0;
                    heroCounter++;
                    if (heroCounter > 2)
                    {
                        heroCounter = 2;
                    }
                }

                // Jump LANDING
                if (heroDisplay.Y >= groundY)
                {
                    heroDisplay.Y = groundY;
                    isJumping = false;
                    velocityY = 0f;
                    heroCounter = 0;
                }
                else if (!isJumping)
                {
                    velocityY = 0f;
                    heroDisplay.Y = groundY;
                }
            }

            if (jumpBufferCounter > 0)
            {
                jumpBufferCounter--;
            }
            #endregion

            #region Fall
            // Fall if not on platform
            if (!isJumping && !onPlatform)
            {
                velocityY += gravity;
                heroDisplay.Y += (int)velocityY;

                // Fall Animation
                fallDelay++;
                if (fallDelay > 8)
                {
                    fallDelay = 0;
                    heroCounter++;

                    if (heroCounter > 3)
                        heroCounter = 0;
                }

                if ((heroCounter + 1) > 4)
                {
                    heroCounter = 1;
                }

                heroSource.X = heroWidth * (heroCounter + 1);
                heroSource.Y = heroHeight * 4;
            }
            else if (!isJumping)
            {
                velocityY = 0f;
                heroDisplay.Y = groundY;
            }

            // Respawn in Start Pos if Fall below
            if (heroDisplay.Y > Window.ClientBounds.Height)
            {
                heroDisplay.X = 25;
                heroDisplay.Y = 650;
                velocityY = 0f;
                isJumping = false;
            }
            #endregion

            #region Hit
            if (isHit)
            {
                hitDelay++;
                if (hitDelay > 10)
                {
                    hitDelay = 0;
                    heroCounter++;
                    if (heroCounter > 2)
                    {
                        // Respawn in Start Pos if Hit Spike
                        isHit = false;
                        heroCounter = 0;
                        heroDisplay.X = 25;
                        heroDisplay.Y = 650;
                        heroEffect = SpriteEffects.None;
                        velocityY = 0f;
                        isJumping = false;
                    }
                }
                heroSource.X = heroWidth * (5 + heroCounter);
                heroSource.Y = heroHeight * 4;

                return;
            }
            #endregion

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(gameBackground.BgTexture, gameBackground.BgRectangle, gameBackground.BgColor);

            foreach (GameSpikes sp in spike)
            {
                _spriteBatch.Draw(sp.SpikeTexture, sp.SpikeDisplay, sp.SpikeSource, sp.SpikeColor);
            }

            foreach (GameTiles gt in tiles)
            {
                _spriteBatch.Draw(gt.TilesTexture, gt.TilesDisplay, gt.TileSource, gt.TileColor);
            }

            foreach (GameStones st in stone)
            {
                _spriteBatch.Draw(st.StoneTexture, st.StoneDisplay, st.StoneSource, st.StoneColor);
            }

            _spriteBatch.Draw(heroTexture, heroDisplay, heroSource, heroColor, 0f, Vector2.Zero, heroEffect, 0f);

            foreach (GameLeaves l in leaves)
            {
                _spriteBatch.Draw(l.LeavesTexture, l.LeavesDisplay, l.LeavesSource, l.LeavesColor);
            }

            foreach (GamePlants p in plant)
            {
                _spriteBatch.Draw(p.PlantTexture, p.PlantDisplay, p.PlantSource, p.PlantColor);
            }

            foreach (GameSigns si in signs)
            {
                _spriteBatch.Draw(si.SignTexture, si.SignDisplay, si.SignSource, si.SignColor);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
