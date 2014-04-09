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
    public enum EquippedItem { SWORD, BOW, BOMB, NONE }

    public class Character : AnimatedSprite
    {
        List<Message> messages;
        List<Particle> particles;
        SoundEffect damaged;
        EquippedItem selectedItem;
        FullAnimation swordMove, bowMove, move, Idle, swordAttack, bowAttack;
        KeyboardState oldKeys;
        Inventory invent;
        Sword sword;
        Bow bow;
        Bomb bomb;
        int money, arrowCount, bombCount,
            maxArrows, maxBombs, bronzeCount, silverCount, goldCount;

        bool attacking, bowShot, bombSet, didTele;

        int healthPointsMax, currentHealthPoints;

        int immunityTimer = 0, attackTimer = 0, healingTimer = 0;
        const float IMMUNITY_TIME = .25f, ATTACK_TIME = .17f, HEALING_SICKNESS = .25f;

        private GemStruct burnTotal, freezeTotal, stunTotal, poisonTotal;
        private int burnTimer, freezeTimer, stunTimer, poisonTimer;

        int r, g, b;

        public new CharacterStruct SaveData
        {
            get
            {
                CharacterStruct myStruct = new CharacterStruct();
                myStruct.BaseStruct = base.SaveData;
                myStruct.currentHP = currentHealthPoints;
                myStruct.maxHP = MaxHitPoints;
                myStruct.currency = money;

                myStruct.inventData = invent.SaveData;

                myStruct.swordData = sword.SaveData;
                myStruct.bombData = bomb.SaveData;
                myStruct.bowData = bow.SaveData;

                myStruct.maxArrows = maxArrows;
                myStruct.maxBombs = maxBombs;
                myStruct.arrowCount = arrowCount;
                myStruct.bombCount = bombCount;
                myStruct.BaseStruct.saveId = "Chr";

                return myStruct;
            }
            set
            {
                base.SaveData = value.BaseStruct;
                healthPointsMax = value.maxHP;
                HitPoints = value.currentHP;
                money = value.currency;

                invent.SaveData = value.inventData;

                sword.SaveData = value.swordData;
                bomb.SaveData = value.bombData;
                bow.SaveData = value.bowData;

                maxArrows = value.maxArrows;
                maxBombs = value.maxBombs;
                arrowCount = value.arrowCount;
                bombCount = value.bombCount;

            }
        }

        public bool Teled
        {
            get { return didTele; }
        }
        
        public int Speed
        {
            get { return speed; }
        }

        public int Money
        {
            get { return money; }
        }

        public int GoldKeys
        {
            get { return goldCount; }
        }

        public int SilverKeys
        {
            get { return silverCount; }
        }

        public int Bronzekeys
        {
            get { return bronzeCount; }
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

        public int BombBag
        {
            get { return bombCount; }
        }

        public int Quiver
        {
            get { return arrowCount; }
        }

        public EquippedItem ItemEquipped
        {
            get { return selectedItem; }
            set { selectedItem = value; }
        }

        public bool QuiverFull
        {
            get { return arrowCount >= maxArrows; }
        }

        public bool BombBagFull
        {
            get { return bombCount >= maxBombs; }
        }

        public Sword Sword
        {
            get { return sword; }
        }

        public Bomb Bomb
        {
            get { return bomb; }
        }

        public Bow Bow
        {
            get { return bow; }
        }

        public Inventory PlayerInvent
        {
            get { return invent; }
        }

        public void load(MapStruct mapStruct)
        {
            SaveData = mapStruct.playerData;
        }

        public Character(Texture2D defaultTexture, float scaleFactor, int displayWidth, float secondsToCrossScreen, Vector2 start)
            : base(defaultTexture, scaleFactor, displayWidth, secondsToCrossScreen, start)
        {
            currentHealthPoints = healthPointsMax = 500;
            arrowCount = bombCount = 0;
            maxBombs = 30;
            maxArrows = 50;

            canMoveDown = true;
            canMoveUp = true;
            canMoveRight = true;
            canMoveLeft = true;
            attacking = false;
            construct();

            r = color.R;
            b = color.B;
            g = color.G;
        }

        private void construct()
        {
            ContentManager content = Game1.GameContent;
            messages = new List<Message>();
            particles = new List<Particle>();
            invent = new Inventory();

            sword = new Sword(.05f);
            bow = new Bow(0f);
            bomb = new Bomb(.03f);

            Texture2D[] left, right, forward, backward;

            left = new Texture2D[3];
            right = new Texture2D[3];
            forward = new Texture2D[3];
            backward = new Texture2D[3];

            backward[0] = content.Load<Texture2D>("Alistar/B");
            backward[1] = content.Load<Texture2D>("Alistar/BR");
            backward[2] = content.Load<Texture2D>("Alistar/BL");

            forward[0] = content.Load<Texture2D>("Alistar/F");
            forward[1] = content.Load<Texture2D>("Alistar/FR");
            forward[2] = content.Load<Texture2D>("Alistar/FL");

            left[0] = content.Load<Texture2D>("Alistar/L");
            left[1] = content.Load<Texture2D>("Alistar/LR");
            left[2] = content.Load<Texture2D>("Alistar/LL");

            right[0] = content.Load<Texture2D>("Alistar/R");
            right[1] = content.Load<Texture2D>("Alistar/RR");
            right[2] = content.Load<Texture2D>("Alistar/RL");
            move = new FullAnimation(backward, forward, left, right, .2f);

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
            swordMove = new FullAnimation(backward, forward, left, right, .2f);

            left = new Texture2D[3];
            right = new Texture2D[3];
            forward = new Texture2D[3];
            backward = new Texture2D[3];

            backward[0] = content.Load<Texture2D>("AlistarBow/B");
            backward[1] = content.Load<Texture2D>("AlistarBow/BR");
            backward[2] = content.Load<Texture2D>("AlistarBow/BL");

            forward[0] = content.Load<Texture2D>("AlistarBow/F");
            forward[1] = content.Load<Texture2D>("AlistarBow/FR");
            forward[2] = content.Load<Texture2D>("AlistarBow/FL");

            left[0] = content.Load<Texture2D>("AlistarBow/L");
            left[1] = content.Load<Texture2D>("AlistarBow/LR");
            left[2] = content.Load<Texture2D>("AlistarBow/LL");

            right[0] = content.Load<Texture2D>("AlistarBow/R");
            right[1] = content.Load<Texture2D>("AlistarBow/RR");
            right[2] = content.Load<Texture2D>("AlistarBow/RL");
            bowMove = new FullAnimation(backward, forward, left, right, .2f);

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

            forward[0] = content.Load<Texture2D>("AlistarSwordAttack/F1");
            forward[1] = content.Load<Texture2D>("AlistarSwordAttack/F2");
            backward[0] = content.Load<Texture2D>("AlistarSwordAttack/B1");
            backward[1] = content.Load<Texture2D>("AlistarSwordAttack/B2");
            right[0] = content.Load<Texture2D>("AlistarSwordAttack/R1");
            right[1] = content.Load<Texture2D>("AlistarSwordAttack/R2");
            left[0] = content.Load<Texture2D>("AlistarSwordAttack/L1");
            left[1] = content.Load<Texture2D>("AlistarSwordAttack/L2");
            swordAttack = new FullAnimation(backward, forward, left, right, .3f);

            left = new Texture2D[2];
            right = new Texture2D[2];
            forward = new Texture2D[2];
            backward = new Texture2D[2];

            forward[0] = content.Load<Texture2D>("AlistarBowAttack/F1");
            forward[1] = content.Load<Texture2D>("AlistarBowAttack/F2");
            backward[0] = content.Load<Texture2D>("AlistarBowAttack/B1");
            backward[1] = content.Load<Texture2D>("AlistarBowAttack/B2");
            right[0] = content.Load<Texture2D>("AlistarBowAttack/R1");
            right[1] = content.Load<Texture2D>("AlistarBowAttack/R2");
            left[0] = content.Load<Texture2D>("AlistarBowAttack/L1");
            left[1] = content.Load<Texture2D>("AlistarBowAttack/L2");
            bowAttack = new FullAnimation(backward, forward, left, right, .1f);

            selectedItem = EquippedItem.SWORD;

            playAnimation(Idle);

            damaged = Game1.GameContent.Load<SoundEffect>("Music/SFX/Hit By Enemy");

            canMoveDown = true;
            canMoveUp = true;
            canMoveRight = true;
            canMoveLeft = true;
            attacking = false;

            invent.addItem(new Potion(Vector2.Zero, .02f, 2, 7), this);
        }

        public override void Update(Map data, GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();
            if (r < 255)
                r++;
            if (b < 255)
                b++;
            if (g < 255)
                g++;

            color = new Color(r, g, b);
            Color c = Color.White;

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

            if (immunityTimer < (IMMUNITY_TIME * 1000))
            {
                immunityTimer += gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                immunityTimer = (int)(IMMUNITY_TIME * 1000) + 1;
            }

            if (healingTimer < (HEALING_SICKNESS * 1000))
            {
                healingTimer += gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                healingTimer = (int)(HEALING_SICKNESS * 1000) + 1;
            }

            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i] != null)
                {
                    Vector2 velo = new Vector2((Center.X - particles[i].Position.X), (Center.Y - particles[i].Position.Y) );
                    velo.Normalize();
                    velo *= 1.075f;
                    particles[i].adjustVelo(velo);
                    if (particles[i].OffScreen || measureDistance(particles[i].Position) < 10)
                    {
                        particles[i].rushOffScreen();
                        particles.RemoveAt(i);
                    }
                }
            }

            updateAffects();
            if (stunTotal.duration > 0)
                return;

            base.Update(data, gameTime);            

            bow.Update(data, gameTime);
            bomb.Update(data, gameTime);
            sword.Update(data.CurrentData, this, gameTime);
            if (attacking && selectedItem == EquippedItem.SWORD)
            {
                sword.toggle(false);
                return;
            }
            else if (attacking && selectedItem == EquippedItem.BOW)
            {
                if (!bowShot)
                {
                    if (arrowCount > 0)
                    {
                        bow.addArrow(data);
                        arrowCount--;
                        bowShot = true;
                    }
                    else
                    {
                        addMessage(new Message("Quiver Empty!", Color.Yellow));
                    }
                }
                return;
            }
            else if (attacking && selectedItem == EquippedItem.BOMB)
            {
                if (!bombSet)
                {
                    if (bombCount > 0)
                    {
                        bomb.addBomb(this.position, data.CurrentData);
                        bombCount--;
                        bombSet = true;
                    }
                    else
                    {
                        addMessage(new Message("Bomb Bag Empty!", Color.Yellow));
                    }
                }
                return;
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
                attackTimer = 0;
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
            if (money <= 9999)
            {
                money += value;
                if (money > 9999)
                    money = 9999;
            }
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

        public void addKey(KeyType type)
        {
            if (type == KeyType.BRONZE)
                bronzeCount++;
            else if (type == KeyType.SILVER)
                silverCount++;
            else if (type == KeyType.GOLD)
                goldCount++;
        }

        public void removeKey(KeyType type)
        {
            if (type == KeyType.BRONZE)
                bronzeCount--;
            else if (type == KeyType.SILVER)
                silverCount--;
            else if (type == KeyType.GOLD)
                goldCount--;
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
                if (freezeTotal.duration > 0)                   
                {
                    damagePoints = damagePoints + (int)((damagePoints * freezeTotal.damage) + .5f);                    
                }

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


        public void damage(Enemy enemy)
        {
            this.damage(enemy.Strength);
            Random rand = new Random();
            if (rand.NextDouble() <= enemy.Gem.chance)
            {
                switch (enemy.Atrib)
                {
                    case GemType.FIRE:
                        burn(enemy.Gem);
                        break;
                    case GemType.FREEZE:
                        freeze(enemy.Gem);
                        break;
                    case GemType.STUN:
                        stun(enemy.Gem);
                        break;
                    case GemType.POISON:
                        poison(enemy.Gem);
                        break;
                }
            }
        }

        public void heal(int healPoints)
        {
            if (healingTimer > (HEALING_SICKNESS * 1000))
            {
                if (currentHealthPoints + healPoints <= healthPointsMax)
                {
                    currentHealthPoints += healPoints;
                    addMessage(new Message("" + healPoints, Color.Green));
                }
                else
                {
                    int display = healthPointsMax - currentHealthPoints;
                    currentHealthPoints = healthPointsMax;
                    addMessage(new Message("" + display, Color.Green));
                }
                healingTimer = 0;
            }
        }

        private void updateAffects()
        {
            if (burnTotal.duration > 0)
            {
                burnTimer++;
                if (burnTimer % 60 == 0)
                {
                    currentHealthPoints -= burnTotal.damage;
                    this.addMessage(new Message(burnTotal.damage.ToString(),
                        new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.Orange));
                }
                else if (burnTimer >= burnTotal.duration * 60)
                {
                    burnTotal.duration = 0;
                    burnTimer = 0;
                }
            }

            if (poisonTotal.duration > 0)
            {
                poisonTimer++;
                if (poisonTimer % 60 == 0)
                {
                    currentHealthPoints -= poisonTotal.damage;
                    this.addMessage(new Message(poisonTotal.damage.ToString(),
                        new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.Purple));
                }
                else if (poisonTimer >= poisonTotal.duration * 60)
                {
                    poisonTotal.duration = 0;
                    poisonTimer = 0;
                }
            }

            if (freezeTotal.duration > 0)
            {
                freezeTimer++;
                if (stunTimer % 60 == 0)
                {
                    this.addMessage(new Message("frozen",
                        new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.Cyan));
                }
                if (freezeTimer >= freezeTotal.duration * 60)
                {
                    freezeTotal.duration = 0;
                    freezeTimer = 0;
                }
                else
                    sword.reduceDamage(freezeTotal);
            }

            if (stunTotal.duration > 0)
            {
                stunTimer++;
                if (stunTimer % 60 == 0)
                {
                    this.addMessage(new Message("stunned",
                        new Vector2(Game1.DisplayWidth, Game1.DisplayHeight), Color.Yellow));
                }
                else if (stunTimer >= stunTotal.duration * 60)
                {
                    stunTotal.duration = 0;
                    stunTimer = 0;
                }
            }

        }

        private void burn(GemStruct gem)
        {
            burnTotal.damage = gem.damage;
            burnTotal.duration = gem.duration;
        }
        private void freeze(GemStruct gem)
        {
            freezeTotal.damage = gem.damage;
            freezeTotal.duration = gem.duration;
        }
        private void stun(GemStruct gem)
        {
            stunTotal.duration = gem.duration;
        }
        private void poison(GemStruct gem)
        {
            poisonTotal.damage = gem.damage;
            poisonTotal.duration = gem.duration;
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

        public void addArrow()
        {
            if (arrowCount >= maxArrows)
            {
                return;
            }
            arrowCount++;
        }

        public void addBomb()
        {
            if (bombCount >= maxBombs)
            {
                return;
            }
            bombCount++;
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
                        objects[i].GetType() != typeof(Character))
                    {
                        objectsColliding.Add(objects[i]);

                    }
                }
            }

            for (int i = 0; i < objectsColliding.Count; i++)
            {
                if (!objectsColliding[i].IsDead)
                {
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
                            canMoveRight = objectsColliding[i].CanMoveRight;
                            canMoveDown = objectsColliding[i].CanMoveDown;
                            canMoveUp = objectsColliding[i].CanMoveUp;
                            canMoveLeft = objectsColliding[i].CanMoveLeft;

                            Vector2 vecToMove = measureCollison(objectsColliding[i].CollisionRec);
                            position += vecToMove;
                        }
                    }
                    else if ((objectsColliding[i].GetType() == typeof(ImmovableObject) ||
                        objectsColliding[i].GetType().IsSubclassOf(typeof(ImmovableObject)) || 
                        objectsColliding[i].GetType().IsSubclassOf(typeof(Enemy))) && objectsColliding[i].GetType() != typeof(Teleporter))
                    {
                        canMoveRight = direction != Orientation.RIGHT;
                        canMoveDown = direction != Orientation.DOWN;
                        canMoveUp = direction != Orientation.UP;
                        canMoveLeft = direction != Orientation.LEFT;

                        Vector2 vecToMove = measureCollison(objectsColliding[i].CollisionRec);
                        position += vecToMove;
                        break;
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

        public void increaseColor(bool red, bool blue, bool green)
        {
            if (red)
            {
                r += 3;
                if (r > 255)
                    r = 255;
            }
            if (blue)
            {
                b += 3;
                if (b > 255)
                    b = 255;
            }
            if (green)
            {
                g += 3;
                if (g > 255)
                    g = 255;
            }
        }

        public void decreaseColor(bool red, bool blue, bool green)
        {
            if (red)
            {
                r -= 3;
                if (r < 0)
                    r = 0;
            }
            if (blue)
            {
                b -= 3;
                if (b < 0)
                    b = 0;
            }
            if (green)
            {
                g -= 3;
                if (g < 0)
                    g = 0;
            }
        }

        public void teleported(MapDataHolder data)
        {
            Random rand = new Random();
            int maxDist = 150;

            for (int i = 0; i < 1500; i++)
            {
                Vector2 tempPos = new Vector2((float)(rand.NextDouble() * rand.Next(-maxDist, maxDist)) + Center.X, (float)(rand.NextDouble() * rand.Next(-maxDist, maxDist)) + Center.Y);
                Particle p = new Particle(Color.Cyan, 2, tempPos, 7, new Vector2(0, 0),
                   Vector2.Zero, ParticleType.TELEPORT);

                particles.Add(p);
                data.addParticle(p);
            }
        }

        public void setTele(bool value)
        {
            didTele = value;
        }

        public void setIdle(Texture2D[] images)
        {
            Texture2D[] left, right, forward, backward;

            left = new Texture2D[1];
            right = new Texture2D[1];
            forward = new Texture2D[1];
            backward = new Texture2D[1];

            forward[0] = images[0];
            backward[0] = images[1];
            left[0] = images[2];
            right[0] = images[3];
            Idle = new FullAnimation(backward, forward, left, right, .2f);
            playAnimation(Idle);
        }

        public void setIsVisible(bool isVis)
        {
            IsVisible = isVis;
        }
    }
}
