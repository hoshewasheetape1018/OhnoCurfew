using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PA2
{
    internal class GameSigns
    {
        Texture2D signTexture;
        Rectangle signDisplay, signSource;
        Color signColor;

        public GameSigns(Texture2D signTexture, Rectangle signDisplay, Rectangle signSource, Color signColor)
        {
            this.signTexture = signTexture;
            this.signDisplay = signDisplay;
            this.signSource = signSource;
            this.signColor = signColor;
        }

        public Texture2D SignTexture { get => signTexture; }
        public Rectangle SignDisplay { get => signDisplay; }
        public Rectangle SignSource { get => signSource; }
        public Color SignColor { get => signColor; }
    }
}
