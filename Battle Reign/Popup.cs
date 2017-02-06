using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Battle_Reign {
    public class Popup : GameObject {
        public Popup(Color color, float scale, float waitTime) {
            Type = PopupType.FLAG;

            SpriteCoords = new Point((SpritesheetSize.X - 3), 0);
            SpriteSize = new Point(3 * Cell, 2 * Cell);

            Waiting = waitTime != 0;
            Finished = false;

            Time = 0;
            WaitTime = waitTime;
            Speed = 5;
            Scale = scale;
            Opacity = 255;

            Color = color;
        }

        public void Update(GameTime gt) {
            Time += (float) gt.ElapsedGameTime.TotalSeconds;

            if (Waiting) {
                if (Time > WaitTime) {
                    Waiting = false;
                    Time = 0;
                }
            } else {
                Opacity -= (256 - Opacity) * Speed * (float) gt.ElapsedGameTime.TotalSeconds;
            }

            if (Opacity < 0) {
                Opacity = 0;
                Finished = true;
            }
        }

        public void Draw(SpriteBatch sb) {
            switch (Type) {
                case PopupType.FLAG:
                    sb.Draw(Spritesheet, Camera.Position + new Vector2(Graphics.PreferredBackBufferWidth / 2 - (SpriteSize.X * Scale) / 2, Graphics.PreferredBackBufferHeight / 2 - (SpriteSize.Y * Scale) / 2 - Scale * 2), new Rectangle(new Point((SpriteCoords.X) * Cell, (SpriteCoords.Y + 8) * Cell), SpriteSize), Color.FromNonPremultiplied(255, 255, 255, (int) Opacity), 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
                    sb.Draw(Spritesheet, Camera.Position + new Vector2(Graphics.PreferredBackBufferWidth / 2 - (SpriteSize.X * Scale) / 2 + 5 * Scale, Graphics.PreferredBackBufferHeight / 2 - (SpriteSize.Y * Scale) / 2 - Scale * 2), new Rectangle(new Point((SpriteCoords.X) * Cell, (SpriteCoords.Y + 4) * Cell), SpriteSize), Color.FromNonPremultiplied(255, 255, 255, (int) Opacity), 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
                    sb.Draw(Spritesheet, Camera.Position + new Vector2(Graphics.PreferredBackBufferWidth / 2 - (SpriteSize.X * Scale) / 2 + 5 * Scale, Graphics.PreferredBackBufferHeight / 2 - (SpriteSize.Y * Scale) / 2), new Rectangle(new Point((SpriteCoords.X) * Cell, SpriteCoords.Y * Cell), SpriteSize), Color.FromNonPremultiplied(Color.R, Color.G, Color.B, (int) Opacity), 0, Vector2.Zero, Scale, SpriteEffects.None, 0);

                    break;
                default:
                    break;
            }
        }

        public string Text { get; set; }

        public float Scale { get; set; }
        public float Time { get; set; }
        public float WaitTime { get; set; }
        public float Opacity { get; set; }
        public float Speed { get; set; }

        public bool Waiting { get; set; }
        public bool Finished { get; set; }

        public Point SpriteCoords { get; set; }
        public Point SpriteSize { get; set; }

        public Color Color { get; set; }

        public PopupType Type { get; set; }
    }

    public enum PopupType {
        TEXT,
        IMAGE,
        TEXTANDIMAGE,
        FLAG
    }
}