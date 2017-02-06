using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Battle_Reign {
    public class GameMouse : GameObject {
        Color color;
        
        float scale = 1f;

        public GameMouse(ContentManager c) {
            Enabled = true;
            CanPress = true;

            ImageNormal = c.Load<Texture2D>("mouse/normal");
            ImageClick = c.Load<Texture2D>("mouse/click");

            int r = Utilities.Next(0, 255), g = Utilities.Next(r / 2, 255 - r / 2), b = Utilities.Next(g / 2, 255 - g / 2);

            //color = Color.FromNonPremultiplied(r, g, b, 255);
            color = Color.White;

            Image = ImageNormal;

            MouseState state = GetState();
            Position = Enabled ? state.Position.ToVector2() : Position;

            Size = new Point(1);
            Hitbox = new Rectangle(Position.ToPoint(), Size);
        }

        public void Update() {
            MouseState state = GetState();

            CanPress = !LeftMouseDown;

            Position = Enabled ? new Vector2(state.Position.X + Camera.Position.X, state.Position.Y + Camera.Position.Y) : Position;
            Hitbox = new Rectangle(Position.ToPoint(), Size);

            LeftMouseDown = state.LeftButton == ButtonState.Pressed;
            RightMouseDown = state.RightButton == ButtonState.Pressed;

            Image = LeftMouseDown ? ImageClick : ImageNormal;
        }

        public void Draw(SpriteBatch sb) {
            sb.Draw(Image, Position, color);
        }

        public MouseState GetState() {
            return Microsoft.Xna.Framework.Input.Mouse.GetState();
        }

        public bool Hovering { get; set; }
        public bool Enabled { get; set; }
        public bool LeftMouseDown { get; set; }
        public bool RightMouseDown { get; set; }

        public bool CanPress { get; set; }

        public Texture2D Image { get; set; }
        public Texture2D ImageNormal { get; set; }
        public Texture2D ImageClick { get; set; }

        public Vector2 Position { get; set; }

        public Point Size { get; set; }

        public Rectangle Hitbox { get; set; }
    }
}
