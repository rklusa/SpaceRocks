using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Utility.Drawing;
using Asteroids.Utils;

namespace Asteroids.GameObjects
{
    public class Ship
    {
        private KeyboardState key;
        private KeyboardState oldKey;

        private Vector2[] verts;
        private Vector2[] thrustVerts;

        public Vector2 position;
        private Vector2 forward;
        private Vector2 velocity;
        private float angle;

        private bool thrusting;
        private int thrustFrames;

        public Ship(float x, float y) 
        {
            position = new Vector2 (x, y);

            verts = new Vector2[] {
                new Vector2(12, 0),
                new Vector2(-12, -9),
                new Vector2(-12, 9)
            };

            thrusting = false;

            thrustVerts = new Vector2[] {
                new Vector2 (-24, 0),
                new Vector2(-12, -6),
                new Vector2(-12, 6)
            };
        }
        public void Update(float deltaTime) 
        {
            forward.X = (float)MathF.Cos(angle);
            forward.Y = (float)MathF.Sin(angle);
            forward.Normalize();

            key = Keyboard.GetState();

            if (key.IsKeyDown(Keys.Space) && oldKey.IsKeyUp(Keys.Space))
            {
                FireBullet();
            }

            if(key.IsKeyDown(Keys.W))
            {
                velocity += forward * 2f;
                thrusting = true;
            }
            else
            {
                thrusting = false;
            }

            if (key.IsKeyDown(Keys.A))
            {
                angle -= 1f * deltaTime;
            }
            if (key.IsKeyDown(Keys.D))
            {
                angle += 1f * deltaTime;
            }

            thrustFrames++;

            if (thrustFrames >= 4) 
            {
                thrustFrames = 0;
            }

            position += velocity * deltaTime;
            velocity *= 0.99f;

            position = Helpers.HandleScreenWrap(position);

            oldKey = key;

            //Debug.WriteLine("angle:" + angle);
        }

        public void Draw(SpriteBatch batch) 
        {
            Vector2[] newVerts = new Vector2[verts.Length];
            Vector2[] newThrustVerts = new Vector2[thrustVerts.Length];

            for (int i = 0; i < verts.Length; i++)
            {
                newVerts[i] = HandleRotation(verts[i], angle);
                newThrustVerts[i] = HandleRotation(thrustVerts[i], angle);
            }

            batch.DrawPolygon(position, newVerts, true);

            if (thrusting)
            {
                if (thrustFrames >= 2)
                {
                    batch.DrawPolygon(position, newThrustVerts, true);
                }
                
            }
        }

        public void FireBullet()
        {
            Bullet tempBullet = new Bullet(position, forward);
            Helpers.bullets.Add(tempBullet);
        }

        public Vector2 HandleRotation(Vector2 newVert, float angle)
        {
            angle = (float)(Math.Atan2(forward.Y, forward.X) * 180 / Math.PI);

            float radians = (float)Math.PI * angle / 180f;
            float sin = (float)Math.Sin(radians);
            float cos = (float)Math.Cos(radians);

            float tempx = newVert.X;
            float tempy = newVert.Y;
            newVert.X = (float)(tempx * cos) - (tempy * sin);
            newVert.Y = (float)(tempx * sin) + (tempy * cos);
            
            return newVert;
        }
    }
}
