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

        public Player(int xpos, int ypos, string state, Texture2D charaTexture, int frameCount) : base(xpos, ypos, state, charaTexture, frameCount)
        {
            this.xpos = xpos;
            this.ypos = ypos;
        }

        public override void Update(GameTiles[] tiles, KeyboardState key, List<Enemy> enemies)
        {
            if (iFrames > 0) iFrames--;
            if (isFlashing)
            {
                flashTimer--;
                if (flashTimer <= 0)
                    isFlashing = false;
            }

            ApplyKnockback();

            if (knockbackFrames > 0)
            {
                HandleAttack(enemies); 
                physics.ApplyPhysics(tiles[0], key);
                return;
            }

            keyboardInput(key);
            if (key.IsKeyDown(Keys.Z) && !isAttacking)
                StartAttack();

            HandleAttack(enemies);
            characterState();
            physics.ApplyPhysics(tiles[0], key);
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
            cboxOffset = facingLeft ? 30 : 35;



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
