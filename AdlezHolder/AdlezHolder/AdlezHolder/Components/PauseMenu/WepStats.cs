using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AdlezHolder
{
    public class WepStats
    {
        Character player;
        Rectangle menuRect, innerRect, leftRect, rightRect, upgradesRect;
        Texture2D sword, bomb, bow, locked;
        List<Texture2D> weapons;
        
        int iconSize, currentIndex = 0, nextIndex = 1, lastIndex = 2,
            equipOptionsIndex = 0, gemChoiceIndex = 0, optionHeight;
        string optionsMessage;
        
        KeyboardState keys, oldKeys;
        Boolean bowLocked = false, bombLocked = false,
            equipOrUnequipMenu = false, gemSelect = false, gemRemove = false,
            weaponSelect = true,
            gotOptions = false;

        List<string> equipOptions, gemChoices;
        List<int> gemIndexInInvent;

        List<Gem> gems;
        EquippedItem itemSelected;

        public WepStats(Game1 game, Vector2 border)
        {
            optionsMessage = "";

            sword = game.Content.Load<Texture2D>("Weapons/sword selected");
            bomb = game.Content.Load<Texture2D>("Weapons/bomb selected");
            bow = game.Content.Load<Texture2D>("Weapons/bow selected");
            locked = game.Content.Load<Texture2D>("Weapons/lock");

            menuRect = new Rectangle((int)border.X, (int)border.Y, (int)(game.GraphicsDevice.Viewport.Width - border.X), game.GraphicsDevice.Viewport.Height);
            
            innerRect = new Rectangle(menuRect.X + (int)((menuRect.Width / 2) - (menuRect.Width * .15)), (int)((menuRect.Height / 3) - (menuRect.Height * .25)), (int)(menuRect.Width * .3), (int)(menuRect.Height * .3));
            leftRect = new Rectangle(innerRect.X - (int)(innerRect.Width * .25), (int)(innerRect.Y + (innerRect.Height / 2) - (innerRect.Height * .25)), (int)(innerRect.Width * .5), (int)(innerRect.Height * .5));
            rightRect = new Rectangle(innerRect.X + innerRect.Width - (int)(innerRect.Width * .25), (int)(innerRect.Y + (innerRect.Height / 2) - (innerRect.Height * .25)), (int)(innerRect.Width * .5), (int)(innerRect.Height * .5));

            weapons = new List<Texture2D>();
            gems = new List<Gem>();

            itemSelected = EquippedItem.SWORD;

            weapons.Add(sword);
            if (bombLocked)
            {
                weapons.Add(locked);
                weapons.Add(locked);
            }
            else if (bowLocked)
            {
                weapons.Add(bomb);
                weapons.Add(locked);
            }
            else
            {
                weapons.Add(bomb);
                weapons.Add(bow);
            }

            iconSize = (int)(innerRect.Height * 1.2);

            upgradesRect = new Rectangle(leftRect.X, menuRect.Y + iconSize, (rightRect.X + rightRect.Width), 500);

            equipOptions = new List<string>();
            gemChoices = new List<string>();
            gemIndexInInvent = new List<int>();

            equipOptions.Add("Equip");
            equipOptions.Add("Remove");
            equipOptions.Add("Cancel");
        }

        public void update()
        {
            keys = Keyboard.GetState();

            if (currentIndex == 0)
                itemSelected = EquippedItem.SWORD;
            else if (currentIndex == 1)
                itemSelected = EquippedItem.BOMB;
            else if (currentIndex == 2)
                itemSelected = EquippedItem.BOW;
            //set up a state, to equip gems into one weapon.
            //check for space being pressed down.


            if (weaponSelect)
            {
                optionsMessage = "Press space to get options";
                if (keys.IsKeyDown(Keys.A) && oldKeys.IsKeyUp(Keys.A))
                {
                    currentIndex--;
                    nextIndex--;
                    lastIndex--;
                    if (currentIndex < 0 && !bowLocked)
                    {
                        currentIndex = 2;
                    }
                    else if (currentIndex < 0 && !bombLocked)
                    {
                        currentIndex = 1;
                    }
                    else if (bowLocked && bombLocked)
                    {
                        currentIndex = 0;
                    }
                }

                if (keys.IsKeyDown(Keys.D) && oldKeys.IsKeyUp(Keys.D))
                {
                    currentIndex++;
                    nextIndex++;
                    lastIndex++;
                    if (currentIndex > 1 && bowLocked)
                    {
                        currentIndex = 0;
                    }
                    else if (bombLocked)
                    {
                        currentIndex = 0;
                    }
                    else if (currentIndex > 2)
                    {
                        currentIndex = 0;
                    }
                }

                if (currentIndex == 0)
                {
                    lastIndex = 2;
                    nextIndex = currentIndex + 1;
                }
                else if (currentIndex == 2)
                {
                    lastIndex = currentIndex - 1;
                    nextIndex = 0;
                }
                else
                {
                    lastIndex = currentIndex - 1;
                    nextIndex = currentIndex + 1;
                }

                if (keys.IsKeyDown(Keys.Space) && oldKeys.IsKeyUp(Keys.Space))
                {
                    equipOrUnequipMenu = true;
                    weaponSelect = false;
                }
            }
            else if (equipOrUnequipMenu)
            {
                optionsMessage = "Press space to select an option";
                gemChoices.Clear();
                gemIndexInInvent.Clear();
                gemChoiceIndex = 0;
                gotOptions = false;
                if (keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S))
                {
                    equipOptionsIndex++;
                    if (equipOptionsIndex >= equipOptions.Count)
                    {
                        equipOptionsIndex = 0;
                    }
                }
                else if (keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W))
                {
                    equipOptionsIndex--;
                    if (equipOptionsIndex < 0)
                    {
                        equipOptionsIndex = equipOptions.Count - 1;
                    }
                }

                if (keys.IsKeyDown(Keys.Space) && oldKeys.IsKeyUp(Keys.Space))
                {
                    if (equipOptions[equipOptionsIndex].Equals("Equip"))
                    {
                        gemSelect = true;
                        equipOrUnequipMenu = false;
                    }
                    else if (equipOptions[equipOptionsIndex].Equals("Remove"))
                    {
                        gemRemove = true;
                        equipOrUnequipMenu = false;
                    }
                    else if (equipOptions[equipOptionsIndex].Equals("Cancel"))
                    {
                        weaponSelect = true;
                        equipOrUnequipMenu = false;
                    }
                }
            }
            else if (gemSelect)
            {
                optionsMessage = "Press space to add a gem";
                if (!gotOptions)
                {
                    List<Item> items = player.PlayerInvent.ItemList;
                    for (int i = 0; i < player.PlayerInvent.ItemList.Count; i++)
                    {
                        if (items[i].ItemName.Contains("Stone"))
                        {
                            gemChoices.Add(items[i].ItemName);
                            gemIndexInInvent.Add(i);
                        }
                    }
                    gemChoices.Add("Cancel");
                    gotOptions = true;
                }

                if (keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S))
                {
                    gemChoiceIndex++;
                    if (gemChoiceIndex >= gemChoices.Count)
                    {
                        gemChoiceIndex = 0;
                    }
                }
                else if (keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W))
                {
                    gemChoiceIndex--;
                    if (gemChoiceIndex < 0)
                    {
                        gemChoiceIndex = gemChoices.Count - 1;
                    }
                }

                if (keys.IsKeyDown(Keys.Space) && oldKeys.IsKeyUp(Keys.Space))
                {
                    if (gemChoiceIndex != gemChoices.Count - 1)
                    {
                        if (player.Sword.MaxGemSlots != player.Sword.Gems.Count)
                        {
                            player.Sword.addGem((Gem)(player.PlayerInvent.ItemList[gemIndexInInvent[gemChoiceIndex]]));
                            player.PlayerInvent.ItemList.RemoveAt(gemIndexInInvent[gemChoiceIndex]);
                        }
                        equipOrUnequipMenu = true;
                        gemSelect = false;
                        gemRemove = false;
                    }
                    else
                    {
                        gemSelect = false;
                        gemRemove = false;
                        equipOrUnequipMenu = true;
                    }
                }
            }
            else if (gemRemove)
            {
                optionsMessage = "Press space to remove and destroy the gem";
                if (player.Sword.Gems.Count == 0 &&
                    player.Bomb.Gems.Count == 0 && 
                    player.Bow.Gems.Count == 0)
                {
                    equipOrUnequipMenu = true;
                    gemRemove = false;
                    gemSelect = false;
                }
                else
                {
                    if (!gotOptions)
                    {
                        if (itemSelected == EquippedItem.SWORD)
                        {
                            for (int i = 0; i < player.Sword.Gems.Count; i++)
                            {
                                gemChoices.Add(player.Sword.Gems[i].ItemName);
                            }
                        }
                        else if (itemSelected == EquippedItem.BOW)
                        {

                        }
                        else if (itemSelected == EquippedItem.BOMB)
                        {

                        }
                        gemChoices.Add("Cancel");
                        gotOptions = true;
                    }

                    if (keys.IsKeyDown(Keys.S) && oldKeys.IsKeyUp(Keys.S))
                    {
                        gemChoiceIndex++;
                        if (gemChoiceIndex >= gemChoices.Count)
                        {
                            gemChoiceIndex = 0;
                        }
                    }
                    else if (keys.IsKeyDown(Keys.W) && oldKeys.IsKeyUp(Keys.W))
                    {
                        gemChoiceIndex--;
                        if (gemChoiceIndex < 0)
                        {
                            gemChoiceIndex = gemChoices.Count - 1;
                        }
                    }

                    if (keys.IsKeyDown(Keys.Space) && oldKeys.IsKeyUp(Keys.Space))
                    {
                        if (gemChoiceIndex != gemChoices.Count - 1)
                        {
                            player.Sword.Gems.RemoveAt(gemChoiceIndex);
                            equipOrUnequipMenu = true;
                            gemSelect = false;
                            gemRemove = false;
                        }
                        else
                        {
                            gemRemove = false;
                            gemSelect = false;
                            equipOrUnequipMenu = true;
                        }
                    }
                }
            }

            oldKeys = keys;
        }

        public void draw(SpriteBatch spriteBatch, SpriteFont spriteFont)
        {
            /* To draw:
             * sword:
             *      lifesteal percent, freeze damage and chance, stun chance, burn damage and chance, poison damage and chance
             *bomb:
             *      stun chance, burn damage and chance
             *bow:
             *      stun chance, burn damage and chance, poison damage and chance, freeze damage and chance
             */
            float originOverLayPercent = 1.1f;
            float categoryOverLayPercent = 1.75f;
            Vector2 origin, chancePos, damagePos, durationPos, baseDamagePos, delayOrRangePos;
            Vector2 weaponStatsVec = origin = new Vector2((int)(menuRect.X * originOverLayPercent), (int)(upgradesRect.Y * 1.25f));
            spriteBatch.DrawString(spriteFont, "Type", weaponStatsVec, Color.White);
            Vector2 spacingVec = spriteFont.MeasureString("Lifesteal");
            chancePos = new Vector2(weaponStatsVec.X + (spacingVec.X * originOverLayPercent), weaponStatsVec.Y);
            spriteBatch.DrawString(spriteFont, "Chance", chancePos, Color.White);
            spacingVec = spriteFont.MeasureString("Chance");
            damagePos = new Vector2(chancePos.X + (spacingVec.X * categoryOverLayPercent), chancePos.Y);
            spriteBatch.DrawString(spriteFont, "Damage", damagePos, Color.White);
            spacingVec = spriteFont.MeasureString("Damage");
            durationPos = new Vector2(damagePos.X + (spacingVec.X * categoryOverLayPercent), damagePos.Y);
            spriteBatch.DrawString(spriteFont, "Duration", durationPos, Color.White);
            spacingVec = spriteFont.MeasureString("Duration");
            baseDamagePos = new Vector2(durationPos.X + (spacingVec.X * (categoryOverLayPercent - .25f)), durationPos.Y);
            spacingVec = spriteFont.MeasureString("Base Damage\n0");
            delayOrRangePos = new Vector2(baseDamagePos.X, baseDamagePos.Y + spacingVec.Y + 3);
            weaponStatsVec = origin;
            spriteBatch.DrawString(spriteFont, "Base Damage", baseDamagePos, Color.White);
            weaponStatsVec.Y += spriteFont.MeasureString("Type").Y * originOverLayPercent;
            spriteBatch.DrawString(spriteFont, "Lifesteal\nFreeze\nPoison\nBurn\nStun", weaponStatsVec, Color.White);
            weaponStatsVec = origin;

            if (itemSelected == EquippedItem.SWORD)
            {
                player.Sword.calcStats();
                Sword pSword = player.Sword;
                spriteBatch.DrawString(spriteFont, "" + pSword.Damage, new Vector2(baseDamagePos.X, 
                    baseDamagePos.Y + spriteFont.MeasureString("Base Damage").Y), Color.Green);

                chancePos.Y += spriteFont.MeasureString(pSword.LifeStealStruct.chance.ToString()).Y;
                spriteBatch.DrawString(spriteFont, "" + (int)(pSword.LifeStealStruct.chance * 100) + "%\n" +
                    pSword.IceStruct.chance * 100 + "%\n" + pSword.PoisonStruct.chance * 100 + "%\n" + pSword.FireStruct.chance * 100 + "%\n" +
                    pSword.StunStruct.chance * 100 + "%", chancePos, Color.Green);

                damagePos.Y += spriteFont.MeasureString("" + 0).Y;
                spriteBatch.DrawString(spriteFont, "0\n" + pSword.IceStruct.damage + "\n" + pSword.PoisonStruct.damage + "\n" + pSword.FireStruct.damage +
                    "\n0", damagePos, Color.Green);

                durationPos.Y += spriteFont.MeasureString("" + 0).Y;
                spriteBatch.DrawString(spriteFont, "0 sec\n" + pSword.IceStruct.duration + " sec\n" + pSword.PoisonStruct.duration + " sec\n" + pSword.FireStruct.duration +
                    " sec\n" + pSword.StunStruct.duration + " sec", durationPos, Color.Green);
            }
            else if (itemSelected == EquippedItem.BOMB)
            {
                player.Bomb.calcStats();
                Bomb pBomb = player.Bomb;

                spriteBatch.DrawString(spriteFont, "" + pBomb.Damage, new Vector2(baseDamagePos.X,
                    baseDamagePos.Y + spriteFont.MeasureString("Base Damage").Y), Color.Green);
                spriteBatch.DrawString(spriteFont, "Delay", delayOrRangePos, Color.White);
                spriteBatch.DrawString(spriteFont, "" + pBomb.Delay,
                    new Vector2(delayOrRangePos.X, delayOrRangePos.Y + spriteFont.MeasureString("Delay").Y), Color.Green);

                chancePos.Y += spriteFont.MeasureString(pBomb.LifeStealStruct.chance.ToString()).Y;
                spriteBatch.DrawString(spriteFont, "" + (int)(pBomb.LifeStealStruct.chance * 100) + "%\n" +
                    pBomb.IceStruct.chance * 100 + "%\n" + pBomb.PoisonStruct.chance * 100 + "%\n" + pBomb.FireStruct.chance * 100 + "%\n" +
                    pBomb.StunStruct.chance * 100 + "%", chancePos, Color.Green);

                damagePos.Y += spriteFont.MeasureString("" + 0).Y;
                spriteBatch.DrawString(spriteFont, "0\n" + pBomb.IceStruct.damage + "\n" + pBomb.PoisonStruct.damage + "\n" + pBomb.FireStruct.damage +
                    "\n0", damagePos, Color.Green);

                durationPos.Y += spriteFont.MeasureString("" + 0).Y;
                spriteBatch.DrawString(spriteFont, "0 sec\n" + pBomb.IceStruct.duration + " sec\n" + pBomb.PoisonStruct.duration + " sec\n" + pBomb.FireStruct.duration +
                    " sec\n" + pBomb.StunStruct.duration + " sec", durationPos, Color.Green);
            }
            else if (itemSelected == EquippedItem.BOW)
            {
                player.Bow.calcStats();
                Bow pBow = player.Bow;

                spriteBatch.DrawString(spriteFont, "" + pBow.Damage, new Vector2(baseDamagePos.X,
                    baseDamagePos.Y + spriteFont.MeasureString("Base Damage").Y), Color.Green);
                spriteBatch.DrawString(spriteFont, "Range", delayOrRangePos, Color.White);
                spriteBatch.DrawString(spriteFont, "" + pBow.Range,
                    new Vector2(delayOrRangePos.X, delayOrRangePos.Y + spriteFont.MeasureString("Delay").Y), Color.Green);

                chancePos.Y += spriteFont.MeasureString(pBow.LifeStealStruct.chance.ToString()).Y;
                spriteBatch.DrawString(spriteFont, "" + (int)(pBow.LifeStealStruct.chance * 100) + "%\n" +
                    pBow.IceStruct.chance * 100 + "%\n" + pBow.PoisonStruct.chance * 100 + "%\n" + pBow.FireStruct.chance * 100 + "%\n" +
                    pBow.StunStruct.chance * 100 + "%", chancePos, Color.Green);

                damagePos.Y += spriteFont.MeasureString("" + 0).Y;
                spriteBatch.DrawString(spriteFont, "0\n" + pBow.IceStruct.damage + "\n" + pBow.PoisonStruct.damage + "\n" + pBow.FireStruct.damage +
                    "\n0", damagePos, Color.Green);

                durationPos.Y += spriteFont.MeasureString("" + 0).Y;
                spriteBatch.DrawString(spriteFont, "0 sec\n" + pBow.IceStruct.duration + " sec\n" + pBow.PoisonStruct.duration + " sec\n" + pBow.FireStruct.duration +
                    " sec\n" + pBow.StunStruct.duration + " sec", durationPos, Color.Green);
            }

            Vector2 directionsVec;
            directionsVec = new Vector2(menuRect.X + (menuRect.Width / 2) - (spriteFont.MeasureString(optionsMessage).X / 2),
                Game1.DisplayHeight - spriteFont.MeasureString(optionsMessage).Y);
            spriteBatch.DrawString(spriteFont, optionsMessage, directionsVec, Color.White);

            spriteBatch.Draw(weapons[lastIndex], leftRect, Color.White);
            spriteBatch.Draw(weapons[nextIndex], rightRect, Color.White);
            spriteBatch.Draw(weapons[currentIndex], innerRect, Color.White);

            if (equipOrUnequipMenu)
            {
                int numOptions;
                Rectangle optionsRec;

                optionHeight = (int)(spriteFont.MeasureString(equipOptions[0]).Y) + 4;
                Vector2 vec = new Vector2(menuRect.X + 4, menuRect.Y);
                numOptions = equipOptions.Count;

                optionsRec = new Rectangle((int)vec.X, (int)vec.Y, 100, optionHeight * numOptions);
                spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("The best thing ever"), optionsRec, Color.White);

                for (int k = 0; k < equipOptions.Count; k++)
                {
                    if (k == equipOptionsIndex)
                    {
                        spriteBatch.DrawString(spriteFont, equipOptions[k], new Vector2(vec.X, optionsRec.Y + (optionHeight * k)), Color.Red);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont, equipOptions[k], new Vector2(vec.X, optionsRec.Y + (optionHeight * k)), Color.White);
                    }
                }
            }
            else if (gemSelect)
            {
                if (gemChoices.Count != 0)
                {
                    int numOptions;
                    Rectangle optionsRec;

                    optionHeight = (int)(spriteFont.MeasureString(gemChoices[0]).Y) + 4;
                    Vector2 vec = new Vector2(menuRect.X + 4, menuRect.Y);
                    numOptions = gemChoices.Count;

                    optionsRec = new Rectangle((int)vec.X, (int)vec.Y, 100, optionHeight * numOptions);
                    spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("The best thing ever"), optionsRec, Color.White);

                    for (int k = 0; k < gemChoices.Count; k++)
                    {
                        if (k == gemChoiceIndex)
                        {
                            spriteBatch.DrawString(spriteFont, gemChoices[k], new Vector2(vec.X, optionsRec.Y + (optionHeight * k)), Color.Red);
                        }
                        else
                        {
                            spriteBatch.DrawString(spriteFont, gemChoices[k], new Vector2(vec.X, optionsRec.Y + (optionHeight * k)), Color.White);
                        }
                    }
                }
            }
            else if (gemRemove)
            {
                if (gemChoices.Count != 0)
                {
                    int numOptions;
                    Rectangle optionsRec;

                    optionHeight = (int)(spriteFont.MeasureString(gemChoices[0]).Y) + 4;
                    Vector2 vec = new Vector2(menuRect.X + 4, menuRect.Y);
                    numOptions = gemChoices.Count;

                    optionsRec = new Rectangle((int)vec.X, (int)vec.Y, 100, optionHeight * numOptions);
                    spriteBatch.Draw(Game1.GameContent.Load<Texture2D>("The best thing ever"), optionsRec, Color.White);

                    for (int k = 0; k < gemChoices.Count; k++)
                    {
                        if (k == gemChoiceIndex)
                        {
                            spriteBatch.DrawString(spriteFont, gemChoices[k], new Vector2(vec.X, optionsRec.Y + (optionHeight * k)), Color.Red);
                        }
                        else
                        {
                            spriteBatch.DrawString(spriteFont, gemChoices[k], new Vector2(vec.X, optionsRec.Y + (optionHeight * k)), Color.White);
                        }
                    }
                }
            }
        }

        public void updatePlayer(Character player)
        {
            this.player = player;
        }
    }
}