using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcanoid_Shelkynov
{
    /// <summary>
    /// Класс кирпичей тут объясняется их текстура, позиция и состояние сломаные или нет
    /// </summary>
    public class Brick
    {
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; private set; }
        public bool Destroyed { get; private set; }

        public Brick(GraphicsDevice graphicsDevice, Vector2 startPosition)
        {
            Texture = CreateTexture(graphicsDevice, 80, 20, Color.Blue);
            Position = startPosition;
            Destroyed = false;
        }

        public void Collision(Ball ball)
        {
            if (!Destroyed &&
                ball.Position.X + ball.Texture.Width >= Position.X &&
                ball.Position.X <= Position.X + Texture.Width &&
                ball.Position.Y + ball.Texture.Height >= Position.Y &&
                ball.Position.Y <= Position.Y + Texture.Height)
            {
                ball.Speed = new Vector2(ball.Speed.X, -ball.Speed.Y);
                Destroyed = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Destroyed)
            {
                spriteBatch.Draw(Texture, Position, Color.White);
            }
        }

        private Texture2D CreateTexture(GraphicsDevice graphicsDevice, int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(graphicsDevice, width, height);
            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = color;
            }
            texture.SetData(data);
            return texture;
        }
    }
}
