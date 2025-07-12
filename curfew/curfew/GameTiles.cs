using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace curfew
{
    public class GameTiles
    {
        Texture2D tilesTexture;
        Rectangle tilesDisplay, tileSource;
        Color tileColor;

        public GameTiles(Texture2D tilesTexture, Rectangle tilesDisplay, Rectangle tileSource, Color tileColor)
        {
            this.tilesTexture = tilesTexture;
            this.tilesDisplay = tilesDisplay;
            this.tileSource = tileSource;
            this.tileColor = tileColor;
        }

        public Texture2D TilesTexture { get => tilesTexture; }
        public Rectangle TilesDisplay { get => tilesDisplay; }
        public Rectangle TileSource { get => tileSource; }
        public Color TileColor { get => tileColor; }
    }
}