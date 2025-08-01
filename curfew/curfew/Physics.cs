﻿using Microsoft.Xna.Framework;
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
        internal float gravity = 0.65f;
        internal float jumpStrength = -10f;

        private Character chara;

        // Constructor
        public Physics(Character chara)
        {
            this.chara = chara;
        }


        public void ApplyPhysics(GameTiles tile, KeyboardState key)
        {

            float previousY = chara.ypos;
            if (chara.velocityY > 0) // falling
            {
                chara.velocityY += gravity * 1.5f; // Fall multiplier
            }
            else if (chara.velocityY < 0) // jumping upward
            {
                chara.velocityY += gravity * 0.75f; // Optional: slower gravity when rising
            }
            else
            {
                chara.velocityY += gravity; // idle? probably won't hit this much
            }

            chara.ypos += (int)chara.velocityY;
            // Update collision box
            chara.collisionBox.X = chara.xpos+chara.cboxOffset;
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
                chara.state = chara.velocityY > 0 ? "fall" : "jump";
            }
            else if (chara.isMoving)
            {
                chara.state = "walkrun";
            }
            else
            {
                chara.state = "idle";
            }


        }


        // Jump
        public void Jump()
            {
                chara.velocityY = jumpStrength;
                chara.isJumping = true;
                chara.isGrounded = false;
            chara.wasJumpingLastFrame = true;
            }
        }
    }
