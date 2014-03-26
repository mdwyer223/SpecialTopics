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
        Game game;
        int count;
        const int TICK_SECOND = 60;

        public new bool Enabled
        {
            get { return base.Enabled; }
            set
            {
                base.Enabled = value;
                if (!value)                
                    count = 0;
                
            }

        }

        public MainMenu(Game game)
            : base(game)
        {
            screenColor = Color.MidnightBlue;
            this.game = game;
        }

        public override void Initialize()
        {
            menu = new MenuInterface();
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

            if (count > TICK_SECOND / 3)
            {
                if (keys.IsKeyDown(Keys.Escape) && oldKeys.IsKeyUp(Keys.Escape))
                {
                    game.Exit();
                }
            }
            else
            {
                count++;
            }
            
            if ((menu.isQuitSelected() == true) && (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter)))
            {   
                game.Exit();
            }
            
           
            
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
