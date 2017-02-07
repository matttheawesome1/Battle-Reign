using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Reign {
    public abstract class Card : GameObject {
        public Card(string name, string desc1, string desc2, string costString, int padding, int spacing, List<Card> hand, World world, Team team, Point spriteCoords, Point spriteSize, Color primaryColor, CardType type) {
            Name = name;
            Team = team;

            Type = type;

            Level = 2;
            Scale = 1;

            Desc1 = "";
            Utilities.SplitToLines(desc1, 13).ToList().ForEach(x => Desc1 += x.Trim() + "\n");

            Desc2 = desc2;
            CostString = costString;

            Costs = new List<string>();

            Padding = padding;
            Spacing = spacing;

            UniqueValue += 1;
            Value = UniqueValue;

            Hand = hand;
            World = world;

            SpriteCoords = spriteCoords;
            SpriteSize = spriteSize;

            Spacing = 125;

            CardSize = new Point(9, 15) * new Point(Cell);
            CardPadding = 4;

            Position = new Vector2(Padding + Index * Spacing, Graphics.PreferredBackBufferHeight - (CardSize.Y) - 35);
            HoverPosition = new Vector2(Position.X, Position.Y - 100);
            OriginalPosition = Position;

            string[] costs = CostString.Split();

            foreach(string s in costs) {
                if (s[0] == 'g') {
                    GoldCost += Convert.ToInt32(s.Replace('g', ' '));
                }

                Costs.Add(s.ToUpper());
            }

            PrimaryColor = primaryColor == null ? Color.FromNonPremultiplied(233, 54, 54, 255) : primaryColor;
            SecondaryColor = Color.White;

            ImageOpacity = 255;
            TextOpacity = 0;
            
            CanPlace = false;

            Hitbox = new Rectangle(Position.ToPoint(), new Point(CardSize.X * Cell, CardSize.Y * Cell));

            LeftButtonClick += (s, e) => ClickCard();
        }

        public virtual void Update(GameTime gt) {
            Index = Hand.IndexOf(this);

            Hovering = Mouse.Hitbox.Intersects(Hitbox);

            Position -= new Vector2((Position.X - (Padding + Index * Spacing)) * 13 * (float) gt.ElapsedGameTime.TotalSeconds, 0);
            
            MouseState state = Mouse.GetState();
            
            if (Hovering) {
                Position -= new Vector2(0, (Position.Y - HoverPosition.Y) * 6f * (float) gt.ElapsedGameTime.TotalSeconds);

                if (state.LeftButton == ButtonState.Pressed) {
                    LeftButtonClicked = true;
                }
            } else {
                Position += new Vector2(0, (OriginalPosition.Y - Position.Y) * 6f * (float) gt.ElapsedGameTime.TotalSeconds);
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed) {
                if (Placing && CanPlace) {
                    CanPlace = false;

                    bool cont = true;

                    for (int i = 0; i < World.Tiles.GetLength(0) && cont; i++) {
                        for (int j = 0; j < World.Tiles.GetLength(1) && cont; j++) {
                            if (World.Tiles[i, j].Hitbox.Intersects(Mouse.Hitbox)) {
                                Type type = GetType();

                                if (Type == CardType.BUILDING) {
                                    if (World.CanPlaceBuilding(PlacementBuilding)) {
                                        World.Buildings.Add(PlacementBuilding);
                                        Team.Discover(Building.VisionRange);

                                        Used = true;
                                        PlacingBuilding = false;
                                    }
                                } else {
                                    if (World.CanPlaceUnit(PlacementUnit)) {
                                        World.Units.Add(PlacementUnit);
                                        PlacementUnit.Place();

                                        Used = true;
                                        PlacingUnit = false;
                                    }
                                }

                                cont = false;

                                break;
                            }
                        }
                    }
                }

                if (Hitbox.Intersects(Mouse.Hitbox)) {
                    if (!Placing) {
                        Type type = GetType();

                        if (Type == CardType.BUILDING) {
                            if (type == typeof(CardFarm)) {
                                PlacementBuilding = new Farm(new Vector2(Mouse.Position.X - Mouse.Position.X % TileWidth, Mouse.Position.Y - Mouse.Position.Y % TileWidth).ToPoint());
                            } else if (type == typeof(CardMine)) {
                                PlacementBuilding = new Mine(new Vector2(Mouse.Position.X - Mouse.Position.X % TileWidth, Mouse.Position.Y - Mouse.Position.Y % TileWidth).ToPoint());
                            } else if (type == typeof(CardQuarry)) {
                                PlacementBuilding = new Quarry(new Vector2(Mouse.Position.X - Mouse.Position.X % TileWidth, Mouse.Position.Y - Mouse.Position.Y % TileWidth).ToPoint());
                            }

                            PlacingBuilding = true;
                        } else {
                            if (type == typeof(CardWarrior)) {
                                PlacementUnit = new UnitWarrior(new Vector2(Mouse.Position.X - Mouse.Position.X % TileWidth, Mouse.Position.Y - Mouse.Position.Y % TileWidth).ToPoint(), World);
                            } else if (type == typeof(CardScout)) {
                                PlacementUnit = new UnitScout(new Vector2(Mouse.Position.X - Mouse.Position.X % TileWidth, Mouse.Position.Y - Mouse.Position.Y % TileWidth).ToPoint(), World);
                            } else if (type == typeof(CardWorker)) {
                                PlacementUnit = new UnitWorker(new Vector2(Mouse.Position.X - Mouse.Position.X % TileWidth, Mouse.Position.Y - Mouse.Position.Y % TileWidth).ToPoint(), World);
                            }

                            PlacingUnit = true;
                        }

                        HiddenPosition = Position;
                    }
                }
            } else {
                if (Placing) CanPlace = true;
            }

            if (LeftButtonClicked && !Hovering && state.LeftButton == ButtonState.Released) {
                LeftButtonClicked = false;
            }

            if (PlacingBuilding) {
                if (PlacementBuilding != null) {
                    PlacementBuilding.Position = new Vector2((Mouse.Position.X - Mouse.Position.X % TileWidth), (Mouse.Position.Y - Mouse.Position.Y % TileWidth));

                    PlacementBuilding.Update(gt);
                } else
                    PlacingBuilding = false;
            } else if (PlacingUnit) {
                if (PlacementUnit != null) {
                    PlacementUnit.Position = new Vector2((Mouse.Position.X - Mouse.Position.X % TileWidth), (Mouse.Position.Y - Mouse.Position.Y % TileWidth));

                    PlacementUnit.Update(gt);
                    PlacementUnit.OriginalPosition = PlacementUnit.Position;
                } else
                    PlacingUnit = false;
            }

            Hitbox = new Rectangle(new Vector2(Camera.Position.X + Position.X + (Placing ? Mouse.Position.X : 0), Camera.Position.Y + Position.Y + (Placing ? Mouse.Position.Y : 0)).ToPoint(), new Vector2(CardSize.X * Scale, CardSize.Y * Scale).ToPoint());
        }

        public virtual void Draw(SpriteBatch sb) {
            DrawCard(sb);

            if (PlacingBuilding) PlacementBuilding.Draw(sb, World.CanPlaceBuilding(PlacementBuilding));
            if (PlacingUnit) PlacementUnit.Draw(sb, World.CanPlaceUnit(PlacementUnit));
        }

        public void DrawCard(SpriteBatch sb) {
            sb.Draw(BlankPixel, new Rectangle(Camera.Position.ToPoint() + Position.ToPoint(), CardSize), SecondaryColor);
            sb.Draw(BlankPixel, new Rectangle(Camera.Position.ToPoint() + Position.ToPoint() + new Vector2(CardPadding).ToPoint(), new Point(CardSize.X, 21) - new Point(CardPadding * 2, 0)), PrimaryColor);

            sb.Draw(Spritesheet, Camera.Position + Position + (new Vector2(CardSize.X - SpriteSize.X * Cell - CardPadding - 5, CardPadding * 2)), new Rectangle(SpriteCoords * new Point(Cell), new Point(SpriteSize.X * Cell, SpriteSize.Y * Cell)), Color.White);

            sb.DrawString(FontTiny, Name, Camera.Position + Position + new Vector2(CardPadding + 3), Color.White);
            sb.DrawString(FontTiny, Desc1, Camera.Position + Position + new Vector2(CardPadding, CardPadding * 2 + 30), Color.Black);
        }

        public override void Click() {
            LeftButtonClick(this, EventArgs);
            LeftButtonClicked = false;
        }

        public void ClickCard() {

        }

        public delegate void Pressed(object sender, EventArgs e);
        public EventArgs EventArgs;
        public EventHandler LeftButtonClick;
        public EventHandler RightButtonClick;

        public string Name { get; set; }
        public string Desc1 { get; set; }
        public string Desc2 { get; set; }
        public string CostString { get; set; }

        public List<string> Costs { get; set; }

        public int GoldCost { get; set; }
        public int Index { get; set; }
        public int Value { get; set; }
        public int Padding { get; set; }
        public int Spacing { get; set; }
        public int CardWidth { get; set; }
        public int CardPadding { get; set; }

        public static int UniqueValue { get; set; }

        public float ImageOpacity { get; set; }
        public float TextOpacity { get; set; }
        public float Scale { get; set; }

        public bool LeftButtonClicked { get; set; }
        public bool RightButtonClicked { get; set; }
        public bool Hovering { get; set; }
        public bool PlacingBuilding { get; set; }
        public bool PlacingUnit { get; set; }
        public bool Placing {
            get { return PlacingBuilding || PlacingUnit; }
        }
        public bool CanPlace { get; set; }
        public bool Used { get; set; }

        public static Point DefaultCoords {
            get { return new Point(0, SpritesheetSize.Y - DefaultSize.Y); }
        }
        public static Point DefaultSize {
            get { return new Point(1); }
        }

        public Point SpriteCoords { get; set; }
        public Point SpriteSize { get; set; }
        public Point CardSize { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 HiddenPosition { get; set; }
        public Vector2 HoverPosition { get; set; }
        public Vector2 OriginalPosition { get; set; }

        public Rectangle Hitbox { get; set; }

        public Color PrimaryColor { get; set; }
        public Color SecondaryColor { get; set; }
        public static Color UnitColor {
            get { return Color.FromNonPremultiplied(237, 52, 52, 255); }
        }
        public static Color BuildingColor {
            get { return Color.FromNonPremultiplied(67, 141, 61, 255); }
        }
        public static Color BlockColor {
            get { return Color.FromNonPremultiplied(52, 92, 238, 255); }
        }

        public Team Team { get; set; }

        public Building PlacementBuilding { get; set; }
        public Unit PlacementUnit { get; set; }

        public List<Card> Hand { get; set; }

        public World World { get; set; }

        public CardType Type { get; set; }
    }

    public enum CardType {
        BUILDING,
        UNIT
    }
}
