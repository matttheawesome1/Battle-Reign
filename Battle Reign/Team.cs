using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Reign {
    public class Team : GameObject {
        public Team(string name, float turnTime, Point basePos, World world, Save save, Scene scene) {
            Name = name;
            Name = Utilities.RandomName();

            Gold = 100;
            GoldIncrease = 5;
            Food = 100;
            FoodIncrease = 0;
            Iron = 100;
            IronIncrease = 0;

            CardPrice = 100;
            CardSpacing = 25;

            TurnTime = turnTime;

            World = world;
            Save = save;

            Cards = new List<Card>(7);
            Stats = new List<Stat>() {
                new Stat("Gold", Gold, GoldIncrease, FontSmall, new Point(0, (SpritesheetSize.Y - 16) * Cell), new Point(Cell)),
                new Stat("Food", Food, FoodIncrease, FontSmall, new Point(1 * Cell, (SpritesheetSize.Y - 16) * Cell), new Point(Cell)),
                new Stat("Iron", Iron, IronIncrease, FontSmall, new Point(2 * Cell, (SpritesheetSize.Y - 16) * Cell), new Point(Cell)),
            };

            UndiscoveredTiles = new Tile[World.Tiles.GetLength(0), World.Tiles.GetLength(1)];

            SpriteSize = new Point(3, 4);
            SpriteCoords = new Point(SpritesheetSize.X - 6, 0);

            Color = new Color(Utilities.Next(0, 255), Utilities.Next(0, 255), Utilities.Next(0, 255));

            Base = new Base(basePos, SpriteCoords, this);

            AddCardButton = new Button(true, "add/medium", new Vector2(0), (s, e) => AddCard(1, true), "add/medium");
            AddCardButton.Position = new Vector2(35, Graphics.PreferredBackBufferHeight - AddCardButton.Background.Height - 35);

            AddCard(StartingCards, false);

            ExitButton = new Button(true, "exit/tiny", Vector2.Zero, (s, e) => scene.Action = Action.EXIT, "square/tiny");

            GenerateFOW();
        }

        public void AddCard(int amount, bool remove) {
            if (remove) {
                if (Gold >= CardPrice * amount) {
                    for (int i = 0; i < (amount > Cards.Capacity ? Cards.Capacity : amount); i++) {
                        int a = Utilities.Next(0, 6);

                        if (a == 0)
                            Cards.Add(new CardMine((int) AddCardButton.Position.X + AddCardButton.Background.Width + 35, CardSpacing, Cards, this, World));
                        else if (a == 1)
                            Cards.Add(new CardFarm((int) AddCardButton.Position.X + AddCardButton.Background.Width + 35, CardSpacing, Cards, this, World));
                        else if (a == 2)
                            Cards.Add(new CardQuarry((int) AddCardButton.Position.X + AddCardButton.Background.Width + 35, CardSpacing, Cards, this, World));
                        else if (a == 3)
                            Cards.Add(new CardWarrior((int) AddCardButton.Position.X + AddCardButton.Background.Width + 35, CardSpacing, Cards, this, World));
                        else if (a == 4)
                            Cards.Add(new CardScout((int) AddCardButton.Position.X + AddCardButton.Background.Width + 35, CardSpacing, Cards, this, World));
                        else if (a == 5)
                            Cards.Add(new CardWorker((int) AddCardButton.Position.X + AddCardButton.Background.Width + 35, CardSpacing, Cards, this, World));
                    }

                    Gold -= CardPrice * amount;
                    FindStat("Gold").Value = Gold;
                }
            } else {
                for (int i = 0; i < (amount > Cards.Capacity ? Cards.Capacity : amount); i++) {
                    int a = Utilities.Next(0, 6);

                    if (a == 0)
                        Cards.Add(new CardMine((int) AddCardButton.Position.X + AddCardButton.Background.Width + 35, CardSpacing, Cards, this, World));
                    else if (a == 1)
                        Cards.Add(new CardFarm((int) AddCardButton.Position.X + AddCardButton.Background.Width + 35, CardSpacing, Cards, this, World));
                    else if (a == 2)
                        Cards.Add(new CardQuarry((int) AddCardButton.Position.X + AddCardButton.Background.Width + 35, CardSpacing, Cards, this, World));
                    else if (a == 3)
                        Cards.Add(new CardWarrior((int) AddCardButton.Position.X + AddCardButton.Background.Width + 35, CardSpacing, Cards, this, World));
                    else if (a == 4)
                        Cards.Add(new CardScout((int) AddCardButton.Position.X + AddCardButton.Background.Width + 35, CardSpacing, Cards, this, World));
                    else if (a == 5)
                        Cards.Add(new CardWorker((int) AddCardButton.Position.X + AddCardButton.Background.Width + 35, CardSpacing, Cards, this, World));
                }
            }
        }
        public Stat FindStat(string name) {
            foreach(Stat c in Stats) {
                if (c.Name == name)
                    return c;
            }

            return null;
        }

        public void Update(GameTime gt) {
            Time += (float) gt.ElapsedGameTime.TotalSeconds;

            Cards.ForEach(x => x.Update(gt));

            foreach(Card c in Cards) {
                if (c.Used) {
                    Cards.Remove(c);
                    break;
                }
            }

            ExitButton.Update(gt);
            AddCardButton.Update(gt);

            //UndiscoveredTiles.Cast<Tile>().ToList().ForEach(x => x.Update(gt));
        }

        public void Draw(SpriteBatch sb) {
            // FOG OF WAR

            foreach (Tile t in UndiscoveredTiles) {
                t.Draw(sb);
            }

            // GUI

            int padding = 10, height = padding * 5, offset = 3;

            sb.Draw(BlankPixel, Camera.Position, new Rectangle(0, 0, Graphics.PreferredBackBufferWidth, height), new Color(62, 6, 6), 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            sb.Draw(BlankPixel, new Vector2(Camera.Position.X + padding, Camera.Position.Y + padding), new Rectangle(0, 0, Graphics.PreferredBackBufferWidth - padding * 2, padding * 3), new Color(129, 21, 21), 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            sb.Draw(Spritesheet, Camera.Position + new Vector2(padding - 7, padding - 3), new Rectangle(new Point((SpritesheetSize.X - 3) * Cell, 0), new Point(3 * Cell, 2 * Cell)), Color, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);
            sb.Draw(Spritesheet, Camera.Position + new Vector2(padding - 7, padding - 3), new Rectangle(new Point((SpritesheetSize.X - 3) * Cell, 4 * Cell), new Point(3 * Cell, 2 * Cell)), Color.White, 0, Vector2.Zero, 1.5f, SpriteEffects.None, 0);

            ExitButton.Position = new Vector2(Graphics.PreferredBackBufferWidth - padding - ExitButton.Background.Width, height + padding);
            ExitButton.Draw(sb);

            // STATS

            int left = 40, margin = 0;

            for (int i = 0; i < Stats.Count; i++) {
                sb.Draw(Spritesheet, Camera.Position + new Vector2(padding + left + margin, padding + offset), new Rectangle(Stats[i].SpriteCoords, Stats[i].SpriteSize), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                
                sb.DrawString(Stats[i].Font, Stats[i].Text, Camera.Position + new Vector2(padding + left + margin + Stats[i].SpriteSize.X + 12, padding + offset), Color.White);

                margin += Stats[i].Width;
            }

            sb.DrawString(FontSmall, ((int) (TurnTime - Time)).ToString(), Camera.Position + new Vector2(Graphics.PreferredBackBufferWidth - padding - FontSmall.MeasureString(((int) (TurnTime - Time)).ToString()).X - offset * 2, padding + offset), Color.White);

            AddCardButton.Draw(sb);

            // CARDS

            Cards.ForEach(x => x.Draw(sb));
            if (HoveringCard != null) HoveringCard.Draw(sb);
        }

        public void GenerateFOW() {
            int buffer = 1;

            for (int i = 0; i < UndiscoveredTiles.GetLength(0); i++) {
                for (int j = 0; j < UndiscoveredTiles.GetLength(1); j++) {
                    if (i <= buffer - 1 || i >= World.Tiles.GetLength(0) - buffer || j <= buffer - 1 || j >= World.Tiles.GetLength(1) - buffer) {
                        UndiscoveredTiles[i, j] = new UndiscoveredTile(true, new Vector2(i * TileWidth, j * TileWidth), UndiscoveredTiles);
                    } else {
                        UndiscoveredTiles[i, j] = new UndiscoveredTile(false, new Vector2(i * TileWidth, j * TileWidth), UndiscoveredTiles);
                    }
                }
            }

            UndiscoveredTiles.Cast<UndiscoveredTile>().ToList().ForEach(x => x.Fix());
        }
        public void Increase() {
            Gold += GoldIncrease;

            Stats.First(x => x.Name == "Gold").Value = Gold;
            Stats.First(x => x.Name == "Gold").ValueIncrease = GoldIncrease;
        }
        public void Discover(int range) {
            List<Point> points = new List<Point>();

            foreach (UndiscoveredTile t in UndiscoveredTiles) {
                if (Mouse.Hitbox.Intersects(t.Hitbox)) {
                    for (int i = t.Coordinates.X - range; i < t.Coordinates.X + range + 1; i++) {
                        for (int j = t.Coordinates.Y - range; j < t.Coordinates.Y + range + 1; j++) {
                            if (i >= 0 && i <= World.Size.X && j >= 0 && j <= World.Size.Y && Utilities.Distance(new Point(i, j), t.Coordinates) <= range) {
                                UndiscoveredTiles[i, j].Discovered = true;
                                points.Add(new Point(i, j));
                            }
                        }
                    }

                    break;
                }
            }

            UndiscoveredTiles.Cast<UndiscoveredTile>().ToList().ForEach(x => x.Fix());
        }
        public void Discover(Point coordinates, int range) {
            List<Point> points = new List<Point>();

            foreach (UndiscoveredTile t in UndiscoveredTiles) {
                if (t.Hitbox.Contains(coordinates * new Point(TileWidth))) {
                    for (int i = t.Coordinates.X - range; i < t.Coordinates.X + range + 1; i++) {
                        for (int j = t.Coordinates.Y - range; j < t.Coordinates.Y + range + 1; j++) {
                            if (i >= 0 && i <= World.Size.X && j >= 0 && j <= World.Size.Y && Utilities.Distance(new Point(i, j), t.Coordinates) <= range) {
                                UndiscoveredTiles[i, j].Discovered = true;
                                points.Add(new Point(i, j));
                            }
                        }
                    }

                    break;
                }
            }

            UndiscoveredTiles.Cast<UndiscoveredTile>().ToList().ForEach(x => x.Fix());
        }

        public string Name { get; set; }

        public int Gold { get; set; }
        public int GoldIncrease { get; set; }
        public int Food { get; set; }
        public int FoodIncrease { get; set; }
        public int Iron { get; set; }
        public int IronIncrease { get; set; }
        public int CardPrice { get; set; }

        public int Buffer {
            get { return 2; }
        }

        public static int StartingCards {
            get { return 15; }
        }

        public int CardSpacing { get; set; }

        public bool Turn { get; set; }

        public float TurnTime { get; set; }
        public float Time { get; set; }

        public Button ExitButton { get; set; }
        public Button EndTurnButton { get; set; }
        public Button AddCardButton { get; set; }

        public Texture2D BaseImage { get; set; }

        public Point SpriteCoords { get; set; }
        public Point SpriteSize { get; set; }

        public Color Color { get; set; }

        public Card HoveringCard { get; set; }

        public Base Base { get; set; }

        public World World { get; set; }
        public Save Save { get; set; }

        public List<Card> Cards { get; set; }
        public List<Stat> Stats { get; set; }
        
        public Tile[,] UndiscoveredTiles { get; set; }
    }
}