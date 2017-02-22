using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Battle_Reign {
    public class RadioGroup : GameObject {
        public RadioGroup(string text, bool outline, Vector2 position) {
            RadioButtons = new List<RadioButton>();

            Position = position;

            Text = text;
            Outline = outline;
        }
        public RadioGroup(string text, bool outline, Vector2 position, List<RadioButton> radioButtons) {
            RadioButtons = new List<RadioButton>();

            Position = position;

            Text = text;
            Outline = outline;

            foreach(RadioButton r in radioButtons) {
                r.Position = new Vector2(Position.X, Position.Y + (RadioButtons.Count + 1) * FontSmall.MeasureString("M").Y + 7);
                r.Parent = this;

                RadioButtons.Add(r);
            }
        }

        public void Update(GameTime gt) {
            RadioButtons.ForEach(x => x.Update(gt));
        }

        public void Draw(SpriteBatch sb) {
            sb.DrawString(FontSmall, Text, Position, Color.FromNonPremultiplied(0, 151, 236, 255));

            for (int i = 0; i < RadioButtons.Count; i++) {
                RadioButtons[i].Draw(sb);
            }

            Console.WriteLine(Checked.Text);
        }

        public void Add(RadioButton r) {
            r.Position = new Vector2(Position.X, Position.Y + (RadioButtons.Count + 1) * FontSmall.MeasureString("M").Y + 7);
            Console.WriteLine(r.Position.ToString());
            RadioButtons.Add(r);
        }

        public string Text { get; set; }

        public int MinWidth { get; set; }
        public int MinHeight { get; set; }

        public bool Outline { get; set; }

        public List<RadioButton> RadioButtons { get; set; }

        public RadioButton Checked {
            get {
                foreach (RadioButton r in RadioButtons) {
                    try {
                        if (r.Checked)
                            return r;
                    } catch (Exception) { } 
                }

                return new RadioButton("Unchecked", null);
            }
        }

        public Vector2 Position { get; set; }
    }
}
