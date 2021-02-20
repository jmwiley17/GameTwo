using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace GameTwo
{
    public class Game2 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ShipSprite ship;
        private AsteroidSprite[] asteroid;
        private SoundEffect explosion;
        private Texture2D ball;
        private bool endGame;

        public Game2()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            endGame = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ship = new ShipSprite(new Vector2(0, 50));
            asteroid = new AsteroidSprite[]
            {
                new AsteroidSprite(new Vector2(200,200)),
                new AsteroidSprite(new Vector2(400, 200)),
                new AsteroidSprite(new Vector2(600, 200)),
                new AsteroidSprite(new Vector2(200, 0)),
                new AsteroidSprite(new Vector2(400, 0)),
                new AsteroidSprite(new Vector2(600, 0)),
                new AsteroidSprite(new Vector2(200, 400)),
                new AsteroidSprite(new Vector2(400, 400)),
                new AsteroidSprite(new Vector2(600, 400)),
            };
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ship.LoadContent(Content);
            foreach (var ast in asteroid) ast.LoadContent(Content);
            explosion = Content.Load<SoundEffect>("Explosion");
            ball = Content.Load<Texture2D>("ball");


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ship.Update(gameTime);


            if (!endGame)
            {
                foreach (var ast in asteroid)
                {
                    if (ast.Bounds.CollidesWith(ship.Bounds))
                    {
                        explosion.Play();
                        endGame = true;

                    }
                }
            }
            
            

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            

            // TODO: Add your drawing code here
            
            if (endGame)
            {
                GraphicsDevice.Clear(Color.Red);
                spriteBatch.Begin();
                spriteBatch.End();
                base.Draw(gameTime);
            }
            else
            {
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.Begin();
                ship.Draw(gameTime, spriteBatch);
                foreach (var ast in asteroid)
                {
                    ast.Draw(gameTime, spriteBatch);
                    /*var rect = new Rectangle((int)(asteroid.Bounds.Center.X - asteroid.Bounds.Radius),
                                             (int)(asteroid.Bounds.Center.Y - asteroid.Bounds.Radius),
                                             (int)(2 * asteroid.Bounds.Radius), (int)(2 * asteroid.Bounds.Radius));
                    spriteBatch.Draw(ball, rect, Color.White);*/
                }

                spriteBatch.End();

                base.Draw(gameTime);
            }
         
        }
    }
}
