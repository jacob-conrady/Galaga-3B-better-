using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Galaga_3b
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum gameState { start,play};
        gameState state = gameState.start;

        Rectangle fighter;
        int playerSpeed;
        Texture2D player;

        Texture2D missle;
        Rectangle missleR;
        List<Rectangle> missles;
        int missleTimer;
        List<Vector2> missleVelocity;

        Texture2D enemy1;
        Texture2D enemy2;
        Texture2D enemy3;
        Texture2D enemy4;
        List<Rectangle> enemy1R;//1 for each row
        List<Rectangle> enemy2R;
        List<Rectangle> enemy3R;
        List<Rectangle> enemy4R;
        List<Rectangle> eMissles;
        Texture2D eMissle;
        List<Vector2> eMissleVelocity;
        int eMissleTimer;
        Random random;
        int ran;

        Rectangle right;
        Rectangle left;
        Rectangle top;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            missles = new List<Rectangle>();
            missleVelocity = new List<Vector2>();
            enemy1R = new List<Rectangle>();
            enemy2R = new List<Rectangle>();
            enemy3R = new List<Rectangle>();
            enemy4R = new List<Rectangle>();
            eMissles = new List<Rectangle>();
            eMissleVelocity = new List<Vector2>();
            fighter = new Rectangle(GraphicsDevice.Viewport.Width / 2,GraphicsDevice.Viewport.Height-100, 50, 50);
            random = new Random();
            ran = random.Next(1)+1;
            playerSpeed = 5;
            missleTimer = 0;
            eMissleTimer = 0;
            right = new Rectangle(0, 0, 0, GraphicsDevice.Viewport.Height);
            left = new Rectangle(GraphicsDevice.Viewport.Width,0,0,GraphicsDevice.Viewport.Height);
            top = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, 0);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = Content.Load<Texture2D>("player");
            missle = Content.Load<Texture2D>("pMissile"); //bug fixed
            eMissle = Content.Load<Texture2D>("White Square");
            enemy1 = Content.Load<Texture2D>("White Square");
            enemy2 = Content.Load<Texture2D>("White Square");
            enemy3 = Content.Load<Texture2D>("White Square");
            enemy4 = Content.Load<Texture2D>("White Square");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            KeyboardState key = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            // TODO: Add your update logic here
            switch (state)
            {
                case gameState.start:
                    state = gameState.play;
                    break;
                case gameState.play:
                    if (key.IsKeyDown(Keys.Right))
                    {
                        fighter.X += playerSpeed;
                    }
                    if (key.IsKeyDown(Keys.Left))
                    {
                        fighter.X -= playerSpeed;
                    }
                    if (key.IsKeyDown(Keys.Space) && missleTimer == 0)
                    {
                        missles.Add(new Rectangle(fighter.X + fighter.Width / 2, fighter.Y - fighter.Height, 10, 25));
                        missleVelocity.Add(new Vector2(1, 2));
                        missleTimer = 60;
                    }
                    for (int x = 0; x < missles.Count; x++)
                    {
                        int y = missles[x].Y - (int)missleVelocity[x].Y;
                        missles[x] = new Rectangle(missles[x].X, y, missles[x].Width, missles[x].Height);
                        if (missles[x].Intersects(top))
                        {
                            missles.Remove(missles[x]);
                        }
                    }
                    if (fighter.Intersects(right)) { fighter.X += playerSpeed; }
                    if (fighter.Intersects(left)) { fighter.X -= playerSpeed; }
                    if (missleTimer > 0) { missleTimer--; }
                    if (eMissleTimer > 0) { eMissleTimer--; }
                    if (eMissleTimer <= 0)
                    {
                        if(enemy1R.Count!=0)
                        {
                            fire(enemy1R[ran]);
                        }else if (enemy2R.Count != 0) { fire(enemy2R[ran]); }
                        else if (enemy3R.Count != 0) { fire(enemy3R[ran]); }
                        else if (enemy4R.Count != 0) { fire(enemy4R[ran]); }
                    }

                    break;
                default:
                    break;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(player, fighter, Color.White);
            for (int x = 0; x < missles.Count; x++)
            {
                spriteBatch.Draw(missle,missles[x], Color.White);
            }
            for(int x=0;x<eMissles.Count;x++)
            {
                spriteBatch.Draw(eMissle, eMissles[x], Color.White);
            }
            for(int x=0;x<enemy1R.Count();x++)
            {
                spriteBatch.Draw(enemy1, enemy1R[x], Color.White);
            }
            for (int x = 0; x < enemy2R.Count(); x++)
            {
                spriteBatch.Draw(enemy2, enemy2R[x], Color.White);
            }
            for (int x = 0; x < enemy3R.Count(); x++)
            {
                spriteBatch.Draw(enemy3, enemy3R[x], Color.White);
            }
            for (int x = 0; x < enemy4R.Count(); x++)
            {
                spriteBatch.Draw(enemy4, enemy4R[x], Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void fire(Rectangle rec)
        {
            eMissles.Add(new Rectangle(rec.X,rec.Y+25,10,25));
        }
    }
    
}
