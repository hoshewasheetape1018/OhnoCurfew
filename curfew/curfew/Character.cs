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

        //PLAYER STATS

        //HP
        int maxlife;
        int currentlife;

        //ATTACK
        int attack;


        //PLAYER ATTRIBUTES + PHYSICS:
        internal int moveSpeed = 3;
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


        int jumpBufferTime = 6;
        int jumpBufferCounter = 0;

        //ANIMATION
        private int currentFrame;
        private int previousStartFrame;
        private int delay;
        private int frameWidth;
        private int frameHeight;
        private int knockbackDuration;

        public Character(int xpos, int ypos, Texture2D charaTexture, int windowWidth, int windowHeight)
        {
            this.xpos = xpos;
            this.ypos = ypos;

            startXpos = xpos;
            startYpos = ypos;

            this.charaTexture = charaTexture;
            charaWidth = charaTexture.Width;
            charaHeight = charaTexture.Height;
            collisionBox = new Rectangle(xpos, ypos, charaWidth, charaHeight);
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
                    Console.WriteLine("idle");
                    Animate(idleStart, idleLast);
                    break;
                case ("walkrun"):
                    Console.WriteLine("walkrun");
                    Animate(walkStart, walkLast);
                    break;
                case ("jump"):
                    Console.WriteLine("jump");
                    Animate(jumpStart, jumpLast);

                    break;
                case ("fall"):
                    Console.WriteLine("fall");
                    Animate(fallStart, fallLast);
                    break;
                case ("attack"):
                    Console.WriteLine("attack");
                    Animate(attackStart, attackLast);
                    break;
                case ("hit"): // + add knockback
                    Console.WriteLine("hit");
                    Animate(hitStart, hitLast);
                    break;
                case ("dead"):
                    Console.WriteLine("dead");
                    Animate(deadStart, deadLast);
                    break;
                default:
                    break;
            }

        }



        void Animate(int startFrame, int endFrame)
        {
            // Reset currentFrame if animation range changed
            if (startFrame != previousStartFrame)
            {
                currentFrame = startFrame;
                previousStartFrame = startFrame;
                delay = 0;
            }

            delay++;

            if (delay > 4)
            {
                currentFrame++;

                if (currentFrame > endFrame)
                    currentFrame = startFrame;

                delay = 0;
            }

            int framePerRow = 9;
            int row = currentFrame / framePerRow;
            int col = currentFrame % framePerRow;

            sourceRectangle = new Rectangle(
                col * frameWidth,
                row * frameHeight,
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
