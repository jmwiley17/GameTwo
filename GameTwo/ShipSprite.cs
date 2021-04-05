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
    public class ShipSprite
    {
        private GamePadState gamePadState;

        private KeyboardState keyboardState;

        private Texture2D texture;

        private Vector2 position = new Vector2(0, 50);
        
        Vector2 velocity = new Vector2(20,0);

        private BoundingCircle bounds;
        public BoundingCircle Bounds => bounds;
        public ShipSprite(Vector2 position)
        {
            this.position = position;
            this.bounds = new BoundingCircle(position + new Vector2(16, 16), 16);
        }

        public Vector2 Position => position;

        public Color Color { get; set; } = Color.White;

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Ship");
        }

        public void Update(GameTime gameTime)
        {
         
            keyboardState = Keyboard.GetState();
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 acceleration = new Vector2(0, 50);
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                acceleration += new Vector2(0, -100);
            }

            velocity += acceleration * t;

            position += velocity * t;


            bounds.Center = new Vector2(position.X + 16, position.Y + 16);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(28, 42, 16, 16), Color.White, 0, new Vector2(0,0), 2f, SpriteEffects.None, 0);
        }

    }
}
