using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace AdlezHolder
{
    public class Enemy : WanderingSprite
    {
        // wizards with bombs on their chests :explode on death or when close to u
        // LoL Zac style slimes
        // small speedy enemies vs Big slow enemies
        // big enemies that spwans smaller ones when it dies

        enum Behavior { WONDER, ATTACK, CHASE }
        Behavior currentBehavior;

        protected FullAnimation attackAn; // move some animations to AnimatiedSprite
        protected List<Message> messages;

        protected int chaseRange, attackRange;
        int attackTimer, tolerence, nodeIndex;
        protected bool isAttacking;

        const int BASE_CHASE_RANGE = 250;
        const int BASE_ATTACK_RANGE = 180;
        protected const float IMMUNITY_TIME = .1f;
        const float SEC_TO_ATTACK = 1;

        protected int immunityTimer = 0;
        protected int maxHealthPoints, hitPoints, strength = 5;

        public int MaxHealthPoints
        {
            get { return maxHealthPoints; }
            protected set
            {
                maxHealthPoints = value;
                hitPoints = value;
            }
        }

        public int CurrentHP
        {
            get { return hitPoints; }
            set { hitPoints = value; }
        }

        public Enemy(Texture2D defaultTexture, float scaleFactor, int SecondsToCrossScreen, Vector2 startPosition)
            : base(defaultTexture, scaleFactor, SecondsToCrossScreen, Game1.DisplayWidth, startPosition)
        {
            chaseRange = (int)(BASE_CHASE_RANGE * scaleFactor);
            attackRange = CollisionRec.Height + CollisionRec.Width + (int)(BASE_ATTACK_RANGE * scaleFactor);
            
            tolerence = speed * 3;

            Texture2D[] ani = new Texture2D[1];
            ani[0] = defaultTexture;
            attackAn = new FullAnimation(ani, 5);

            MaxHealthPoints = 100;
            messages = new List<Message>();
        }

        public override void Update(Map data, GameTime gameTime)
        {
            if (IsDead)
                return;

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

            if (immunityTimer <= IMMUNITY_TIME * 1000)
            {
                immunityTimer += gameTime.ElapsedGameTime.Milliseconds;
            }
            else
            {
                immunityTimer = (int)(IMMUNITY_TIME * 1000);
            }

            if (measureDistance(data.Player.Center) >= attackRange)
            {
                // TODO: wander back to start when not chasing
                wander();
            }
            else
            {
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

        protected virtual void dropItem(MapDataHolder data)
        {
            Random rand = new Random();
            int dropValue = rand.Next(1, 10);
          

            if (dropValue < 6)
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
            else if (dropValue >= 6)
            {
                 data.addItem(new Money(.01f, this.position, "Coins", 5));
            }
        }

        public virtual void damage(MapDataHolder data, int hit)
        {
            if (immunityTimer >= (IMMUNITY_TIME * 1000))
            {
                hitPoints -= hit;
                addMessage(new Message("" + hit, Color.Red));
                if (hitPoints <= 0)
                {
                    hitPoints = 0;
                    dropItem(data);
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
            resetWander();
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
            if (velocity.X > 0)
            {
                if (canMoveRight)
                    moveRight();
                else
                    moveLeft();
            }
            else
            {
                if (canMoveLeft)
                    moveLeft();
                else
                    moveRight();
            }
        }

        private void moveY(Vector2 velocity)
        {
            if (velocity.Y > 0)
            {
                if (canMoveDown)
                    moveDown();
                else
                    moveUp();
            }
            else
            {
                if (canMoveUp)
                    moveUp();
                else
                    moveDown();
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
