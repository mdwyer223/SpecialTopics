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
    public enum ItemShopGameState
    {
        SELLITEMS, BUYITEMS, ITEMSHOP
    }

    public class ItemShopHome
    {
        KeyboardState keys, oldKeys;
        Button SellButton, BuyButton, placementButton;
        bool buySelected;
        sellItemShop sellitems;
        buyItemShop buyitems;
        Game tempgame;
        bool addplayer = true;

        static ItemShopGameState CurrentItemGameState = ItemShopGameState.ITEMSHOP;
        public static ItemShopGameState CurrentItemState
        {
            get { return CurrentItemGameState; }
            set { CurrentItemGameState = value; }
        }

        public ItemShopHome(Game game)
        {
            Rectangle overScan;
            Vector2 startPosition;
            int displayWidth, displayHeight;
            int middleScreen, middleButton, widthSeperation, heightSeparation;

            displayWidth = Game1.DisplayWidth;
            displayHeight = Game1.DisplayHeight;
            buySelected = true;

            placementButton = new Button(Game1.GameContent.Load<Texture2D>("exit"), displayWidth, Vector2.Zero);

            int marginWidth = (int)(displayWidth * .1);
            int marginHeight = (int)(displayHeight * .1);


            overScan.Width = displayWidth - marginWidth;
            overScan.Height = displayHeight - marginHeight;

            overScan.X = displayWidth + marginWidth;

            overScan.Y = displayHeight + marginHeight;
            tempgame = game;

            middleScreen = overScan.Width;
            middleButton = placementButton.DrawnRec.Width / 2;
            

            widthSeperation = overScan.Width /2;
            heightSeparation = overScan.Height / 3;

            startPosition.Y = heightSeparation;
            startPosition.X = widthSeperation / 3;

            BuyButton = new Button(Game1.GameContent.Load<Texture2D>("Buy"), .4f, startPosition);
            startPosition.X += widthSeperation;
            SellButton = new Button(Game1.GameContent.Load<Texture2D>("Sell"), .4f, startPosition);

            BuyButton.Selected = true;
        }

        public void Update(GameTime gameTime)
        {
            if (addplayer)
            {
                sellitems = new sellItemShop(tempgame, Game1.WorldReference.Map.Player);
                buyitems = new buyItemShop(tempgame, Game1.WorldReference.Map.Player);
                sellitems.Enabled = false;
                sellitems.Visible = false;
                buyitems.Enabled = false;
                buyitems.Visible = false; 
                addplayer = false;
            }
            keys = Keyboard.GetState();
            if (CurrentItemGameState == ItemShopGameState.BUYITEMS)
            {
                buyitems.Enabled = true;
                buyitems.Visible = true;
                sellitems.Enabled = false;
                sellitems.Visible = false;
            }
            else if (CurrentItemGameState == ItemShopGameState.SELLITEMS)
            {
                sellitems.Enabled = true;
                sellitems.Visible = true;
                buyitems.Enabled = false;
                buyitems.Visible = false;
            }
            if(CurrentItemGameState == ItemShopGameState.ITEMSHOP)
            {
                sellitems.Enabled = false;
                sellitems.Visible = false;
                buyitems.Enabled = false;
                buyitems.Visible = false; 
                if ((keys.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A)) || (keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D)))
                {
                    buySelected = !buySelected;

                }

                if (buySelected)
                {
                    BuyButton.Selected = true;
                    SellButton.Selected = false;
                }
                else
                {
                    BuyButton.Selected = false;
                    SellButton.Selected = true;
                }

                //if (keys.IsKeyDown(Keys.Enter) && oldKeys.IsKeyUp(Keys.Enter))
                //{
                //    if (buySelected)
                //        this.changeItemGameState(ItemShopGameState.BUYITEMS);
                //    else
                //        this.changeItemGameState(ItemShopGameState.SELLITEMS);
                //}

                if (keys.IsKeyDown(Keys.Escape) && oldKeys.IsKeyUp(Keys.Escape))
                {
                    changeGameState(GameState.PLAYING);
                }
                oldKeys = keys;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (buyitems.Enabled != true && sellitems.Enabled != true)
            {
                BuyButton.Draw(spriteBatch);
                SellButton.Draw(spriteBatch);
                spriteBatch.DrawString(Game1.GameContent.Load<SpriteFont>("SpriteFont3"), "Item shop", new Vector2(Game1.DisplayWidth / 3, Game1.DisplayHeight * (1 / 3)), Color.White);
                spriteBatch.DrawString(Game1.GameContent.Load<SpriteFont>("SpriteFont3"), "Item shop", new Vector2(Game1.DisplayWidth / 3 - 4, Game1.DisplayHeight * (1 / 3) - 4), Color.Blue);
            }     
        }

        private void changeItemGameState(ItemShopGameState newState)
        {
            CurrentItemGameState = newState;
        }

        private void changeGameState(GameState newState)
        {
            Game1.MainGameState = newState;
        }
    }
}
