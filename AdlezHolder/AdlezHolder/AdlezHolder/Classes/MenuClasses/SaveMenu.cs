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
    public enum SenderButton
    {
        NEW, LOAD
    }

    public class SaveMenu
    {
        Game1 game;

        int buttonIndex;
        KeyboardState keys, oldKeys;
        Button[] buttonArray;
        BaseSprite[] picArray;
        SenderButton sender;

        public SaveMenu(Game1 game, SenderButton sender)
        {
            this.game = game;
            this.sender = sender;
            oldKeys = keys = Keyboard.GetState();

            int displayWidth, displayHeight;
            int middleScreen, middleButton, buttonSeparation, widthSeperation, heightSeparation;
            Vector2 buttonPosition, picPosition, swordUpgradePosition;
            Vector2 startPosition;
            BaseSprite bowPic, swordPic, bombPic;
            Rectangle overScan;
            Button placementButton, file1Button, file2Button, file3Button;

            
            // PLACMENT CODE_____________________________________________________________________________
            displayWidth = Game1.DisplayWidth;
            displayHeight = Game1.DisplayHeight;
            buttonIndex = 0;
            placementButton = new Button(Game1.GameContent.Load<Texture2D>("transparent"), .3f, Vector2.Zero);

            int marginWidth = (int)(displayHeight * .1);
            int marginHeight = (int)(displayHeight * .1);

            overScan.Width = displayWidth - marginWidth;
            overScan.Height = displayHeight - marginHeight;

            overScan.X = marginWidth;
            overScan.Y = marginHeight;

            middleScreen = overScan.Width / 2;
            middleButton = placementButton.DrawnRec.Width / 2;
            buttonPosition.X = middleScreen - middleButton;

            startPosition.X = overScan.X;
            startPosition.Y = overScan.Y;
            
            picPosition.X = startPosition.Y * 7;
            picPosition.Y = (float)(startPosition.X * 1.05);
            buttonSeparation = overScan.Y * 3;
            widthSeperation = overScan.Width / 5;

            heightSeparation = overScan.Height / 5;
            swordUpgradePosition.X = heightSeparation * 3;
            swordUpgradePosition.Y = widthSeperation;

            const float PIC_SCALE_FACTOR = .1f;
            swordPic = new BaseSprite(Game1.GameContent.Load<Texture2D>("Wep Icons/sword selected"), PIC_SCALE_FACTOR, displayWidth, 0, picPosition);
            picPosition.X = picPosition.X + widthSeperation;
            bombPic = new BaseSprite(Game1.GameContent.Load<Texture2D>("Wep Icons/bomb selected"), PIC_SCALE_FACTOR, displayWidth, 0, picPosition);
            picPosition.X = picPosition.X + widthSeperation;
            bowPic = new BaseSprite(Game1.GameContent.Load<Texture2D>("Wep Icons/bow selected"), PIC_SCALE_FACTOR, displayWidth, 0, picPosition);


            Texture2D buttonImage = Game1.GameContent.Load<Texture2D>("The best thing ever");

            file1Button = new Button(buttonImage, .25f, startPosition);
            startPosition.Y += heightSeparation;
            file2Button = new Button(buttonImage, .25f, startPosition);
            startPosition.Y += heightSeparation;
            file3Button = new Button(buttonImage, .25f, startPosition);
           
            buttonArray = new Button[3];
            buttonArray[0] = file1Button;
            buttonArray[1] = file2Button;
            buttonArray[2] = file3Button;

            picArray = new BaseSprite[3];
            picArray[0] = swordPic;
            picArray[1] = bombPic;
            picArray[2] = bowPic;

            file1Button.Selected = true;
        }
        
        public void Update(GameTime gameTime)
        {
            keys = Keyboard.GetState();
           
            //Key Press S&W
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

            // Select current button 
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
                SaveFile file = new SaveFile(buttonIndex + 1);
                switch (sender)
                {
                    case SenderButton.NEW:
                        file.save(new GameData());
                        game.loadGame(file.Data);
                        Game1.MainGameState = GameState.CUTSCENE;
                        break;
                    case SenderButton.LOAD:
                        file.load();
                        game.loadGame(file.Data);
                        Game1.MainGameState = GameState.PLAYING;
                        break;
                }
                MediaPlayer.Stop();
            }
            oldKeys = keys;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < buttonArray.Length; i++)
            {
                buttonArray[i].Draw(spriteBatch);
            }
            for (int a = 0; a < picArray.Length; a++)
            {
                picArray[a].Draw(spriteBatch);
            }
        }


    }
}
