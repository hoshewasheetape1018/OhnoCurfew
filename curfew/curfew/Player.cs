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
            Console.WriteLine("Player moved to: " + this.xpos + ", " + this.ypos);

            
            // Horizontal movement
            if (key.IsKeyDown(Keys.Left))
            {
                xpos -= moveSpeed;
                facingLeft = true;
                isMoving = true;
            }
            else if (key.IsKeyDown(Keys.Right))
            {
                xpos += moveSpeed;
                facingLeft = false;
                isMoving = true;
            }


            if (facingLeft)
            {
                flip = SpriteEffects.FlipHorizontally;
            }
            else { flip = SpriteEffects.None; }
        }

        public void checkXposOOB(int windowWidth)
        {
            if (xpos > windowWidth)
            {
                Console.WriteLine("Out of X bounds");
                xpos = startXpos;
            }

        }
        public void checkYposOOB(int windowHeight)
        {
            if (ypos > windowHeight)
            {
                Console.WriteLine("Out of Y bounds");
                ypos = startYpos;
            }

        }

    }
}
