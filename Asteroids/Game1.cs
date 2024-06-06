using Asteroids.GameObjects;
using Asteroids.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace Asteroids
{
    public class Game1 : Game
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private Ship player;
        private SpaceRock rock;

        private float width;
        private float height;

        private float delta; // delta time

        private bool menuActive = true;
        private int currentRound;

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
            currentRound = 0;
            Helpers.SpawnRocks(currentRound);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("Font");
        }

        protected override void Update(GameTime gameTime)
        {
            if (menuActive)
            {

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    menuActive = false;
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    menuActive = true;
                }

                delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
                player.Update(delta);

                if (Helpers.spaceRocks.Count == 0)
                {
                    currentRound += 1;
                    Helpers.SpawnRocks(currentRound);
                }

                foreach (Bullet obj in Helpers.bullets.ToList())
                {
                    if (!obj.isAlive)
                    {
                        Helpers.bullets.Remove(obj);
                    }

                    foreach (SpaceRock rock in Helpers.spaceRocks.ToList())
                    {
                        if (Helpers.CircleOverlap(obj.position, rock.position, rock.size + 1))
                        {
                            obj.isAlive = false;
                            rock.isAlive = false;
                        }
                    }

                    obj.Update(delta);
                }
            }

            foreach (SpaceRock rock in Helpers.spaceRocks.ToList())
            {

                if (!rock.isAlive)
                {
                    if (rock.size > 20)
                    {
                        Helpers.spawnRockFragments(rock.size / 2, rock.position);
                    }

                    Helpers.spaceRocks.Remove(rock);
                }

                rock.Update(delta);

                if (Helpers.CircleOverlap(player.position, rock.position, rock.size + 1))
                {
                    RestartGame();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            if (menuActive)
            {
                _spriteBatch.DrawString(_font, "Space Rocks", new Vector2(width / 3, height / 3), Color.White);
                _spriteBatch.DrawString(_font, "Press Space to Play.", new Vector2(width / 4, height / 2), Color.White);
                
            }
            else
            {
                string waveString = "Wave:" + currentRound.ToString();
                _spriteBatch.DrawString(_font, waveString, new Vector2(10, 10), Color.White);

                player.Draw(_spriteBatch);

                foreach (Bullet obj in Helpers.bullets.ToList())
                {
                    obj.Draw(_spriteBatch);
                }
            }

            foreach (SpaceRock rock in Helpers.spaceRocks.ToList())
            {
                rock.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void RestartGame()
        {
            currentRound = 0;
            player.position = new Vector2(width / 2, height / 2);
            Helpers.spaceRocks = new List<SpaceRock>();
            Helpers.SpawnRocks(currentRound);
            menuActive = true;
        }
    }
}