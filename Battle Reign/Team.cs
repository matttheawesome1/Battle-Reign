using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Reign {
    public class Team : GameObject {
        public Team(string name, float turnTime, Point basePos, World world, Save save, Scene scene) {
            Name = name;
            Name = Utilities.RandomName();

            CardSpacing = 20;
            WindowPadding = 20;

            TurnTime = turnTime;

            World = world;
            Save = save;

            Cards = new List<Card>(7);
            StatList = new Stats(17, new Point(3), new Point(100, 0), this, new List<Stat>() {
                new Stat("Gold", DefaultGold, 5, FontSmall, new Point(0, (SpritesheetSize.Y - 16) * Cell), new Point(Cell)),
                new Stat("Food", DefaultFood, 5, FontSmall, new Point(1 * Cell, (SpritesheetSize.Y - 16) * Cell), new Point(Cell)),
                new Stat("Iron", DefaultSilver, 5, FontSmall, new Point(2 * Cell, (SpritesheetSize.Y - 16) * Cell), new Point(Cell)),
            });

            UndiscoveredTiles = new Tile[World.Tiles.GetLength(0), World.Tiles.GetLength(1)];

            SpriteSize = new Point(3, 4);
            SpriteCoords = new Point(SpritesheetSize.X - 6, 0);

            Color = new Color(Utilities.Next(0, 255), Utilities.Next(0, 255), Utilities.Next(0, 255));

            Base = new Base(basePos, SpriteCoords, this);

            AddCardButton = new Button(true, "add/medium", new Vector2(0), (s, e) => AddCard(1, true), "add/medium");
            AddCardButton.Position = new Vector2(WindowPadding, Graphics.PreferredBackBufferHeight - AddCardButton.Background.Height - WindowPadding);

            AddCard(StartingCards, false);

            ExitButton = new Button(true, "exit/tiny", Vector2.Zero, (s, e) => scene.Action = Action.EXIT, "square/tiny");
            ExitButton.Position = new Vector2(Graphics.PreferredBackBufferWidth - ExitButton.Background.Width - WindowPadding, 135);

            GenerateFOW();
        }

        public void AddCard(int amount, bool remove) {
            if (remove) {
                if (StatList.StatList[(int) StatOrder.GOLD].Value >= CardPrice * amount) {
                    for (int i = 0; i < (amount > Cards.Capacity ? Cards.Capacity : amount); i++) {
                        int a = Utilities.Next(0, 6);

                        if (a == 0)
                            Cards.Add(new CardMine(WindowPadding, CardSpacing, Cards, this, World));
                        else if (a == 1)
                            Cards.Add(new CardFarm(WindowPadding, CardSpacing, Cards, this, World));
                        else if (a == 2)
                            Cards.Add(new CardQuarry(WindowPadding, CardSpacing, Cards, this, World));
                        else if (a == 3)
                            Cards.Add(new CardWarrior(WindowPadding, CardSpacing, Cards, this, World));
                        else if (a == 4)
                            Cards.Add(new CardScout(WindowPadding, CardSpacing, Cards, this, World));
                        else if (a == 5)
                            Cards.Add(new CardWorker(WindowPadding, CardSpacing, Cards, this, World));
                    }

                    StatList.StatList[(int) StatOrder.GOLD].Value -= CardPrice * amount;
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

        public void Update(GameTime gt) {
            Time += (float) gt.ElapsedGameTime.TotalSeconds;

            Cards.ForEach(x => x.Update(gt));

            foreach (Card c in Cards) {
                if (c.Used) {
                    Cards.Remove(c);
                    break;
                }
            }

            if (Debug.DiscoverOnClick && Mouse.CanPress && Mouse.LeftMouseDown) {
                Discover(5);
            }

            ExitButton.Update(gt);
            AddCardButton.Update(gt);

            UndiscoveredTiles.Cast<Tile>().ToList().ForEach(x => x.Update(gt));
        }

        public void Draw(SpriteBatch sb) {
            //FOG OF WAR

            foreach (Tile t in UndiscoveredTiles) {
                t.Draw(sb);
            }

            //GUI

            if (Debug.DebugActive)
                DrawDebugMenu(sb);

            if (Debug.DrawGUI)
                DrawGUI(sb);
        }
        public void DrawDebugMenu(SpriteBatch sb) {
            int offset = 2;

            sb.DrawString(Font15, Debug.DebugMenu, Camera.Position + new Vector2(WindowPadding, 100), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, GUILayer);
            sb.DrawString(Font15, Debug.DebugMenu, Camera.Position + new Vector2(WindowPadding + offset, 100 + offset), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, GUILayer);
        }
        public void DrawGUI(SpriteBatch sb) {
            int padding = 10, height = padding * 5, offset = 3;

            sb.Draw(Spritesheet, Camera.Position + new Vector2(padding - 7, padding - 3), new Rectangle(new Point((SpritesheetSize.X - 3) * Cell, 0), new Point(3 * Cell, 2 * Cell)), Color, 0, Vector2.Zero, 4, SpriteEffects.None, GUILayer);
            sb.Draw(Spritesheet, Camera.Position + new Vector2(padding - 7, padding - 3), new Rectangle(new Point((SpritesheetSize.X - 3) * Cell, 4 * Cell), new Point(3 * Cell, 2 * Cell)), Color.White, 0, Vector2.Zero, 4, SpriteEffects.None, GUILayer);

            //ExitButton.Position = new Vector2(Graphics.PreferredBackBufferWidth - padding - ExitButton.Background.Width, height + padding);
            ExitButton.Draw(sb);

            //STATS
            StatList.StatList.ForEach(x => x.Draw(sb));

            //TIMER
            sb.DrawString(FontSmall, ((int) (TurnTime - Time)).ToString(), Camera.Position + new Vector2(Graphics.PreferredBackBufferWidth - padding - FontSmall.MeasureString(((int) (TurnTime - Time)).ToString()).X - offset * 2, padding + offset), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, GUILayer);
            
            //ADD CARD BUTTON
            AddCardButton.Draw(sb);

            //CARDS
            Cards.ForEach(x => x.Draw(sb));

            if (HoveringCard != null)
                HoveringCard.Draw(sb);
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
            //Gold += GoldIncrease;

            StatList.Change();
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

        public int CardPrice {
            get { return 100; }
        }
        public int DefaultGold {
            get { return 100; }
        }
        public int DefaultFood {
            get { return 250; }
        }
        public int DefaultSilver {
            get { return 25; }
        }

        public int Buffer {
            get { return 2; }
        }

        public static int StartingCards {
            get { return 15; }
        }

        public int CardSpacing { get; set; }
        public int WindowPadding { get; set; }

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
        public Stats StatList { get; set; }
        
        public Tile[,] UndiscoveredTiles { get; set; }
    }
}