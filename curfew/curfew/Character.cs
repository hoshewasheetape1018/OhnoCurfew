using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace curfew
{

    //plan:
    // Add HP, Physics, 
    // Constructor: xpos, ypos

    internal class Character
    {
        //PLAYER PROPERTIES
        int ypos;
        int xpos;
        int charaWidth;
        int charaHeight;
        int velocity;


        //PLAYER STATS

        //HP
        int maxlife;
        int currentlife;

        //ATTACK
        int attack;


        //PLAYER ATTRIBUTES + PHYSICS:
        int moveSpeed;
        float jumpSpeed = -12f;
        float gravity = 0.55f;
        float velocityY = 0f;
        int groundY;

        //BOOL
        bool isWalkRun;
        bool onPlatform;
        bool isDead;
        bool isHit;
        bool facingDirection; // right = false, left = true


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
        private Rectangle sourceRectangle;

        public Character(int xpos, int ypos, Rectangle charaDisplayRectangle)
        {
            this.ypos = ypos;
            this.xpos = xpos;
            collisionBox = new Rectangle(xpos, ypos, charaWidth, charaHeight);
        }

        public void getcharaSize(int width, int height)
        {
            charaWidth = width;
            charaHeight = height;
        }
        public void characterPhysics(GameTiles tiles)
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

        public void characterDraw(Texture2D charaTexture)
        {
            _spriteBatch.Draw(charaTexture, new Vector2(xpos, ypos), sourceRectangle, Color.White);
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

    }
}
