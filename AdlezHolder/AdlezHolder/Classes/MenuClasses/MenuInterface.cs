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
            Button Newbutton, Loadbutton, Continuebutton, placementButton;
            Rectangle overScan;
            Vector2 adlezPos, startPosition;
            BaseSprite tempAdlezPic;

            music = Game1.GameContent.Load<Song>("Music/Theme");

            displayWidth = Game1.DisplayWidth;
            displayHeight = Game1.DisplayHeight;

            buttonIndex = 0;
            ePressed = true;

            int marginWidth = (int)(displayWidth * .1);
            int marginHeight = (int)(displayHeight * .1);
            overScan.Width = displayWidth - marginWidth;
            overScan.Height = displayHeight - marginHeight;
            overScan.X = marginWidth;
            overScan.Y = marginHeight;

            placementButton = new Button(Game1.GameContent.Load<Texture2D>("transparent"), .5f, Vector2.Zero);
            middleScreen = overScan.Width / 2;
            middleButton = placementButton.DrawnRec.Width / 2;

            heightSeparation = overScan.Height / 6;
            widthSeparation = overScan.Width / 4;

            startPosition.X = (widthSeparation * 2) - middleButton;
            startPosition.Y = heightSeparation * 3;

            adlezPos.X = widthSeparation * 2;
            adlezPos.Y = heightSeparation *2;

            tempAdlezPic = new BaseSprite(Game1.GameContent.Load<Texture2D>("AdlezTitle"), .5f, overScan.Width, 0, adlezPos);
            adlezPos.X -= (tempAdlezPic.DrawnRec.Width/2);
            adlezPos.Y -= (tempAdlezPic.DrawnRec.Height /2);
            
            adlezGraphic = new BaseSprite(Game1.GameContent.Load<Texture2D>("AdlezTitle"), .5f, overScan.Width, 0, adlezPos);

            Newbutton = new Button(Game1.GameContent.Load<Texture2D>("MenuButtons/NewGame"), .5f, startPosition);
            startPosition.Y = startPosition.Y + heightSeparation;
            Loadbutton = new Button(Game1.GameContent.Load<Texture2D>("MenuButtons/LoadGame"), .5f, startPosition);
            startPosition.Y = startPosition.Y + heightSeparation;
            Continuebutton = new Button(Game1.GameContent.Load<Texture2D>("MenuButtons/Continue"), .5f, startPosition);

            Newbutton.Selected = true;
            buttonArray = new Button[3];
            buttonArray[0] = Newbutton;
            buttonArray[1] = Loadbutton;
            buttonArray[2] = Continuebutton;

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
                    if (buttonIndex < 2)
                    {
                        buttonIndex = (buttonIndex + 1) % 3;
                    }

                }

                if (keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W))
                {
                    if (buttonIndex > 0)
                    {
                        buttonIndex = buttonIndex - 1;
                    }
                    else
                    {
                        buttonIndex = 2;
                    }

                }
                if (buttonIndex == 0)
                {
                    buttonArray[0].Selected = true;
                    buttonArray[1].Selected = false;
                    buttonArray[2].Selected = false;
                }
                else if (buttonIndex == 1)
                {
                    buttonArray[0].Selected = false;
                    buttonArray[1].Selected = true;
                    buttonArray[2].Selected = false;
                }
                else
                {
                    buttonArray[0].Selected = false;
                    buttonArray[1].Selected = false;
                    buttonArray[2].Selected = true;
                }

                //Checks if enter is pressed and which button it is pressed on
                if (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter))
                {
                    if (buttonIndex == 0)
                    {
                        MediaPlayer.Stop();
                        //save = new SaveMenu(senderButton.NEW);
                        changeGameState(GameState.CUTSCENE);
                        ePressed = false;
                    }

                    else if (buttonIndex == 1)
                    {
                        this.changeGameState(GameState.SAVEMENU);
                    }
                    else if (buttonIndex == 2)
                    {
                        MediaPlayer.Stop();
                        changeGameState(GameState.UPGRADESHOP);
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

