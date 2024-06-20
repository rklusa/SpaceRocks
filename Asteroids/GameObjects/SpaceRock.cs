using Asteroids.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Utility.Drawing;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Asteroids.GameObjects
{
    public class SpaceRock
    {
        public Vector2 position;
        private Vector2 forward;
        private Vector2 rotation;
        private float angle;
        private float rot;
        private Vector2[] verts;
        private Random rand = new Random();

        public float size;
        public bool isAlive;

        public SpaceRock(float radius, Vector2 Pos) 
        {
            verts = Generate(radius);
            size = radius;

            if (Pos == Vector2.Zero) 
            {
                position = SpawnRock(position);
            }
            else
            {
                position = Pos;
            }

            angle = rand.Next(0, 7);
            isAlive = true;
        }

        public void Update(float deltaTime)
        {
            if (isAlive)
            {
                forward.X = (float)MathF.Cos(angle);
                forward.Y = (float)MathF.Sin(angle);
                forward.Normalize();

                rotation.X = (float)MathF.Cos(rot);
                rotation.Y = (float)MathF.Sin(rot);
                rotation.Normalize();

                position += forward * 1f;
                rot += 1f * deltaTime;

                position = Helpers.HandleScreenWrap(position);
            }
        }

        public void Draw(SpriteBatch batch)
        {
            if(isAlive) 
            {
                Vector2[] newVerts = new Vector2[verts.Length];

                for (int i = 0; i < verts.Length; i++)
                {
                    newVerts[i] = HandleRotation(verts[i], rot);
                }
                batch.DrawPolygon(position, newVerts, true);
            }
        }

        public Vector2[] Generate(float radius)
        {
            int numVerts = 6;
            Vector2[] genVerts = new Vector2[numVerts];

            float deg;
            float r = radius;

            for (int i = 0; i < genVerts.Length; i++)
            {
                deg = i * (360 / numVerts);
                float radian = ((int)(deg * (Math.PI / 180)));
                float x = (int)(r * Math.Cos(radian));
                float y = (int)(r * Math.Sin(radian));
                genVerts[i].X = x;
                genVerts[i].Y = y;
            }
            return genVerts;
        }

        public Vector2 HandleRotation(Vector2 newVert, float angle)
        {
            angle = (float)(Math.Atan2(rotation.Y, rotation.X) * 180 / Math.PI);

            float radians = (float)Math.PI * angle / 180f;
            float sin = (float)Math.Sin(radians);
            float cos = (float)Math.Cos(radians);

            float tempx = newVert.X;
            float tempy = newVert.Y;
            newVert.X = (float)(tempx * cos) - (tempy * sin);
            newVert.Y = (float)(tempx * sin) + (tempy * cos);

            return newVert;
        }

        public Vector2 SpawnRock(Vector2 pos)
        {
            int w = Helpers.gfx.PreferredBackBufferWidth;
            int h = Helpers.gfx.PreferredBackBufferHeight;
            int p = (2 * h) + (2 * w);

            Rectangle spawnRect = new Rectangle(20, 20, Helpers.gfx.PreferredBackBufferWidth - 40, Helpers.gfx.PreferredBackBufferHeight - 40);

            float randEdge = (float)rand.NextDouble() * p;

            if (randEdge < h)
            {
                //left side
                pos = new Vector2(spawnRect.Left, spawnRect.Bottom + h);
            }
            else if (randEdge < h + w) 
            {
                //top side
                pos = new Vector2(spawnRect.Right + randEdge - h, spawnRect.Bottom + spawnRect.Size.Y);
            }
            else if (randEdge < h + w + h)
            {
                // right side
                pos = new Vector2(spawnRect.Right + spawnRect.Width, spawnRect.Top + randEdge - (h + w));
            }
            else
            {
                //bottom side
                pos = new Vector2(spawnRect.Left + randEdge - (h + w + h), spawnRect.Top);
            }

            return pos;
        }
    }
}
