using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace curfew
{
    internal class Enemy
    {
        Texture2D texture;
        Rectangle displayRect;
        Rectangle sourceRect;

        Texture2D collisionmapTexture;
        Rectangle collisionmapDisplay;
        Color[] collisionPixels;

        Vector2 direction = new Vector2(1, 0);
        float speed = 1.5f;

        public bool IsDead = false;
        public bool IsHit = false;

        public Enemy(Texture2D tex, Texture2D colTex, Rectangle startRect, Rectangle colDisplay)
        {
            texture = tex;
            displayRect = startRect;
            sourceRect = new Rectangle(0, 0, startRect.Width, startRect.Height);

            collisionmapTexture = colTex;
            collisionmapDisplay = colDisplay;

            collisionPixels = new Color[collisionmapTexture.Width * collisionmapTexture.Height];
            collisionmapTexture.GetData(collisionPixels);
        }

        public void Update(GameTime gameTime)
        {
            if (IsDead) return;

            Rectangle next = displayRect;
            next.X += (int)(direction.X * speed);

            if (!IsCollidingWithTile(next))
            {
                displayRect = next;
            }
            else
            {
                direction.X *= -1; // flip direction
            }
        }

        public void TakeDamage(Vector2 from)
        {
            IsHit = true;
            IsDead = true;
        }

        public Rectangle GetBounds() => displayRect;

        public bool CheckCollision(Rectangle other) => displayRect.Intersects(other);

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsDead)
                spriteBatch.Draw(texture, displayRect, sourceRect, Color.White);
        }

        private bool IsCollidingWithTile(Rectangle rect)
        {
            float scaleX = (float)collisionmapTexture.Width / collisionmapDisplay.Width;
            float scaleY = (float)collisionmapTexture.Height / collisionmapDisplay.Height;

            for (int y = rect.Top; y < rect.Bottom; y++)
            {
                for (int x = rect.Left; x < rect.Right; x++)
                {
                    int texX = (int)((x - collisionmapDisplay.X) * scaleX);
                    int texY = (int)((y - collisionmapDisplay.Y) * scaleY);

                    if (texX < 0 || texY < 0 || texX >= collisionmapTexture.Width || texY >= collisionmapTexture.Height)
                        continue;

                    int index = texY * collisionmapTexture.Width + texX;
                    if (collisionPixels[index].A > 10)
                        return true;
                }
            }

            return false;
        }
    }
}
