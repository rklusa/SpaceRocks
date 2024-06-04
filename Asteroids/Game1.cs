using Asteroids.GameObjects;
using Asteroids.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame;
using System.Diagnostics;
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
            rock = new SpaceRock(25, Vector2.Zero);
            Helpers.spaceRocks.Add(rock);

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

            foreach(SpaceRock rock in Helpers.spaceRocks.ToList())
            { 

                if (!rock.isAlive)
                {
                    if (rock.size > 20)
                    {
                        spawnRockFragments(rock.size / 2, rock.position);
                    }

                    Helpers.spaceRocks.Remove(rock);
                }

                rock.Update(delta);
                if (Helpers.CircleOverlap(player.position,rock.position, rock.size + 1))
                {
                    // todo: add lives mechanic for when player gets hit by an asteroid
                    Debug.WriteLine("the ship hit a rock!");
                }
            }

            foreach(Bullet obj in Helpers.bullets.ToList())
            {
                if (!obj.isAlive)
                {
                    Helpers.bullets.Remove(obj);
                }

                foreach(SpaceRock rock in Helpers.spaceRocks.ToList())
                {
                    if (Helpers.CircleOverlap(obj.position, rock.position, rock.size + 1))
                    {
                        obj.isAlive = false;
                        rock.isAlive = false;

                    }
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
            foreach(SpaceRock rock in Helpers.spaceRocks.ToList() )
            {
                rock.Draw(_spriteBatch);
            }
            
            foreach(Bullet obj in Helpers.bullets.ToList())
            {
                obj.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void spawnRockFragments(float size, Vector2 pos)
        {
            for (int i = 0; i < 2; i++)
            {
                SpaceRock rock = new SpaceRock(size, pos);
                Helpers.spaceRocks.Add(rock);
            }
        }
    }
}