﻿using Asteroids.GameObjects;
using Asteroids.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;
using System.Linq;
using Utility.Drawing;

namespace Asteroids
{
    public class Game1 : Game
    {
        private SpriteBatch _spriteBatch;
        private Ship player;
        private SpaceRock rock;

        private float width;
        private float height;

        private float delta; // delta time

        public Game1()
        {
            Helpers.gfx = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            width = Helpers.gfx.PreferredBackBufferWidth;
            height = Helpers.gfx.PreferredBackBufferHeight;

            player = new Ship(width / 2, height / 2);
            rock = new SpaceRock(new Vector2(100, 100));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            player.Update(delta);

            rock.Update(delta);

            foreach(Bullet obj in Helpers.bullets.ToList())
            {
                if (!obj.isAlive)
                {
                    Helpers.bullets.Remove(obj);
                }

                obj.Update(delta);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            player.Draw(_spriteBatch);

            rock.Draw(_spriteBatch);
            
            foreach(Bullet obj in Helpers.bullets.ToList())
            {
                obj.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}