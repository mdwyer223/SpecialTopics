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
        SaveMenu save;
        KeyboardState keys, oldKeys;       
        BaseSprite adlezGraphic;
        int buttonIndex;
        bool ePressed;
        Button[] buttonArray;

        public MenuInterface()
        {
            int middleScreen, middleButton, widthSeparation;
            int displayWidth, displayHeight, heightSeparation;
            Button Quitbutton, Newbutton, Loadbutton, Continuebutton, PlacementButton;
            Rectangle overScan;
            Vector2 adlezPos, startPosition;
            BaseSprite tempAdlezPic;

            music = Game1.GameContent.Load<Song>("Music/Theme");

            displayWidth = Game1.DisplayWidth;
            displayHeight = Game1.DisplayHeight;

            buttonIndex = 1;
            ePressed = true;

            int marginWidth = (int)(displayWidth * .1);
            int marginHeight = (int)(displayHeight * .1);
            overScan.Width = displayWidth - marginWidth;
            overScan.Height = displayHeight - marginHeight;
            overScan.X = marginWidth;
            overScan.Y = marginHeight;
            
            //image was transparent
            PlacementButton = new Button(Game1.GameContent.Load<Texture2D>("Random/Particle"), .5f, Vector2.Zero);
            middleScreen = overScan.Width / 2;
            middleButton = PlacementButton.DrawnRec.Width / 2;

            heightSeparation = overScan.Height / 6;
            widthSeparation = overScan.Width / 4;

            startPosition.X = (widthSeparation * 2) - middleButton;
            startPosition.Y = heightSeparation * 3;

            adlezPos.X = widthSeparation * 2;
            adlezPos.Y = heightSeparation *2;

            tempAdlezPic = new BaseSprite(Game1.GameContent.Load<Texture2D>("Menu Pics/AdlezTitle"), .5f, overScan.Width, 0, adlezPos);
            adlezPos.X -= (tempAdlezPic.DrawnRec.Width/2);
            adlezPos.Y -= (tempAdlezPic.DrawnRec.Height /2);
            
            adlezGraphic = new BaseSprite(Game1.GameContent.Load<Texture2D>("Menu Pics/AdlezTitle"), .5f, overScan.Width, 0, adlezPos);

            Newbutton = new Button(Game1.GameContent.Load<Texture2D>("MenuButtons/NewGame"), .5f, startPosition);
            startPosition.Y = startPosition.Y + heightSeparation;
            Loadbutton = new Button(Game1.GameContent.Load<Texture2D>("MenuButtons/LoadGame"), .5f, startPosition);
            startPosition.Y = startPosition.Y + heightSeparation;
            Continuebutton = new Button(Game1.GameContent.Load<Texture2D>("MenuButtons/Continue"), .5f, startPosition);

            Quitbutton = new Button(Game1.GameContent.Load<Texture2D>("Exit"), .25f, startPosition);
            startPosition.Y = displayHeight - 60;
            startPosition.X = displayWidth - 110;
            Quitbutton = new Button(Game1.GameContent.Load<Texture2D>("Exit"), .25f, startPosition);

            Newbutton.Selected = true;
            buttonArray = new Button[4];
            buttonArray[0] = Quitbutton;
            buttonArray[1] = Newbutton;
            buttonArray[2] = Loadbutton;
            buttonArray[3] = Continuebutton;
            

        }

        public bool isQuitSelected()
        {
            if (buttonIndex == 0)
                return true;
            return false;
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

            if (ePressed)//TODO: change to game state
            {

                if (keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S))
                {
                    if (buttonIndex != 3)
                    {
                        buttonIndex += 1;
                    }
                    else
                    {
                        buttonIndex = 0;
                    }

                }

                if (keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W))
                {
                    if (buttonIndex != 0)
                    {
                        buttonIndex -= 1;
                    }
                    else
                    {
                        buttonIndex = 3;
                    }

                }
                if (buttonIndex == 0)
                {
                    buttonArray[0].Selected = true;
                    buttonArray[1].Selected = false;
                    buttonArray[2].Selected = false;
                    buttonArray[3].Selected = false;
                }
                else if (buttonIndex == 1)
                {
                    buttonArray[0].Selected = false;
                    buttonArray[1].Selected = true;
                    buttonArray[2].Selected = false;
                    buttonArray[3].Selected = false;
                }
                else if (buttonIndex == 2)
                {
                    buttonArray[0].Selected = false;
                    buttonArray[1].Selected = false;
                    buttonArray[2].Selected = true;
                    buttonArray[3].Selected = false;
                }
                else
                {
                    buttonArray[0].Selected = false;
                    buttonArray[1].Selected = false;
                    buttonArray[2].Selected = false;
                    buttonArray[3].Selected = true;
                }


                //Checks if enter is pressed and which button it is pressed on
                if (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter))
                {
                    if (buttonIndex == 1)
                    {
                        MediaPlayer.Stop();
                        //save = new SaveMenu(senderButton.NEW);
                        changeGameState(GameState.INTRO);
                        ePressed = false;
                    }

                    else if (buttonIndex == 2)
                    {
                        this.changeGameState(GameState.SAVEMENU);
                    }
                    else if (buttonIndex == 3)
                    {
                        MediaPlayer.Stop();
                        changeGameState(GameState.PLAYING);
                    }
                }

                oldKeys = keys;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
                adlezGraphic.Draw(spriteBatch);
                for (int i = 0; i < buttonArray.Length; i++)
                {
                    buttonArray[i].Draw(spriteBatch);
                }
                
        }

        private void changeGameState(GameState newState)
        {
            Game1.MainGameState = newState;
        }

    }
}

