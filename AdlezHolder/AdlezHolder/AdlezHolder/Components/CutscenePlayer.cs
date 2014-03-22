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
    public class CutscenePlayer : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        MapDataHolder data, newMap;
        World world;
        Cutscene currentCutscene;
        bool setMap;

        public MapDataHolder Data
        {
            set { data = value; }
        }

        public Cutscene Scene
        {
            get { return currentCutscene; }
        }

        public CutscenePlayer(Game game, World mainWorld)
            : base(game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            world = mainWorld;
        }

        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (currentCutscene != null && currentCutscene.Over == true)
            {
                Game1.MainGameState = GameState.PLAYING;
                return;
            }
            else if (currentCutscene !=null)
            {
                addData(world.Map.CurrentData);
                currentCutscene.play(gameTime);//, spriteBatch);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public void addData(MapDataHolder data)
        {
            currentCutscene.Data = data;
        }

        private void changeGameState(GameState newState)
        {
            Game1.MainGameState = newState; 
        }

        public void playCutscene(Cutscene newScene, Character player)//, MapDataHolder nextMap)
        { 
            if (currentCutscene == newScene)
                return;
            //if (!setMap)
            //{
            //    world.Map.changeMap(new LeftPassage());
            //    setMap = true;
            //}
            currentCutscene = newScene;
            currentCutscene.Data = data;
            currentCutscene.Player = player;
            //newMap = nextMap;
            Game1.PreviousGameState = GameState.CUTSCENE;

          
        }
    }
}
