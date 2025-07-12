using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
        int velocityY;

        //PLAYER STATS

        //   HP
        int maxlife;
        int currentlife;

        //ATTACK
        int attack;


        //PLAYER ATTRIBUTES:
        int jumpStrength;

        //PHYSICS
        float gravity;

        public Character(int xpos, int ypos)
        {
            this.ypos = ypos;
            this.xpos = xpos;

        }


    }
}
