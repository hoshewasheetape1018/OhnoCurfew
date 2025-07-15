using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace curfew
{

    internal class Character
    {
        //PLAYER PROPERTIES

        internal int ypos;
        internal int xpos;
        internal int startXpos = 69;
        internal int startYpos = 69;
        internal int charaWidth;
        internal int charaHeight;
        internal int velocity;
        internal Texture2D charaTexture;
        internal Rectangle sourceRectangle;
        internal string state;
        internal SpriteEffects flip;
        internal Vector2 knockbackVelocity = Vector2.Zero;
        internal int knockbackFrames = 0;

        //PLAYER STATS

        //HP
        internal int maxlife;
        internal int currentlife = 4;

        //ATTACK
        int attack;
        public Rectangle attackHitbox;
        public int attackDuration = 10;
        internal int attackTimer = 0;

        // Flash + invincibility
        internal bool isFlashing;
        internal int flashTimer;
        internal int iFrames;
        internal int maxIFrames = 30;


        //PHYSICS
        public Physics physics;
        internal int moveSpeed = 10;
        internal float jumpStrength = -20f;
        internal float velocityY = 0f;


        //BOOL
        internal bool isWalkRun;
        internal bool onPlatform;
        internal bool isDead;
        internal bool isHit;
        internal bool facingLeft;
        internal bool isGrounded;
        internal bool isJumping;
        internal bool isMoving;
        public bool isAttacking;
        private bool hasDealtDamageThisAttack = false;


        // COLLISION BOX
        public Rectangle collisionBox;
        internal int cboxOffset;

        //ANIMATION
        private int currentFrame;
        private int previousStartFrame;
        private int delay;
        private int frameWidth;
        private int frameHeight;
        private int knockbackDuration;
        internal bool wasJumpingLastFrame = false;
        internal int frameCount;

        //STATE TEXTURES
        Texture2D charaIdle, charaWalk, charaJump, charaFall, charaAttack, charaHit, charaDead;

        // OTHERS
        KeyboardState currentKeyState;
        KeyboardState prevKeyState;
        private SpriteBatch _spriteBatch;
        private List<Enemy> enemiesHit = new List<Enemy>();

        int jumpBufferTime = 6;
        int jumpBufferCounter = 0;

        public Character(int xpos, int ypos, string state, Texture2D charaTexture, int frameCount)
        {
            this.xpos = xpos;
            this.ypos = ypos;
            this.state = state;

            this.charaTexture = charaTexture;

            charaWidth = charaTexture.Width;
            charaHeight = charaTexture.Height;
            physics = new Physics(this);
            this.frameCount = frameCount;
            collisionBox = new Rectangle(xpos, ypos, charaWidth/frameCount-60, charaHeight);

        }


        public void setStateTexture(Texture2D charaIdle, Texture2D charaWalk, Texture2D charaJump, Texture2D charaFall, Texture2D charaAttack, Texture2D charaHit, Texture2D charaDead)
        {
            charaTexture = charaIdle;
            this.charaIdle = charaIdle;
            this.charaWalk = charaWalk;
            this.charaJump = charaJump;
            this.charaFall = charaFall;
            this.charaAttack = charaAttack;
            this.charaHit = charaHit;
            this.charaDead = charaDead;
        }


        public void characterState()
        {
            switch (state)
            {
                case ("idle"):
                    charaTexture = charaIdle;
                    Animate(0, 4);
                    break;
                case ("walkrun"):
                    charaTexture = charaWalk;
                    Animate(0, 4);
                    break;
                case ("jump"):
                    charaTexture = charaJump;
                    Animate(0, 4);
                    break;
                case ("fall"):
                    charaTexture = charaFall;
                    Animate(0, 1);
                    break;
                case ("attack"):
                    charaTexture = charaAttack;
                    Animate(0,1);
                    break;
                case ("hit"): // + add knockback
                    charaTexture = charaHit;
                    Animate(0,1);
                    break;
                case ("dead"):
                    charaTexture = charaDead;
                    Animate(0,4);
                    break;
                default:
                    break;
            }

        }

        void Animate(int startFrame, int lastFrame)
        {
            if (startFrame != previousStartFrame)
            {
                currentFrame = startFrame;
                previousStartFrame = startFrame;
                delay = 0;
            }

            delay++;

            if (delay > 4)
            {
                currentFrame++;

                if (currentFrame >= frameCount)
                    currentFrame = startFrame;

                delay = 0;
            }

            int frameWidth = charaTexture.Width / frameCount;
            int frameHeight = charaTexture.Height;

            sourceRectangle = new Rectangle(
                currentFrame * frameWidth,
                0,
                frameWidth,
                frameHeight
            );
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Color tint = isFlashing ? Color.Red : Color.White;

            spriteBatch.Draw(
                charaTexture,
                new Rectangle(xpos, ypos, 163, 163),
                sourceRectangle,
                tint,
                0,
                Vector2.Zero,
                flip,
                0
            );
        }


        internal void StartAttack()
        {
            isAttacking = true;
            state = "attack";
            attackTimer = attackDuration;
            hasDealtDamageThisAttack = false; // ← Reset

            int hitboxWidth = 50;
            int hitboxHeight = 50;
            int offsetX = facingLeft ? -collisionBox.Width+70: collisionBox.Width+50;

            attackHitbox = new Rectangle(xpos + offsetX, ypos, hitboxWidth, hitboxHeight);
        }



        public void TriggerKnockback(bool fromLeft)
        {
            int knockbackStrength = 6;
            int verticalLift = -3;

            knockbackVelocity = new Vector2(fromLeft ? knockbackStrength : -knockbackStrength, verticalLift);
            knockbackFrames = 10;

            Console.WriteLine($"[Knockback] Velocity set to {knockbackVelocity}, Frames: {knockbackFrames}");
        }
        public void ApplyKnockback()
        {
            if (knockbackFrames > 0)
            {
                xpos += (int)knockbackVelocity.X;
                ypos += (int)knockbackVelocity.Y;
                knockbackVelocity.Y += 0.5f; // simulate gravity
                knockbackFrames--;

                Console.WriteLine($"[ApplyKnockback] xpos: {xpos}, ypos: {ypos}, velocityY: {knockbackVelocity.Y}, frames left: {knockbackFrames}");

                // Clamp to ground
                int groundY = 864 - 100;
                int bottom = ypos + charaHeight;

                if (bottom > groundY)
                {
                    ypos = groundY - charaHeight;
                    knockbackVelocity.Y = 0;
                    Console.WriteLine($"[ApplyKnockback] Ground hit. Y clamped to {ypos}");
                }
            }
        }



        public virtual void TakeDamage(int damage, bool fromLeft)
        {
            if (iFrames > 0) return;

            currentlife -= damage;
            isHit = true;
            isFlashing = true;
            flashTimer = 10;
            iFrames = maxIFrames;
            state = "hit";

            Console.WriteLine($"[{this.GetType().Name}] Took {damage} damage. Life left: {currentlife}");

            TriggerKnockback(fromLeft);

            if (currentlife <= 0)
            {
                isDead = true;
                state = "dead";
                Console.WriteLine($"[{this.GetType().Name}] DIED.");
            }
        }

        internal void HandleAttack(List<Enemy> enemies)
        {
            if (isAttacking)
            {
                attackTimer--;

                foreach (Enemy enemy in enemies)
                {
                    if (attackHitbox.Intersects(enemy.collisionBox) && !enemiesHit.Contains(enemy))
                    {
                        bool hitFromLeft = !facingLeft;
                        enemy.TakeDamage(1, hitFromLeft);
                        enemiesHit.Add(enemy);
                    }
                }

                if (attackTimer <= 0)
                {
                    isAttacking = false;
                    state = "idle";
                    enemiesHit.Clear(); // Clear after attack ends
                }
            }
        }




        public virtual void Update(GameTiles[] tiles, KeyboardState key, List<Enemy> enemies)
        {
            if (iFrames > 0) iFrames--;
            if (isFlashing)
            {
                flashTimer--;
                if (flashTimer <= 0)
                    isFlashing = false;
            }

            HandleAttack(enemies);
            characterState();
            physics.ApplyPhysics(tiles[0], key);

            prevKeyState = currentKeyState;
        }

        public void DrawHitbox(SpriteBatch spriteBatch, Texture2D debugTexture)
        {
            if (isAttacking)
            {
                spriteBatch.Draw(
                    debugTexture,
                    attackHitbox,
                    Color.Red * 0.4f // Semi-transparent red
                );
            }
        }

        public void DrawCollisionBox(SpriteBatch spriteBatch, Texture2D debugTexture)
        {
            spriteBatch.Draw(debugTexture, collisionBox, Color.Blue * 0.3f);
        }



    }
}


