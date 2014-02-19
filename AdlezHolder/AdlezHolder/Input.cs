using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AdlezHolder
{
    public class Input
    {
        public static bool UpPressed()
        {
            KeyboardState keys = Keyboard.GetState();
            if (keys.IsKeyDown(Keys.W) || keys.IsKeyDown(Keys.Up))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DownPressed()
        {
            KeyboardState keys = Keyboard.GetState();
            if (keys.IsKeyDown(Keys.S) || keys.IsKeyDown(Keys.Down))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool LeftPressed()
        {
            KeyboardState keys = Keyboard.GetState();
            if (keys.IsKeyDown(Keys.A) || keys.IsKeyDown(Keys.Left))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool RightPressed()
        {
            KeyboardState keys = Keyboard.GetState();
            if (keys.IsKeyDown(Keys.D) || keys.IsKeyDown(Keys.Right))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
