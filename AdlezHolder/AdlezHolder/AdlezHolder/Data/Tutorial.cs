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
    public class Tutorial : MapDataHolder
    {
        bool moveDown, moveUp, moveRight, moveLeft, hasMoved,
            switchWeapons, attacked, hasAttacked, pauseUsed, hasPaused, introDone, changePos, finalMessage;

        KeyboardState keys, oldkeys;

        float timerPerMove = 0, introTimer = 0, messageIndex = 0;

        public Tutorial()
            : base()
        {
            keys = oldkeys = Keyboard.GetState();
            box = new MessageBox(1f);
            box.show("Hi! This Adlez, and this is where you are going to learn the basic controls for the game. Simply, it is WASD movement and most actions can be dealt with by pressing space.");
            introDone = true;
        }

        public override void Update(Map map, GameTime gameTime)
        {
            keys = Keyboard.GetState();
            if (!changePos)
            {
                map.Player.Position = new Vector2(400, 240);
                changePos = true;
            }

            if (!box.Visible && introDone)
            {
                box = new MessageBox(1f);
                box.show("Now let's try moving to around");
                introDone = false;
            }
            if (!box.Visible && (moveDown && moveLeft && moveRight && moveUp))
            {
                box = new MessageBox(1f);
                box.show("Well done! Now lets press the space bar to see the sword swing, if you are next to a chest it will open as long as you have enough keys which is displayed in the bottom right.");
                moveUp = moveRight = moveLeft = moveDown = false;
                hasMoved = true;
            }
            if (!box.Visible && attacked)
            {
                box = new MessageBox(1f);
                box.show("Very nice! That will be key for hacking and slicing through the adventure! Last thing you can do is open the pause menu with the escape key.");
                hasAttacked = true;
                attacked = false;
            }

            if (!hasMoved)
            {
                if (keys.IsKeyDown(Keys.W))
                    moveUp = true;
                if (keys.IsKeyDown(Keys.A))
                    moveLeft = true;
                if (keys.IsKeyDown(Keys.S))
                    moveDown = true;
                if (keys.IsKeyDown(Keys.D))
                    moveRight = true;
            }
            else
            {
                if (!hasAttacked)
                {
                    if (keys.IsKeyDown(Keys.Space) && oldkeys.IsKeyUp(Keys.Space))
                        attacked = true;
                }
                else
                {
                    if (!box.Visible)
                    {
                        box = new MessageBox(1f);
                        box.show("Well the rest is up to you! Have fun playing through the beta. A couple of last things to go over:  O and P are used to switch weapons. Use enter to navigate menus (for the most part). Lastly, there are only two dungeons and the major town of the game in this insatllation.");
                        finalMessage = true;
                    }
                }
            }

            introTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            base.Update(map, gameTime);

            if (!box.Visible && finalMessage && hasMoved && hasAttacked)
            {
                map.changeMap(new Nwot());
            }
            
            oldkeys = keys;
        }
    }
}
