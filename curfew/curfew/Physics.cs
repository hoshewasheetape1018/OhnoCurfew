using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace curfew
{
    internal class Physics
    {
        // Physics values
        internal float gravity = 0.55f;
        internal float jumpStrength = -20f;

        private Character chara;

        // Constructor
        public Physics(Character chara)
        {
            this.chara = chara;
        }


        public void ApplyPhysics(GameTiles tile, KeyboardState key)
        {

            float previousY = chara.ypos;
            chara.velocityY += gravity;
            chara.ypos += (int)chara.velocityY;

            // Update collision box
            chara.collisionBox.X = chara.xpos;
            chara.collisionBox.Y = chara.ypos;

            // Ground collision detection: only snap if falling
            if (chara.collisionBox.Intersects(tile.TilesDisplay) && chara.velocityY >= 0)
            {
                chara.ypos = tile.TilesDisplay.Top - chara.charaHeight;
                chara.velocityY = 0;
                chara.isGrounded = true;
                chara.isJumping = false;
                chara.collisionBox.Y = chara.ypos;
            }
            // Determine state
            if (!chara.isGrounded)
            {
                Console.WriteLine("called jumpfall");
                chara.state = chara.velocityY > 0 ? "fall" : "jump";
            }
            else if (chara.isMoving)
            {
                Console.WriteLine("called moving");
                chara.state = "walkrun";
            }
            else
            {
                Console.WriteLine("called idle");
                chara.state = "idle";
            }



        }


        // Jump
        public void Jump()
            {
            Console.WriteLine("Called");
                chara.velocityY = jumpStrength;
                chara.isJumping = true;
                chara.isGrounded = false;
            chara.wasJumpingLastFrame = true;
            }
        }
    }
