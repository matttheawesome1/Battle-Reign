using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_Reign {
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

        public string Text {
            get { return Value + "(+" + ValueIncrease + ")"; }
        }
        public string Name { get; }

        public int Value { get; set; }
        public int ValueIncrease { get; set; }
        public int Width {
            get { return (int) Font.MeasureString(Text).X + SpriteSize.X + 15; }
        }
        public int Height {
            get { return (int) Font.MeasureString(Text).Y + SpriteSize.Y; }
        }

        public Point Size {
            get { return new Point(Width, Height); }
        }
        public Point SpriteCoords { get; set; }
        public Point SpriteSize { get; set; }

        public SpriteFont Font { get; set; }
    }
}
