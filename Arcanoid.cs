using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Arcanoid_Shelkynov
{
    public class Arcanoid : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Platform platform;
        private Ball ball;
        private Brick[] bricks;
        private int score;
        private bool isGameOver;

        public Arcanoid() //This is the constructor, this function is called whenever the game class is created.
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// This function is automatically called when the game launches to initialize any non-graphic variables.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Automatically called when your game launches to load any game assets (graphics, audio etc.)
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            platform = new Platform(GraphicsDevice, new Vector2(400, 450));
            ball = new Ball(GraphicsDevice, new Vector2(400, 300), new Vector2(150, 150));

            int rows = 5;
            int columns = 10;
            float brickWidth = 80;
            float brickHeight = 20;
            float margin = 10;

            bricks = new Brick[rows * columns];
            for (int row = 0; row < rows; row++)
            {
                for (int colum = 0; colum < columns; colum++)
                {
                    int index = row * columns + colum;
                    Vector2 position = new Vector2(colum * (brickWidth + margin), row * (brickHeight + margin) + 50);
                    bricks[index] = new Brick(GraphicsDevice, position);
                }
            }
        }

        /// <summary>
        /// Called each frame to update the game. Games usually runs 60 frames per second.
        /// Each frame the Update function will run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {

            if (isGameOver)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    RestartGame();
                }
                return;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            platform.Update();
            ball.Update(gameTime, platform);

            foreach (var brick in bricks)
            {
                brick.Collision(ball);
                if (brick.Destroyed)
                {
                    score += 10;
                }
            }

            if (ball.Position.Y > graphics.PreferredBackBufferHeight)
            {
                isGameOver = true;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game is ready to draw to the screen, it's also called each frame.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            platform.Draw(spriteBatch);
            ball.Draw(spriteBatch);

            foreach (var brick in bricks)
            {
                brick.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void RestartGame()
        {
            isGameOver = false;
            score = 0;
            ball = ball = new Ball(GraphicsDevice, new Vector2(400, 300), new Vector2(150, 150));
            platform = new Platform(GraphicsDevice, new Vector2(400, 450));
        }
    }
}
