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
        Game1 game;

        KeyboardState keys, oldKeys;       

        Button Newbutton, Loadbutton, Continuebutton;
        BaseSprite adlezGraphic;
        int buttonIndex;
        bool ePressed;

        public MenuInterface(Game1 game)
        {
            this.game = game;

            int middleScreen, middleButton, buttonSeparation;
            int displayWidth, displayHeight;
            Button placementButton;
            Rectangle overScan;            
            Vector2 buttonPosition, adlezPos;
            Vector2 startPosition;

            buttonPosition = Vector2.Zero;

            music = Game1.GameContent.Load<Song>("Music/Theme");

            displayWidth = Game1.DisplayWidth;
            displayHeight = Game1.DisplayHeight;

            buttonIndex = 0;
            // save = new SaveMenu();
            ePressed = true;

            placementButton = new Button(Game1.GameContent.Load<Texture2D>("transparent"), .3f, buttonPosition);

            int marginWidth = (int)(displayWidth * .1);
            int marginHeight = (int)(displayHeight * .1);

            overScan.Width = displayWidth - marginWidth;
            overScan.Height = displayHeight - marginHeight;

            overScan.X = marginWidth;
            overScan.Y = marginHeight;

            middleScreen = overScan.Width / 2;
            middleButton = placementButton.DrawnRec.Width / 2;
            buttonPosition.X = middleScreen - middleButton;

            startPosition.X = buttonPosition.X;
            startPosition.Y = overScan.Y;
            
            buttonSeparation = (overScan.Height / 4) ;
            adlezPos.X = startPosition.X;
            adlezPos.Y = 0;//TODO: cahnge
            
            adlezGraphic = new BaseSprite(Game1.GameContent.Load<Texture2D>("Menu Pics/AdlezTitle"), .4f, overScan.Width, 0, adlezPos);
           

            startPosition.Y = startPosition.Y +( buttonSeparation );
            Newbutton = new Button(Game1.GameContent.Load<Texture2D>("MenuButtons/NewGame"), .4f, startPosition);
            startPosition.Y = startPosition.Y + buttonSeparation;
            Loadbutton = new Button(Game1.GameContent.Load<Texture2D>("MenuButtons/LoadGame"), .4f, startPosition);
            startPosition.Y = startPosition.Y + buttonSeparation;
            Continuebutton = new Button(Game1.GameContent.Load<Texture2D>("MenuButtons/Continue"), .4f, startPosition);

            Newbutton.Selected = true;

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
                    Newbutton.Selected = true;
                    Loadbutton.Selected = false;
                    Continuebutton.Selected = false;
                }
                else if (buttonIndex == 1)
                {
                    Newbutton.Selected = false;
                    Loadbutton.Selected = true;
                    Continuebutton.Selected = false;                    
                }
                else
                {
                    Newbutton.Selected = false;
                    Loadbutton.Selected = false;
                    Continuebutton.Selected = true;                    
                }

                //Checks if enter is pressed and which button it is pressed on
                if (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter))
                {
                    if (buttonIndex == 0)
                    {                        
                        save = new SaveMenu(game, SenderButton.NEW);
                        ePressed = false;
                    }
                    else if (buttonIndex == 1)
                    {
                        save = new SaveMenu(game, SenderButton.LOAD);
                        ePressed = false;
                    }
                    else if (buttonIndex == 2)
                    {
                        MediaPlayer.Stop();
                        changeGameState(GameState.PLAYING);
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

