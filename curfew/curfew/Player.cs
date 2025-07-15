using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;

namespace curfew
{
    internal class Player : Character

    {
        internal int windowHeight;

        public Player(int xpos, int ypos, string state, Texture2D charaTexture) : base(xpos, ypos, state, charaTexture)
        {
            this.xpos = xpos;
            this.ypos = ypos;
        }

        public void Update( GameTiles[] tiles, KeyboardState key)
        {
            characterState();
            keyboardInput(key);
            physics.ApplyPhysics(tiles[0], key);
            resetPlayerPos(windowHeight);

        }

        public void keyboardInput(KeyboardState key)
        {
            isMoving = false;
            bool left = key.IsKeyDown(Keys.Left);
            bool right = key.IsKeyDown(Keys.Right);
            bool jump = key.IsKeyDown(Keys.Up) || key.IsKeyDown(Keys.Space);


            if (left)
            {
                xpos -= moveSpeed;
                facingLeft = true;
                isMoving = true;
            }
            else if (right)
            {
                xpos += moveSpeed;
                facingLeft = false;
                isMoving = true;

            }

            if (jump && isGrounded && !wasJumpingLastFrame)
            {
                physics.Jump();
            }

            wasJumpingLastFrame = jump;
            flip = facingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None;



            // DEBUG
            //Console.WriteLine($"isMoving: {isMoving}, isGrounded: {isGrounded}, state: {state}");
        }

        public void resetPlayerPos(int windowHeight)
        {
            if ( ypos > 1000)
            {
                Console.WriteLine("reset");
                ypos = startYpos;
                xpos = startXpos;
                velocityY = 0;
                isJumping = false;
                isGrounded = false;
            }

        }

    }
}
