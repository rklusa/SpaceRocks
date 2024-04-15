using Asteroids.GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids.Utils
{
    public static class Helpers
    {
        public static List<Bullet> bullets = new List<Bullet> ();

        public static List<SpaceRock> spaceRocks = new List<SpaceRock> ();

        public static GraphicsDeviceManager gfx;
        public static Vector2 HandleScreenWrap(Vector2 objectPos)
        {
            if (objectPos.X > gfx.PreferredBackBufferWidth + 20)
            {
                return new Vector2(0, objectPos.Y);
            }
            else if (objectPos.X < -20)
            {
                return new Vector2(gfx.PreferredBackBufferWidth, objectPos.Y);
            }
            else if (objectPos.Y > gfx.PreferredBackBufferHeight + 20)
            {
                return new Vector2(objectPos.X, 0);
            }
            else if (objectPos.Y < -20)
            {
                return new Vector2(objectPos.X, gfx.PreferredBackBufferHeight);
            }
            else
            {
                return objectPos;
            }
        }
    }
}
