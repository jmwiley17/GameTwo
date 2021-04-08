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
        private Texture2D planet; 
        private SpriteFont spriteFont;
        private bool endGame;
        ExplosionParticleSystem _explosions;
        FireworkParticleSystem _fireworks;
        RainParticleSystem _rain;




        public Game2()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
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
                new AsteroidSprite(new Vector2(300, 75)),
                new AsteroidSprite(new Vector2(300, 325)),
                new AsteroidSprite(new Vector2(500, 75)),
                new AsteroidSprite(new Vector2(500, 325)),
            };

            _rain = new RainParticleSystem(this, new Rectangle(0, 0, 800, 10));
            Components.Add(_rain);

            _explosions = new ExplosionParticleSystem(this, 20);
            Components.Add(_explosions);

            _fireworks = new FireworkParticleSystem(this, 20);
            Components.Add(_fireworks);

            


            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ship.LoadContent(Content);
            foreach (var ast in asteroid) ast.LoadContent(Content);
            explosion = Content.Load<SoundEffect>("Explosion");
            ball = Content.Load<Texture2D>("ball");
            spriteFont = Content.Load<SpriteFont>("arial");
            planet = Content.Load<Texture2D>("Planet14");


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
                        _explosions.PlaceExplosion(ship.Position);
                        endGame = true;

                    }
                }
            }

            
            if(ship.Position.X > 700)
            {
                _fireworks.PlaceFirework(ship.Position);
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
                spriteBatch.DrawString(spriteFont, $"You have Died. Press R to restart!", new Vector2(200, 200), Color.Black);
                spriteBatch.End();
                base.Draw(gameTime);
                if (Keyboard.GetState().IsKeyDown(Keys.R)){
                    endGame = false;
                    Initialize();
                }
                
                
            }
            else
            {
                GraphicsDevice.Clear(Color.Black);

                float shipX = MathHelper.Clamp(ship.Position.X, 200, 720);
                float offsetX = 200 - shipX;
                Matrix transform;
                transform = Matrix.CreateTranslation(offsetX * 0.2f, 0, 0);

                spriteBatch.Begin(transformMatrix: transform);
                spriteBatch.Draw(planet, Vector2.Zero, Color.White);
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
