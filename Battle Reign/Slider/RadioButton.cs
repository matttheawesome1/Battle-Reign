using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Battle_Reign {
    public class RadioButton : Option {
        public RadioButton(string text, Vector2 position) {
            Position = position;
            Text = text;

            Checked = false;
            CanCheck = true;

            ImageUnchecked = Content.Load<Texture2D>("buttons/radio/unchecked");
            ImageChecked = Content.Load<Texture2D>("buttons/radio/checked");

            Hitbox = new Rectangle(position.ToPoint(), ImageChecked.Bounds.Size);
        }
        public RadioButton(string text, RadioGroup parent) {
            Parent = parent;
            Text = text;

            ApartOfGroup = true;
            Checked = false;
            CanCheck = true;

            ImageUnchecked = Content.Load<Texture2D>("buttons/radio/unchecked");
            ImageChecked = Content.Load<Texture2D>("buttons/radio/checked");
        }

        public override void Update(GameTime gt) {
            if (Mouse.Hitbox.Intersects(Hitbox)) {
                Mouse.Hovering = true;

                if (Mouse.LeftMouseDown && CanCheck) {
                    Checked = true;
                    CanCheck = false;

                    if (ApartOfGroup && Checked) {
                        Parent.RadioButtons.ForEach(x => x.Checked = x != this ? false : true);
                    }
                } else if (!Mouse.LeftMouseDown)
                    CanCheck = true;
            }

            Hitbox = new Rectangle(Position.ToPoint(), ImageChecked.Bounds.Size);
        }

        public override void Draw(SpriteBatch sb) {
            sb.Draw(Checked ? ImageChecked : ImageUnchecked, Position, Color.White);
            sb.DrawString(FontSmall, Text, new Vector2(Position.X + ImageChecked.Width + 4, Position.Y), Color.White);
        }

        public string Text { get; set; }

        public bool Checked { get; set; }
        public bool CanCheck { get; set; }
        public bool ApartOfGroup { get; set; }

        public RadioGroup Parent { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Hitbox { get; set; }

        public Texture2D ImageUnchecked { get; set; }
        public Texture2D ImageChecked { get; set; }
    }
}
