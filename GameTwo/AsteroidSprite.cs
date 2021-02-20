using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameTwo.Collisions;

namespace GameTwo
{
    public class AsteroidSprite
    {
        private const float ANIMATION_SPEED = 0.1f;

        private double animationTimer;

        private int animationFrame;

        private Vector2 position;

        private Texture2D texture;

        private BoundingCircle bounds;

        public BoundingCircle Bounds => bounds;

        public AsteroidSprite(Vector2 position)
        {
            this.position = position;
            this.bounds = new BoundingCircle(position + new Vector2(64, 64), 48);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Asteroid");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (animationTimer > ANIMATION_SPEED)
            {
                animationFrame++;
                if (animationFrame > 7) animationFrame = 0;
                animationTimer -= ANIMATION_SPEED;
            }
            var source = new Rectangle(animationFrame * 128, 0, 128, 128);
            
            spriteBatch.Draw(texture, position, source, Color.White);
        }
    }
}
