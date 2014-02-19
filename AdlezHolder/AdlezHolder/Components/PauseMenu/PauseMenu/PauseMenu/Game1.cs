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

namespace PauseMenu
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Vector2 position, inventPos, mapPos, resumePos, savePos, quitPos;
        Rectangle screenRect;
        PauseComponent menu;

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
            menu = new PauseComponent(this);
            Components.Add(menu);
            menu.Enabled = true;
            spriteFont = Content.Load<SpriteFont>("SpriteFont1");
            screenRect = new Rectangle((int)(GraphicsDevice.Viewport.Width * .05), (int)(GraphicsDevice.Viewport.Height * .05), (int)(GraphicsDevice.Viewport.Width * .9), (int)(GraphicsDevice.Viewport.Height * .9));
            position = new Vector2(screenRect.X, screenRect.Y);
            inventPos = new Vector2(position.X, (float)(position.Y * 4));
            mapPos = new Vector2(position.X, (float)(inventPos.Y * 1.75));
            resumePos = new Vector2(position.X, (mapPos.Y * ((float)10 / 7)));
            savePos = new Vector2(position.X, (resumePos.Y * ((float)13 / 10)));
            quitPos = new Vector2(position.X, (savePos.Y * ((float)16 / 13)));
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

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            menu.draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
