using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_Reign {
    public class Stats : GameObject {
        public Stats(int padding, Point offset, Point margin, Team team, List<Stat> stats) {
            Padding = padding;

            Offset = offset;
            Margin = margin;

            foreach (Stat s in stats) {
                s.Stats = StatList;
                s.Parent = this;
                s.Index = stats.IndexOf(s);
            }

            Team = team;

            StatList = stats;
            StatList.ForEach(x => x.Stats = StatList);
        }
        public void Change() {
            StatList.ForEach(x => x.Value += x.ValueIncrease);
        }

        public int Padding { get; set; }

        public Point Offset { get; set; }
        public Point Margin { get; set; }

        public Team Team { get; set; }

        public List<Stat> StatList { get; set; }
    }

    public class Stat : GameObject {
        public Stat(string name, int value, int valueIncrease, SpriteFont font) {
            Name = name;

            Value = value;
            ValueIncrease = valueIncrease;

            Font = font;

            SpriteCoords = new Point(0);
            SpriteSize = new Point(0);
        }
        public Stat(string name, int value, int valueIncrease, SpriteFont font, Point spriteCoords, Point spriteSize) {
            Name = name;

            Value = value;
            ValueIncrease = valueIncrease;

            Font = font;

            SpriteCoords = spriteCoords;
            SpriteSize = spriteSize;
        }
        public int GetX() {
            if (Stats.Count != 0) {
                int x = 0;

                for (int i = 0; i < Index; i++) {
                    x += Stats[i].Width;
                }

                return x;
            } else {
                return 0;
            }
        }

        public void Draw(SpriteBatch sb) {
            sb.Draw(Spritesheet, new Rectangle(Camera.Position.ToPoint() + new Point(Parent.Margin.X + Parent.Padding + Parent.Offset.X + GetX() + BackgroundSize.X - 7, Parent.Margin.Y + Parent.Padding + Parent.Offset.Y), new Vector2(Font.MeasureString(Text).X + 15, 6 * Cell).ToPoint()), new Rectangle(new Point(12, 3) * new Point(Cell), new Point(1, Cell * 6)), Parent.Team.Color, 0, Vector2.Zero, SpriteEffects.None, GUILayer);
            sb.Draw(Spritesheet, Camera.Position + new Vector2(Parent.Margin.X + Parent.Padding + Parent.Offset.X + GetX() + BackgroundSize.X - 7 + Font.MeasureString(Text).X + 15, Parent.Margin.Y + Parent.Padding + Parent.Offset.Y), new Rectangle(new Point(13, 3) * new Point(Cell), new Point(8, Cell * 6)), Parent.Team.Color, 0, Vector2.Zero, 1, SpriteEffects.None, GUILayer);

            sb.Draw(Spritesheet, Camera.Position + new Vector2(Parent.Margin.X + Parent.Padding + Parent.Offset.X + GetX(), Parent.Margin.Y + Parent.Padding + Parent.Offset.Y), new Rectangle(BackgroundCoords * new Point(Cell), BackgroundSize), Parent.Team.Color, 0, Vector2.Zero, 1, SpriteEffects.None, GUILayer);
            sb.Draw(Spritesheet, Camera.Position + new Vector2(Parent.Margin.X + Parent.Padding + Parent.Offset.X + GetX(), Parent.Margin.Y + Parent.Padding + Parent.Offset.Y), new Rectangle((BackgroundCoords + new Point(6, 0)) * new Point(Cell), BackgroundSize), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, GUILayer);
            sb.Draw(Spritesheet, Camera.Position + new Vector2(Parent.Margin.X + Parent.Padding + Parent.Offset.X + GetX() + BackgroundSize.X / 2 - (SpriteSize.X * 3 / 2), Parent.Margin.Y + Parent.Padding + Parent.Offset.Y + BackgroundSize.Y / 2 - (SpriteSize.Y * 3 / 2)), new Rectangle(SpriteCoords, SpriteSize), Color.White, 0, Vector2.Zero, 3, SpriteEffects.None, GUILayer);

            sb.DrawString(Font, Text, Camera.Position + new Vector2(Parent.Margin.X + Parent.Padding + Parent.Offset.X + GetX() + BackgroundSize.X + 5, Parent.Margin.Y + Parent.Padding + Parent.Offset.Y + BackgroundSize.Y / 2 - Font.MeasureString(Text).Y / 2), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, GUILayer);
        }

        public string Text {
            get { return Value + "(+" + ValueIncrease + ")"; }
        }
        public string Name { get; }
        
        public int Index { get; set; }
        public int Value { get; set; }
        public int ValueIncrease { get; set; }
        public int Width {
            get { return (int) Font.MeasureString(Text).X + BackgroundSize.X + 20; }
        }
        public int Height {
            get { return (int) Font.MeasureString(Text).Y + BackgroundSize.Y; }
        }

        public Point Size {
            get { return new Point(Width, Height); }
        }
        public Point SpriteCoords { get; set; }
        public Point SpriteSize { get; set; }

        public Point BackgroundCoords {
            get { return new Point(0, 3); }
        }
        public Point BackgroundSize {
            get { return new Point(6 * Cell); }
        }

        public SpriteFont Font { get; set; }

        public Stats Parent { get; set; }

        public List<Stat> Stats { get; set; }
    }

    public enum StatOrder {
        GOLD, 
        FOOD,
        SILVER
    }
}
