using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace curfew
{

    internal class Character
    {
        //PLAYER PROPERTIES

        internal int ypos;
        internal int xpos;
        internal int startXpos;
        internal int startYpos;
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
        internal float jumpStrength = -12f;
        internal float gravity = 0.55f;
        internal float velocityY = 0f;
        int groundY;
        int knockbackVelocity;


        //BOOL
        internal bool isWalkRun;
        internal bool onPlatform;
        internal bool isDead;
        internal bool isHit;
        internal bool facingDirection; // right = false, left = true
        internal bool isGrounded;
        internal bool isJumping;

        // COLLISION BOX
        Rectangle collisionBox;

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


            collisionBox = new Rectangle(xpos, ypos, charaWidth, charaHeight);
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

        public void characterPhysics(GameTiles[] tiles)
        {
            // Check Collision
            foreach (GameTiles tile in tiles)
            {
                Rectangle tileBox = tile.TilesDisplay;

                if (velocityY >= 0 && collisionBox.Intersects(tileBox))
                {
                    groundY = tileBox.Top - charaHeight;
                    onPlatform = true;
                    break;
                }
            }
        }

        public void characterState(string state, int idleStart, int idleLast, int walkStart, int walkLast, int jumpStart, int jumpLast, int fallStart, int fallLast, int attackStart, int attackLast, int hitStart, int hitLast, int deadStart, int deadLast)
        {
            switch (state)
            {
                case ("idle"):
                    Animate(idleStart, idleLast, 4);
                    break;
                case ("walkrun"):
                    Animate(walkStart, walkLast, 0);
                    break;
                case ("jump"):
                    Animate(jumpStart, jumpLast, 4);
                    break;
                case ("fall"):
                    Animate(fallStart, fallLast, 4);
                    break;
                case ("attack"):
                    Animate(attackStart, attackLast, 4);
                    break;
                case ("hit"): // + add knockback
                    Animate(hitStart, hitLast, 4);
                    break;
                case ("dead"):
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


        public void Jump(GameTiles tiles)
        {
            velocityY += gravity;
            collisionBox.Y += (int)velocityY;

            if (collisionBox.Intersects(tiles.tilesDisplay))
            {
                if (velocityY > 0)
                {
                    isGrounded = true;
                    isJumping = false;
                }
                velocityY = 0;
            }
            velocityY = jumpStrength;
            isJumping = true;
            isGrounded = false;
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

    }
}


