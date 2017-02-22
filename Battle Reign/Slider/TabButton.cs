using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Battle_Reign {
    public class TabButton : Button {
        public TabButton(string text, Vector2 position, int index, EventHandler ev, Color color, Scene scene) : base(false, "", Vector2.Zero, (s, e) => scene.Action = Action.CHANGESTATE, "")
        {
            TabSize = new Point(150, 60);
            Position = new Vector2(Position.X + index * Size.X, Position.Y - Size.Y - 15);

            Color = color;
        }


        public override void Draw(SpriteBatch sb) {
            sb.Draw(BlankPixel, Position, new Rectangle(Point.Zero, TabSize), Color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            sb.DrawString(Font, Text, new Vector2(Position.X + 15, Position.Y + 15), Color.Black);
        }

        public Point TabSize { get; set; }
        public Color Color { get; set; }
    }
}