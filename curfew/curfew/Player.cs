using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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

        public void Move(KeyboardState key)
        {

            // Horizontal movement
            if (key.IsKeyDown(Keys.Left))
            {
                xpos -= moveSpeed;
                Console.WriteLine("Left");
                Console.WriteLine(xpos);
            }
            else if (key.IsKeyDown(Keys.Right))
            {
                xpos += moveSpeed;
                Console.WriteLine("Right");
                Console.WriteLine(xpos);

            }

            // Jump input
            if ((key.IsKeyDown(Keys.Up) || key.IsKeyDown(Keys.Space)) && isGrounded)
            {
                Jump(tiles);

            }

            // Out of bounds reset
            if (ypos > windowHeight)
            {
                ypos = startYpos;
                xpos = startXpos;
                velocityY = 0;
                isJumping = false;
                isGrounded = false;
            }

        }

    }
}
