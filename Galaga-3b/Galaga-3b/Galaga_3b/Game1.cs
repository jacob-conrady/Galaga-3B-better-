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

        Rectangle fighter;
        int playerSpeed;
        int missleSpeed;
        Texture2D player;

        Texture2D missle;
        Rectangle missleR;
        List<Rectangle> missles;
        List<int> missleTimer;
        List<Vector2> missleVelocity;

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
            missleTimer = new List<int>();
            missleVelocity = new List<Vector2>();
            fighter = new Rectangle(GraphicsDevice.Viewport.Width / 2,GraphicsDevice.Viewport.Height-100, 50, 50);
            playerSpeed = 5;
            missleSpeed = 5;
            missleR = new Rectangle(500, 500, 25, 25);
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
            player = Content.Load<Texture2D>("White Square");
            missle = Content.Load<Texture2D>("White Square");
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            KeyboardState key = Keyboard.GetState();
            // TODO: Add your update logic here
            if (key.IsKeyDown(Keys.Right))
            {
                fighter.X += playerSpeed;
            }
            if(key.IsKeyDown(Keys.Left))
            {
                fighter.X -= playerSpeed;
            }
            if(key.IsKeyDown(Keys.Space))
            {
                missles.Add(new Rectangle(fighter.X-fighter.Width/2, fighter.Y-fighter.Height, 10, 25));
                missleVelocity.Add(new Vector2(2, 3));
            }
            for(int x=0;x<missles.Count;x++)
            {
                int y = missles[x].Y - (int)missleVelocity[x].Y;
                missles[x] = new Rectangle(missles[x].X, y, missles[x].Width, missles[x].Height);
                if (missles[x].Intersects(top))
                {
                    missles.Remove(missles[x]);
                }
            }
            if (fighter.Intersects(right)){ fighter.X += playerSpeed; }
            if (fighter.Intersects(left)) { fighter.X -= playerSpeed; }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(player, fighter, Color.White);
            for (int x = 0; x < missles.Count; x++)
            {
                spriteBatch.Draw(missle,missles[x], Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
