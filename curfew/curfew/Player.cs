using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace curfew
{
    internal class Player
    {
        Texture2D heroTexture;
        Rectangle heroDisplay;
        Rectangle heroSource;
        Color currentColor = Color.White;

        Texture2D collisionmapTexture;
        Rectangle collisionmapDisplay;
        Color[] collisionPixels;

        float gravity = 0.55f;
        float velocityY = 0f;
        float jumpStrength = -12f;
        int speed = 3;

        bool isJumping = false;
        bool isGrounded = false;

        Vector2 knockbackVelocity;
        float knockbackDuration = 0f;

        float attackCooldown = 0f;
        bool isHit = false;
        float hitFlashTime = 0.25f;
        float hitTimer = 0f;
        int health = 3;

        public Player() { }

        public Rectangle displayRectangle => heroDisplay;
        public bool IsHit => isHit;

        public void LoadContent(Texture2D heroTex, Texture2D collisionTex, Rectangle startRect, Rectangle collisionDisplay)
        {
            heroTexture = heroTex;
            collisionmapTexture = collisionTex;
            heroDisplay = startRect;
            collisionmapDisplay = collisionDisplay;
            heroSource = new Rectangle(0, 0, startRect.Width, startRect.Height);

            // Cache collision pixels
            collisionPixels = new Color[collisionmapTexture.Width * collisionmapTexture.Height];
            collisionmapTexture.GetData(collisionPixels);
        }

        public void Update(KeyboardState key, GameTime gameTime, List<Enemy> enemies)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Rectangle oldDisplay = heroDisplay;

            // Apply knockback if active
            if (knockbackDuration > 0)
            {
                knockbackDuration -= delta;
                heroDisplay.X += (int)knockbackVelocity.X;
                heroDisplay.Y += (int)knockbackVelocity.Y;
                return; // skip rest of movement while in knockback
            }

            // Jump
            velocityY += gravity;
            heroDisplay.Y += (int)velocityY;

            if (IsCollidingWithTile(GetCollisionBox()))
            {
                heroDisplay.Y = oldDisplay.Y;
                if (velocityY > 0)
                {
                    isGrounded = true;
                    isJumping = false;
                }
                velocityY = 0;
            }

            // Horizontal movement
            if (key.IsKeyDown(Keys.Left))
            {
                heroDisplay.X -= speed;
                if (IsCollidingWithTile(GetCollisionBox()))
                    heroDisplay.X = oldDisplay.X;
            }
            else if (key.IsKeyDown(Keys.Right))
            {
                heroDisplay.X += speed;
                if (IsCollidingWithTile(GetCollisionBox()))
                    heroDisplay.X = oldDisplay.X;
            }

            // Jump input
            if ((key.IsKeyDown(Keys.Up) || key.IsKeyDown(Keys.Space)) && isGrounded)
            {
                velocityY = jumpStrength;
                isJumping = true;
                isGrounded = false;
            }

            // Out of bounds reset
            if (heroDisplay.Y > collisionmapDisplay.Height + 200)
            {
                heroDisplay = new Rectangle(120, 620, heroDisplay.Width, heroDisplay.Height);
                velocityY = 0;
                isJumping = false;
                isGrounded = false;
            }

            // Red flash when hit
            if (isHit)
            {
                hitTimer -= delta;
                if (hitTimer <= 0)
                {
                    isHit = false;
                    currentColor = Color.White;
                }
            }

            // Attack
            if (attackCooldown > 0)
                attackCooldown -= delta;

            if (key.IsKeyDown(Keys.Z) && attackCooldown <= 0)
            {
                Rectangle attackHitbox = new Rectangle(
                    heroDisplay.X - heroDisplay.Width / 2,
                    heroDisplay.Y,
                    heroDisplay.Width * 2,
                    heroDisplay.Height
                );

                foreach (var enemy in enemies)
                {
                    if (!enemy.IsDead && attackHitbox.Intersects(enemy.GetBounds()))
                        enemy.TakeDamage(heroDisplay.Center.ToVector2());
                }

                attackCooldown = 0.3f;
            }
        }

        public void TakeDamage(Vector2 fromPosition)
        {
            if (isHit) return;

            isHit = true;
            hitTimer = hitFlashTime;
            currentColor = Color.Red;

            Vector2 direction = heroDisplay.Center.ToVector2() - fromPosition;
            direction.Normalize();
            knockbackVelocity = direction * 8f; // simple fixed movement
            knockbackDuration = 0.15f;

            health--;
        }

        Rectangle GetCollisionBox()
        {
            return new Rectangle(
                heroDisplay.X + 25,
                heroDisplay.Y + 20,
                heroDisplay.Width - 60,
                heroDisplay.Height - 20
            );
        }

        bool IsCollidingWithTile(Rectangle rect)
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

                    if (collisionPixels[texY * collisionmapTexture.Width + texX].A > 10)
                        return true;
                }
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(heroTexture, heroDisplay, heroSource, currentColor);
        }
    }
}
