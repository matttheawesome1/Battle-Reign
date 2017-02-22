using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Battle_Reign {
    public class Slider : Option {
        public Slider(string text, int width, float progress, Vector2 position) {
            Text = text;
            Font = Content.Load<SpriteFont>("fonts/fontSmall");

            ImageSlider = Content.Load<Texture2D>("slider/slider");
            ImageBackground = Content.Load<Texture2D>("slider/sliderBackground");
            ImageProgress = Content.Load<Texture2D>("slider/sliderProgress");

            Progress = progress;
            SliderSize = new Point(width, 5);

            Position = position;
            PositionSlider = new Vector2(position.X, position.Y + Font.MeasureString("A").Y * 2);
            SliderY = (int) (PositionSlider.Y - ImageSlider.Height / 2 + SliderSize.Y / 2);

            Hex = "";

            IsPressing = false;

            Size = new Point(SliderSize.X, (int) (PositionSlider.Y - Position.Y) + ImageSlider.Height);
            Console.WriteLine("Size: " + Size.ToString());

            Hitbox = new Rectangle(new Point((int) PositionSlider.X, SliderY), new Point(SliderSize.X, ImageSlider.Height));
        }

        public override void Update(GameTime gt) {
            int increase = 50;

            if (Mouse.Hitbox.Intersects(Hitbox) && Mouse.LeftMouseDown) {
                Progress = (Mouse.Position.X - Position.X) / SliderSize.X;

                Hitbox = new Rectangle(new Vector2(PositionSlider.X, SliderY - increase).ToPoint(), new Point(SliderSize.X, ImageSlider.Height + increase * 2));
            } else {
                Hitbox = new Rectangle(new Vector2((int) PositionSlider.X, SliderY).ToPoint(), new Point(SliderSize.X, ImageSlider.Height));
            }
            

            //Console.WriteLine(Hitbox.ToString());
            //Console.WriteLine("Hitbox X: " + Hitbox.X + " // Slider X: " + PositionSlider.X + " // " + Mouse.MouseDown);

            //Console.WriteLine("TEST: " + (Convert.ToInt32(Progress * 16777215f).ToString("X")));
            Hex = Convert.ToInt32(Math.Round(Progress, 2) * 16777215f).ToString("X");

            for (int i = 0; i < 6 - Hex.Length; i++) {
                Hex = "0" + Hex;
            }

            //Mouse.Color = Color.FromNonPremultiplied(int.Parse(Hex[0].ToString() + Hex[1].ToString(), System.Globalization.NumberStyles.HexNumber), int.Parse(Hex[2].ToString() + Hex[3].ToString(), System.Globalization.NumberStyles.HexNumber), int.Parse(Hex[4].ToString() + Hex[5].ToString(), System.Globalization.NumberStyles.HexNumber), 255);
         }

        public override void Draw(SpriteBatch sb) {
            Rectangle source = new Rectangle(0, 0, (int) (ImageProgress.Width * Progress), ImageProgress.Height);
            Vector2 origin = new Vector2(0, 0);

            sb.DrawString(Font, Text, Position, Color.White);
            //sb.DrawString(Font, Hex, new Vector2(Position.X, Position.Y + 100), Color.Black);
            
            sb.Draw(BlankPixel, PositionSlider, new Rectangle(Point.Zero, SliderSize), Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);
            sb.Draw(BlankPixel, PositionSlider, new Rectangle(Point.Zero, new Point((int) (SliderSize.X * Progress), SliderSize.Y)), Color.FromNonPremultiplied(136, 0, 0, 255), 0f, origin, 1f, SpriteEffects.None, 0f);
            sb.Draw(ImageSlider, new Vector2(PositionSlider.X + (Progress * SliderSize.X) - ImageSlider.Width / 2, SliderY), Color.FromNonPremultiplied(136, 0, 0, 255));
        }

        public string Text { get; set; }
        public string Hex { get; set; }

        public int SliderY { get; }

        public float Progress { get; set; }
        public bool IsPressing { get; set; }

        public Point SliderSize { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 PositionSlider { get; set; }

        public Rectangle Hitbox { get; set; }

        public Texture2D ImageSlider { get; set; }
        public Texture2D ImageBackground { get; set; }
        public Texture2D ImageProgress { get; set; }

        public SpriteFont Font { get; }
    }
}
