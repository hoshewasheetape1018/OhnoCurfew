using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PA2
{
    internal class GamePlants
    {
        Texture2D plantTexture;
        Rectangle plantDisplay, plantSource;
        Color plantColor;

        public GamePlants(Texture2D plantTexture, Rectangle plantDisplay, Rectangle plantSource, Color plantColor)
        {
            this.plantTexture = plantTexture;
            this.plantDisplay = plantDisplay;
            this.plantSource = plantSource;
            this.plantColor = plantColor;
        }

        public Texture2D PlantTexture { get => plantTexture; }
        public Rectangle PlantDisplay { get => plantDisplay; }
        public Rectangle PlantSource { get => plantSource; }
        public Color PlantColor { get => plantColor; }
    }
}
