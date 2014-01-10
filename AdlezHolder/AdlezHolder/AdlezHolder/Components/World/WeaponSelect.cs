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
    public class WeaponSelect
    {
        public enum SelectedWeapon
        {
            SWORD, BOW, BOMB, LOCKED
        }

        SelectedWeapon current;
        Texture2D sword, bomb, bow;
        Rectangle selectedRectangle, screenRectangle;
        Rectangle swordRect, bombRect, bowRect;
        KeyboardState keys, oldKeys;
        int inDisplayWidth, inDisplayHeight, weaponIndex;
        float elapse;
        List<SelectedWeapon> weapons;
        Boolean allVisible = false;


        public WeaponSelect(SelectedWeapon weapon)
        {
            current = weapon;
            weapons = new List<SelectedWeapon>();
            weapons.Add(SelectedWeapon.SWORD);
            weapons.Add(SelectedWeapon.BOMB);
            weapons.Add(SelectedWeapon.BOW);

            sword = Game1.GameContent.Load<Texture2D>("Weapons/sword unselected");
            bomb = Game1.GameContent.Load<Texture2D>("Weapons/bomb unselected");
            bow = Game1.GameContent.Load<Texture2D>("Weapons/bow unselected"); 

            inDisplayWidth = Game1.DisplayWidth;
            inDisplayHeight = Game1.DisplayHeight;
            screenRectangle = new Rectangle((int)(inDisplayWidth * .1), (int)(inDisplayHeight * .1), 
                (int)(inDisplayWidth * .97), (int)(inDisplayHeight * .97));

            swordRect = new Rectangle((int)(screenRectangle.X), (int)(screenRectangle.Height - sword.Height), sword.Width, sword.Height);
            bombRect = new Rectangle((int)(screenRectangle.X), (int)(screenRectangle.Height - sword.Height), sword.Width, sword.Height);
            bowRect = new Rectangle((int)(screenRectangle.X), (int)(screenRectangle.Height - sword.Height), sword.Width, sword.Height);

            swordRect = scale(.09f, swordRect);
            bombRect = scale(.09f, bombRect);
            bowRect = scale(.09f, bowRect);
        }

        public Rectangle scale(float scaleFactor, Rectangle rectangle)
        {
            rectangle.Width = (int)((inDisplayWidth * scaleFactor) + 0.5f);
            float aspectRatio = (float)sword.Width / sword.Height;
            rectangle.Height = (int)((rectangle.Width / aspectRatio) + 0.5f);
            rectangle.Y = screenRectangle.Height - rectangle.Height;

            return rectangle;
        }

        public void update(Character player, GameTime gameTime)
        {
            keys = Keyboard.GetState();

            if ((keys.IsKeyDown(Keys.P) && oldKeys.IsKeyUp(Keys.P)) || (keys.IsKeyDown(Keys.O) && oldKeys.IsKeyUp(Keys.O)))
            {
                    allVisible = true;
            }

            if (allVisible && !(keys.IsKeyDown(Keys.P) && oldKeys.IsKeyUp(Keys.P)) || (keys.IsKeyDown(Keys.O) && oldKeys.IsKeyUp(Keys.O)))
            {
                elapse += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                elapse = 0;
            }

            if (allVisible)
            {
                if (keys.IsKeyDown(Keys.O) && oldKeys.IsKeyUp(Keys.O))
                {
                    weaponIndex--;
                    if (weaponIndex < 0)
                    {
                        weaponIndex = 2;
                    }
                    current = weapons[weaponIndex];
                }

                if (keys.IsKeyDown(Keys.P) && oldKeys.IsKeyUp(Keys.P))
                {
                    weaponIndex++;
                    if (weaponIndex > 2)
                    {
                        weaponIndex = 0;
                    }
                    current = weapons[weaponIndex];
                }

                if (current == SelectedWeapon.BOMB)
                {
                    bomb = Game1.GameContent.Load<Texture2D>("Weapons/bomb selected");
                    sword = Game1.GameContent.Load<Texture2D>("Weapons/sword unselected");
                    bow = Game1.GameContent.Load<Texture2D>("Weapons/bow unselected");

                    selectedRectangle = scale(.12f, bombRect);
                    selectedRectangle.X = (int)(swordRect.X + swordRect.Width * 1.1);
                    bowRect.X = (int)(selectedRectangle.X + selectedRectangle.Width * 1.1);

                    player.ItemEquipped = EquippedItem.BOMB;
                }
                else if (current == SelectedWeapon.BOW)
                {
                    bomb = Game1.GameContent.Load<Texture2D>("Weapons/bomb unselected");
                    sword = Game1.GameContent.Load<Texture2D>("Weapons/sword unselected");
                    bow = Game1.GameContent.Load<Texture2D>("Weapons/bow selected");

                    bombRect.X = (int)(swordRect.X + swordRect.Width * 1.1);
                    selectedRectangle = scale(.12f, bowRect);
                    selectedRectangle.X = (int)(bombRect.X + bombRect.Width * 1.1);
                    swordRect.X = (int)(screenRectangle.X);

                    player.ItemEquipped = EquippedItem.BOW;
                }
                else
                {
                    bomb = Game1.GameContent.Load<Texture2D>("Weapons/bomb unselected");
                    sword = Game1.GameContent.Load<Texture2D>("Weapons/sword selected");
                    bow = Game1.GameContent.Load<Texture2D>("Weapons/bow unselected");

                    selectedRectangle = scale(.12f, swordRect);
                    bombRect.X = (int)(selectedRectangle.X + selectedRectangle.Width * 1.1);
                    bowRect.X = (int)(bombRect.X + bombRect.Width * 1.15);

                    player.ItemEquipped = EquippedItem.SWORD;
                }
            }

            oldKeys = keys;

            if (elapse >= 2)
            {
                allVisible = false;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (allVisible)
            {

                if (current == SelectedWeapon.BOMB)
                {
                    spriteBatch.Draw(bomb, selectedRectangle, Color.White);
                    spriteBatch.Draw(sword, swordRect, Color.White);
                    spriteBatch.Draw(bow, bowRect, Color.White);
                }
                else if (current == SelectedWeapon.SWORD)
                {
                    spriteBatch.Draw(sword, selectedRectangle, Color.White);
                    spriteBatch.Draw(bomb, bombRect, Color.White);
                    spriteBatch.Draw(bow, bowRect, Color.White);
                }
                else
                {
                    spriteBatch.Draw(bow, selectedRectangle, Color.White);
                    spriteBatch.Draw(sword, swordRect, Color.White);
                    spriteBatch.Draw(bomb, bombRect, Color.White);
                }
            }
            else
            {
                if (current == SelectedWeapon.BOMB)
                {
                    bombRect.X = screenRectangle.X;
                    spriteBatch.Draw(bomb, bombRect, Color.White);
                }
                else if (current == SelectedWeapon.SWORD)
                {
                    spriteBatch.Draw(sword, swordRect, Color.White);
                }
                else
                {
                    bowRect.X = screenRectangle.X;
                    spriteBatch.Draw(bow, bowRect, Color.White);
                }
            }
        }
    }
}
