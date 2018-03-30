using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using C3.MonoGame;
using System.Collections.Generic;
using System;

namespace TRACK
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // 0 = menu, 1 = game, 2 = game over, 3 = Shop
        public int state = 0;
        public int mX, mY;
        public int score = 0, highScore = 0;
        public int sButtonTextLength;
        public int sButtonTextHeight;
        public int eButtonTextLength;
        public int eButtonTextHeight;
        public int mButtonTextLength;
        public int mButtonTextHeight;
        public int shButtonTextLength;
        public int shButtonTextHeight;
        public int timer = 60 * 5;
        public int currency = 10;
        public int previousScore = 0;
        public float lerpSpeed = 0.07f;
        public bool hasBegun = false;
        public bool newHighscore = false;
        public List<Block> blocks = new List<Block>();
        public Random r = new Random();
        public KeyboardState ks;
        public KeyboardState oldKs;
        public MouseState ms;
        public MouseState oldMs;
        public Rectangle sButtonBB;
        public Rectangle eButtonBB;
        public Rectangle shButtonBB;
        public Rectangle btmButtonBB;
        public Rectangle mBB;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 600
            };
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            Values.LoadContent(Content);
            InitBoxes();
        }

        public void InitBoxes()
        {
            mBB = new Rectangle(0, 0, 8, 8);
            sButtonBB = new Rectangle((graphics.PreferredBackBufferWidth / 2) - 100, (graphics.PreferredBackBufferHeight / 3) - 25, 200, 50);
            Vector2 sButtonSize = Values.titleFont.MeasureString("Start");
            sButtonTextLength = (int)sButtonSize.X;
            sButtonTextHeight = (int)sButtonSize.Y;

            shButtonBB = new Rectangle((graphics.PreferredBackBufferWidth / 2) - 100, (graphics.PreferredBackBufferHeight / 3) + 50, 200, 50);
            Vector2 shButtonSize = Values.titleFont.MeasureString("Shop");
            shButtonTextLength = (int)shButtonSize.X;
            shButtonTextHeight = (int)shButtonSize.Y;

            eButtonBB = new Rectangle((graphics.PreferredBackBufferWidth / 2) - 75, (graphics.PreferredBackBufferHeight / 5) * 4, 150, 50);
            Vector2 eButtonSize = Values.titleFont.MeasureString("Exit");
            eButtonTextLength = (int)eButtonSize.X;
            eButtonTextHeight = (int)eButtonSize.Y;

            Vector2 mButtonSize = Values.titleFont.MeasureString("Menu");
            mButtonTextLength = (int)eButtonSize.X;
            mButtonTextHeight = (int)eButtonSize.Y;

            btmButtonBB = new Rectangle(20, graphics.PreferredBackBufferHeight - 65, 200, 50);
        }
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }
        
        protected override void UnloadContent()
        {
            Values.UnloadContent();
        }

        void ResetGame()
        {
            score = 0;
            blocks.Clear();
            hasBegun = false;
            timer = 5 * 60;
            lerpSpeed = 0.07f;
        }
        
        protected override void Update(GameTime gameTime)
        {
            ks = Keyboard.GetState();
            if (oldKs == null) oldKs = ks;
            ms = Mouse.GetState();
            if (oldMs == null) oldMs = ms;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                if (state == 0 && oldKs.IsKeyDown(Keys.Escape) != true)
                {
                    Exit();
                } else if (state == 1 || state == 3)
                {
                    state = 0;
                    ResetGame();
                }
            }

            if (state == 1 && !hasBegun && ks.IsKeyDown(Keys.Space))
            {
                hasBegun = true;
                blocks.Add(new Block(r.Next(graphics.PreferredBackBufferWidth - 40), r.Next(graphics.PreferredBackBufferHeight - 40), lerpSpeed));
            }

            if (state == 1 && hasBegun)
            {
                for (int i = 0; i < blocks.Count; i++)
                {
                    blocks[i].Update();

                    if (mBB.Intersects(blocks[i].bb))
                    {
                        blocks[i].Lerp();
                    }

                    if (blocks[i].isDone)
                    {
                        blocks.RemoveAt(i);
                        Values.blockRemoved.Play(1f, (float)r.NextDouble(), 1f);
                        timer = 60 * 5 + (score * 2);
                        lerpSpeed = lerpSpeed - 0.001f;
                        blocks.Add(new Block(r.Next(graphics.PreferredBackBufferWidth - 40), r.Next(graphics.PreferredBackBufferHeight - 40), lerpSpeed));
                        score += 1;
                    }

                    if (timer == 0)
                    {
                        state = 2;
                        if (score > highScore)
                        {
                            highScore = score;
                            newHighscore = true;
                        }
                        previousScore = score;
                        ResetGame();
                    }
                }
                timer--;
            }

            mX = ms.X;
            mY = ms.Y;

            mBB.X = mX;
            mBB.Y = mY;

            if (state == 0) /* Check current state is menu */
            {
                /* Check if the start button is pressed */
                if (ms.LeftButton == ButtonState.Pressed && oldMs.LeftButton == ButtonState.Released)
                {
                    if (mBB.Intersects(sButtonBB))
                    {
                        newHighscore = false;
                        state = 1;
                    }

                    if (mBB.Intersects(eButtonBB))
                    {
                        Exit();
                    }

                    if (mBB.Intersects(shButtonBB))
                    {
                        state = 3;
                    }
                }
            }

            if (state == 2)
            {
                if (ms.LeftButton == ButtonState.Pressed && oldMs.LeftButton == ButtonState.Released)
                {
                    if (mBB.Intersects(sButtonBB))
                    {
                        state = 0;
                    }
                }
            }

            if (state == 3)
            {
                if (ms.LeftButton == ButtonState.Pressed && oldMs.LeftButton == ButtonState.Released)
                {
                    if (mBB.Intersects(btmButtonBB))
                    {
                        state = 0;
                    }
                }
            }


            //if (state == 1 && !hasBegun)
            //{
            //    if (ms.LeftButton == ButtonState.Pressed && oldMs.LeftButton == ButtonState.Released)
            //    {
            //        hasBegun = true;
            //    }
            //}

            if (ks != null) oldKs = ks;
            oldMs = ms;

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            #region Menu
            if (state == 0)
            {
                /* Menu State
                   Start Button */
                spriteBatch.DrawString(Values.titleFont, Values.gameName, Values.gameNamePos, Color.White);
                spriteBatch.DrawString(Values.descriptionFont, "HIGHSCORE: " + highScore, Values.gameText1Pos, Color.White);
                spriteBatch.Draw(Values.trackingOrb, Values.gameText1p2Pos, Color.White);
                spriteBatch.DrawString(Values.descriptionFont, currency.ToString(), Values.gameText1p2Pos + new Vector2(40, 8), Color.White);
                spriteBatch.DrawString(Values.descriptionFont, Values.creditsText, new Vector2(5, graphics.PreferredBackBufferHeight - 18), Color.White);
                if (mBB.Intersects(sButtonBB))
                {
                    spriteBatch.FillRectangle(sButtonBB, Color.DarkGreen);
                } else
                {
                    spriteBatch.FillRectangle(sButtonBB, Color.White);
                }
                spriteBatch.DrawString(Values.titleFont, "Start", new Vector2((graphics.PreferredBackBufferWidth / 2) - sButtonTextLength / 2, 
                    (graphics.PreferredBackBufferHeight / 3) - sButtonTextHeight / 2), Color.Black);

                if (mBB.Intersects(shButtonBB))
                {
                    spriteBatch.FillRectangle(shButtonBB, Color.Gray);
                }
                else
                {
                    spriteBatch.FillRectangle(shButtonBB, Color.White);
                }
                spriteBatch.DrawString(Values.titleFont, "Shop", new Vector2((graphics.PreferredBackBufferWidth / 2) - shButtonTextLength / 2,
                    (graphics.PreferredBackBufferHeight / 3) - (shButtonTextHeight / 2) + 75), Color.Black);

                if (mBB.Intersects(eButtonBB))
                {
                    spriteBatch.FillRectangle(eButtonBB, Color.DarkRed);
                }
                else
                {
                    spriteBatch.FillRectangle(eButtonBB, Color.White);
                }
                spriteBatch.DrawString(Values.titleFont, "Exit", new Vector2((graphics.PreferredBackBufferWidth / 2) - eButtonTextLength / 2,
                    (graphics.PreferredBackBufferHeight / 5 * 4) + 5), Color.Black);
            }
            #endregion

            #region Game
            if (state == 1)
            {
                if (!hasBegun)
                {
                    spriteBatch.DrawString(Values.titleFont, Values.gameName, Values.gameNamePos, Color.White);
                    spriteBatch.DrawString(Values.descriptionFont, Values.gameText1, Values.gameText1Pos, Color.White);
                    spriteBatch.DrawString(Values.descriptionFont, Values.gameText1p2, Values.gameText1p2Pos, Color.White);
                    spriteBatch.DrawString(Values.descriptionFont, Values.gameText2, Values.gameText2Pos, Color.White);
                }
                else
                {
                    if (score < highScore)
                    {
                        spriteBatch.DrawString(Values.titleFont, "Score: " + score, Values.gameNamePos, Color.White);
                    } else
                    {
                        spriteBatch.DrawString(Values.titleFont, "Score: " + score + " - NEW HIGHSCORE!", Values.gameNamePos, Color.White);
                    }
                    spriteBatch.DrawString(Values.descriptionFont, "Timer: " + timer / 60 + "s", Values.gameText1Pos, Color.White);
                    spriteBatch.DrawString(Values.descriptionFont, "Removal Speed: " + Math.Round(lerpSpeed, 3), Values.gameText1p2Pos, Color.White);
                }

                if (hasBegun)
                {
                    for (int i = 0; i < blocks.Count; i++)
                    {
                        blocks[i].Render(spriteBatch);
                    }
                }
            }
            #endregion

            #region Game Over Screen
            if (state == 2)
            {
                spriteBatch.DrawString(Values.titleFont, Values.gameOver, Values.gameNamePos, Color.White);
                if (newHighscore)
                {
                    spriteBatch.DrawString(Values.descriptionFont, "SCORE: " + previousScore + " (NEW HIGHSCORE!)", Values.gameText1Pos, Color.White);
                } else
                {
                    spriteBatch.DrawString(Values.descriptionFont, "SCORE: " + previousScore, Values.gameText1Pos, Color.White);
                }
                if (mBB.Intersects(sButtonBB))
                {
                    spriteBatch.FillRectangle(sButtonBB, Color.Gray);
                }
                else
                {
                    spriteBatch.FillRectangle(sButtonBB, Color.White);
                }
                spriteBatch.DrawString(Values.titleFont, "Menu", new Vector2((graphics.PreferredBackBufferWidth / 2) - mButtonTextLength / 2 - 10,
                    (graphics.PreferredBackBufferHeight / 3) - mButtonTextHeight / 2), Color.Black);
            }
            #endregion

            #region Shop
            if (state == 3)
            {
                if (mBB.Intersects(btmButtonBB))
                {
                    spriteBatch.FillRectangle(btmButtonBB, Color.Gray);
                }
                else
                {
                    spriteBatch.FillRectangle(btmButtonBB, Color.White);
                }
                spriteBatch.DrawString(Values.titleFont, "Menu", new Vector2(btmButtonBB.X + 64, btmButtonBB.Y + 6), Color.Black);
            }
            #endregion

            spriteBatch.Draw(Values.crosshair, new Vector2(mX, mY), Color.White);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
