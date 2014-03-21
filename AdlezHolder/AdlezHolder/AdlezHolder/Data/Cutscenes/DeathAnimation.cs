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
    class DeathAnimation : AlphaCutscene
    {
        Rectangle dFront, dBack, dLeft, dRight;
        Character player;
        MapDataHolder data;
        int playOrder;

        public MapDataHolder Data
        {
            set { data = value; }
        }

        public Character Player
        {
            set { this.player = value; }
        }
        
        public DeathAnimation()
        {
            //int playerX = (int)player.Position.X;
            //int playerY = (int)player.Position.X;
            //int playerWidth = (int)player.DrawnRec.Width;
            //int playerHeight = (int)player.DrawnRec.Height; 
            ////dFront = new Rectangle(player.Position.X, player.Position.Y, player.DrawnRec.X, player.DrawnRec.Y);
           
        }

        public void Update()
        {
            if (playOrder == 0)
            {
                ContentManager content = Game1.GameContent;

                if (player.Direction.Equals(Orientation.UP))
                {
                    content.Load<Texture2D>("Alistar/Dead Alistar/Dead Front");
                }
                else if (player.Direction.Equals(Orientation.DOWN))
                {
                    content.Load<Texture2D>("Alistar/Dead Alistar/Dead Back");
                }
                else if (player.Direction.Equals(Orientation.LEFT))
                {
                    content.Load<Texture2D>("Alistar/Dead Alistar/Dead Left");
                }
                else if (player.Direction.Equals(Orientation.RIGHT))
                {
                    content.Load<Texture2D>("Alistar/Dead Alistar/Dead Right");
                }
            }
            else if (playOrder == 1)
            {
                if (player.Direction.Equals(Orientation.UP))
                {
                    moveTo(new Vector2(player.Position.X, (player.Position.Y + (Game1.DisplayHeight / 5))), player, 3);
                    if (isAtPosition(new Vector2(player.Position.X, (player.Position.Y + (Game1.DisplayHeight / 5))), player) == true)
                        playOrder++;
                }
                else if (player.Direction.Equals(Orientation.DOWN))
                {
                    moveTo(new Vector2(player.Position.X, (player.Position.Y - (Game1.DisplayHeight / 5))), player, 3);
                    if (isAtPosition(new Vector2(player.Position.X, (player.Position.Y - (Game1.DisplayHeight / 5))), player) == true)
                        playOrder++;
                }
                else if (player.Direction.Equals(Orientation.LEFT))
                {
                    moveTo(new Vector2((player.Position.X - (Game1.DisplayWidth / 5)), player.Position.Y), player, 3);
                    if (isAtPosition(new Vector2((player.Position.X - (Game1.DisplayWidth / 5)), player.Position.Y), player) == true)
                        playOrder++;
                }
                else if (player.Direction.Equals(Orientation.RIGHT))
                {
                    moveTo(new Vector2((player.Position.X + (Game1.DisplayWidth / 5)), player.Position.Y), player, 3);
                    if (isAtPosition(new Vector2((player.Position.X + (Game1.DisplayWidth / 5)), player.Position.Y), player) == true)
                        playOrder++;
                }
            }
        }

        
    }
}
