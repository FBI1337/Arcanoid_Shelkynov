using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcanoid_Shelkynov
{
    /// <summary>
    /// Класс мячика сдесь инициализируем его текстуру, позицию и скорость
    /// </summary>
    public class Ball
    {
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; private set; }
        public Vector2 Speed { get; set; }

        public Ball(GraphicsDevice graphicsDevice, Vector2 startPosition, Vector2 startSpeed)
        {
            Texture = CreateTexture(graphicsDevice, 10, Color.Red);
            Position = startPosition;
            Speed = startSpeed;
        }

        public void Update(GameTime gameTime, Platform platform)
        {
            Position += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Position.X <= 0 || Position.X + Texture.Width >= 800)
            {
                Speed = new Vector2(-Speed.X, Speed.Y);
            }
            if (Position.Y + Texture.Height >= platform.Position.Y &&
                Position.X + Texture.Width >= platform.Position.X &&
                Position.X <= platform.Position.X + platform.Texture.Width)
            {
                Speed = new Vector2(Speed.X, -Speed.Y);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        private Texture2D CreateTexture(GraphicsDevice graphicsDevice, int radius, Color color)
        {
            int diameter = radius * 2;
            Texture2D texture = new Texture2D(graphicsDevice, diameter, diameter);
            Color[] data = new Color[diameter * diameter];

            for (int y = 0; y < diameter; y++)
            {
                for (int x = 0; x < diameter; x++)
                {
                    int dx = radius - x;
                    int dy = radius - y;
                    if (dx * dx + dy * dy <= radius * radius)
                    {
                        data[x + y * diameter] = color;
                    }
                    else
                    {
                        data[x + y * diameter] = Color.Transparent;
                    }
                }
            }

            texture.SetData(data);
            return texture;
        }
    }
}
