using Asteroids.GameObjects;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Asteroids.Utils
{
    public static class Helpers
    {
        public static GraphicsDeviceManager gfx;

        public static List<Bullet> bullets = new List<Bullet> ();

        public static List<SpaceRock> spaceRocks = new List<SpaceRock> ();

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

        public static bool CircleOverlap(Vector2 point, Vector2 center, float radius)
        {
            return (point - center).Length() < radius + radius;
        }

        public static void spawnRockFragments(float size, Vector2 pos)
        {
            for (int i = 0; i < 2; i++)
            {
                SpaceRock rock = new SpaceRock(size, pos);
                Helpers.spaceRocks.Add(rock);
            }
        }
        public static void SpawnRocks(int currWave)
        {

            for (int i =0; i < (currWave * 2); i++)
            {
                SpaceRock rock = new SpaceRock(25, Vector2.Zero);
                Helpers.spaceRocks.Add(rock);
            }
        }
    }
}
