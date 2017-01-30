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
        public Card(string name, string desc1, string desc2, string costString, int tileWidth, int padding, List<Card> hand, Tile[,] tiles, Point spriteCoords, Point spriteSize) {
            Name = name;

            Desc1 = "";
            Utilities.SplitInParts(desc1, 9).ToList().ForEach(x => Desc1 += x.Trim() + "\n");

            Desc2 = desc2;
            CostString = costString;

            Costs = new List<string>();

            TileWidth = tileWidth;
            Padding = padding;
            Spacing = 35;

            UniqueValue += 1;
            Value = UniqueValue;

            Hand = hand;
            Tiles = tiles;

            SpriteCoords = spriteCoords;
            SpriteSize = spriteSize;

            Position = new Vector2(Padding + Index * Spacing, Graphics.PreferredBackBufferHeight - (SpriteSize.Y * (TileWidth / 3)) - Padding);
            HoverPosition = new Vector2(Position.X, Position.Y - 100);
            OriginalPosition = Position;

            string[] costs = CostString.Split();

            foreach(string s in costs) {
                if (s[0] == 'g') {
                    GoldCost += Convert.ToInt32(s.Replace('g', ' '));
                }

                Costs.Add(s.ToUpper());
            }

            Hitbox = new Rectangle(Position.ToPoint(), new Point(SpriteSize.X * (TileWidth / 3), SpriteSize.Y * (TileWidth / 3)));

            LeftButtonClick += (s, e) => ClickCard();
            RightButtonClick = null;
        }

        public void CheckHovering(List<Card> hoveringCards) {
            Hovering = Hitbox.Intersects(Mouse.Hitbox);

            if (Hovering) {
                hoveringCards.Add(this);
            }
        }

        public virtual void Update(GameTime gt, bool hovering) {
            Index = Hand.IndexOf(this);

            Position = new Vector2(Padding + Index * Spacing, Position.Y);
            
            MouseState state = Mouse.GetState();
            
            if (hovering) {
                Position -= new Vector2(0, (Position.Y - HoverPosition.Y) * 3f * (float) gt.ElapsedGameTime.TotalSeconds);

                if (state.LeftButton == ButtonState.Pressed) LeftButtonClicked = true;
                if (state.RightButton == ButtonState.Pressed) RightButtonClicked = true;
            } else {
                Position += new Vector2(0, (OriginalPosition.Y - Position.Y) * 3f * (float) gt.ElapsedGameTime.TotalSeconds);
            }

            if (LeftButtonClicked && LeftButtonClick != null && Hovering && state.LeftButton == ButtonState.Released) {
                LeftButtonClick(this, EventArgs);
                LeftButtonClicked = false;
            }
            if (LeftButtonClicked && !Hovering && state.LeftButton == ButtonState.Released) {
                LeftButtonClicked = false;
            }

            if (RightButtonClicked && RightButtonClick != null && Hovering && state.RightButton == ButtonState.Released) {
                RightButtonClick(this, EventArgs);
                RightButtonClicked = false;
            }
            if (RightButtonClicked && !Hovering && state.RightButton == ButtonState.Released) {
                RightButtonClicked = false;
            }

            //Position = new Vector2(Padding + Index * spacing, Graphics.PreferredBackBufferHeight - (SpriteSize.Y * (TileWidth / 3)) - Padding);

            Hitbox = new Rectangle(new Vector2(Camera.Position.X + Position.X, Camera.Position.Y + Position.Y).ToPoint(), new Point(SpriteSize.X * (TileWidth / 3), SpriteSize.Y * (TileWidth / 3)));
        }

        public virtual void Draw(SpriteBatch sb) {
            int offset = 2;

            sb.Draw(Spritesheet, new Vector2(Camera.Position.X + Position.X, Camera.Position.Y + Position.Y), new Rectangle(new Point(SpriteCoords.X * (TileWidth / 3), SpriteCoords.Y * (TileWidth / 3)), new Point(SpriteSize.X * (TileWidth / 3), SpriteSize.Y * (TileWidth / 3))), Color.White, 0f, new Vector2(0), 1f, SpriteEffects.None, 0f);

            sb.DrawString(FontSmall, Name, new Vector2(Camera.Position.X + Position.X + 13, Camera.Position.Y + Position.Y + 9), Color.Black);
            sb.DrawString(FontSmall, Name, new Vector2(Camera.Position.X + Position.X + 13 - offset, Camera.Position.Y + Position.Y + 9 - offset), Color.White);

            sb.DrawString(FontTiny, Desc1, new Vector2(Camera.Position.X + Position.X + 13, Camera.Position.Y + Position.Y + 80), Color.Black);
            sb.DrawString(FontTiny, Desc1, new Vector2(Camera.Position.X + Position.X + 13 - offset, Camera.Position.Y + Position.Y + 80 - offset), Color.White);

            foreach (string s in Costs) {

            }
        }

        public void ClickCard() {

        }

        public delegate void Click(object sender, EventArgs e);
        public EventArgs EventArgs;
        public EventHandler LeftButtonClick;
        public EventHandler RightButtonClick;

        public string Name { get; set; }
        public string Desc1 { get; set; }
        public string Desc2 { get; set; }
        public string CostString { get; set; }

        public List<string> Costs { get; set; }

        public int GoldCost { get; set; }
        public int TileWidth { get; set; }
        public int Index { get; set; }
        public int Value { get; set; }
        public int Padding { get; set; }
        public int Spacing { get; set; }

        public static int UniqueValue { get; set; }

        public bool LeftButtonClicked { get; set; }
        public bool RightButtonClicked { get; set; }
        public bool Hovering { get; set; }

        public static Point DefaultCoords {
            get { return new Point(0, SpritesheetSize.Y - DefaultSize.Y); }
        }
        public static Point DefaultSize {
            get { return new Point(9, 15); }
        }

        public Point SpriteCoords { get; set; }
        public Point SpriteSize { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 HoverPosition { get; set; }
        public Vector2 OriginalPosition { get; set; }

        public Rectangle Hitbox { get; set; }

        public List<Card> Hand { get; set; }

        public Tile[,] Tiles { get; set; }
    }
}
