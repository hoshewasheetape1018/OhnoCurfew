using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PA2
{
    internal class GameSpikes
    {
        Texture2D spikeTexture;
        Rectangle spikeDisplay, spikeSource;
        Color spikeColor;

        public GameSpikes(Texture2D spikeTexture, Rectangle spikeDisplay, Rectangle spikeSource, Color spikeColor)
        {
            this.spikeTexture = spikeTexture;
            this.spikeDisplay = spikeDisplay;
            this.spikeSource = spikeSource;
            this.spikeColor = spikeColor;
        }
        public Texture2D SpikeTexture { get => spikeTexture;}
        public Rectangle SpikeDisplay { get => spikeDisplay;}
        public Rectangle SpikeSource { get => spikeSource; }
        public Color SpikeColor { get => spikeColor; }
    }
}
