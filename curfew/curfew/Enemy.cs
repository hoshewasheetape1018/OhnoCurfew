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
        public Enemy(int xpos, int ypos, string state, Texture2D charaTexture, int frameCount, List<Enemy> enemies) : base(xpos, ypos, state, charaTexture, frameCount)
        {
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
                characterState(); // optional animation change??
                return;
            }

            // enemy AI logic here (movement, etc)

            characterState();
            physics.ApplyPhysics(tiles[0], key);
            flip = facingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            cboxOffset = facingLeft ? 30 : 35;


            collisionBox.X = xpos + cboxOffset;
            collisionBox.Y = ypos;
        }



    }
}
