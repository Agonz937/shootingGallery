using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootingGallery
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        // To start the sprite
        private SpriteBatch _spriteBatch;

        // The Spritesheets we will be using.
        Texture2D targetSprite;
        Texture2D crosshairsSprite;
        Texture2D backgroundSprite;
        SpriteFont gameFont;

        // Target
        Vector2 targetPosition = new Vector2(300, 300);
        const int targetRadius = 45;
        Random rand = new Random();

        // Mouse movement
        MouseState mState;
        bool mReleased = true;

        // Score Count
        int score = 0;

        // Timer
        double timer = 10;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// Loads the art asssets into the game
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            targetSprite = Content.Load<Texture2D>("target");
            crosshairsSprite = Content.Load<Texture2D>("crosshairs");
            backgroundSprite = Content.Load<Texture2D>("sky");
            gameFont = Content.Load<SpriteFont>("galleryFont");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Timer logic

            if(timer > 0)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;

            }

            if(timer < 0 )
            {
                timer = 0;
            }

            // TODO: Add your update logic here
            mState = Mouse.GetState();

            if (mState.LeftButton == ButtonState.Pressed && mReleased == true)
            {
                float mouseTargetDist = Vector2.Distance(targetPosition, mState.Position.ToVector2());

                if(mouseTargetDist <= targetRadius && timer != 0) 
                {
                    score++;

                    targetPosition.X = rand.Next(0, _graphics.PreferredBackBufferWidth);
                    targetPosition.Y = rand.Next(0, _graphics.PreferredBackBufferHeight);
                }
                mReleased = false;
            }

            if(mState.LeftButton == ButtonState.Released)
            {
                mReleased = true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);
            _spriteBatch.DrawString(gameFont, $"Score: {score}", new Vector2(3,3), Color.White);
            _spriteBatch.DrawString(gameFont, $"Timer: {Math.Ceiling(timer)}", new Vector2(3, 40), Color.White);

            if(timer > 0) 
            {
                _spriteBatch.Draw(targetSprite, new Vector2(targetPosition.X - targetRadius, targetPosition.Y - targetRadius), Color.White);

            } else
            {
                int displayX = _graphics.PreferredBackBufferWidth / 2;
                int displayY = _graphics.PreferredBackBufferHeight / 2;
                _spriteBatch.DrawString(gameFont, "Game Over", new Vector2(displayX, displayY), Color.White);

            }

            _spriteBatch.Draw(crosshairsSprite, new Vector2(mState.X - 25, mState.Y - 25), Color.White);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
