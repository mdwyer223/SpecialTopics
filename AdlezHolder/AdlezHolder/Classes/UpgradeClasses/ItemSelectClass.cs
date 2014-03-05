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
    public enum TreeGameState
    {
        BOWTREE,BOMBTREE,SWORDTREE,ITEMSELECT
    }

    public class ItemSelectClass
    {
        //BowUpgradeClass bow;
        KeyboardState keys, oldKeys;
        Button swordButton, bombButton, bowButton, placementButton;
        int numOfButton;
        SwordTree swordtree;
        BombTree bombtree;
        BowTree bowtree;
        Character tempCharacter;
        int count = 0;
        const int TICK_IN_SEC = 60;
        BasicItemShop bItemShop;

        static TreeGameState currentTreeGameState = TreeGameState.ITEMSELECT;
        public static TreeGameState CurrentTreeGameState
        {
            get { return currentTreeGameState; }
            set { currentTreeGameState = value; }
        }
      
        public ItemSelectClass(Game game)
        {
            Rectangle overScan;
            Vector2 buttonPosition;
            Vector2 startPosition;
            int displayWidth, displayHeight;
            int middleScreen, middleButton, widthSeperation, heightSeparation;


            displayWidth = Game1.DisplayWidth;
            displayHeight = Game1.DisplayHeight;
            numOfButton = 0;
           // bow = new BowUpgradeClass();

            placementButton = new Button(Game1.GameContent.Load<Texture2D>("transparent"), displayWidth, Vector2.Zero);
           
            int marginWidth = (int)(displayWidth * .3);
            int marginHeight = (int)(displayHeight * .3);
           

            overScan.Width = displayWidth - marginWidth;
            overScan.Height = displayHeight - marginHeight;

            overScan.X = displayWidth + marginWidth;
            overScan.Y = displayHeight + marginHeight;

            middleScreen = overScan.Width / 2;
            middleButton = placementButton.DrawnRec.Width / 2;
            buttonPosition.X = middleScreen - middleButton;

            widthSeperation = overScan.Width / 3;
            heightSeparation = overScan.Height / 3;

            startPosition.X = heightSeparation;
            startPosition.Y = widthSeperation / 3;



            swordButton = new Button(Game1.GameContent.Load<Texture2D>("sword selected"), .3f, startPosition);
            startPosition.X = startPosition.X + widthSeperation;
            bombButton = new Button(Game1.GameContent.Load<Texture2D>("bomb selected"), .3f, startPosition);    
            startPosition.X = startPosition.X + widthSeperation;         
            bowButton = new Button(Game1.GameContent.Load<Texture2D>("bow selected"), .3f, startPosition);

            tempCharacter = new Character(Game1.GameContent.Load<Texture2D>("MenuButtons/Continue"), .3f, 4, 3, Vector2.Zero);
            tempCharacter.addFunds(25000);
            

            swordButton.Selected = true;
            swordtree = new SwordTree(game, tempCharacter);
            game.Components.Add(swordtree);
            swordtree.Enabled = false;
            swordtree.Initialize();
            swordtree.Visible = false;
            bombtree = new BombTree(game, tempCharacter);
            game.Components.Add(bombtree);
            bombtree.Enabled = false;
            bombtree.Initialize();
            bombtree.Visible = false;
            bowtree = new BowTree(game, tempCharacter);
            game.Components.Add(bowtree);
            bowtree.Enabled = false;
            bowtree.Initialize();
            bowtree.Visible = false;



            bItemShop = new BasicItemShop(tempCharacter);



        }
        public void Update(GameTime gameTime)
        { 
            bItemShop.Update(gameTime);
            //keys = Keyboard.GetState();
            //if (CurrentTreeGameState == TreeGameState.BOMBTREE)
            //{
            //    swordtree.Enabled = false;
            //    swordtree.Visible = false;

            //    bowtree.Enabled = false;
            //    bowtree.Visible = false;

            //    bombtree.Enabled = true;
            //    bombtree.Visible = true;
            //}
            //else if (CurrentTreeGameState == TreeGameState.BOWTREE)
            //{
            //    swordtree.Enabled = false;
            //    swordtree.Visible = false;

            //    bowtree.Enabled = true;
            //    bowtree.Visible = true;

            //    bombtree.Enabled = false;
            //    bombtree.Visible = false;
            //}
            //else if (CurrentTreeGameState == TreeGameState.SWORDTREE)
            //{
            //    swordtree.Enabled = true;
            //    swordtree.Visible = true;

            //    bowtree.Enabled = false;
            //    bowtree.Visible = false;

            //    bombtree.Enabled = false;
            //    bombtree.Visible = false;
            //}

            //if (CurrentTreeGameState == TreeGameState.ITEMSELECT)
            //{
            //    swordtree.Enabled = false;
            //    swordtree.Visible = false;

            //    bowtree.Enabled = false;
            //    bowtree.Visible = false;

            //    bombtree.Enabled = false;
            //    bombtree.Visible = false;

            //    if (keys.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A))
            //    {
            //        if (numOfButton > 0)
            //            numOfButton -= 1;
            //        else
            //        {
            //            numOfButton = 2;
            //        }

            //    }

            //    if (keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D))
            //    {
            //        if (numOfButton < 2)
            //            numOfButton += 1;
            //        else
            //            numOfButton = 0;

            //    }

            //    if (numOfButton == 0)
            //    {
            //        swordButton.Selected = true;
            //        bombButton.Selected = false;
            //        bowButton.Selected = false;

            //    }
            //    else if (numOfButton == 1)
            //    {
            //        bombButton.Selected = true;
            //        swordButton.Selected = false;
            //        bowButton.Selected = false;
            //    }
            //    else if (numOfButton == 2)
            //    {
            //        bowButton.Selected = true;
            //        bombButton.Selected = false;
            //        swordButton.Selected = false;
            //    }

            //    //Checks if enter is pressed and which button it is pressed on
            //    keys = Keyboard.GetState();
            //    if (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter))
            //    {
            //        if (numOfButton == 0)
            //        {
            //            this.changeTreeGameState(TreeGameState.SWORDTREE);
            //        }

            //        else if (numOfButton == 1)
            //        {
            //            this.changeTreeGameState(TreeGameState.BOMBTREE);
            //        }
            //        else if (numOfButton == 2)
            //        {
            //            this.changeTreeGameState(TreeGameState.BOWTREE);
            //        }



            //    }
            //    oldKeys = keys;

            //}
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           
            
            //if(CurrentTreeGameState == TreeGameState.ITEMSELECT )
            //{
            //    swordButton.Draw(spriteBatch);
            //    bombButton.Draw(spriteBatch);
            //    bowButton.Draw(spriteBatch);
            //}
        }

        private void changeTreeGameState( TreeGameState newState)    
        {
           currentTreeGameState = newState;
        }
    }
}