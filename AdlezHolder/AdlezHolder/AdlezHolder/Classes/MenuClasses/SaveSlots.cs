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
    public class SaveSlots
    {
        Rectangle overScan;
        int middleScreen, middleButton, buttonSeparation, PicSeparation, CharacterSeparation,upgradeSeparation;
        Vector2 buttonPosition, PicPosition, CharacterPosition, SwordUpgradePosition;
        Vector2 startPosition;
        int width1, numOfButton, displayWidth;
        Button File1, File2, File3,Character, Character2;
        Button placementButton;
        KeyboardState keys, oldKeys;
        BaseSprite bowPic, swordPic, bombPic, upgradeDamageStatSword, upgradeRangeStatSword, upgradeSpeedStatSword;
        Texture2D swordDamageFile1, swordRangeFile1, swordSpeedFile1;
        Sword Sone, Stwo, Sthree;

        public SaveSlots(Viewport view, float scaleFactor)
        {

            // PLACMENT CODE_____________________________________________________________________________
            displayWidth = view.Width;
            placementButton = new Button(Game1.GameContent.Load<Texture2D>("transparent"), .3f, displayWidth,
                buttonPosition, Game1.GameContent.Load<SpriteFont>("SpriteFont1"), "");

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

            startPosition.X = overScan.X;
            startPosition.Y = overScan.Y;

            CharacterPosition.X = overScan.X;
            CharacterPosition.Y = overScan.Y * 2;

            CharacterSeparation = overScan.Y * 4;

            PicPosition.X = startPosition.Y * 7;
            PicPosition.Y = (float)(startPosition.X * 1.05);
            buttonSeparation = overScan.Y * 3;
            PicSeparation = overScan.X * 2;

            upgradeSeparation = overScan.Y * 2;

            SwordUpgradePosition.X = overScan.X * 4;
            SwordUpgradePosition.Y = overScan.Y / 10;



            // PLACMENT CODE____________________________________________________________________________

            width1 = displayWidth;
            numOfButton = 0;

            swordSpeedFile1 = Game1.GameContent.Load<Texture2D>("Upgrade Bar Default");
            swordDamageFile1 = Game1.GameContent.Load<Texture2D>("Upgrade Bar Default");
            swordRangeFile1 = Game1.GameContent.Load<Texture2D>("Upgrade Bar Default");

            SwordUpgradePosition.Y = SwordUpgradePosition.Y + upgradeSeparation * 2;
            upgradeDamageStatSword = new BaseSprite(swordDamageFile1, .15f, displayWidth, 0, SwordUpgradePosition);
            SwordUpgradePosition.Y = SwordUpgradePosition.Y + upgradeSeparation;
            upgradeRangeStatSword = new BaseSprite(swordRangeFile1, .15f, displayWidth, 0, SwordUpgradePosition);
            SwordUpgradePosition.Y = SwordUpgradePosition.Y + upgradeSeparation;
            upgradeSpeedStatSword = new BaseSprite(swordSpeedFile1, .15f, displayWidth, 0, SwordUpgradePosition);

            //Test Swords__________________________________________________________________________________________________________
            Sone = new Sword(.05f);
            Stwo = new Sword(.05f);
            Sthree = new Sword(.05f);

            Sone.UpgradeDamage();

            Stwo.UpgradeDamage();
            Stwo.UpgradeDamage();

            Sthree.UpgradeDamage();
            Sthree.UpgradeDamage();
            Sthree.UpgradeDamage();

            Sthree.UpgradeSpeed();
            Sthree.UpgradeSpeed();

            Stwo.UpgradeRange();

            Stwo.UpgradeSpeed();
            Stwo.UpgradeSpeed();
            Stwo.UpgradeSpeed();
            Stwo.UpgradeSpeed();
            Stwo.UpgradeSpeed();


            Sone.UpgradeRange();
            Sone.UpgradeRange();
            Sone.UpgradeRange();
            Sone.UpgradeRange();

            //Test Swords__________________________________________________________________________________________________________


            swordPic = new BaseSprite(Game1.GameContent.Load<Texture2D>("sword selected"), .3f, displayWidth - 600, 0, PicPosition);
            PicPosition.X = PicPosition.X + PicSeparation;
            bombPic = new BaseSprite(Game1.GameContent.Load<Texture2D>("bomb selected"), .3f, displayWidth - 600, 0, PicPosition);
            PicPosition.X = PicPosition.X + PicSeparation;
            bowPic = new BaseSprite(Game1.GameContent.Load<Texture2D>("bow selected"), .3f, displayWidth - 600, 0, PicPosition);

            File1 = new Button(Game1.GameContent.Load<Texture2D>("The best thing ever"), .25f, displayWidth - 100, startPosition, Game1.GameContent.Load<SpriteFont>("SpriteFont1"), "File 1");
            startPosition.Y = startPosition.Y + buttonSeparation;
            File2 = new Button(Game1.GameContent.Load<Texture2D>("The best thing ever"), .25f, displayWidth - 100, startPosition, Game1.GameContent.Load<SpriteFont>("SpriteFont1"), "File 2");
            startPosition.Y = startPosition.Y + buttonSeparation;
            File3 = new Button(Game1.GameContent.Load<Texture2D>("The best thing ever"), .25f, displayWidth - 100, startPosition, Game1.GameContent.Load<SpriteFont>("SpriteFont1"), "File 3");

            Character = new Button(Game1.GameContent.Load<Texture2D>("ForwardRightFootSword"), .3f, displayWidth - 400, CharacterPosition, Game1.GameContent.Load<SpriteFont>("SpriteFont1"), " ");
            CharacterPosition.Y = CharacterPosition.Y + CharacterSeparation;

            Character2 = new Button(Game1.GameContent.Load<Texture2D>("FL"), .3f, 
                displayWidth - 400, CharacterPosition, Game1.GameContent.Load<SpriteFont>("SpriteFont1"), " ");

            File1.Selected(File1);
        }


        public void Draw(SpriteBatch spriteBatch)
        {

            File1.Draw(spriteBatch);
            File2.Draw(spriteBatch);
            File3.Draw(spriteBatch);

            upgradeDamageStatSword.Draw(spriteBatch);
            upgradeRangeStatSword.Draw(spriteBatch);
            upgradeSpeedStatSword.Draw(spriteBatch);
            

            
            swordPic.Draw(spriteBatch);
            bombPic.Draw(spriteBatch);
            bowPic.Draw(spriteBatch);


            Character.Draw(spriteBatch);
            Character2.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {

            keys = Keyboard.GetState();


            File1.Update(gameTime);
            File2.Update(gameTime);
            File3.Update(gameTime);
           
            swordPic.Update(gameTime);
            bombPic.Update(gameTime);
            bowPic.Update(gameTime);

            Character.Update(gameTime);
            Character2.Update(gameTime);

            //Key Press S&W ________________________________________________________________________________________________________________
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
            oldKeys = Keyboard.GetState();


            //Key Press S&W ________________________________________________________________________________________________________________
            
            
            
            
            //First Sword Checking Upgrade Level Set Pics___________________________________________________________________________________
            if (numOfButton == 0)
            {
                File1.Selected(File1);

                if (Sone.getDamageUpgradeLev == 1)
                {
                    upgradeDamageStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 1)"));
                }

                else if (Sone.getDamageUpgradeLev == 2)
                {
                    upgradeDamageStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 2)"));

                }
                else if (Sone.getDamageUpgradeLev == 3)
                {
                    upgradeDamageStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 3)"));
                }

                else if (Sone.getDamageUpgradeLev == 4)
                {
                    upgradeDamageStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 4)"));

                }
                else if (Sone.getDamageUpgradeLev == 5)
                {
                    upgradeDamageStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar Full(lev 5)"));

                }

                //End Of Getting Sword Damage Lev, Start range 
                if (Sone.getRangeUpgradeLev == 1)
                {
                    upgradeRangeStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 1)"));

                }

                else if (Sone.getRangeUpgradeLev == 2)
                {
                    upgradeRangeStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 2)"));

                }
                else if (Sone.getRangeUpgradeLev == 3)
                {
                    upgradeRangeStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 3)"));

                }
                else if (Sone.getRangeUpgradeLev == 4)
                {
                    upgradeRangeStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 4)"));

                }
                else if (Sone.getRangeUpgradeLev == 5)
                {
                    upgradeRangeStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar Full(lev 5)"));

                }

                // End Getting Range of Sword, Start Getting Speed

                if (Sone.getSpeedUpgradeLev == 1)
                {
                    upgradeSpeedStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 1)"));

                }

                else if (Sone.getSpeedUpgradeLev == 2)
                {
                    upgradeSpeedStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 2)"));

                }
                else if (Sone.getSpeedUpgradeLev == 3)
                {
                    upgradeSpeedStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 3)"));

                }
                else if (Sone.getSpeedUpgradeLev == 4)
                {
                    upgradeSpeedStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 4)"));

                }
                else if (Sone.getSpeedUpgradeLev == 5)
                {
                    upgradeSpeedStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar Full(lev 5)"));

                }

                File2.OrignalColor(File2);
                File3.OrignalColor(File3);

                //First Sword Checking Upgrade Level Set Pics___________________________________________________________________________________
            }
            else if (numOfButton == 1)
            {
                File2.Selected(File2);

                if (Stwo.getDamageUpgradeLev == 1)
                {
                    upgradeDamageStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 1)"));

                }

                else if (Stwo.getDamageUpgradeLev == 2)
                {
                    upgradeDamageStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 2)"));

                }
                else if (Stwo.getDamageUpgradeLev == 3)
                {
                    upgradeDamageStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 3)"));

                }
                else if (Stwo.getDamageUpgradeLev == 4)
                {
                    upgradeDamageStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 4)"));

                }
                else if (Stwo.getDamageUpgradeLev == 5)
                {
                    upgradeDamageStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar Full(lev 5)"));

                }

                //End Of Getting Sword Damage Lev, Start range 
                if (Stwo.getRangeUpgradeLev == 1)
                {
                    upgradeRangeStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 1)"));

                }

                else if (Stwo.getRangeUpgradeLev == 2)
                {
                    upgradeRangeStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 2)"));

                }
                else if (Stwo.getRangeUpgradeLev == 3)
                {
                    upgradeRangeStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 3)"));

                }
                else if (Stwo.getRangeUpgradeLev == 4)
                {
                    upgradeRangeStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 4)"));

                }
                else if (Stwo.getRangeUpgradeLev == 5)
                {
                    upgradeRangeStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar Full(lev 5)"));

                }

                // End Getting Range of Sword, Start Getting Speed

                if (Stwo.getSpeedUpgradeLev == 1)
                {
                    upgradeSpeedStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 1)"));

                }

                else if (Stwo.getSpeedUpgradeLev == 2)
                {
                    upgradeSpeedStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 2)"));

                }
                else if (Stwo.getSpeedUpgradeLev == 3)
                {
                    upgradeSpeedStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 3)"));

                }
                else if (Stwo.getSpeedUpgradeLev == 4)
                {
                    upgradeSpeedStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 4)"));

                }
                else if (Stwo.getSpeedUpgradeLev == 5)
                {
                    upgradeSpeedStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar Full(lev 5)"));

                }

                File3.OrignalColor(File3);
                File1.OrignalColor(File1);
            }
            else
            {
                File3.Selected(File3);

                if (Sthree.getDamageUpgradeLev == 1)
                {
                    upgradeDamageStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 1)"));

                }

                else if (Sthree.getDamageUpgradeLev == 2)
                {
                    upgradeDamageStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 2)"));

                }
                else if (Sthree.getDamageUpgradeLev == 3)
                {
                    upgradeDamageStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 3)"));

                }
                else if (Sthree.getDamageUpgradeLev == 4)
                {
                    upgradeDamageStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 4)"));

                }
                else if (Sthree.getDamageUpgradeLev == 5)
                {
                    upgradeDamageStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar Full(lev 5)"));

                }

                //End Of Getting Sword Damage Lev, Start range 
                if (Sthree.getRangeUpgradeLev == 1)
                {
                    upgradeRangeStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 1)"));

                }

                else if (Sthree.getRangeUpgradeLev == 2)
                {
                    upgradeRangeStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 2)"));

                }
                else if (Sthree.getRangeUpgradeLev == 3)
                {
                    upgradeRangeStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 3)"));

                }
                else if (Sthree.getRangeUpgradeLev == 4)
                {
                    upgradeRangeStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 4)"));

                }
                else if (Sthree.getRangeUpgradeLev == 5)
                {
                    upgradeRangeStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar Full(lev 5)"));

                }

                // End Getting Range of Sword, Start Getting Speed

                if (Sthree.getSpeedUpgradeLev == 1)
                {
                    upgradeSpeedStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 1)"));

                }

                else if (Sthree.getSpeedUpgradeLev == 2)
                {
                    upgradeSpeedStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 2)"));

                }
                else if (Sthree.getSpeedUpgradeLev == 3)
                {
                    upgradeSpeedStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 3)"));

                }
                else if (Sthree.getSpeedUpgradeLev == 4)
                {
                    upgradeSpeedStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar (lev 4)"));

                }
                else if (Sthree.getSpeedUpgradeLev == 5)
                {
                    upgradeSpeedStatSword.setImage(Game1.GameContent.Load<Texture2D>("Upgrade bar Full(lev 5)"));

                }

                File2.OrignalColor(File2);
                File1.OrignalColor(File1);
            }


            //Checks if enter is pressed and which button it is pressed on
            if (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter))
            {
                if (numOfButton == 0)
                {
                    File1.enteredPress();
                }
                if (numOfButton == 1)
                {
                    File2.enteredPress();
                }
                if (numOfButton == 2)
                {
                    File3.enteredPress();
                }
            }
        }
    }
}
