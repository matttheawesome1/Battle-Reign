using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Battle_Reign {
    public class Button : GameObject {
        event Click OnClick;
        public delegate void Click(object sender, EventArgs e);
        EventArgs e;

        EventHandler ev;
        
        /// <summary>
        /// Creates a button
        /// </summary>
        /// <param name="stat">Static or not</param>
        /// <param name="text">Button text</param>
        /// <param name="fontPath">Font path</param>
        /// <param name="borderWidth">Border width</param>
        /// <param name="position">Position on the screen</param>
        /// <param name="size">Size of the button</param>
        /// <param name="_ev">Event when clicked USE THIS: (s, e) => //EVENT//</param>
        /// <param name="primary">Primary color</param>
        /// <param name="secondary">Secondary color</param>
        public Button(bool stat, string text, string fontPath, int borderWidth, Vector2 position, Point size, EventHandler _ev, Nullable<Color> primary, Nullable<Color> secondary) {
            Static = stat;

            Text = text;
            Position = position;
            HoverPosition = new Vector2(position.X, position.Y - 25);
            OriginalPosition = position;
            Size = size;

            BorderWidth = borderWidth;

            Hovering = false;

            Type = ButtonType.TEXT;

            e = new EventArgs();
            ev = _ev;

            PrimaryColor = primary == null ? Color.Black : (Color) primary;
            SecondaryColor = secondary == null ? Color.White : (Color) secondary;

            Font = Content.Load<SpriteFont>("fonts/" + fontPath);

            Hitbox = new Rectangle(Position.ToPoint(), Size);
        }

        /// <summary>
        /// Creates a button
        /// </summary>
        /// <param name="stat">Static or not</param>
        /// <param name="iconPath">Path for the icon</param>
        /// <param name="borderWidth">Border width</param>
        /// <param name="position">Position on the screen</param>
        /// <param name="size">Size of the button</param>
        /// <param name="_ev">Event when clicked USE THIS: (s, e) => //EVENT//</param>
        /// <param name="primary">Primary color</param>
        /// <param name="secondary">Secondary color</param>
        public Button(bool stat, string iconPath, int borderWidth, Vector2 position, Point size, EventHandler _ev, Nullable<Color> primary, Nullable<Color> secondary) {
            Static = stat;

            Position = position;
            HoverPosition = new Vector2(position.X, position.Y - 25);
            OriginalPosition = position;
            Size = size;

            BorderWidth = borderWidth;

            Hovering = false;

            Type = ButtonType.ICON;

            Icon = Content.Load<Texture2D>("buttons/icons/" + iconPath);

            e = new EventArgs();
            ev = _ev;

            PrimaryColor = primary == null ? Color.Black : (Color) primary;
            SecondaryColor = secondary == null ? Color.White : (Color) secondary;

            Hitbox = new Rectangle(Position.ToPoint(), Size);
        }     
        public Button(bool stat, string iconPath, string text, string fontPath, int borderWidth, Vector2 position, Point size, EventHandler _ev, Nullable<Color> primary, Nullable<Color> secondary) {
            Static = stat;

            Text = text;
            Position = position;
            HoverPosition = new Vector2(position.X, position.Y - 25);
            OriginalPosition = position;
            Size = size;

            BorderWidth = borderWidth;

            Hovering = false;

            Type = ButtonType.BOTH;

            Icon = Content.Load<Texture2D>("buttons/icons/" + iconPath);

            e = new EventArgs();
            ev = _ev;

            PrimaryColor = primary == null ? Color.Black : (Color) primary;
            SecondaryColor = secondary == null ? Color.White : (Color) secondary;

            Font = Content.Load<SpriteFont>("fonts/" + fontPath);

            Hitbox = new Rectangle(Position.ToPoint(), Size);
        }
        /// <summary>
        /// Creates a button
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="text"></param>
        /// <param name="fontPath"></param>
        /// <param name="position"></param>
        /// <param name="_ev"></param>
        /// <param name="backgroundPath"></param>
        public Button(bool stat, string text, string fontPath, Vector2 position, EventHandler _ev, string backgroundPath) {
            Static = stat;

            Text = text;
            Position = position;
            HoverPosition = new Vector2(position.X, position.Y - 25);
            OriginalPosition = position;

            Hovering = false;

            Type = ButtonType.TEXT;

            e = new EventArgs();
            ev = _ev;

            PrimaryColor = Color.Black;
            SecondaryColor = Color.White;

            Font = Content.Load<SpriteFont>("fonts/" + fontPath);
            
            Background = Content.Load<Texture2D>("buttons/" + backgroundPath);
            Size = Background.Bounds.Size;

            Hitbox = new Rectangle(Position.ToPoint(), Size);
        }
        /// <summary>
        /// Creates a button
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="iconPath"></param>
        /// <param name="position"></param>
        /// <param name="_ev"></param>
        /// <param name="backgroundPath"></param>
        public Button(bool stat, string iconPath, Vector2 position, EventHandler _ev, string backgroundPath) {
            Static = stat;

            Position = position;
            HoverPosition = new Vector2(position.X, position.Y - 25);
            OriginalPosition = position;

            Hovering = false;

            Type = ButtonType.ICON;

            Icon = Content.Load<Texture2D>("buttons/icons/" + iconPath);

            e = new EventArgs();
            ev = _ev;

            PrimaryColor = Color.Black;
            SecondaryColor = Color.White;

            Background = Content.Load<Texture2D>("buttons/" + backgroundPath);
            Size = Background.Bounds.Size;

            Hitbox = new Rectangle(Position.ToPoint(), Size);
        }
        /// <summary>
        /// Creates a button
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="iconPath"></param>
        /// <param name="text"></param>
        /// <param name="fontPath"></param>
        /// <param name="position"></param>
        /// <param name="_ev"></param>
        /// <param name="backgroundPath"></param>
        public Button(bool stat, string iconPath, string text, string fontPath, Vector2 position, EventHandler _ev, string backgroundPath) {
            Static = stat;

            Text = text;
            Position = position;
            HoverPosition = new Vector2(position.X, position.Y - 25);
            OriginalPosition = position;

            Hovering = false;

            Type = ButtonType.BOTH;

            Icon = Content.Load<Texture2D>("buttons/icons/" + iconPath);

            e = new EventArgs();
            ev = _ev;

            PrimaryColor = Color.Black;
            SecondaryColor = Color.White;

            Font = Content.Load<SpriteFont>("fonts/" + fontPath);

            Background = Content.Load<Texture2D>("buttons/" + backgroundPath);
            Size = Background.Bounds.Size;

            Hitbox = new Rectangle(Position.ToPoint(), Size);
        }

        /// <summary>
        /// Updates the buttons position.
        /// </summary>
        /// <param name="gt"></param>
        public void Update(GameTime gt) {
            Hitbox = new Rectangle(Position.ToPoint(), Size);
            MouseState state = Mouse.GetState();

            Hovering = Hitbox.Intersects(Mouse.Hitbox);

            if (Hovering) {
                Mouse.Hovering = true;

                if (Mouse.CanPress && state.LeftButton == ButtonState.Pressed) {
                    Clicked = true;
                    Mouse.CanPress = false;
                }
            }

            if (!Static) {
                if (Hovering) {
                    Mouse.Hovering = true;

                    if (Mouse.CanPress && state.LeftButton == ButtonState.Pressed) {
                        Clicked = true;
                        Mouse.CanPress = false;
                    }

                    Position -= new Vector2(0, (Position.Y - HoverPosition.Y) * 3f * (float) gt.ElapsedGameTime.TotalSeconds);
                } else {
                    Position += new Vector2(0, (OriginalPosition.Y - Position.Y) * 3f * (float) gt.ElapsedGameTime.TotalSeconds);
                }
            } else {
                if (Hovering) {
                    Mouse.Hovering = true;

                    if (Mouse.CanPress && state.LeftButton == ButtonState.Pressed) {
                        Clicked = true;
                        Mouse.CanPress = false;
                    }
                }
            }

            if (Clicked && ev != null && Hovering && state.LeftButton == ButtonState.Released) {
                ev(this, e);
                Clicked = false;
            }

            if (Clicked && !Hovering && state.LeftButton == ButtonState.Released) {
                Clicked = false;
            }
        }

        /// <summary>
        /// Draws the button to screen.
        /// </summary>
        /// <param name="sb"></param>
        public void Draw(SpriteBatch sb) {
            if (!HasBackground) {
                sb.Draw(BlankPixel, new Rectangle(Position.ToPoint(), Size), Hovering ? SecondaryColor : PrimaryColor);
                sb.Draw(BlankPixel, new Rectangle(new Vector2(Position.X + BorderWidth, Position.Y + BorderWidth).ToPoint(), new Point(Size.X - BorderWidth * 2, Size.Y - BorderWidth * 2)), Hovering ? PrimaryColor : SecondaryColor);
            }

            switch (Type) {
                case ButtonType.TEXT:
                    if (HasBackground)
                        sb.Draw(Background, Position, Color.White);

                    sb.DrawString(Font, Text, new Vector2(Position.X + Size.X / 2 - Font.MeasureString(Text).X / 2, Position.Y + Size.Y / 2 - Font.MeasureString(Text).Y / 2 + 2), HasBackground ? Color.White : (Hovering ? SecondaryColor : PrimaryColor));

                    break;
                case ButtonType.ICON:
                    if (HasBackground)
                        sb.Draw(Background, Position, Color.White);

                    sb.Draw(Icon, new Vector2(Position.X + Size.X / 2 - Icon.Width / 2, Position.Y + Size.Y / 2 - Icon.Height / 2), HasBackground ? Color.White : (Hovering ? SecondaryColor : PrimaryColor));

                    break;
                case ButtonType.BOTH:
                    if (HasBackground)
                        sb.Draw(Background, Position, Color.White);

                    int spacing = 10, height = (int) Font.MeasureString(Text).Y + Icon.Height;

                    sb.Draw(Icon, new Vector2(Position.X + Size.X / 2 - Icon.Width / 2, Position.Y + Size.Y / 2 - Icon.Height / 2 - spacing), HasBackground ? Color.White : (Hovering ? SecondaryColor : PrimaryColor));
                    sb.DrawString(Font, Text, new Vector2(Position.X + Size.X / 2 - Font.MeasureString(Text).X / 2, Position.Y + Size.Y / 2 - 2 + spacing), HasBackground ? Color.White : (Hovering ? SecondaryColor : PrimaryColor));

                    break;
                default:
                    break;
            }
        }

        public int BorderWidth { get; set; }

        public bool Static { get; set; }
        public bool Clicked { get; set; }
        public bool Hovering { get; set; }
        public bool HasText { get; set; }
        public bool HasBackground {
            get { return Background != null; }
        }

        public string Text { get; set; }

        public Texture2D Background { get; set; }
        public Texture2D Icon { get; set; }
        public Texture2D IconHover { get; set; }

        public Point Size { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 HoverPosition { get; set; }
        public Vector2 OriginalPosition { get; set; }
        public Rectangle Hitbox { get; set; }

        public SpriteFont Font { get; set; }

        public Color PrimaryColor { get; set; }
        public Color SecondaryColor { get; set; }

        public ButtonType Type { get; set; }
    }

    public enum ButtonType {
        TEXT,
        ICON,
        BOTH
    }
}