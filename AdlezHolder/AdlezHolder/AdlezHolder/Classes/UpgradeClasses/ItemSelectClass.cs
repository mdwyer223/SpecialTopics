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
    public class ItemSelectClass
    {
        UpgradeNode[,] multiNodeArray;
        SwordUpgradeClass upgradeSwordTree;
       // BombUpgradeClass bomb;
       // BowUpgradeClass bow;
       // ArmorUpgradeClass armor;

        KeyboardState keys, oldKeys;
        Button swordButton, bombButton, bowButton, armorButton, placementButton;
        int numOfButton;
        bool ePressed;

        Rectangle overScan;
        Vector2 buttonPosition;
        Vector2 startPosition;
        int nodeSeperationHeight,nodeSeperationWidth;
        Vector2 nodePosition;
        Sword swordTest;

        public ItemSelectClass(Sword x)
        {
            swordTest = x;
            Rectangle overScan;
            Vector2 buttonPosition;
            Vector2 startPosition;
            int displayWidth, displayHeight;
            int middleScreen, middleButton, widthSeperation, heightSeparation;


            displayWidth = Game1.DisplayWidth;
            displayHeight = Game1.DisplayHeight;
            numOfButton = 0;
            //bomb = new BombUpgradeClass(view, scaleFactor);
           // bow = new BowUpgradeClass(view, scaleFactor);
           // armor = new ArmorUpgradeClass(view, scaleFactor);
            ePressed = true;

            placementButton = new Button(Game1.GameContent.Load<Texture2D>("transparent"), displayWidth, Vector2.Zero);
           
            int marginWidth = (int)(displayWidth * .1);
            int marginHeight = (int)(displayHeight * .1);
           

            overScan.Width = displayWidth - marginWidth;
            overScan.Height = displayHeight - marginHeight;

            overScan.X = displayWidth + marginWidth;

            overScan.Y = displayHeight + marginHeight;

            middleScreen = overScan.Width / 2;
            middleButton = placementButton.DrawnRec.Width / 2;
            buttonPosition.X = middleScreen - middleButton;

            widthSeperation = overScan.Width / 6;
            heightSeparation = overScan.Height / 6;

            startPosition.Y = heightSeparation;
            startPosition.X = widthSeperation * 2;



            swordButton = new Button(Game1.GameContent.Load<Texture2D>("MenuButtons/NewGame"), .3f, startPosition);
            startPosition.X = startPosition.X + widthSeperation;
            
            //if(bombUnlocked)
            bombButton = new Button(Game1.GameContent.Load<Texture2D>("MenuButtons/NewGame"), .3f, startPosition);
            //else
            //    bombButton = new Button(Game1.otherContent.Load<Texture2D>("lock"), .3f, displayWidth, startPosition, Game1.otherContent.Load<SpriteFont>("SpriteFont1"), "");
            startPosition.X = startPosition.X + widthSeperation;
            
            //if(bowUnlocked)
            bowButton = new Button(Game1.GameContent.Load<Texture2D>("MenuButtons/NewGame"), .3f, startPosition);
            //else
            //    bowButton = new Button(Game1.otherContent.Load<Texture2D>("lock"), .3f, displayWidth, startPosition, Game1.otherContent.Load<SpriteFont>("SpriteFont1"),"");
                startPosition.X = startPosition.X + widthSeperation;

                armorButton = new Button(Game1.GameContent.Load<Texture2D>("MenuButtons/NewGame"), .3f, startPosition);

            swordButton.Selected = true;
            upgradeSwordTree = new SwordUpgradeClass(swordTest);

        }   
        public void Update(GameTime gameTime)
        {
            upgradeSwordTree.Update(gameTime);
            keys = Keyboard.GetState();

            if (ePressed)
            {

                if (keys.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A))
                {
                    if (numOfButton > 0)
                        numOfButton -= 1;
                    else
                        numOfButton = 3;

                }

                if (keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D))
                {
                    if (numOfButton < 3)
                        numOfButton += 1;
                    else
                        numOfButton = 0;

                }
                
                if (numOfButton == 0)
                {
                    swordButton.Selected = true;

                    bombButton.Selected = false;
                    armorButton.Selected = false;

                }
                else if (numOfButton == 1)
                {
                    bombButton.Selected = true;

                    swordButton.Selected = false;
                    bowButton.Selected = false;
                }
                else if (numOfButton == 2)
                {
                    bowButton.Selected = true;

                    bombButton.Selected = false;
                    armorButton.Selected = false;
                }
                else
                {
                    armorButton.Selected = true;

                    bowButton.Selected = false;
                    swordButton.Selected  = false;
                }

                //Checks if enter is pressed and which button it is pressed on
                keys = Keyboard.GetState();
                if (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter))
                {
                    if (numOfButton == 0)
                    {
                        //start SwordUpgradeClass
                        ePressed = false;
                    }

                    else if (numOfButton == 1)
                    {
                        //start BombUpgradeButton
                        ePressed = false;
                    }
                    else if (numOfButton == 2)
                    {
                        //start BowUpgradeButton
                        ePressed = false;
                    }
                    else
                    {
                        //start ArmorUpgradeButton
                        ePressed = false;
                    }

                }
                oldKeys = keys;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

                //swordButton.Draw(spriteBatch);
                //bombButton.Draw(spriteBatch);
                //bowButton.Draw(spriteBatch);
                //armorButton.Draw(spriteBatch);
            upgradeSwordTree.Draw(spriteBatch);

        }

        private void changeGameState(GameState newState)    
        {
            Game1.MainGameState = newState;
        }
    }
}