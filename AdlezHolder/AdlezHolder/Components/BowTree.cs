﻿
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
    public class BowTree : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Color screenColor;
        BowUpgradeClass bowNodes;
        Character tempCharacter;
        bool oldEnabled;

        public BowTree(Game game, Character character)
            : base(game)
        {
            screenColor = Color.DarkSlateGray;
            tempCharacter = character;
        }

        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            bowNodes = new BowUpgradeClass(tempCharacter);
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (oldEnabled != this.Enabled)
                bowNodes.stopKeyPress();

            bowNodes.Update(gameTime);

            oldEnabled = this.Enabled;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(screenColor);

            spriteBatch.Begin();

            bowNodes.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}



