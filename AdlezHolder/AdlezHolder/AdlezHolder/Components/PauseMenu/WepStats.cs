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
        
        int iconSize, currentIndex = 0, nextIndex = 1, lastIndex = 2;
        
        KeyboardState keys, oldKeys;
        Boolean bowLocked = false, bombLocked = false;

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
                spriteBatch.DrawString(spriteFont, "" + (int)(pSword.LifeStealStruct.chance * 10) + "%\n" +
                    pSword.IceStruct.chance + "%\n" + pSword.PoisonStruct.chance * 10 + "%\n" + pSword.FireStruct.chance * 10 + "%\n" +
                    pSword.StunStruct.chance * 10 + "%", chancePos, Color.Green);

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
        }

        public void updatePlayer(Character player)
        {
            this.player = player;
        }
    }
}