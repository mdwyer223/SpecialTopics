using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace AdlezHolder
{
    public class AlphaCutscene : Cutscene
    {
        int playOrder;
        int lastOrder = 9;
        bool setMap;
        Song music;

        MessageBox box;
        bool show = false;

        public AlphaCutscene(World ogWorld)
        {
            music = Game1.GameContent.Load<Song>("Music/TravelingSong");
            box = new MessageBox(1f);
            world = ogWorld;
        }

        public AlphaCutscene()
        {
            music = Game1.GameContent.Load<Song>("Music/TravelingSong");
            box = new MessageBox(1f);
        }

        public override void play(GameTime gameTime)
        {
            if (MediaPlayer.State == MediaState.Paused || MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Resume();
            }
            else
            {
                if (music != null)
                {
                    MediaPlayer.Play(music);
                }
            }

            if (playOrder == 0)
            {
                player.Position = new Vector2(765, 362);
                world.Map.changeMap(new LeftPassage());
                playOrder++;
            }
            else if (playOrder == 1)
            {
                moveTo(new Vector2(665, 362), player, 1);
                //moveLeft
                if (isAtPosition(new Vector2(665, 362), player) == true)
                    playOrder++;
            }
            else if (playOrder == 2)
            {
                //moveUp
                moveTo(new Vector2(665, 315), player, 1);
                if (isAtPosition(new Vector2(665, 315), player) == true)
                    playOrder++;
            }
            else if (playOrder == 3)
            {
                //moveLeft
                moveTo(new Vector2(375, 315), player, 1);
                if (isAtPosition(new Vector2(375, 315), player) == true)
                    playOrder++;
            }
            else if (playOrder == 4)
            {
                //moveUp
                moveTo(new Vector2(375, 255), player, 1);
                if (isAtPosition(new Vector2(375, 255), player) == true)
                    playOrder++;
            }
            else if (playOrder == 5)
            {
                Game1.MainGameState = GameState.TALKING;
                if (!show)
                {
                    box = new MessageBox(1f);
                    box.show("Hi this is bob. Hi this is bob.Hi this is bob.Hi this is bob.Hi this is bob.Hi this is bob.Hi this is bob.Hi this is bob.Hi this is bob.Hi this is bob.Hi this is bob.");
                    data.addMBox(box);
                    show = true;
                }

                if (!box.Visible)
                {
                    show = false;
                    playOrder++;
                }
            }
            else if (playOrder == 6)
            {
                //moveLeft
                moveTo(new Vector2(140, 255), player, 1);

                if (isAtPosition(new Vector2(140, 255), player) == true)
                    playOrder++;
            }
            else if (playOrder == 7)
            {
                Game1.MainGameState = GameState.TALKING;;
                playOrder++;
            }
            else if (playOrder == 8)
            {
                //moveUp(player, 1);
                moveTo(new Vector2(140, 155), player, 1);

                if (isAtPosition(new Vector2(140, 155), player) == true)
                {
                    playOrder++;
                    player.cutsceneMove(Orientation.UP);
                    over = true;
                    world.Map.changeMap(new MainRoom());
                }
            }

            player.cutsceneUpdate(gameTime);

            if (playOrder == lastOrder)
            {
                music = null;
                MediaPlayer.Stop();
                
            }
        }
    }
}
