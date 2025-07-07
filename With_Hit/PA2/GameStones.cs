using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PA2
{
    internal class GameStones
    {
        Texture2D stoneTexture;
        Rectangle stoneDisplay, stoneSource;
        Color stoneColor;

        public GameStones(Texture2D stoneTexture, Rectangle stoneDisplay, Rectangle stoneSource, Color stoneColor)
        {
            this.stoneTexture = stoneTexture;
            this.stoneDisplay = stoneDisplay;
            this.stoneSource = stoneSource;
            this.stoneColor = stoneColor;
        }

        public Texture2D StoneTexture { get => stoneTexture; }
        public Rectangle StoneDisplay { get => stoneDisplay; }
        public Rectangle StoneSource { get => stoneSource; }
        public Color StoneColor { get => stoneColor; }
    }
}
