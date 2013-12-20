using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace AdlezHolder
{
    public class Character : AnimatedSprite
    {
        public enum EquippedItem { SWORD, BOW, BOMB, NONE }

        List<Message> messages;
        SoundEffect damaged;
        EquippedItem selectedItem;
        FullAnimation swordMove, bowMove, move, Idle, swordAttack, bowAttack;
        KeyboardState oldKeys;
        Inventory invent;
        Sword sword;
        Bow bow;
        Bomb bomb;
        int money;

        bool attacking, bowShot, bombSet;

        int healthPointsMax, currentHealthPoints;
        int immunityTimer = 0, attackTimer = 0;
        const float IMMUNITY_TIME = .06f, ATTACK_TIME = .17f;
        
        public int Speed
        {
            get { return speed; }
        }

        public int Money
        {
            get { return money; }
        }

        public int HitPoints
        {
            get { return currentHealthPoints; }
            set { currentHealthPoints = value; }
        }

        public int MaxHitPoints
        {
            get { return healthPointsMax; }
        }

        public EquippedItem ItemEquipped
        {
            get { return selectedItem; }
            set { selectedItem = value; }
        }

        public Sword Sword
        {
            get { return sword; }
        }

        public Inventory PlayerInvent
        {
            get { return invent; }
        }

        private Character() { }

        public void load(CharacterVars inPlayerVar)
        {
            sword = inPlayerVar.sword;
            HitPoints = inPlayerVar.curHealth;
            speed = inPlayerVar.speed;
            base.load(inPlayerVar);
            construct();
        }

        public Character(Texture2D defaultTexture, float scaleFactor, int displayWidth, float secondsToCrossScreen, Vector2 start)
            : base(defaultTexture, scaleFactor, displayWidth, secondsToCrossScreen, start)
        {
            construct();
        }

        private void construct()
        {
            ContentManager content = Game1.GameContent;
            messages = new List<Message>();

            sword = new Sword(.05f);

            Texture2D[] left, right, forward, backward;

            left = new Texture2D[3];
            right = new Texture2D[3];
            forward = new Texture2D[3];
            backward = new Texture2D[3];

            backward[0] = content.Load<Texture2D>("AlistarSword/B");
            backward[1] = content.Load<Texture2D>("AlistarSword/BR");
            backward[2] = content.Load<Texture2D>("AlistarSword/BL");

            forward[0] = content.Load<Texture2D>("AlistarSword/F");
            forward[1] = content.Load<Texture2D>("AlistarSword/FR");
            forward[2] = content.Load<Texture2D>("AlistarSword/FL");

            left[0] = content.Load<Texture2D>("AlistarSword/L");
            left[1] = content.Load<Texture2D>("AlistarSword/LR");
            left[2] = content.Load<Texture2D>("AlistarSword/LL");

            right[0] = content.Load<Texture2D>("AlistarSword/R");
            right[1] = content.Load<Texture2D>("AlistarSword/RR");
            right[2] = content.Load<Texture2D>("AlistarSword/RL");
            move = new FullAnimation(backward, forward, left, right, .2f);

            left = new Texture2D[1];
            right = new Texture2D[1];
            forward = new Texture2D[1];
            backward = new Texture2D[1];

            forward[0] = content.Load<Texture2D>("AlistarSword/F");
            backward[0] = content.Load<Texture2D>("AlistarSword/B");
            left[0] = content.Load<Texture2D>("AlistarSword/L");
            right[0] = content.Load<Texture2D>("AlistarSword/R");
            Idle = new FullAnimation(backward, forward, left, right, .2f);

            left = new Texture2D[2];
            right = new Texture2D[2];
            forward = new Texture2D[2];
            backward = new Texture2D[2];

            forward[0] = content.Load<Texture2D>("AlistarAttack/F1");
            forward[1] = content.Load<Texture2D>("AlistarAttack/F2");
            Animation frontAttack = new Animation(forward, .1f, Orientation.DOWN);
            backward[0] = content.Load<Texture2D>("AlistarAttack/B1");
            backward[1] = content.Load<Texture2D>("AlistarAttack/B2");
            Animation backAttack = new Animation(backward, .1f, Orientation.UP);
            right[0] = content.Load<Texture2D>("AlistarAttack/R1");
            right[1] = content.Load<Texture2D>("AlistarAttack/R2");
            Animation rightAttack = new Animation(right, .1f, Orientation.RIGHT);
            left[0] = content.Load<Texture2D>("AlistarAttack/L1");
            left[1] = content.Load<Texture2D>("AlistarAttack/L2");
            Animation leftAttack = new Animation(left, .1f, Orientation.LEFT);
            Attack = new FullAnimation(backAttack, frontAttack, leftAttack, rightAttack);
            selectedItem = EquippedItem.NONE;

            playAnimation(Idle);

            currentHealthPoints = healthPointsMax = 200;

            canMoveDown = true;
            canMoveUp = true;
            canMoveRight = true;
            canMoveLeft = true;
            attacking = false;
        }

        public override void Update(Map data, GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();

            for (int i = 0; i < messages.Count; i++)
            {
                if (messages[i] != null)
                {
                    messages[i].Update(gameTime, new Vector2(position.X, position.Y - 20), collisionRec.Width);
                    if (messages[i].TimeUp)
                    {
                        messages.RemoveAt(i);
                        i--;

                        if (i < 0)
                            i = 0;
                    }
                }
            }

            attackTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (attackTimer >= (ATTACK_TIME * 1000))
            {
                attackTimer = (int)(ATTACK_TIME * 1000);
                attacking = false;
                bowShot = false;
                bombSet = false;
                if (selectedItem == EquippedItem.SWORD)
                {
                    sword.toggle(true);
                }
            }

            base.Update(data, gameTime);

            bow.Update(data, gameTime);
            bomb.Update(data, gameTime);
            if (attacking && selectedItem == EquippedItem.SWORD)
            {
                sword.toggle(false);
                sword.Update(data.CurrentData, this, gameTime);
                return;
            }
            else if (attacking && selectedItem == EquippedItem.BOW)
            {
                if (!bowShot)
                {
                    bow.addArrow(data);
                    bowShot = true;
                }
                return;
            }
            else if (attacking && selectedItem == EquippedItem.BOMB)
            {
                if (!bombSet)
                {
                    bomb.addBomb(this.position, data.CurrentData);
                    bombSet = true;
                }
                return;
            }

            if (immunityTimer < (IMMUNITY_TIME * 1000))
            {
                immunityTimer += gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                immunityTimer = (int)(IMMUNITY_TIME * 1000) + 1;
            }

            if (keys.IsKeyDown(Keys.W))
            {
                direction = Orientation.UP;
                selectMove();
            }
            else if (keys.IsKeyDown(Keys.S))
            {
                direction = Orientation.DOWN;
                selectMove();
            }
            else if (keys.IsKeyDown(Keys.A))
            {
                direction = Orientation.LEFT;
                selectMove();
            }
            else if (keys.IsKeyDown(Keys.D))
            {
                direction = Orientation.RIGHT;
                selectMove();
            }
            else
            {
                if (!attacking)
                {
                    playAnimation(Idle);
                }
            }

            if (keys.IsKeyDown(Keys.Space) && oldKeys.IsKeyUp(Keys.Space))
            {
                if (selectedItem == EquippedItem.SWORD)
                {
                    playAnimation(swordAttack);
                }
                else if (selectedItem == EquippedItem.BOW)
                {
                    playAnimation(bowAttack);
                }
                attack();
            }

            fixSpacing(data.CurrentData.Everything.ToArray(), keys);

            oldKeys = keys;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (attacking)
            {
                sword.Draw(spriteBatch);
            }
            for (int i = 0; i < messages.Count; i++)
            {
                messages[i].Draw(spriteBatch);
            }
            bow.Draw(spriteBatch);
            bomb.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }

        public void addFunds(int value)
        {
            money += value;
        }

        public void subtractFunds(int value)
        {
            money -= value;
            if (money < 0)
                money = 0;
        }

        public void addItem(Item item)
        {
            invent.addItem(item, this);
        }

        public void addMessage(Message message)
        {
            char[] a = message.Text.Substring(0,1).ToCharArray();
            char b = 'A';

            if (a[0] > b)
            {

                if (messages.Count - 1 >= 0 && message.Text.Equals(messages[messages.Count - 1].Text,
                    StringComparison.CurrentCultureIgnoreCase))
                {
                    return;
                }
                else
                {
                    messages.Add(message);
                }
            }
            else
            {
                messages.Add(message);
            }
        }

        public void damage(int damagePoints)
        {
            if(immunityTimer >= (IMMUNITY_TIME * 1000))
            {
                currentHealthPoints -= damagePoints;
                if (currentHealthPoints <= 0)
                {
                    currentHealthPoints = 0;
                }
                damaged.Play();
                this.addMessage(new Message(damagePoints.ToString(), 
                    new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.Red));
                immunityTimer = 0;
            }
        }

        private void heal(int healPoints)
        {
            if (currentHealthPoints + healPoints <= healthPointsMax)
            {
                currentHealthPoints += healPoints;
            }
        }

        private void attack()
        {
            attacking = true;

            //play animation, display sword length stuff

            if (attackTimer >= ATTACK_TIME * 1000)
            {
                attackTimer = 0;
            }
        }

        private void knockBack(Vector2 vecToMove)
        {
        }

        private void fixSpacing(BaseSprite[] objects, KeyboardState keys)
        {
            Rectangle futureRec = new Rectangle(CollisionRec.X, CollisionRec.Y,
                CollisionRec.Width, CollisionRec.Height);

            if (canMoveUp && keys.IsKeyDown(Keys.W))
            {
                position.Y -= speed;
                futureRec.Y -= speed * 2;
            }
            else if (canMoveDown && keys.IsKeyDown(Keys.S))
            {
                position.Y += speed;
                futureRec.Y += speed * 2;
            }
            else if (canMoveLeft && keys.IsKeyDown(Keys.A))
            {
                position.X -= speed;
                futureRec.X -= speed * 2;
            }
            else if (canMoveRight && keys.IsKeyDown(Keys.D))
            {
                position.X += speed;
                futureRec.X += speed * 2;
            }

            canMoveRight = true;
            canMoveDown = true;
            canMoveUp = true;
            canMoveLeft = true;

            List<BaseSprite> objectsColliding = new List<BaseSprite>();

            for (int i = 0; i < objects.Length; i++)
            {
                if (!objects[i].IsDead)
                {
                    if (futureRec.Intersects(objects[i].CollisionRec) && 
                        objects[i].CollisionRec.GetType() != typeof(Character))
                    {
                        objectsColliding.Add(objects[i]);

                    }
                }
            }

            for (int i = 0; i < objectsColliding.Count; i++)
            {
                if (!objectsColliding[i].IsDead)
                {
                    if (objectsColliding[i].GetType() == typeof(ImmovableObject)
                        || objectsColliding[i].GetType() == typeof(HittableObject)
                        || objectsColliding[i].GetType() == typeof(Wall)
                        || objectsColliding[i].GetType() == typeof (Chest)
                        || objectsColliding[i].GetType() == typeof(Skeleton)
                        || objectsColliding[i].GetType() == typeof(Minotaur)
                        || objectsColliding[i].GetType() == typeof(Mage)
                        || objectsColliding[i].GetType() == typeof(Thing)
                        || objectsColliding[i].GetType() == typeof (BuildingObject))
                    {
                        canMoveRight = direction != Orientation.RIGHT;
                        canMoveDown = direction != Orientation.DOWN;
                        canMoveUp = direction != Orientation.UP;
                        canMoveLeft = direction != Orientation.LEFT;

                        Vector2 vecToMove = measureCollison(objectsColliding[i].CollisionRec);
                        position += vecToMove;
                        break;
                    }

                    if (objectsColliding[i].GetType() == typeof(MovableObject))
                    {
                        if (!keys.IsKeyDown(Keys.LeftShift))
                        {
                            canMoveRight = direction != Orientation.RIGHT;
                            canMoveDown = direction != Orientation.DOWN;
                            canMoveUp = direction != Orientation.UP;
                            canMoveLeft = direction != Orientation.LEFT;

                            Vector2 vecToMove = measureCollison(objectsColliding[i].CollisionRec);
                            position += vecToMove;
                            break;
                        }
                        else
                        {
                            canMoveRight = objectsColliding[i].CanMoveRight; // make these properties
                            canMoveDown = objectsColliding[i].CanMoveDown;
                            canMoveUp = objectsColliding[i].CanMoveUp;
                            canMoveLeft = objectsColliding[i].CanMoveLeft;

                            Vector2 vecToMove = measureCollison(objectsColliding[i].CollisionRec);
                            position += vecToMove;
                        }
                    }
                }
                
            }
        }

        private void selectMove()
        {
            Texture2D[] left, right, forward, backward;

            left = new Texture2D[1];
            right = new Texture2D[1];
            forward = new Texture2D[1];
            backward = new Texture2D[1];

            switch (selectedItem)
            {
                case EquippedItem.NONE:
                    playAnimation(move);
                    forward[0] = Game1.GameContent.Load<Texture2D>("Alistar/F");
                    backward[0] = Game1.GameContent.Load<Texture2D>("Alistar/B");
                    left[0] = Game1.GameContent.Load<Texture2D>("Alistar/L");
                    right[0] = Game1.GameContent.Load<Texture2D>("Alistar/R");
                    Idle = new FullAnimation(backward, forward, left, right, .2f);
                    break;
                case EquippedItem.SWORD:
                    playAnimation(swordMove);
                    forward[0] = Game1.GameContent.Load<Texture2D>("AlistarSword/F");
                    backward[0] = Game1.GameContent.Load<Texture2D>("AlistarSword/B");
                    left[0] = Game1.GameContent.Load<Texture2D>("AlistarSword/L");
                    right[0] = Game1.GameContent.Load<Texture2D>("AlistarSword/R");
                    Idle = new FullAnimation(backward, forward, left, right, .2f);
                    break;
                case EquippedItem.BOW:
                    playAnimation(bowMove);
                    forward[0] = Game1.GameContent.Load<Texture2D>("AlistarBow/F");
                    backward[0] = Game1.GameContent.Load<Texture2D>("AlistarBow/B");
                    left[0] = Game1.GameContent.Load<Texture2D>("AlistarBow/L");
                    right[0] = Game1.GameContent.Load<Texture2D>("AlistarBow/R");
                    Idle = new FullAnimation(backward, forward, left, right, .2f);
                    break;
                case EquippedItem.BOMB:
                    playAnimation(move);
                    forward[0] = Game1.GameContent.Load<Texture2D>("Alistar/F");
                    backward[0] = Game1.GameContent.Load<Texture2D>("Alistar/B");
                    left[0] = Game1.GameContent.Load<Texture2D>("Alistar/L");
                    right[0] = Game1.GameContent.Load<Texture2D>("Alistar/R");
                    Idle = new FullAnimation(backward, forward, left, right, .2f);
                    break;
            }
        }

        public void cutsceneMove(Orientation direction)
        {
            this.direction = direction;
            selectMove();
        }

        public void cutsceneUpdate(GameTime gt)
        {
            base.Update(null, gt);
        }

    }
}
