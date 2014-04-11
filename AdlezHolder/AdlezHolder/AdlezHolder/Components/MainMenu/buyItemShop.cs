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
    public class buyItemShop : Microsoft.Xna.Framework.DrawableGameComponent
    {
        KeyboardState keys, oldKeys;
        SpriteBatch spriteBatch;
        Color screenColor;
        // buy sell menu
        Character tempcharacter;
        BasicItemShop itemShopBuying;
        bool addPlayer = false;
        Game1 game;

        public buyItemShop(Game game, Character character)
            : base(game)
        {
            screenColor = Color.MidnightBlue;
            tempcharacter = character;
            this.game = (Game1)game;
        }

        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (!addPlayer)
            {
                itemShopBuying = new BasicItemShop(tempcharacter);
                addPlayer = true;
            }
            keys = Keyboard.GetState();
            oldKeys = keys;
            itemShopBuying.getTownsItems(1);
            itemShopBuying.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();

            itemShopBuying.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void changeGameState(GameState newState)
        {
            Game1.MainGameState = newState;
        }
    }
}