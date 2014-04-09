using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace AdlezHolder
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class InGameEditor : Microsoft.Xna.Framework.DrawableGameComponent
    {
        Map m;
        World w;
        SpriteBatch spriteBatch;
        KeyboardState keys, oldkeys;

        string command, option;

        bool editing = false, enteringCommand = true;

        public InGameEditor(Game game, World world)
            : base(game)
        {
            keys = oldkeys = Keyboard.GetState();

            command = option = "";

            w = world;
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }
        public override void Initialize()
        {
            base.Initialize();
        }

        
        public override void Update(GameTime gameTime)
        {
            keys = Keyboard.GetState();

            if (keys.IsKeyDown(Keys.Enter) && oldkeys.IsKeyUp(Keys.Enter) && Game1.MainGameState == GameState.PLAYING)
            {
                editing = !editing;
                command += ">";
            }

            if (Game1.MainGameState != GameState.PLAYING)
            {
                editing = false;
            }

            if (editing)
            {
                if (enteringCommand)
                {
                    //adjust command string
                    Keys k = Keys.Space;
                    if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                    {
                        enteringCommand = false;
                    }
                    if (keys.GetPressedKeys().Length != 0)
                    {
                        k = keys.GetPressedKeys()[0];
                    }

                    switch (k)
                    {
                        case Keys.A:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "A";
                                break;
                            }
                        case Keys.B:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "B";
                                break;
                            }
                        case Keys.C:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "C";
                                break;
                            }
                        case Keys.D:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "D";
                                break;
                            }
                        case Keys.E:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "E";
                                break;
                            }
                        case Keys.F:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "F";
                                break;
                            }
                        case Keys.G:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "G";
                                break;
                            }
                        case Keys.H:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "H";
                                break;
                            }
                        case Keys.I:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "I";
                                break;
                            }
                        case Keys.J:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "J";
                                break;
                            }
                        case Keys.K:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "K";
                                break;
                            }
                        case Keys.L:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "L";
                                break;
                            }
                        case Keys.M:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "M";
                                break;
                            }
                        case Keys.N:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "N";
                                break;
                            }
                        case Keys.O:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "O";
                                break;
                            }
                        case Keys.P:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "P";
                                break;
                            }
                        case Keys.Q:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "Q";
                                break;
                            }
                        case Keys.R:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "R";
                                break;
                            }
                        case Keys.S:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "S";
                                break;
                            }
                        case Keys.T:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "T";
                                break;
                            }
                        case Keys.U:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "U";
                                break;
                            }
                        case Keys.V:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "V";
                                break;
                            }
                        case Keys.W:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "W";
                                break;
                            }
                        case Keys.X:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "X";
                                break;
                            }
                        case Keys.Y:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "Y";
                                break;
                            }
                        case Keys.Z:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    command += "Z";
                                break;
                            }
                    }

                }
                else
                {
                    //adjust option string
                    Keys k = Keys.Space;
                    if (keys.GetPressedKeys().Length != 0)
                    {
                        k = keys.GetPressedKeys()[0];
                    }

                    switch (k)
                    {
                        case Keys.A:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "A";
                                break;
                            }
                        case Keys.B:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "B";
                                break;
                            }
                        case Keys.C:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "C";
                                break;
                            }
                        case Keys.D:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "D";
                                break;
                            }
                        case Keys.E:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "E";
                                break;
                            }
                        case Keys.F:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "F";
                                break;
                            }
                        case Keys.G:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "G";
                                break;
                            }
                        case Keys.H:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "H";
                                break;
                            }
                        case Keys.I:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "I";
                                break;
                            }
                        case Keys.J:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "J";
                                break;
                            }
                        case Keys.K:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "K";
                                break;
                            }
                        case Keys.L:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "L";
                                break;
                            }
                        case Keys.M:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "M";
                                break;
                            }
                        case Keys.N:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "N";
                                break;
                            }
                        case Keys.O:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "O";
                                break;
                            }
                        case Keys.P:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "P";
                                break;
                            }
                        case Keys.Q:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "Q";
                                break;
                            }
                        case Keys.R:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "R";
                                break;
                            }
                        case Keys.S:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "S";
                                break;
                            }
                        case Keys.T:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "T";
                                break;
                            }
                        case Keys.U:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "U";
                                break;
                            }
                        case Keys.V:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "V";
                                break;
                            }
                        case Keys.W:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "W";
                                break;
                            }
                        case Keys.X:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "X";
                                break;
                            }
                        case Keys.Y:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "Y";
                                break;
                            }
                        case Keys.Z:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                    option += "Z";
                                break;
                            }
                        case Keys.D1:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                {
                                    option += "1";
                                }
                                break;
                            }
                        case Keys.D2:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                {
                                    option += "2";
                                }
                                break;
                            }
                        case Keys.D3:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                {
                                    option += "3";
                                }
                                break;
                            }
                        case Keys.D4:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                {
                                    option += "4";
                                }
                                break;
                            }
                        case Keys.D5:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                {
                                    option += "5";
                                }
                                break;
                            }
                        case Keys.D6:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                {
                                    option += "6";
                                }
                                break;
                            }
                        case Keys.D7:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                {
                                    option += "7";
                                }
                                break;
                            }
                        case Keys.D8:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                {
                                    option += "8";
                                }
                                break;
                            }
                        case Keys.D9:
                            {
                                if (keys.IsKeyDown(k) && oldkeys.IsKeyUp(k))
                                {
                                    option += "9";
                                }
                                break;
                            }
                    }
                }
            }
            else
            {
                if (command.Length != 0)
                {
                    command.Remove(0, 1);
                }

                if (command.Contains("CHANGE"))
                {
                    //check options
                    if (option.Equals("MAINROOM"))
                    {
                        m.changeMap(new MainRoom());
                    }
                    else if (option.Equals("MAINROOM2"))
                    {
                        m.changeMap(new MainRoom2());
                    }
                    else if (option.Equals("TESTINGFIELD"))
                    {
                        m.changeMap(new TestingField());
                    }
                    else if (option.Equals("NWOT"))
                    {
                        m.changeMap(new Nwot());
                    }
                    else if (option.Equals("VOID"))
                    {
                        m.changeMap(new TheVoid());
                    }
                    m.Player.Position = new Vector2(Game1.DisplayWidth / 2, Game1.DisplayHeight / 2);
                }
                else if (command.Contains("SPAWN"))
                {
                    if (option.Equals("ARROW"))
                    {
                        m.CurrentData.addItem(new Arrow(.02f, false, "Arrow", 10, 
                            new Vector2(Game1.DisplayWidth / 2, Game1.DisplayHeight / 2), 99));
                    }
                    else if (option.Equals("LSTONE"))
                    {
                        m.CurrentData.addItem((new LightningStone(.02f,
                            new Vector2(Game1.DisplayWidth / 2, Game1.DisplayHeight / 2), 4, 1)));
                    }
                    else if (option.Equals("VSTONE"))
                    {
                        m.CurrentData.addItem((new VampiricStone(.02f,
                            new Vector2(Game1.DisplayWidth / 2, Game1.DisplayHeight / 2), 4, 1)));
                    }
                    else if (option.Equals("ISTONE"))
                    {
                        m.CurrentData.addItem((new IceStone(.02f,
                            new Vector2(Game1.DisplayWidth / 2, Game1.DisplayHeight / 2), 4, 1)));
                    }
                    else if (option.Equals("FSTONE"))
                    {
                        m.CurrentData.addItem((new FireStone(.02f,
                            new Vector2(Game1.DisplayWidth / 2, Game1.DisplayHeight / 2), 4, 1)));
                    }
                    else if (option.Equals("PSTONE"))
                    {
                        m.CurrentData.addItem((new PoisonStone(.02f,
                            new Vector2(Game1.DisplayWidth / 2, Game1.DisplayHeight / 2), 4, 1)));
                    }
                    else if (option.Equals("MONEY"))
                    {
                        m.CurrentData.addItem(new Money(.02f, new Vector2(Game1.DisplayWidth / 2, Game1.DisplayHeight / 2), "Lotsa money", 99999999));
                    }
                    else if (option.Equals("POTION"))
                    {
                        m.CurrentData.addItem(new Potion(new Vector2(Game1.DisplayWidth / 2, Game1.DisplayHeight / 2), .02f, 2, 1));
                    }
                    else if (option.Equals("BOMB"))
                    {
                        m.CurrentData.addItem(new BombItem(.02f, new Vector2(Game1.DisplayWidth / 2, Game1.DisplayHeight / 2), 99));
                    }
                    else if (option.Equals("SKELETON"))
                    {
                        m.CurrentData.addEnemy(new Skeleton(Game1.GameContent.Load<Texture2D>("ComputerPpl/Enemies/Skeleton/Move/SF"), .04f, 7, new Vector2(400, 300)));
                    }
                    else if (option.Equals("MAGE"))
                    {
                        m.CurrentData.addEnemy(new Mage(Game1.GameContent.Load<Texture2D>("ComputerPpl/Enemies/Mage/Move/F"), .04f, new Vector2(400, 300)));
                    }
                    else if (option.Equals("THING"))
                    {
                        m.CurrentData.addEnemy(new Thing(Game1.GameContent.Load<Texture2D>("ComputerPpl/Enemies/Thing/Move/TF"), .04f, new Vector2(400, 300)));
                    }
                    else if (option.Equals("BKEY"))
                    {
                        m.CurrentData.addItem(new Key(new Vector2(400, 240), KeyType.BRONZE, 1));
                    }
                    else if (option.Equals("SKEY"))
                    {
                        m.CurrentData.addItem(new Key(new Vector2(400, 240), KeyType.SILVER, 1));
                    }
                    else if (option.Equals("GKEY"))
                    {
                        m.CurrentData.addItem(new Key(new Vector2(400, 240), KeyType.GOLD, 1));
                    }

                }
                else if (command.Contains("RAIN"))
                {
                    Game1.ParticleState = ParticleState.RAIN;
                }
                else if (command.Contains("SNOW"))
                {
                    Game1.ParticleState = ParticleState.SNOW;
                }
                else if (command.Contains("LIBRARY"))
                {
                    //Game1.newCutscene(new LibraryCutscene(), m.Player);
                    w.Map.changeMap(new OutsideCave());
                }

                command = option = "";
                enteringCommand = true;
            }

            oldkeys = keys;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(Game1.GameContent.Load<SpriteFont>("SpriteFont1"), command + " " + option, 
                new Vector2(25, 50), Color.Blue);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public virtual void getMap(Map map)
        {
            m = map;
        }
    }
}
