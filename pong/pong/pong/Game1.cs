using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace pong
{
    public class Game1 : Game
    {
        Texture2D ballTexture;
        Texture2D paddleTexture;
        Vector2 ballPosition;
        Vector2 paddlePosition;
        float paddleSpeed;
        float ballYSpeed;
        float ballXSpeed;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            ballXSpeed = 100f;
            ballYSpeed = 100f;
            paddlePosition = new Vector2(_graphics.PreferredBackBufferWidth / 20, _graphics.PreferredBackBufferHeight / 2);
            paddleSpeed = 200f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ballTexture = Content.Load<Texture2D>("ball");
            paddleTexture = Content.Load<Texture2D>("paddle");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //Control the paddle
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
            {
                paddlePosition.Y -= paddleSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (kstate.IsKeyDown(Keys.Down))
            {
                paddlePosition.Y += paddleSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            //Setting Boundaries for paddle
            if(paddlePosition.Y > _graphics.PreferredBackBufferHeight - paddleTexture.Height / 2) 
            {
                paddlePosition.Y = _graphics.PreferredBackBufferHeight - paddleTexture.Height / 2;
            }
            else if(paddlePosition.Y < paddleTexture.Height / 2)
            {
                paddlePosition.Y = paddleTexture.Height / 2;
            }

            //Change ball position
            ballPosition.Y += ballYSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds; 
            ballPosition.X += ballXSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (ballPosition.Y > _graphics.PreferredBackBufferHeight - paddleTexture.Height / 2)
            {
                ballYSpeed *= -1;
            }
            else if (ballPosition.Y < paddleTexture.Height / 2)
            {
                ballYSpeed *= -1;
            }

            if (ballPosition.X > _graphics.PreferredBackBufferWidth - paddleTexture.Width / 2)
            {
                ballXSpeed *= -1;
            }
            else if (ballPosition.X < paddleTexture.Width / 2)
            {
                ballXSpeed *= -1;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(paddleTexture, paddlePosition, null, Color.Black, 0f,
                new Vector2(paddleTexture.Width / 2, paddleTexture.Height / 2),
                Vector2.One, SpriteEffects.None, 0f);
            _spriteBatch.Draw(ballTexture, ballPosition, null, Color.Black, 0f, 
                new Vector2(ballTexture.Width / 2, ballTexture.Height / 2), 
                Vector2.One, SpriteEffects.None, 0f);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}