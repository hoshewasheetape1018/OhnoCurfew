using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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
            Console.WriteLine("Window width: " + windowWidth + " Window Height: " + windowHeight + " Xpos: " + player.xpos + " Ypos: " + player.ypos);

            Console.WriteLine("Player height: " + player.charaHeight + " Player width: " + player.charaWidth);

            if (player.xpos < windowWidth)
            {
                Console.WriteLine("Player Inbounds X position");
            }
            else
            {
                Console.WriteLine("Player Out of bounds x position");
            }
            if (player.ypos < windowHeight)
            {
                Console.WriteLine("Player Inbounds Y position");
            }
            else
            {
                Console.WriteLine("Player Out of bounds Y position");
            }

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

        public void keyPressed(KeyboardState prevKeyState, KeyboardState currentKeyState)
        {


            if (prevKeyState != currentKeyState)
            {
                Console.WriteLine(currentKeyState.ToString());
            }
        }
    }
}
