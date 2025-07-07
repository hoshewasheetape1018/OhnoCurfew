using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PA2
{
    internal class GameLeaves
    {
        Texture2D leavesTexture;
        Rectangle leavesDisplay, leavesSource;
        Color leavesColor;

        public GameLeaves(Texture2D leavesTexture, Rectangle leavesDisplay, Rectangle leavesSource, Color leavesColor)
        {
            this.leavesTexture = leavesTexture;
            this.leavesDisplay = leavesDisplay;
            this.leavesSource = leavesSource;
            this.leavesColor = leavesColor;
        }

        public Texture2D LeavesTexture { get => leavesTexture; }
        public Rectangle LeavesDisplay { get => leavesDisplay; }
        public Rectangle LeavesSource { get => leavesSource; }
        public Color LeavesColor { get => leavesColor; }
    }
}
