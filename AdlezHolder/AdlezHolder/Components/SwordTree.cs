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
    public class SwordTree : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Color screenColor;
        SwordUpgradeClass swordNodes;
        Character tempCharacter;

        int count;
        const int TICK_IN_SEC = 60;

        public new bool Enabled
        {
            get { return base.Enabled; }
            set
            {
                base.Enabled = value;
                if (value == false)
                    count = 0;
            }
        }

        public SwordTree(Game game, Character character)
            : base(game)
        {
            screenColor = Color.DarkSlateBlue;
            tempCharacter = character;
        }

        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            swordNodes = new SwordUpgradeClass(tempCharacter);
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (count > TICK_IN_SEC / 3)
                swordNodes.Update(gameTime);
            else
                count++;
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(screenColor);

            spriteBatch.Begin();

            swordNodes.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}


