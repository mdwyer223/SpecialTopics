using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AdlezHolder
{
    public class LibraryCutscene : Cutscene
    {
        int playOrder = 0;
        MessageBox box;
        bool show = false;
        bool openPit = false;

        public bool Pit
        {
            get { return openPit; }
            set { openPit = value; }
        }

        public LibraryCutscene()
        {
        }

        public override void play(GameTime gameTime)
        {
            //if (MediaPlayer.State == MediaState.Paused || MediaPlayer.State == MediaState.Playing)
            //{
            //    MediaPlayer.Resume();
            //}
            //else
            //{
            //    if (music != null)
            //    {
            //        MediaPlayer.Play(music);
            //    }
            //}

            if (playOrder == 0)
            {
                //world.Map.changeMap(new OutsideCave());
                Game1.ParticleState = ParticleState.RAIN;
                player.Position = new Vector2(-100, 235);
                playOrder++;
            }
            else if (playOrder == 1)
            {
                moveTo(new Vector2(88, 235), player, 1);
                //move right
                if (isAtPosition(new Vector2(88, 235), player) == true)
                    playOrder++;

                player.cutsceneUpdate(gameTime);
            }
            else if (playOrder == 2)
            {
                Game1.MainGameState = GameState.TALKING;
                if (!show)
                {
                    box = new MessageBox(1f, false);
                    box.show("Damn, It's raining. We better make this quick");
                    data.addMBox(box);
                    show = true;
                }

                if (!box.Visible)
                {
                    show = false;
                    playOrder++;
                }
            }
            else if (playOrder == 3)
            {
                moveTo(new Vector2(125, 270), player, 1);
                //move down and right
                if (isAtPosition(new Vector2(125, 270), player) == true)
                    playOrder++;

                player.cutsceneUpdate(gameTime);
            }
            else if (playOrder == 4)
            {
                moveTo(new Vector2(147, 300), player, 1);
                //move down and right
                if (isAtPosition(new Vector2(147, 300), player) == true)
                    playOrder++;

                player.cutsceneUpdate(gameTime);
            }
            else if (playOrder == 5)
            {
                moveTo(new Vector2(187, 334), player, 1);
                //move down and right
                if (isAtPosition(new Vector2(187, 334), player) == true)
                    playOrder++;

                player.cutsceneUpdate(gameTime);
            }
            else if (playOrder == 6)
            {
                Game1.MainGameState = GameState.TALKING;
                if (!show)
                {
                    box = new MessageBox(1f, true);
                    box.show("Blah, it's raining.");
                    data.addMBox(box);
                    show = true;
                }

                if (!box.Visible)
                {
                    show = false;
                    playOrder++;
                }
            }
            else if (playOrder == 7)
            {
                moveTo(new Vector2(230, 350), player, 1);
                //move down and right
                if (isAtPosition(new Vector2(230, 350), player) == true)
                    playOrder++;

                player.cutsceneUpdate(gameTime);
            }
            else if (playOrder == 8)
            {
                moveTo(new Vector2(286, 380), player, 1);
                //move down and right
                if (isAtPosition(new Vector2(286, 380), player) == true)
                    playOrder++;

                player.cutsceneUpdate(gameTime);
            }
            else if (playOrder == 9)
            {
                moveTo(new Vector2(386, 405), player, 1);
                //move down and right
                if (isAtPosition(new Vector2(386, 405), player) == true)
                    playOrder++;

                player.cutsceneUpdate(gameTime);
            }
            else if (playOrder == 10)
            {
                moveTo(new Vector2(550, 405), player, 1);
                //move right
                if (isAtPosition(new Vector2(550, 405), player) == true)
                    playOrder++;

                player.cutsceneUpdate(gameTime);
            }
            else if (playOrder == 11)
            {
                moveTo(new Vector2(590, 380), player, 1);
                //move up and right
                if (isAtPosition(new Vector2(590, 380), player) == true)
                    playOrder++;

                player.cutsceneUpdate(gameTime);
            }
            else if (playOrder == 12)
            {
                moveTo(new Vector2(630, 350), player, 1);
                //move up and right
                if (isAtPosition(new Vector2(630, 350), player) == true)
                    playOrder++;

                player.cutsceneUpdate(gameTime);
            }
            else if (playOrder == 13)
            {
                Game1.MainGameState = GameState.TALKING;
                player.setIsVisible(false);
                openPit = true;
                if (!show)
                {
                    box = new MessageBox(1f, true);
                    box.show("AAAAAAAAAHHHHHHHHHHH!!!!!!!!!!!!!!!");
                    data.addMBox(box);
                    show = true;
                }

                if (!box.Visible)
                {
                    show = false;
                    playOrder++;
                }
            }
            else if (playOrder == 14)
            {
                Pit = true;
                world.Map.changeMap(new Library());
                player.setIsVisible(true);
                player.Position = new Vector2(300, -200);
                playOrder++;
            }
            else if (playOrder == 15)
            {
                moveTo(new Vector2(300, 350), player, 6);
                //fall down
                if (isAtPosition(new Vector2(300, 346), player) == true)
                    playOrder++;

                //player.cutsceneUpdate(gameTime);
            }
            else if (playOrder == 16)
            {
                Game1.MainGameState = GameState.TALKING;
                if (!show)
                {
                    box = new MessageBox(1f, true);
                    box.show("Woah. Where am I?");
                    data.addMBox(box);
                    show = true;
                }

                if (!box.Visible)
                {
                    show = false;
                    playOrder++;
                }
            }
            else if (playOrder == 17)
            {
                moveTo(new Vector2(516, 269), player, 1);
                //move up and right
                if (isAtPosition(new Vector2(516, 269), player) == true)
                    playOrder++;

                player.cutsceneUpdate(gameTime);
            }
            else if (playOrder == 18)
            {
                moveTo(new Vector2(516, 274), player, 1);
                //move down
                if (isAtPosition(new Vector2(516, 274), player) == true)
                    playOrder++;

                player.cutsceneUpdate(gameTime);
            }

            //if (Game1.MainGameState == GameState.CUTSCENE) //makes it so that the character will stop walking during dialog
                //player.cutsceneUpdate(gameTime);

        }
    
    }
}
