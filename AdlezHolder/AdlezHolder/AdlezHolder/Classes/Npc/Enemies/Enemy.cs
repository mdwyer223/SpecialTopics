using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace AdlezHolder
{
    public abstract class Enemy : WanderingSprite
    {
        // TODO: make MapData.add smart, make an AdvancedSprite, RangedEnemy, path finding

        // wizards with bombs on their chests : explode on death or when close to u
        // LoL Zac style slimes : big enemies that spwans smaller ones when it dies
        // enemy that steels your money
        // knight:tanky,slow
        // shock wave mage        
        // ranger
        
        protected FullAnimation attackAn; // move some animations to AnimatiedSprite

        protected bool isAttacking, burned, poisoned, stunned, frozen;

        protected int burnTimer, burnDamagePerTick, bTickTimer,
            freezeTimer, freezeSpeed, originalSpeed, originalDamage, frozenDamage,
            poisonTimer, poisonDamagePerTick, pTickTimer,
            stunTimer;
        protected float stunDuration, burnDuration, freezeDuration, poisonDuration;
            

        int attackTimer, tolerence, nodeIndex;
        List<Message> messages;

        protected const float IMMUNITY_TIME = .25f;
        protected const float SEC_TO_ATTACK = 1;
        protected int immunityTimer = 0;

        protected SoundEffect damaged;

        public bool Frozen
        {
            get { return frozen; }
        }

        public bool Burned
        {
            get { return burned; }
        }

        public bool Poisoned
        {
            get { return poisoned; }
        }     
        
        public int AttackRange
        {
            get;
            protected set;
        }

        float attackRangeMod = 2;
        public float AttackRangeMod
        {
            get { return attackRangeMod; }
            protected set { attackRangeMod = value; }
        }

        int hitPoints = 150;
        public int HitPoints
        {
            get { return hitPoints; }
            protected set 
            {
                if (hitPoints > maxHealthPoints)
                    hitPoints = maxHealthPoints;
                else
                    hitPoints = value;
            } 
        }

        int maxHealthPoints = 150;
        public int MaxHealthPoints
        {
            get { return maxHealthPoints; }
            protected set
            {
                maxHealthPoints = value;
                if (hitPoints > value)
                    hitPoints = value;
            }

        }

        int strength = 5;
        public int Strength
        {
            get { return strength; }
            protected set { strength = value; }
        }

        public Enemy(Texture2D defaultTexture, float scaleFactor, int SecondsToCrossScreen, Vector2 startPosition)
            : base(defaultTexture, scaleFactor, SecondsToCrossScreen, Game1.DisplayWidth, startPosition)
        {
            setAttributes();
            AttackRange = (int)(attackRangeMod * (CollisionRec.Height + CollisionRec.Width));
            
            tolerence = speed * 3;
            originalSpeed = speed;

            Texture2D[] ani = new Texture2D[1];
            ani[0] = defaultTexture;
            attackAn = new FullAnimation(ani, 5);

            messages = new List<Message>();
            originalDamage = this.strength;
        }

        protected abstract void setAttributes();

        public override void Update(Map data, GameTime gameTime)
        {
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
            if (IsDead)
                return;

            if (immunityTimer <= IMMUNITY_TIME * 1000)
            {
                immunityTimer += gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                immunityTimer = (int)(IMMUNITY_TIME * 1000);
            }

            if (stunned)
            {
                stunTimer += gameTime.ElapsedGameTime.Milliseconds;
                addMessage(new Message("Stun", Color.Yellow));
                if (stunTimer >= stunDuration * 1000)
                {
                    stunned = false;
                    stunTimer = 0;
                    stunDuration = 0;
                }
                return;
            }

            if (frozen)
            {
                this.Strength = frozenDamage;
                freezeTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (freezeTimer >= (freezeDuration * 1000))
                {
                    freezeDuration = 0;
                    freezeTimer = 0;
                    frozen = false;
                }
            }
            else
            {
                this.Strength = this.originalDamage;
            }

            if (burned)
            {
                burnTimer += gameTime.ElapsedGameTime.Milliseconds;
                bTickTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (burnTimer < burnDuration * 1000)
                {
                    if (bTickTimer > 1000)
                    {
                        this.damage(data.CurrentData, (int)burnDamagePerTick);
                        addMessage(new Message("" + burnDamagePerTick, Color.Orange));
                        bTickTimer = 0;
                    }
                }
                else
                {
                    burned = false;
                    burnTimer = 0;
                    burnDuration = 0;
                    burnDamagePerTick = 0;
                }
            }

            if (poisoned)
            {
                poisonTimer += gameTime.ElapsedGameTime.Milliseconds;
                pTickTimer += gameTime.ElapsedGameTime.Milliseconds;
                if (poisonTimer < poisonDuration * 1000)
                {
                    if (pTickTimer > 1000)
                    {
                        this.damage(data.CurrentData, (int)poisonDamagePerTick);
                        addMessage(new Message("" + poisonDamagePerTick, Color.Purple));
                        pTickTimer = 0;
                    }
                }
                else
                {
                    poisoned = false;
                    poisonTimer = 0;
                    poisonDuration = 0;
                    poisonDamagePerTick = 0;
                }
            }

            //if (xDis > yDis && Math.Abs(target.X - Center.X) >= tolerence) 
            if (measureDistance(data.Player.Center) >= AttackRange)
            {
                startWander();
                wander();
            }
            else
            {
                stopWander();
                attack(data);
                chase(data);
            }

            base.Update(data, gameTime);            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsDead && IsVisible)
            {
                for (int i = 0; i < messages.Count; i++)
                {
                    messages[i].Draw(spriteBatch);
                }
                base.Draw(spriteBatch);
            }
        }

        protected virtual void dropItem(MapDataHolder data)
        {
            Random rand = new Random();
            int dropValue = rand.Next(1, 10);

            if (dropValue < 2)
            {
                int arrow = rand.Next(1, 4);
                if (arrow < 4)
                {
                    data.addItem(new Arrow(.03f, false, "Wooden Arrow", 0, this.Position));
                }
                else
                {
                    data.addItem(new Arrow(.03f, true, "Steel Arrow", 0, this.Position));
                }
            }
            else if (dropValue < 4 && dropValue > 2)
            {
                data.addItem(new Money(.01f, this.position, "Coins", 5));
            }
        }

        public virtual void burn(int damage, float duration)
        {
            burned = true;
            burnDamagePerTick = (int)((damage / duration) + .5f);
            burnDuration = duration;
            burnTimer = 0;
        }

        public virtual void stun(float duration)
        {
            stunned = true;
            stunDuration = duration;
            stunTimer = 0;
        }

        public virtual void freeze(int damage, float duration)
        {
            frozen = true;
            //speed cut by percentage
            this.HitPoints -= damage;
            addMessage(new Message("" + damage, Color.Cyan));
            frozenDamage = (int)((this.Strength * .5f) + .5f);
            freezeDuration = duration;
            freezeTimer = 0;
        }

        public virtual void poison(int damage, float duration)
        {
            poisoned = true;
            poisonDamagePerTick = (int)((damage / duration) + .5f);
            poisonDuration = duration;
            poisonTimer = 0;
        }

        public virtual void damage(MapDataHolder data, int hit)
        {
            if (immunityTimer >= (IMMUNITY_TIME * 1000))
            {
                hitPoints -= hit;
                if (hitPoints <= 0)
                {
                    hitPoints = 0;
                    IsDead = true;
                    IsVisible = false;
                }

                immunityTimer = 0;
            }

            this.knockBack();
        }

        protected void knockBack()
        {

        }

        public void addMessage(Message message)
        {
            char[] a = message.Text.Substring(0, 1).ToCharArray();
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

        protected virtual void attack(Map data)
        {
            // Attacking code
            if (TopRec.Intersects(data.Player.CollisionRec))
            {
                direction = Orientation.UP;
                playAnimation(attackAn);
                isAttacking = true;
            }
            else if (BottomRec.Intersects(data.Player.CollisionRec))
            {
                direction = Orientation.DOWN;
                playAnimation(attackAn);
                isAttacking = true;
            }
            else if (LeftRec.Intersects(data.Player.CollisionRec))
            {
                direction = Orientation.LEFT;
                playAnimation(attackAn);
                isAttacking = true;
            }
            else if (RightRec.Intersects(data.Player.CollisionRec))
            {
                direction = Orientation.RIGHT;
                playAnimation(attackAn);
                isAttacking = true;
            }
            else
            {
                attackTimer = 0;
                isAttacking = false;
            }

            if (isAttacking)
            {
                attackTimer++;
                if (attackTimer >= SEC_TO_ATTACK * 60)
                {
                    attackTimer = 0;
                    isAttacking = false;
                    data.Player.damage(strength);
                }
            }
        }

        protected virtual void chase(Map data)
        {
            // Movement code
            Vector2 target = data.Player.Center;

            Vector2 velocity = target - Center;
            float xDis = Math.Abs(velocity.X);
            float yDis = Math.Abs(velocity.Y);

            if (!isAttacking)
                if (xDis > yDis && Math.Abs(target.X - Center.X) >= tolerence)
                {
                    playAnimation(move);
                    if (velocity.X > 0)
                    {
                        if (canMoveRight)
                            moveRight();
                        else
                            moveY(velocity);
                    }
                    else
                    {
                        if (canMoveLeft)
                            moveLeft();
                        else                            
                            moveY(velocity);
                    }
                }
                else if (xDis <= yDis && Math.Abs(target.Y - Center.Y) >= tolerence)
                {
                    playAnimation(move);
                    if (velocity.Y > 0)
                    {
                        if (canMoveDown)
                            moveDown();
                        else
                            moveX(velocity);
                    }
                    else
                    {
                        if (canMoveUp)
                            moveUp();
                        else
                             moveX(velocity);
                    }
                }
        }

        private Node[] findPath(Map data)
        {
            Node[] allNodes = getAllNodes(data);
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();
            bool hasTarget;
            
            Node curNode = new Node(Position);
            openList.Add(curNode);

            Node playerNode = new Node();
            
            do 
            {
                // get lowest F on openlist
                float minDis = float.MaxValue;
                foreach (Node node in openList)
                {
                    float dis = node.AvgDis;
                    if (dis < minDis)
                    {
                        minDis = dis;
                        curNode = node;
                    }
                }
                openList.Remove(curNode);
                closedList.Add(curNode);

                // get surrounding
                foreach (Node node in getSuroundingNode(allNodes, curNode, 3))
                {
                    if (!closedList.Contains(node))
                    {
                        if (!openList.Contains(node))
                        {
                            openList.Add(node);
                            node.parent = curNode;

                            setUpNode(node, data.Player.Position);
                        }
                        else
                        {
                            // check if goin through curnode will be faster
                            if (node.PathDis > curNode.PathDis + (float)measureDistance(curNode.Location, node.Location))
                            {
                                node.parent = curNode;

                                node.PathDis = 0;
                                Node parent = node.parent;
                                while (parent != null)
                                {
                                    node.PathDis += (float)measureDistance(node.Location, parent.Location);
                                    parent = parent.parent;
                                }
                            }
                        }
                    }

                    if (!closedList.Contains(node) || !openList.Contains(node))
                    {
                        // count++; node doesn't count, get a new one
                    }
                }

                hasTarget = false;
                foreach (Node node in closedList)
                    if (node.Location == data.Player.Position)
                    {
                        hasTarget = true;
                        playerNode = node;
                        break;
                    }


            }
            while (!hasTarget && openList.Count != 0);


            List<Node> path = new List<Node>();
            Node lastNode = playerNode;
            while (lastNode != null)
            {
                path.Add(lastNode);
                lastNode = lastNode.parent;
                // never findes the enemey, 
            }

            path.Reverse();

            return path.ToArray();
        }

        private Node[] getAllNodes(Map data)
        {
            List<Node> validNodes = new List<Node>();

            // get obj nodes
            foreach (ImmovableObject obj in data.CurrentData.AllObjects)
                addNodes(validNodes, obj);

            // validate nodes
            int i=0;
            foreach (ImmovableObject obj in data.CurrentData.AllObjects)
                while (i < validNodes.Count)
                {
                    if (validNodes[i].isColliding(obj.CollisionRec))
                    {
                        validNodes.Remove(validNodes[i]);
                        i--;
                    }

                    i++;
                }

            validNodes.Add(new Node(data.Player.Position));
            return validNodes.ToArray();
        }

        private void addNodes(List<Node> nodes, ImmovableObject obj)
        {
            // Space nodes, dont add twice
            foreach (Vector2 nodePos in spaceNodes(obj))
            {
                Node node = new Node(nodePos);
                node.CollisionRec = new Rectangle((int)node.Location.X, (int)node.Location.Y, CollisionRec.Width,
                        CollisionRec.Height);

                if (!nodes.Contains(node))
                    nodes.Add(node);
            }
                
        }

        private Vector2[] spaceNodes(ImmovableObject obj)
        {
            Vector2[] inNodes = obj.nodes;
            Vector2[] outNodes = new Vector2[8];
            // 0, 2,4,6 - corner nodes
            outNodes[0] = new Vector2(inNodes[0].X - CollisionRec.Width, inNodes[0].Y - CollisionRec.Height);
            outNodes[2] = new Vector2(inNodes[2].X + CollisionRec.Width, inNodes[2].Y - CollisionRec.Height);
            outNodes[4] = new Vector2(inNodes[4].X + CollisionRec.Width, inNodes[4].Y + CollisionRec.Height);
            outNodes[6] = new Vector2(inNodes[6].X - CollisionRec.Width, inNodes[6].Y + CollisionRec.Height);

            outNodes[1] = new Vector2(inNodes[1].X, inNodes[1].Y - CollisionRec.Height);
            outNodes[3] = new Vector2(inNodes[3].X + CollisionRec.Width, inNodes[3].Y);
            outNodes[5] = new Vector2(inNodes[5].X, inNodes[5].Y + CollisionRec.Height);
            outNodes[7] = new Vector2(inNodes[7].X - CollisionRec.Width, inNodes[7].Y);
                
            return inNodes;
        }

        private void setUpNode(Node inNode, Vector2 endPos)
        {
            // set pathDis, endDis
            float nodeX, nodeY, xDis, yDis;
            nodeX = (float)inNode.Location.X;
            nodeY = (float)inNode.Location.Y;

            xDis = (float)measureDistance(new Vector2(nodeX, 0), new Vector2(endPos.X,0));
            yDis = (float)measureDistance(new Vector2(0, nodeY), new Vector2(0, endPos.Y));
            inNode.EndDis = xDis + yDis;

            Node parent = inNode.parent;
            while (parent != null)
            {
                inNode.PathDis += (float)measureDistance(inNode.Location, parent.Location);
                parent = parent.parent;
            }       
            
        }

        private Node[] getSuroundingNode(Node[] allNodes, Node selectedNode, int returnCount)
        {
            List<Node> outNodes = new List<Node>();

            int count = 0;
            float minDis;
            Node minNode;
            while (count < returnCount)
            {
                minNode = new Node();
                minDis = float.MaxValue;
                foreach (Node node in allNodes)
                {
                    float dis = (float)measureDistance(node.Location, selectedNode.Location);
                    if (dis < minDis && !outNodes.Contains(node) && node != selectedNode)
                    {
                        minDis = dis;
                        minNode = node;
                    }

                }
                count++;
                outNodes.Add(minNode);
            }

            return outNodes.ToArray();
        }



        private void moveX(Vector2 velocity)
        {
            if (velocity.X >= 0)
            {
                if (canMoveRight)
                    moveRight();
                else if (canMoveLeft)
                    moveLeft();
                //else
                //    moveY(velocity);
            }
            else if (velocity.X < 0)
            {
                if (canMoveLeft)
                    moveLeft();
                else if (canMoveRight)
                    moveRight();
                //else
                //    moveY(velocity);
            }
        }

        private void moveY(Vector2 velocity)
        {
            if (velocity.Y >= 0)
            {
                if (canMoveDown)
                    moveDown();
                else
                    moveUp();
                //else
                //    moveX(velocity);
            }
            else if (velocity.Y < 0)
            {
                if (canMoveUp)
                    moveUp();
                else 
                    moveDown();
                //else
                //    moveX(velocity);
            }
        }

        private void moveRight()
        {
            position.X += speed;
            direction = Orientation.RIGHT;
        }

        private void moveLeft()
        {
            position.X -= speed;
            direction = Orientation.LEFT;
        }

        private void moveUp()
        {
            position.Y -= speed;
            direction = Orientation.UP;
        }

        private void moveDown()
        {
            position.Y += speed;
            direction = Orientation.DOWN;
        }




    }
}
