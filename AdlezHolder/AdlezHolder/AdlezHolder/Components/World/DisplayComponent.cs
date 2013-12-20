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
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class DisplayComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        WeaponSelect weaponSelect;
        HealthBar health;
        Character player;

        int max, current;

        public DisplayComponent(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            weaponSelect = new WeaponSelect(WeaponSelect.SelectedWeapon.SWORD);
            health = new HealthBar(((Game1)Game));
            spriteBatch = new SpriteBatch(((Game1)Game).GraphicsDevice);
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            health.update(max, current, player);
            weaponSelect.update(player, gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            weaponSelect.draw(spriteBatch);
            health.draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void getPlayerHealth(int max, int current)
        {
            this.max = max;
            this.current = current;
        }

        public void getPlayer(Character player)
        {
            this.player = player;
        }
    }
}
