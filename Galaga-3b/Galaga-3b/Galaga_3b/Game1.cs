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

<<<<<<< HEAD
        enum gameState { start,play,quit};
=======
        enum gameState { start,play,hit,quit};
>>>>>>> master
        gameState state = gameState.start;
        string str;
        Vector2 vc2;
        SpriteFont spritefont1;

        Rectangle fighter;
        int playerSpeed;
        Texture2D player;
        int playerLifes;

        Texture2D missle;
        Rectangle missleR;
        List<Rectangle> missles;
        int missleTimer;
        List<Vector2> missleVelocity;
        int hitTimer;
        int score;

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
        Rectangle bottom;
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
<<<<<<< HEAD
            str = "welcome to galaga 3B demo. \n press T to begin the game and R to quit the game at anytime";
            vc2 = new Vector2(0, 50);
=======
            str = "welcome to galaga 3B demo. \n Press T to begin the game and R to quit the game at anytime";
            vc2 = new Vector2(0, 50);
            playerLifes = 3;
            score = 0;
>>>>>>> master

            random = new Random();
            ran = random.Next(1)+1;
            playerSpeed = 3;
            missleTimer = 0;
            eMissleTimer = 0;
<<<<<<< HEAD
=======
            hitTimer = 0;
>>>>>>> master

            right = new Rectangle(0, 0, 0, GraphicsDevice.Viewport.Height);
            left = new Rectangle(GraphicsDevice.Viewport.Width,0,0,GraphicsDevice.Viewport.Height);
            top = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, 0);
            bottom = new Rectangle(GraphicsDevice.Viewport.Height, 0, GraphicsDevice.Viewport.Width, 0);
            enemy1R.Add(new Rectangle(GraphicsDevice.Viewport.Width-100,150,50,50));
            for(int x=1;x<6;x++)
            {
                enemy1R.Add(new Rectangle(enemy1R[x-1].X-100,150,50,50));
            }
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
            spritefont1 = Content.Load<SpriteFont>("SpriteFont1");
            player = Content.Load<Texture2D>("player");
            missle = Content.Load<Texture2D>("pMissile"); //bug fixed
            eMissle = Content.Load<Texture2D>("White Square");
            enemy1 = Content.Load<Texture2D>("enemy 1");
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
<<<<<<< HEAD
                    
                    if (key.IsKeyDown(Keys.T))
                    {
                        state = gameState.play;
                        str = "";
                    }
                    else if(key.IsKeyDown(Keys.R))
=======

                    if (key.IsKeyDown(Keys.T))
                    {
                        state = gameState.play;
                    }
                    else if (key.IsKeyDown(Keys.R))
>>>>>>> master
                    {
                        state = gameState.quit;
                    }
                    break;
                case gameState.play:

                    str = "player lifes " + playerLifes+"\n total score "+score;
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
                        missleTimer = 30;
                    }
                    for (int x = 0; x < missles.Count; x++)
                    {
                        int y = missles[x].Y - (int)missleVelocity[x].Y;
                        missles[x] = new Rectangle(missles[x].X, y, missles[x].Width, missles[x].Height);

                        for (int e = 0; e < enemy1R.Count; e++)
                        {
                            if (missles[x].Intersects(enemy1R[e]))
                            {
                                enemy1R.Remove(enemy1R[e]);
                                e = -1;
                                score += 100;
                            }
                        }
                        for (int e = 0; e < enemy2R.Count; e++) { if (missles[x].Intersects(enemy2R[e])) { enemy2R.Remove(enemy2R[e]); e = -1; score += 100; } }
                        for (int e = 0; e < enemy3R.Count; e++) { if (missles[x].Intersects(enemy3R[e])) { enemy1R.Remove(enemy3R[e]); e = -1; score += 100; } }
                        for (int e = 0; e < enemy4R.Count; e++) { if (missles[x].Intersects(enemy4R[e])) { enemy1R.Remove(enemy4R[e]); e = -1; score += 100; } }
                        if (missles[x].Intersects(top))
                        {
                            missles.Remove(missles[x]);
                        }
                        x = 0;
                    }
                    if (fighter.Intersects(right)) { fighter.X += playerSpeed; }
                    if (fighter.Intersects(left)) { fighter.X -= playerSpeed; }
                    if (missleTimer > 0) { missleTimer--; }
                    if (eMissleTimer > 0) { eMissleTimer--; }
                    if (eMissleTimer <= 0)
                    {
                        if (enemy1R.Count != 0)
                        {
                            ran = random.Next(enemy1R.Count);
                            fire(enemy1R[ran]);
<<<<<<< HEAD
                            ran = random.Next(enemy1R.Count);
                        }
                        else if (enemy2R.Count != 0) { ran = random.Next(enemy2R.Count); fire(enemy2R[ran]); }
                        else if (enemy3R.Count != 0) { ran = random.Next(enemy3R.Count); fire(enemy3R[ran]); }
                        else if (enemy4R.Count != 0) { ran = random.Next(enemy4R.Count); fire(enemy4R[ran]); } 
=======
                        }
                        else if (enemy2R.Count != 0) { fire(enemy2R[ran]); }
                        else if (enemy3R.Count != 0) { fire(enemy3R[ran]); }
                        else if (enemy4R.Count != 0) { fire(enemy4R[ran]); }

                    }
                    for(int x=0;x<eMissles.Count;x++)
                    {
                        int y = eMissles[x].Y + (int)eMissleVelocity[x].Y;
                        eMissles[x] = new Rectangle(eMissles[x].X, y, eMissles[x].Width, eMissles[x].Height);
                        if (eMissles[x].Intersects(bottom)) { eMissles.Remove(eMissles[x]); }
                        if (eMissles[x].Intersects(fighter))
                        {
                            hitTimer = 180;
                            playerLifes--;
                            eMissles.Remove(eMissles[x]);
                            for (int c = 0; c < eMissles.Count; c++) { eMissles.Remove(eMissles[c]); }
                            state = gameState.hit;
                        }
>>>>>>> master
                    }
                    if (key.IsKeyDown(Keys.R))
                    {
                        state = gameState.quit;
                    }
                    break;
                case gameState.quit:
                    this.Exit();
<<<<<<< HEAD
=======
                    break;
                case gameState.hit:
                    hitTimer--;
                    str = "hit";
                    if (hitTimer == 0)
                    {
                        str = "done";
                        state = gameState.play;
                    }
>>>>>>> master
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
            spriteBatch.DrawString(spritefont1, str, vc2, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void fire(Rectangle rec)
        {
            eMissles.Add(new Rectangle(rec.X+rec.Width/2,rec.Y+25,10,25));
            eMissleVelocity.Add(new Vector2(1, 2));
            if (eMissles.Count != 1) { eMissleTimer = 30; }else { eMissleTimer = 180; }
        }
    }
    
}
