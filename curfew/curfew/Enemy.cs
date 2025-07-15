using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace curfew
{
    internal class Enemy : Character
    {
        public Enemy(int xpos, int ypos, string state, Texture2D charaTexture) : base(xpos, ypos, state, charaTexture)
        {
        }

        public void CheckIfHit(Rectangle playerAttackHitbox)
        {
            if (!isHit && playerAttackHitbox.Intersects(this.collisionBox))
            {
                isHit = true;
                state = "hit";
                // Apply knockback or reduce HP here
            }
        }


    }
}
