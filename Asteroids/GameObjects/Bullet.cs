using Asteroids.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Utility.Drawing;

namespace Asteroids.GameObjects
{
    public class Bullet
    {
        private Vector2 position;
        private Vector2 forward;
        private float width;
        private float height;

        public bool isAlive;

        public Bullet(Vector2 pos, Vector2 dir)
        {
            position = pos;
            forward = dir;
            width = 4f;
            height = 4f; 

            isAlive = true;
        }
        public void Update(float deltaTime)
        {
            if (isAlive)
            {
                position += forward * 300 * deltaTime;

                HandleScreenBounds();
            }
        }

        public void Draw(SpriteBatch batch)
        {
            if (isAlive)
            {
                batch.DrawRectangle(position, new Vector2(width, height), Color.White);
            }
        }

        public void HandleScreenBounds()
        {
            if (position.X > Helpers.gfx.PreferredBackBufferWidth + 10 || position.X < -10)
            {
                isAlive = false;
            }
            else if(position.Y > Helpers.gfx.PreferredBackBufferHeight + 10 || position.Y < -10)
            {
                isAlive = false;
            }
        }
    }
}
