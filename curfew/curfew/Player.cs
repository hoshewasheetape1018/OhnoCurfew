using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace curfew
{
    internal class Player : Character

    {
        public Player(int xpos, int ypos, Texture2D charaTexture, int windowWidth, int windowHeight) : base(xpos, ypos, charaTexture, windowWidth, windowHeight)
        {
        }

        public void Update(KeyboardState key)
        {

            // Horizontal movement
            if (key.IsKeyDown(Keys.Left))
            {
                xpos -= moveSpeed;

            }
            else if (key.IsKeyDown(Keys.Right))
            {
                xpos += moveSpeed;

            }

            // Jump input
            if ((key.IsKeyDown(Keys.Up) || key.IsKeyDown(Keys.Space)) && isGrounded)
            {

            }

            // Out of bounds reset
            if (ypos > +200)
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
