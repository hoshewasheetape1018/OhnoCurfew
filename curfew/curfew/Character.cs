using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace curfew
{

    internal abstract class Character
    {
        //PLAYER PROPERTIES

        internal int ypos;
        internal int xpos;
        internal int startXpos = 69;
        internal int startYpos = 69;
        internal int charaWidth;
        internal int charaHeight;
        internal int velocity;
        internal Texture2D charaTexture;
        internal Rectangle sourceRectangle;
        internal string state;

        //PLAYER STATS

        //HP
        int maxlife;
        int currentlife;

        //ATTACK
        int attack;


        //PLAYER ATTRIBUTES + PHYSICS:
        internal int moveSpeed = 10;
        internal float jumpStrength = -20f;
        internal float gravity = 0.55f;
        internal float velocityY = 0f;
        int groundY;
        int knockbackVelocity;
        internal SpriteEffects flip;

        //BOOL
        internal bool isWalkRun;
        internal bool onPlatform;
        internal bool isDead;
        internal bool isHit;
        internal bool facingLeft;
        internal bool isGrounded;
        internal bool isJumping;
        internal bool isMoving;


        // COLLISION BOX
        public Rectangle collisionBox;

        // OTHERS
        KeyboardState currentKeyState;
        KeyboardState prevKeyState;
        private SpriteBatch _spriteBatch;
        internal GameTiles tiles;

        int jumpBufferTime = 6;
        int jumpBufferCounter = 0;

        //ANIMATION
        private int currentFrame;
        private int previousStartFrame;
        private int delay;
        private int frameWidth;
        private int frameHeight;
        private int knockbackDuration;
        private bool wasJumpingLastFrame = false;

        public Character(int xpos, int ypos, string state, Texture2D charaTexture)
        {
            this.xpos = xpos;
            this.ypos = ypos;
            this.state = state;

            this.charaTexture = charaTexture;
            charaWidth = charaTexture.Width;
            charaHeight = charaTexture.Height;

            frameWidth = charaWidth;
            frameHeight = charaHeight;


            collisionBox = new Rectangle(this.xpos, this.ypos, charaWidth, charaHeight);
        }

        public GameTiles getTiles(GameTiles tiles)
        {
            return tiles;
        }

        public void getcharaSize(int width, int height)
        {
            charaWidth = width;
            charaHeight = height;
        }

        public void characterPhysics(GameTiles[] tiles, KeyboardState key)
        {
            // Apply gravity
            velocityY += gravity;
            ypos += (int)velocityY;

            // Update collision box
            collisionBox.X = xpos;
            collisionBox.Y = ypos;

            // Ground check (land on tile)
            isGrounded = false;
            foreach (var tile in tiles)
            {
                if (collisionBox.Intersects(tile.TilesDisplay))
                {
                    ypos = tile.TilesDisplay.Top - charaHeight;
                    velocityY = 0;
                    isGrounded = true;
                    isJumping = false;

                    collisionBox.Y = ypos;
                    break;
                }
            }

            // Jump
            ypos += (int)velocityY;
            collisionBox.Y = ypos;

            if ((key.IsKeyDown(Keys.Up) || key.IsKeyDown(Keys.Space)) && isGrounded && !wasJumpingLastFrame)
            {
                velocityY = jumpStrength;
                isJumping = true;
                isGrounded = false;
            }

            // Apply gravity
            velocityY += gravity;

            if (!isGrounded)
            {
                if (velocityY > 0)
                    state = "fall";
                else
                    state = "jump";
            }
            else
            {
                state = "idle";
            }
            if (isMoving)
            {
                state = "walkrun";
            }

            isMoving = key.IsKeyDown(Keys.Left) || key.IsKeyDown(Keys.Right);

            wasJumpingLastFrame = key.IsKeyDown(Keys.Up) || key.IsKeyDown(Keys.Space);

        }


        public void characterState(string state, int idleStart, int idleLast, Texture2D charaIdle, int walkStart, int walkLast, Texture2D charaWalk, int jump, Texture2D charaJump, int fall, Texture2D charaFall, int attackStart, int attackLast, Texture2D charaAttack, int hitStart, int hitLast, Texture2D charaHit, int deadStart, int deadLast, Texture2D charaDead)
        {
            switch (state)
            {
                case ("idle"):
                    charaTexture = charaIdle;
                    Animate(idleStart, idleLast, 4);
                    break;
                case ("walkrun"):
                    charaTexture = charaWalk;
                    Animate(walkStart, walkLast, 4);
                    break;
                case ("jump"):
                    charaTexture = charaJump;
                    Animate(jump, jump, 1);
                    break;
                case ("fall"):
                    charaTexture = charaFall;
                    Animate(fall, fall, 1);
                    break;
                case ("attack"):
                    charaTexture = charaAttack;
                    Animate(attackStart, attackLast, 4);
                    break;
                case ("hit"): // + add knockback
                    charaTexture = charaHit;
                    Animate(hitStart, hitLast, 4);
                    break;
                case ("dead"):
                    charaTexture = charaDead;
                    Animate(deadStart, deadLast, 4);
                    break;
                default:
                    break;
            }

        }



        void Animate(int startFrame, int endFrame, int framesPerRow)
        {
            if (startFrame != previousStartFrame)
            {
                currentFrame = startFrame;
                previousStartFrame = startFrame;
                delay = 0;
            }

            delay++;

            if (delay > 4) // animation speed
            {
                currentFrame++;

                if (currentFrame > endFrame)
                    currentFrame = startFrame;

                delay = 0;
            }

            int frameWidth = charaTexture.Width / framesPerRow;
            int frameHeight = charaTexture.Height;

            sourceRectangle = new Rectangle(
                currentFrame * frameWidth,
                0,
                frameWidth,
                frameHeight
            );
        }




        public void Hit()
        {
            if (knockbackDuration > 0)
            {
                knockbackDuration -= knockbackVelocity;
                xpos += knockbackVelocity;
                ypos += knockbackVelocity;
                return; // skip rest of movement while in knockback
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(charaTexture, new Rectangle(xpos, ypos, 163, 163), sourceRectangle, Color.White, 0, Vector2.Zero, flip, 0);
        }
    }
}


