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

            int marginWidth = (int)(displayWidth * .1);
            int marginHeight = (int)(displayHeight * .1);


            overScan.Width = displayWidth - marginWidth;
            overScan.Height = displayHeight - marginHeight;

            overScan.X = displayWidth + marginWidth;

            overScan.Y = displayHeight + marginHeight;

            middleScreen = overScan.Width / 2;
            middleButton = placementButton.DrawnRec.Width / 2;
            buttonPosition.X = middleScreen - middleButton;

            widthSeperation = overScan.Width / 3;
            heightSeparation = overScan.Height / 3;

            startPosition.Y = heightSeparation;
            startPosition.X = widthSeperation / 3;



            swordButton = new Button(Game1.GameContent.Load<Texture2D>("sword selected"), .3f, startPosition);
            startPosition.X += widthSeperation;

            //if (bombUnlocked)
                bombButton = new Button(Game1.GameContent.Load<Texture2D>("bomb selected"), .3f, startPosition);
            //else
                //bombButton = new Button(Game1.GameContent.Load<Texture2D>("Weapons/lock"), .3f, startPosition);
            startPosition.X += widthSeperation;

            //if (bowUnlocked)
                bowButton = new Button(Game1.GameContent.Load<Texture2D>("bow selected"), .3f, startPosition);
            //else
                //bowButton = new Button(Game1.GameContent.Load<Texture2D>("Weapons/lock"), .3f, startPosition);
            

            swordButton.Selected = true;
            swordtree = new SwordTree(game);
            game.Components.Add(swordtree);
            swordtree.Enabled = false;
            swordtree.Initialize();
            swordtree.Visible = false;
            bombtree = new BombTree(game);
            game.Components.Add(bombtree);
            bombtree.Enabled = false;
            bombtree.Initialize();
            bombtree.Visible = false;
            
        }
        public void Update(GameTime gameTime)
        {
            keys = Keyboard.GetState();

            if (CurrentTreeGameState == TreeGameState.SWORDTREE)
            {
                swordtree.Enabled = true;
                swordtree.Visible = true;
                bombtree.Enabled = false;
                bombtree.Visible = false;
            }
            else if (CurrentTreeGameState == TreeGameState.BOMBTREE)
            {
                bombtree.Enabled = true;
                bombtree.Visible = true;
                swordtree.Enabled = false;
                swordtree.Visible = false;
            }
            else
            {
                if (keys.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A))
                {
                    if (numOfButton > 0)
                        numOfButton -= 1;
                    else
                    {
                        numOfButton = 2;
                    }

                }

                if (keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D))
                {
                    if (numOfButton < 2)
                        numOfButton += 1;
                    else
                        numOfButton = 0;

                }

                if (numOfButton == 0)
                {
                    swordButton.Selected = true;
                    bombButton.Selected = false;
                    bowButton.Selected = false;

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
                    swordButton.Selected = false;
                }

                //Checks if enter is pressed and which button it is pressed on
                keys = Keyboard.GetState();
                if (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter))
                {
                    if (numOfButton == 0)
                    {
                        this.changeTreeGameState(TreeGameState.SWORDTREE);
                    }

                    else if (numOfButton == 1)
                    {
                        this.changeTreeGameState(TreeGameState.BOMBTREE);
                    }
                    else if (numOfButton == 2)
                    {
                        this.changeTreeGameState(TreeGameState.BOWTREE);
                    }
                }
                if (keys.IsKeyDown(Keys.Escape) && oldKeys.IsKeyUp(Keys.Escape))
                    changeGameState(GameState.PLAYING);

                oldKeys = keys;

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.GameContent.Load<SpriteFont>("SpriteFont3"), "Weapon shop", new Vector2(Game1.DisplayWidth / 3, Game1.DisplayHeight*(1/3)), Color.Black);
            spriteBatch.DrawString(Game1.GameContent.Load<SpriteFont>("SpriteFont3"), "Weapon shop", new Vector2(Game1.DisplayWidth /3 - 4, Game1.DisplayHeight*(1/3) - 4), Color.White);
            if(swordtree.Enabled != true && bombtree.Enabled != true )
            {
                swordButton.Draw(spriteBatch);
                bombButton.Draw(spriteBatch);
                bowButton.Draw(spriteBatch);
            }
        }

        private void changeTreeGameState( TreeGameState newState)    
        {
           currentTreeGameState = newState;
        }
        private void changeGameState(GameState newState)
        {
            Game1.MainGameState = newState;
        }
    }
}