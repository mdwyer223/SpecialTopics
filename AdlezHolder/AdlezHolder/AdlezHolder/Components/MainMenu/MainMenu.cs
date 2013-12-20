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


namespace AdlezHolder
{
    public class MainMenu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        KeyboardState keys, oldKeys;
        SpriteBatch spriteBatch;
        Color screenColor;
        MenuInterface menu;

        public MainMenu(Game game)
            : base(game)
        {
            screenColor = Color.MidnightBlue;
        }

        public override void Initialize()
        {
            menu = new MenuInterface(Game.GraphicsDevice.Viewport, .3f);
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            keys = Keyboard.GetState();

            menu.Update(gameTime);

            oldKeys = keys;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(screenColor);

            spriteBatch.Begin();

            menu.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void changeGameState(GameState newState)
        {
            Game1.MainGameState = newState;
        }
    }
}
