using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AdlezHolder
{
    public class Bow
    {
        int damage, range;
        int upgradeDamageLev, upgradeRangeLev, 
            upgradeSpeedLev;
        float speed;

        Color color;
        Vector2 velocity, start;
        Texture2D texture;
        
        public Vector2 Velocity
        {
            get{ return velocity; }
        }

        public int Damage
        {
            get { return damage; }
        }
       
        public int Range
        {
            get{ return range; }
        }

        public int getDamageUpgradeLev
        {
            get{return upgradeDamageLev;}
        }

        public int getRangeUpgradeLev
        {
            get{return upgradeRangeLev;}
        }

        public int getSpeedUpgradeLev
        {
            get{return upgradeSpeedLev;}
        }

        public Bow(float scaleFactor)
        {   
            color = Color.White;

            upgradeDamageLev = 1;
            upgradeRangeLev = 1;
            upgradeSpeedLev = 1;
            speed = 3;
            damage = 5;
            range = 300;
        }

        public void UpgradeDamage()
        {
            if (upgradeDamageLev <= 5)
            {
                damage = (int)(damage * 1.25);
                upgradeDamageLev++;
            }
        }
           
        public void UpgradeRange()
        {
            if (upgradeRangeLev <= 5)
            {
                range = (int)(range * 1.25);
                upgradeRangeLev++;
            }
        }

        public void UpgradeSpeed()
        {
            if (upgradeSpeedLev <= 5)
            {
                speed = (int)(speed * 1.25);
                upgradeSpeedLev++;
            }
        }

        public void Update(Map data, GameTime gameTime)
        {
            if (data.Player.Direction == Orientation.DOWN)
            {
                velocity = new Vector2(0, speed);
                texture = Game1.GameContent.Load<Texture2D>("Projectiles/SteelArrowDown");
                start = new Vector2(data.Player.Position.X, data.Player.Position.Y + data.Player.CollisionRec.Height + 1);
            }
            else if (data.Player.Direction == Orientation.UP)
            {
                velocity = new Vector2(0, -speed);
                texture = Game1.GameContent.Load<Texture2D>("Projectiles/SteelArrowUp");
                start = new Vector2(data.Player.Position.X, data.Player.Position.Y - 30);
            }
            else if (data.Player.Direction == Orientation.RIGHT)
            {
                velocity = new Vector2(speed, 0);
                texture = Game1.GameContent.Load<Texture2D>("Projectiles/SteelArrowRight");
                start = new Vector2(data.Player.Position.X + data.Player.CollisionRec.Width + 1, data.Player.Position.Y);
            }
            else if (data.Player.Direction == Orientation.LEFT)
            {
                velocity = new Vector2(-speed, 0);
                texture = Game1.GameContent.Load<Texture2D>("Projectiles/SteelArrowLeft");
                start = new Vector2(data.Player.Position.X - 30, data.Player.Position.Y);
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        public void addArrow(Map data)
        {
            data.CurrentData.addProjectile(new Projectile(damage, range, speed, .035f, 
                velocity, start, texture));
        }

        public void changeImage(Texture2D texture)
        {
            this.texture = texture;
        }
    }
}
