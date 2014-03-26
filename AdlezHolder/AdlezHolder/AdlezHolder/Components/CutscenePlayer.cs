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
    //public class CutscenePlayer : Microsoft.Xna.Framework.GameComponent
    //{
    //    KeyboardState keys, oldKeys;
    //    SpriteBatch spriteBatch;
    //    MapDataHolder data;

    //    Cutscene currentCutscene;

    //    public MapDataHolder Data
    //    {
    //        set { data = value; }
    //    }

    //    public Cutscene Scene
    //    {
    //        get { return currentCutscene; }
    //    }

    //    public CutscenePlayer(Game game)
    //        : base(game)
    //    {
    //        spriteBatch = new SpriteBatch(game.GraphicsDevice);
    //    }

    //    public override void Initialize()
    //    {
    //        // TODO: Add your initialization code here

    //        base.Initialize();
    //    }

    //    /// <summary>
    //    /// Allows the game component to update itself.
    //    /// </summary>
    //    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    //    public override void Update(GameTime gameTime)
    //    {
    //        if (currentCutscene == null)
    //        {
    //            Game1.MainGameState = GameState.PLAYING;
    //            return;
    //        }
    //        else
    //        {
    //            currentCutscene.play(gameTime, spriteBatch);
    //        }

    //        base.Update(gameTime);
    //    }

    //    private void changeGameState(GameState newState)
    //    {
    //        Game1.MainGameState = newState;
    //    }

    //    public void playCutscene(Cutscene newScene, Character player)
    //    {
    //        if (currentCutscene == newScene)
    //            return;
    //        currentCutscene = newScene;
    //        currentCutscene.Data = data;
    //        currentCutscene.Player = player;
    //    }
    //}
}
