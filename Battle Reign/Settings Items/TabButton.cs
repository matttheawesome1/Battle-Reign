using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Battle_Reign {
    public class TabButton : Button {
        public TabButton(string text, Vector2 position, int index, EventHandler ev, Color colors) : base(true, "square/small", position, ev, "square/small") {
            size = new Point(150, 60);
            Position = new Vector2(Position.X + index * Size.X, Position.Y - Size.Y - 15);

            Colors = colors;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(BlankPixel, Position, new Rectangle(Point.Zero, Size), Colors, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            sb.DrawString(Font, Text, new Vector2(Position.X + 15, Position.Y + 15), Color.Black);
        }

        public Point size { get; set; }
        public Color Colors { get; set; }
    }
}
