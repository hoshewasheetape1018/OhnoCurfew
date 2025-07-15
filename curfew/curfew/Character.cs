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
        internal SpriteEffects flip;


        //PLAYER STATS

        //HP
        int maxlife;
        int currentlife;

        //ATTACK
        int attack;
        public Rectangle attackHitbox;
        public int attackDuration = 10;
        internal int attackTimer = 0;

        //PHYSICS
        public Physics physics;
        internal int moveSpeed = 10;
        internal float jumpStrength = -20f;
        internal float velocityY = 0f;


        //BOOL
        internal bool isWalkRun;
        internal bool onPlatform;
        internal bool isDead;
        internal bool isHit;
        internal bool facingLeft;
        internal bool isGrounded;
        internal bool isJumping;
        internal bool isMoving;
        public bool isAttacking;


        // COLLISION BOX
        public Rectangle collisionBox;

        //ANIMATION
        private int currentFrame;
        private int previousStartFrame;
        private int delay;
        private int frameWidth;
        private int frameHeight;
        private int knockbackDuration;
        internal bool wasJumpingLastFrame = false;

        // OTHERS
        KeyboardState currentKeyState;
        KeyboardState prevKeyState;
        private SpriteBatch _spriteBatch;

        int jumpBufferTime = 6;
        int jumpBufferCounter = 0;

        public Character(int xpos, int ypos, string state, Texture2D charaTexture)
        {
            this.xpos = xpos;
            this.ypos = ypos;
            this.state = state;

            this.charaTexture = charaTexture;


            charaWidth = charaTexture.Width;
            charaHeight = charaTexture.Height;
            collisionBox = new Rectangle(xpos, ypos, charaWidth, charaHeight);
            physics = new Physics(this);

        }

        public void characterState(string state, Texture2D charaIdle, Texture2D charaWalk, Texture2D charaJump, Texture2D charaFall, Texture2D charaAttack, Texture2D charaHit, Texture2D charaDead)
        {
            switch (state)
            {
                case ("idle"):
                    charaTexture = charaIdle;
                    Animate(0, 4);
                    break;
                case ("walkrun"):
                    charaTexture = charaWalk;
                    Animate(0, 4);
                    break;
                case ("jump"):
                    charaTexture = charaJump;
                    Animate(0, 4);
                    break;
                case ("fall"):
                    charaTexture = charaFall;
                    Animate(0, 1);
                    break;
                case ("attack"):
                    charaTexture = charaAttack;
                    Animate(0,1);
                    break;
                case ("hit"): // + add knockback
                    charaTexture = charaHit;
                    Animate(0,1);
                    break;
                case ("dead"):
                    charaTexture = charaDead;
                    Animate(0,4);
                    break;
                default:
                    break;
            }

        }

        void Animate(int startFrame, int frameCount)
        {
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

                if (currentFrame >= frameCount)
                    currentFrame = startFrame;

                delay = 0;
            }

            int frameWidth = charaTexture.Width / frameCount;
            int frameHeight = charaTexture.Height;

            sourceRectangle = new Rectangle(
                currentFrame * frameWidth,
                0,
                frameWidth,
                frameHeight
            );
        }


        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(charaTexture, new Rectangle(xpos, ypos, 163, 163), sourceRectangle, Color.White, 0, Vector2.Zero, flip, 0);
        }
    }
}


