using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdlezHolder
{
    public enum ChestType { GOLD, SILVER, BRONZE, DUNGEON, NONE };

    public class Chest : ImmovableObject
    {
        Texture2D openTexture;
        ChestType type;
        bool open;

        public override BaseSpriteStruct SaveData
        {
            get
            {
                BaseSpriteStruct myData = base.SaveData;
                myData.saveId = "ICh";
                return myData;
            }
            set
            {
                base.SaveData = value;
            }
        }

        public bool Open
        {
            get { return open; }
            set { open = value; }
        }

        public Chest(float scaleFactor, Vector2 start, ChestType type)
            : base(Game1.GameContent.Load<Texture2D>("Chests/Chest"), scaleFactor, 
                0, start)
        {
            openTexture = Game1.GameContent.Load<Texture2D>("Chests/EmptyChest");
            this.type = type;

            if (type == ChestType.GOLD)
            {
                color = Color.Gold;
            }
            else if (type == ChestType.SILVER)
            {
                color = Color.Gray;
            }
            else if (type == ChestType.BRONZE)
            {
                color = Color.SandyBrown;
            }
        }

        public override void Update(Map data, GameTime gameTime)
        {
            if (!open && data.Player.Sword.CollisionRec.Intersects(this.CollisionRec) 
                && !data.Player.Sword.isDead)
            {
                //open = true;
                //data.Player.addMessage(new Message("You got a key", 
                //    new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.White));
                if (type == ChestType.GOLD)
                {
                    if (data.Player.GoldKeys > 0)
                    {
                        open = true;
                        dropItem(data.Player);
                        data.Player.removeKey(KeyType.GOLD);
                    }
                    else
                    {
                        data.Player.addMessage(new Message("You need a key!", Color.Gold));
                    }
                }
                else if (type == ChestType.SILVER)
                {
                    if (data.Player.SilverKeys > 0)
                    {
                        open = true;
                        dropItem(data.Player);
                        data.Player.removeKey(KeyType.SILVER);
                    }
                    else
                    {
                        data.Player.addMessage(new Message("You need a key!", Color.Silver));
                    }
                }
                else if (type == ChestType.BRONZE)
                {
                    if (data.Player.Bronzekeys > 0)
                    {
                        open = true;
                        dropItem(data.Player);
                        data.Player.removeKey(KeyType.BRONZE);
                    }
                    else
                    {
                        data.Player.addMessage(new Message("You need a key!", Color.SandyBrown));
                    }
                }
                else if (type == ChestType.DUNGEON)
                {
                    open = true;
                }
            }

            if (open)
            {
                this.Image = openTexture;
            }
        }

        public void dropItem(Character player)
        {
            Random rand = new Random();
            float dropValue = (float)(rand.NextDouble() * 100);
            string message = "";

            if (type == ChestType.BRONZE)
            {
                int tier = 2;
                int typeOf = rand.Next(1, 6);

                if (dropValue <= 15)
                {
                    switch (typeOf)
                    {
                        case 1:
                            {
                                LightningStone stone = new LightningStone(.02f, this.Position, tier, 1);
                                player.addItem(stone);
                                message += stone.ItemName;
                                break;
                            }
                        case 2:
                            {
                                PoisonStone stone = new PoisonStone(.02f, this.Position, tier, 1);
                                player.addItem(stone);
                                message += stone.ItemName;
                                break;
                            }
                        case 3:
                            {
                                FireStone stone = new FireStone(.02f, this.Position, tier, 1);
                                player.addItem(stone);
                                message += stone.ItemName;
                                break;
                            }
                        case 4:
                            {
                                VampiricStone stone = new VampiricStone(.02f, this.Position, tier, 1);
                                player.addItem(stone);
                                message += stone.ItemName;
                                break;
                            }
                        case 5:
                            {
                                IceStone stone = new IceStone(.02f, this.Position, tier, 1);
                                player.addItem(stone);
                                message += stone.ItemName;
                                break;
                            }
                    }
                }
                else if (dropValue <= 50)
                {
                    if (rand.Next(1, 3) == 1)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            player.addArrow();
                        }
                        message += "20 Arrows";
                    }
                    else
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            player.addBomb();
                        }
                        message += "10 Bombs";
                    }
                }
                else
                {
                    int value = rand.Next(100, 250);
                    player.addFunds(value);
                    message += value + " Coins";
                }
                player.addMessage(new Message(message, Color.SandyBrown));
            }
            else if (type == ChestType.SILVER)
            {
                int tier = 3;
                int typeOf = rand.Next(1, 6);

                if (dropValue <= 20)
                {
                    switch (typeOf)
                    {
                        case 1:
                            {
                                LightningStone stone = new LightningStone(.02f, this.Position, tier, 1);
                                player.addItem(stone);
                                message += stone.ItemName;
                                break;
                            }
                        case 2:
                            {
                                PoisonStone stone = new PoisonStone(.02f, this.Position, tier, 1);
                                player.addItem(stone);
                                message += stone.ItemName;
                                break;
                            }
                        case 3:
                            {
                                FireStone stone = new FireStone(.02f, this.Position, tier, 1);
                                player.addItem(stone);
                                message += stone.ItemName;
                                break;
                            }
                        case 4:
                            {
                                VampiricStone stone = new VampiricStone(.02f, this.Position, tier, 1);
                                player.addItem(stone);
                                message += stone.ItemName;
                                break;
                            }
                        case 5:
                            {
                                IceStone stone = new IceStone(.02f, this.Position, tier, 1);
                                player.addItem(stone);
                                message += stone.ItemName;
                                break;
                            }
                    }
                }
                else if (dropValue <= 50)
                {
                    if (rand.Next(1, 3) == 1)
                    {
                        for (int i = 0; i < 30; i++)
                        {
                            player.addArrow();
                        }
                        message += "30 Arrows";
                    }
                    else
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            player.addBomb();
                        }
                        message += "15 Bombs";
                    }
                }
                else
                {
                    int value = rand.Next(250, 750);
                    player.addFunds(value);
                    message += value + " Coins";
                }
                player.addMessage(new Message(message, Color.Silver));
            }
            else if (type == ChestType.GOLD)
            {
                int tier = 5;
                int typeOf = rand.Next(1, 6);

                if (dropValue <= 35)
                {
                    switch (typeOf)
                    {
                        case 1:
                            {
                                LightningStone stone = new LightningStone(.02f, this.Position, tier, 1);
                                player.addItem(stone);
                                message += stone.ItemName;
                                break;
                            }
                        case 2:
                            {
                                PoisonStone stone = new PoisonStone(.02f, this.Position, tier, 1);
                                player.addItem(stone);
                                message += stone.ItemName;
                                break;
                            }
                        case 3:
                            {
                                FireStone stone = new FireStone(.02f, this.Position, tier, 1);
                                player.addItem(stone);
                                message += stone.ItemName;
                                break;
                            }
                        case 4:
                            {
                                VampiricStone stone = new VampiricStone(.02f, this.Position, tier, 1);
                                player.addItem(stone);
                                message += stone.ItemName;
                                break;
                            }
                        case 5:
                            {
                                IceStone stone = new IceStone(.02f, this.Position, tier, 1);
                                player.addItem(stone);
                                message += stone.ItemName;
                                break;
                            }
                    }
                }
                else if (dropValue <= 50)
                {
                    if (rand.Next(1, 3) == 1)
                    {
                        for (int i = 0; i < 40; i++)
                        {
                            player.addArrow();
                        }
                        message += "40 Arrows";
                    }
                    else
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            player.addBomb();
                        }
                        message += "20 Bombs";
                    }
                }
                else
                {
                    int value = rand.Next(1000, 2500);
                    player.addFunds(value);
                    message += value + " Coins";
                }
                player.addMessage(new Message(message, Color.Gold));
            }
        }

        //protected virtual void dropItem(MapDataHolder data)
        //{
        //    Random rand = new Random();
        //    float dropValue = (float)(rand.NextDouble() * 100);

        //    if (dropValue <= 5)
        //    {
        //        float tier = (float)(rand.NextDouble() * 100);
        //        int type = rand.Next(1, 6);
        //        if (tier <= 10)
        //        {
        //            switch (type)
        //            {
        //                case 1:
        //                    {
        //                        LightningStone stone = new LightningStone(.02f, this.Position, 5, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //                case 2:
        //                    {
        //                        PoisonStone stone = new PoisonStone(.02f, this.Position, 5, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //                case 3:
        //                    {
        //                        FireStone stone = new FireStone(.02f, this.Position, 5, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //                case 4:
        //                    {
        //                        VampiricStone stone = new VampiricStone(.02f, this.Position, 5, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //                case 5:
        //                    {
        //                        IceStone stone = new IceStone(.02f, this.Position, 5, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //            }
        //        }
        //        else if (tier <= 20)
        //        {
        //            switch (type)
        //            {
        //                case 1:
        //                    {
        //                        LightningStone stone = new LightningStone(.02f, this.Position, 4, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //                case 2:
        //                    {
        //                        PoisonStone stone = new PoisonStone(.02f, this.Position, 4, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //                case 3:
        //                    {
        //                        FireStone stone = new FireStone(.02f, this.Position, 4, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //                case 4:
        //                    {
        //                        VampiricStone stone = new VampiricStone(.02f, this.Position, 4, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //                case 5:
        //                    {
        //                        IceStone stone = new IceStone(.02f, this.Position, 4, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //            }
        //        }
        //        else if (tier <= 30)
        //        {
        //            switch (type)
        //            {
        //                case 1:
        //                    {
        //                        LightningStone stone = new LightningStone(.02f, this.Position, 3, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //                case 2:
        //                    {
        //                        PoisonStone stone = new PoisonStone(.02f, this.Position, 3, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //                case 3:
        //                    {
        //                        FireStone stone = new FireStone(.02f, this.Position, 3, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //                case 4:
        //                    {
        //                        VampiricStone stone = new VampiricStone(.02f, this.Position, 3, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //                case 5:
        //                    {
        //                        IceStone stone = new IceStone(.02f, this.Position, 3, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //            }
        //        }
        //        else if (tier <= 50)
        //        {
        //            switch (type)
        //            {
        //                case 1:
        //                    {
        //                        LightningStone stone = new LightningStone(.02f, this.Position, 2, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //                case 2:
        //                    {
        //                        PoisonStone stone = new PoisonStone(.02f, this.Position, 2, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //                case 3:
        //                    {
        //                        FireStone stone = new FireStone(.02f, this.Position, 2, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //                case 4:
        //                    {
        //                        VampiricStone stone = new VampiricStone(.02f, this.Position, 2, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //                case 5:
        //                    {
        //                        IceStone stone = new IceStone(.02f, this.Position, 2, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //            }
        //        }
        //        else
        //        {
        //            switch (type)
        //            {
        //                case 1:
        //                    {
        //                        LightningStone stone = new LightningStone(.02f, this.Position, 1, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //                case 2:
        //                    {
        //                        PoisonStone stone = new PoisonStone(.02f, this.Position, 1, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //                case 3:
        //                    {
        //                        FireStone stone = new FireStone(.02f, this.Position, 1, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //                case 4:
        //                    {
        //                        VampiricStone stone = new VampiricStone(.02f, this.Position, 1, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //                case 5:
        //                    {
        //                        IceStone stone = new IceStone(.02f, this.Position, 1, 1);
        //                        data.addItem(stone);
        //                        break;
        //                    }
        //            }
        //        }
        //    }
        //    else if (dropValue <= 30)
        //    {
        //        int wepSelect = rand.Next(1, 10);
        //        if (wepSelect <= 7)
        //        {
        //            Arrow a = new Arrow(.02f, false, "Arrow", 20, this.Position, 1);
        //            data.addItem(a);
        //        }
        //        else
        //        {
        //            BombItem b = new BombItem(.02f, this.Position, 1);
        //            data.addItem(b);
        //        }
        //    }
        //    else if (dropValue <= 70)
        //    {
        //        int val = rand.Next(25, 70);
        //        Money m = new Money(.02f, this.Position, "" + val + " Coins", val);
        //        data.addItem(m);
        //    }
        //}
    }
}
