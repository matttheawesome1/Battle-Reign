using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Battle_Reign {
    public class Tab : GameObject {
        public Tab(string text, int index, TabManager parent) {
            Text = text;
            Parent = parent;

            Size = new Point(Graphics.PreferredBackBufferWidth - Margin * 2, Graphics.PreferredBackBufferHeight - MarginTop - Margin * 2);
            Position = new Vector2(Margin, Margin + MarginTop);

            Image = Content.Load<Texture2D>("backgrounds/tabBackground");
            ImageBorder = Content.Load<Texture2D>("backgrounds/tabBorderBackground");

            Options = new List<Option>();

            Color = Color.FromNonPremultiplied(220, 220, 220, 255);

            int padding = 20;

            Button = new Button(true, text, "square/medium", Vector2.Zero, (s, e)=> Select(), "square/medium");
            Button.Position = new Vector2(Position.X + index * Button.Background.Bounds.X + (index > 0 ? padding * index : 0), Position.Y - Button.Background.Bounds.Y - 15);

            Selected = false;
        }
        public Tab(string text, int index, TabManager parent, List<Option> options) {
            Text = text;
            Parent = parent;

            Size = new Point(Graphics.PreferredBackBufferWidth - Margin * 2, Graphics.PreferredBackBufferHeight - MarginTop - Margin * 2);
            Position = new Vector2(Margin, Margin + MarginTop);

            Image = Content.Load<Texture2D>("backgrounds/tabBackground");
            ImageBorder = Content.Load<Texture2D>("backgrounds/tabBorderBackground");

            Options = options;

            Color = Color.FromNonPremultiplied(220, 220, 220, 255);

            int padding = 20;

            //Button = new Button(text, new Vector2(Position.X, Position.Y), index, (s, e) => Select(), Color);
            Button = new Button(true, text, "square/medium", Vector2.Zero, (s, e) => Select(), "square/medium");
            Button.Position = new Vector2(Position.X + index * Button.Background.Bounds.X + (index > 0 ? padding * index : 0), Position.Y - Button.Background.Bounds.Y - 15);

            Selected = false;
        }

        public void Update(GameTime gt) {
            Button.Update(gt);

            Options.ForEach(x => x.Update(gt));
        }

        public void Draw(SpriteBatch sb) {
            Rectangle source = new Rectangle(Point.Zero, Size);

            sb.Draw(BlankPixel, Position, new Rectangle(Point.Zero, Size), Color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            //sb.Draw(Image, Position, null, new Rectangle(Point.Zero, Size), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            sb.Draw(Image, Position, source, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            sb.Draw(ImageBorder, Position, source, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            sb.Draw(ImageBorder, Position, new Rectangle(new Point(ImageBorder.Width - Size.X, ImageBorder.Height - Size.Y), Size), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);

            Options.ForEach(x => x.Draw(sb));
        }
        public void DrawButton(SpriteBatch sb) {
            Button.Draw(sb);
        }

        public void Select() {
            Parent.Tabs.ForEach(x => x.Selected = false);
            Selected = true;
            Parent.SelectedTab = this;
        }

        public int Margin {
            get { return 100; }
        }
        public int MarginTop {
            get { return 100; }
        }
        public bool Selected { get; set; }
        public string Text { get; set; }

        public List<Option> Options { get; set; }

        public Vector2 Position { get; set; }
        public Point Size { get; set; }

        public Texture2D Image { get; set; }
        public Texture2D ImageBorder { get; set; }

        public Button Button { get; set; }

        public TabManager Parent { get; set; }

        public Color Color { get; set; }
    }
}
