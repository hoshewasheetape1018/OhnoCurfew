﻿using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curfew
{
    internal class Debug
    {
        int windowWidth;
        int windowHeight;
        bool checkState = false;



        public Debug(int windowWidth, int windowHeight)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
        }

        public void playerInfo(Player player)
        {
            Console.WriteLine("Window width: " + windowWidth + " Window Height: " + windowHeight + " Xpos in Game1: " + player.xpos + " Ypos in Game1: " + player.ypos);

            //Console.WriteLine("Player height: " + player.charaHeight + " Player width: " + player.charaWidth);

            Console.WriteLine("Player moved to: " + player.xpos + ", " + player.ypos
                //+ "\nCollision Box: X:" + player.collisionBox.X + " Y: " + player.collisionBox.Y
                );
            //" Velocity Y: " + player.velocityY + "CharaTexture: " + player.charaTexture.Height + ", " + player.charaTexture.Width);

            Console.WriteLine("Current State: " + player.state);

            //if (player.xpos < windowWidth)
            //{
            //    Console.WriteLine("Player Inbounds X position");
            //}
            //else
            //{
            //    Console.WriteLine("Player Out of bounds x position");
            //}
            //if (player.ypos < windowHeight)
            //{
            //    Console.WriteLine("Player Inbounds Y position");
            //}
            //else
            //{
            //    Console.WriteLine("Player Out of bounds Y position");
            //}

        }

        public void playerState(Player player)
        {
            string currentState = player.state;
            if (checkState == false)
            {
                for (int i = 3; i > 0; i--)
                {
                    Console.WriteLine("State: " + currentState);
                    checkState = true;
                }
            }

        }
        public void keyPressed(KeyboardState prevKeyState, KeyboardState currentKeyState, Player player)
        {
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (prevKeyState.IsKeyUp(key) && currentKeyState.IsKeyDown(key))
                {
                    Console.WriteLine("Key just pressed: " + key.ToString());
                }
            }
        }

        public void enemyInfo(Player player, List<Enemy> enemies)
        {


                Console.WriteLine($"Enemy {0}: Position = ({enemies[0].xpos}, {enemies[0].ypos})" + "Enemy onGround: " + enemies[0].isGrounded);
                Console.WriteLine($"Enemy texture null? {enemies[0].charaTexture == null}");
                //Console.WriteLine("Player moved to: " + player.xpos + ", " + player.ypos);

            

        }

    }
}
