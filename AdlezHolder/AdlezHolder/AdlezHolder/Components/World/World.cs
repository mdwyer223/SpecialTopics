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
    
    public class World : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;

        KeyboardState keys, oldKeys;
        Color screenColor;

        Map map;

        public Map Map
        {
            get { return map; }
        }

        public World(Game game)
            : base(game)
        {
            screenColor = Color.Green;
            
        }
        
        public override void Initialize()
        {
            map = new Map();

            map.changeMap(new TestingField());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //font = Game1.GameContent.Load<SpriteFont>("PlayingFont");

            //for the world class, when the different maps are drawn, pass the ContentManager
            //in as a parameter to load objects for each different map

            //WORLD CLASS, FOR DIFFERENT PARTS OF THE ADLEZ WORLD
            
            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.Escape) && oldKeys.IsKeyUp(Keys.Escape))
            {
                changeGameState(GameState.PAUSEMENU);
            }

            if (map.CurrentData != null)
            {
                map.Update(gameTime);
            }

            oldKeys = keys;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.Green);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            //spriteBatch.DrawString(font, "P = Invent \nSpace = Cutscene\nO = shop\nEscape = Pause\nEnter = to return back", 
                //Vector2.Zero, Color.Blue);
            if (map.CurrentData != null)
            {
                map.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void changeGameState(GameState newState)
        {
            Game1.MainGameState = newState;
        }

        public void activateCutscene()
        {
            Game1.MainGameState = GameState.CUTSCENE;
        }
    }
}
