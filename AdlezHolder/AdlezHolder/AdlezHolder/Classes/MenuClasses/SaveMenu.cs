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

    public class SaveMenu
    {
        int buttonIndex;
        KeyboardState keys, oldKeys;
        Button[] buttonArray;
        BaseSprite[] picArray;
        Vector2 swordTextPosition;
        SaveFile file;
        ItemShopHome itemShop;

        public SaveMenu()
        {
            int displayWidth, displayHeight;
            int middleScreen, middleButton, widthSeperation, heightSeparation;
            Vector2 buttonPosition, picPosition;
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
            
            widthSeperation = overScan.Width / 5;
            heightSeparation = overScan.Height / 5;

            startPosition.X = overScan.X;
            startPosition.Y = heightSeparation;
            picPosition.X = widthSeperation * 2;
            picPosition.Y = overScan.Y;

            swordTextPosition.X = picPosition.X;
            swordTextPosition.Y = picPosition.Y + heightSeparation;

          
            const float PIC_SCALE_FACTOR = .075f;
            swordPic = new BaseSprite(Game1.GameContent.Load<Texture2D>("AdlezTitle"), PIC_SCALE_FACTOR, displayWidth, 0, picPosition);
            picPosition.X = picPosition.X + widthSeperation;
            bombPic = new BaseSprite(Game1.GameContent.Load<Texture2D>("AdlezTitle"), PIC_SCALE_FACTOR, displayWidth, 0, picPosition);
            picPosition.X = picPosition.X + widthSeperation;
            bowPic = new BaseSprite(Game1.GameContent.Load<Texture2D>("AdlezTitle"), PIC_SCALE_FACTOR, displayWidth, 0, picPosition);


            Texture2D buttonImage = Game1.GameContent.Load<Texture2D>("MenuButtons/LoadGame");

            file1Button = new Button(buttonImage, .4f, startPosition);
            startPosition.Y += heightSeparation;
            file2Button = new Button(buttonImage, .4f, startPosition);
            startPosition.Y += heightSeparation;
            file3Button = new Button(buttonImage, .4f, startPosition);
           
            buttonArray = new Button[3];
            buttonArray[0] = file1Button;
            buttonArray[1] = file2Button;
            buttonArray[2] = file3Button;

            picArray = new BaseSprite[3];
            picArray[0] = swordPic;
            picArray[1] = bombPic;
            picArray[2] = bowPic;

            file1Button.Selected = true;
            Sword sword = new Sword(.3f);

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
          //  String[] swordArray;

          ////swordArray = file.Data.mapVars.playerVar.sword.getStatBoxes();

          //  for (int a = 0; a < swordArray.Length; a++)
          //  {
          //      //swordArray[a].Draw(spriteBatch);
          //  }
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
                else
                {
                    buttonIndex = 0;
                }
            }

            if (keys.IsKeyDown(Keys.Escape) && oldKeys.IsKeyUp(Keys.Escape))
            {
                this.changeGameState(GameState.MAINMENU);
            }

            if (keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W))
            {
                this.changeGameState(GameState.UPGRADESHOP);
                if (buttonIndex > 0)
                {
                    buttonIndex = (buttonIndex - 1);
                }
                else
                {
                    buttonIndex = 2;
                }

            }
            oldKeys = keys;
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

                file = new SaveFile(buttonIndex + 1);
                if (buttonIndex == 0)
                {
                    this.changeGameState(GameState.UPGRADESHOP);
                }
            }
            oldKeys = keys;
        }
         private void changeGameState(GameState newState)
        {
            Game1.MainGameState = newState;
        }  
       }

    }


