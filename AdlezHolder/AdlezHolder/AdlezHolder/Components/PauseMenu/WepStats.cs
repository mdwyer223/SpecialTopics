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
            sword = game.Content.Load<Texture2D>("Weapons/sword selected");
            bomb = game.Content.Load<Texture2D>("Weapons/bomb selected");
            bow = game.Content.Load<Texture2D>("Weapons/bow selected");
            locked = game.Content.Load<Texture2D>("Weapons/lock");

            menuRect = new Rectangle((int)border.X, (int)border.Y, (int)(game.GraphicsDevice.Viewport.Width - border.X), game.GraphicsDevice.Viewport.Height);
            innerRect = new Rectangle(menuRect.X + (int)((menuRect.Width / 2) - (menuRect.Width * .25)), (int)((menuRect.Height / 3) - (menuRect.Height * .25)), (int)(menuRect.Width * .5), (int)(menuRect.Height * .5));
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
            equipOptions.Add("Unequip");
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
                gemChoices.Clear();
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
                    else if (equipOptions[equipOptionsIndex].Equals("Unequip"))
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
                        player.Sword.addGem((Gem)(player.PlayerInvent.ItemList[gemIndexInInvent[gemChoiceIndex]]));
                        player.PlayerInvent.ItemList.RemoveAt(gemIndexInInvent[gemChoiceIndex]);
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
                if (player.Sword.Gems.Count == 0)
                {
                    equipOrUnequipMenu = true;
                    gemRemove = false;
                    gemSelect = false;
                }
                else
                {
                    if (!gotOptions)
                    {
                        for (int i = 0; i < player.Sword.Gems.Count; i++)
                        {
                            gemChoices.Add(player.Sword.Gems[i].ItemName);
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
            Vector2 origin, chancePos, damagePos, durationPos, baseDamagePos;
            Vector2 weaponStatsVec = origin = new Vector2((int)(menuRect.X * originOverLayPercent), upgradesRect.Y);
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
                spriteBatch.DrawString(spriteFont, "0\n" + pSword.IceStruct.duration + "\n" + pSword.PoisonStruct.duration + "\n" + pSword.FireStruct.duration +
                    "\n" + pSword.StunStruct.duration, durationPos, Color.Green);
            }

            spriteBatch.Draw(weapons[lastIndex], leftRect, Color.White);
            spriteBatch.Draw(weapons[nextIndex], rightRect, Color.White);
            spriteBatch.Draw(weapons[currentIndex], innerRect, Color.White);

            if (equipOrUnequipMenu)
            {
                int numOptions;
                Rectangle optionsRec;

                optionHeight = (int)(spriteFont.MeasureString(equipOptions[0]).Y) + 4;
                Vector2 vec = Vector2.Zero;
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
                    Vector2 vec = Vector2.Zero;
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
                    Vector2 vec = Vector2.Zero;
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