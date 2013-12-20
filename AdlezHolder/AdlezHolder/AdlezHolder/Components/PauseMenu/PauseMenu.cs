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
    public class PauseMenu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        KeyboardState keys, oldKeys;
        Color screenColor;

        public PauseMenu(Game game)
            : base(game)
        {
            screenColor = Color.Red;
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter))
            {
                changeGameState(GameState.PLAYING);
            }

            oldKeys = keys;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);

            base.Draw(gameTime);
        }

        private void changeGameState(GameState newState)
        {
            Game1.MainGameState = newState;
        }
    }
}
