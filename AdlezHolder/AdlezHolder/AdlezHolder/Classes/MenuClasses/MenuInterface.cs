using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AdlezHolder
{
    public class MenuInterface
    {
        Song music;

        SaveSlots save;

        KeyboardState keys, oldKeys;
        int displayWidth;

        Button Newbutton, Loadbutton, Continuebutton, placementButton;
        int numOfButton;
        bool ePressed, contPressed;
        BaseSprite adlezGraphic;
        Rectangle overScan;
        int middleScreen, middleButton, buttonSeparation;
        Vector2 buttonPosition, adlezPos;
        Vector2 startPosition;
        //CutscenePlayer cutscene;
        //AlphaCutscene alphaScene;
        World world;
        Game game;
        //MapDataHolder data;

        //public MapDataHolder Data
        //{
        //    set { data = value; }
        //}

        public MenuInterface(Viewport view, float scaleFactor, World ogWorld, Game ogGame)
        {
            music = Game1.GameContent.Load<Song>("Music/Theme");

            displayWidth = view.Width;
            numOfButton = 0;
            save = new SaveSlots(view, scaleFactor);
            ePressed = true;
            contPressed = false;

            placementButton = new Button(Game1.GameContent.Load<Texture2D>("transparent"),
                .3f, displayWidth, buttonPosition, Game1.GameContent.Load<SpriteFont>("SpriteFont1"), "");

            int marginWidth = (int)(view.Width * .1);
            int marginHeight = (int)(view.Height * .1);
            int marginX = (int)(view.X * .1);

            overScan.Width = view.Width - marginWidth;
            overScan.Height = view.Height - marginHeight;

            overScan.X = view.X + marginWidth;
            overScan.Y = view.Y + marginHeight;

            middleScreen = overScan.Width / 2;
            middleButton = placementButton.DrawnRec.Width / 2;
            buttonPosition.X = middleScreen - middleButton;

            startPosition.X = buttonPosition.X;
            startPosition.Y = overScan.Y;
            
            buttonSeparation = (overScan.Height / 4) ;
            adlezPos.X = startPosition.X;
            adlezPos.Y = view.Y;
            
            adlezGraphic = new BaseSprite(Game1.GameContent.Load<Texture2D>("AdlezTitle"), .4f, overScan.Width,0, adlezPos);
            //adlezGraphic.Position = new Vector2((Game1.DisplayWidth / 2) - (adlezGraphic.DrawnRec.Width /2) , 0);
           

            startPosition.Y = startPosition.Y +( buttonSeparation );
            Newbutton = new Button(Game1.GameContent.Load<Texture2D>("MenuButtons/NewGame"), .4f, overScan.Width, startPosition, Game1.GameContent.Load<SpriteFont>("SpriteFont1"), "");
            startPosition.Y = startPosition.Y + buttonSeparation;
            Loadbutton = new Button(Game1.GameContent.Load<Texture2D>("MenuButtons/LoadGame"), .4f, overScan.Width, startPosition, Game1.GameContent.Load<SpriteFont>("SpriteFont1"), "");
            startPosition.Y = startPosition.Y + buttonSeparation;
            Continuebutton = new Button(Game1.GameContent.Load<Texture2D>("MenuButtons/Continue"), .4f, overScan.Width, startPosition, Game1.GameContent.Load<SpriteFont>("SpriteFont1"), "");

            Newbutton.Selected(Newbutton);

            world = ogWorld;
            game = ogGame;

        }

        public void Update(GameTime gameTime)
        {
            keys = Keyboard.GetState();

            if (MediaPlayer.State == MediaState.Paused || MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Resume();
            }
            else
            {
                MediaPlayer.Play(music);
            }

            if (ePressed)
            {
                Newbutton.Update(gameTime);
                Loadbutton.Update(gameTime);
                Continuebutton.Update(gameTime);
                adlezGraphic.Update(gameTime);

                if (keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S))
                {
                    if (numOfButton < 2)
                    {
                        numOfButton = numOfButton + 1;
                    }
                    else
                    {
                        numOfButton = 0;
                    }

                }

                if (keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W))
                {
                    if (numOfButton > 0)
                    {
                        numOfButton = numOfButton - 1;
                    }
                    else
                    {
                        numOfButton = 2;
                    }

                }
                if (numOfButton == 0)
                {
                    Newbutton.Selected(Newbutton);

                    Loadbutton.OrignalColor(Loadbutton);
                    Continuebutton.OrignalColor(Continuebutton);

                }
                else if (numOfButton == 1)
                {
                    Loadbutton.Selected(Loadbutton);

                    Continuebutton.OrignalColor(Continuebutton);
                    Newbutton.OrignalColor(Newbutton);
                }
                else
                {
                    Continuebutton.Selected(Continuebutton);

                    Loadbutton.OrignalColor(Loadbutton);
                    Newbutton.OrignalColor(Newbutton);
                }

                //Checks if enter is pressed and which button it is pressed on
                if (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter))
                {
                    if (numOfButton == 0)
                    {
                        MediaPlayer.Stop();
                        changeGameState(GameState.CUTSCENE);
                        //alphaScene = new AlphaCutscene(world);
                        //cutscene = new CutscenePlayer(game, world);
                        //cutscene.playCutscene(alphaScene, world.Map.Player);
                        //cutscene.Update(gameTime);
                    }

                    else if (numOfButton == 1)
                    {
                        //ePressed = false;
                    }
                    else if (numOfButton == 2)
                    {
                        MediaPlayer.Stop();
                        changeGameState(GameState.PLAYING);
                        contPressed = true;
                    }
                }

                oldKeys = keys;
            }
            else
            {
                save.Update(gameTime);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (ePressed)
            {
                adlezGraphic.Draw(spriteBatch);
                Newbutton.Draw(spriteBatch);
                Loadbutton.Draw(spriteBatch);
                Continuebutton.Draw(spriteBatch);
            }
            else
            {
                save.Draw(spriteBatch);
            }
        }

        private void changeGameState(GameState newState)
        {
            Game1.MainGameState = newState;
        }

    }
}

