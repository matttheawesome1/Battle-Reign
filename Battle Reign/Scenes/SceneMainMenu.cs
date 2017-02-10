using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Battle_Reign {
    public class SceneMainMenu : Scene {
        public SceneMainMenu() : base("Main Menu") {
            int padding = 70, spacing = 25;

            Background = Content.Load<Texture2D>("backgrounds/mainmenu");

            Buttons = new List<Button>() {
                new Button(false, "play/small", new Vector2(Graphics.PreferredBackBufferWidth * .25f, Graphics.PreferredBackBufferHeight /2), (s, e) => Action = Action.CHANGESTATE, "square/small"),
                new Button(false, "settings/small", new Vector2(Graphics.PreferredBackBufferWidth / 2, Graphics.PreferredBackBufferHeight / 2), null, "square/small"),
                new Button(false, "exit/small", new Vector2(Graphics.PreferredBackBufferWidth * .75f, Graphics.PreferredBackBufferHeight / 2), (s, e) => Action = Action.EXIT, "square/small"),
            };

            foreach(Button b in Buttons)
            {
                b.Position -= new Vector2(b.Background.Width / 2);
            }
        }

        public override void Update(GameTime gt) {
            Buttons.ForEach(x => x.Update(gt));

            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {
            sb.Draw(Background, Vector2.Zero, Color.White);
            sb.DrawString(FontXtraLarge, "Battle Reign", new Vector2(Graphics.PreferredBackBufferWidth / 2 - FontLarge.MeasureString("Battle Reign").X / 2, 100), Color.White);

            Buttons.ForEach(x => x.Draw(sb));

            base.Draw(sb);
        }

        public Texture2D Background { get; set; }
        public List<Button> Buttons { get; set; }
    }
}