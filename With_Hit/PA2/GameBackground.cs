using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PA2
{
    internal class GameBackground
    {
        Texture2D bgTexture;
        Rectangle bgRectangle;
        Color bgColor;

        public GameBackground(Texture2D bgTexture, Rectangle bgRectangle, Color bgColor)
        {
            this.bgTexture = bgTexture;
            this.bgRectangle = bgRectangle;
            this.bgColor = bgColor;
        }

        public Texture2D BgTexture { get => bgTexture; }
        public Rectangle BgRectangle { get => bgRectangle; }
        public Color BgColor { get => bgColor; }
    }
}
