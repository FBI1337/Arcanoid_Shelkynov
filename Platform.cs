using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arcanoid_Shelkynov
{
    public class Platform
    {
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; private set; }
        private float speed = 5f;

        public Platform(GraphicsDevice graphicsDevice, Vector2 startPosition)
        {
            Texture = CreateTexture(graphicsDevice, 100, 20, Color.White);
            Position = startPosition;
        }

        public void Update()
        {
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                Position = new Vector2(Position.X - speed, Position.Y);
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                Position = new Vector2(Position.X + speed, Position.Y);
            }
            Position = new Vector2(
                MathHelper.Clamp(Position.X, 0, 800 - Texture.Width),
                Position.Y
                );
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
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
