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
            Buttons = new List<Button>() {
                new Button(false, "play/small", new Vector2(Graphics.PreferredBackBufferWidth * .25f - (int) ButtonSize.Small / 2, Graphics.PreferredBackBufferHeight / 2), (s, e) => Action = Action.CHANGESTATE, "square/small"),
                new Button(false, "settings/small", new Vector2(Graphics.PreferredBackBufferWidth * .5f - (int) ButtonSize.Small / 2, Graphics.PreferredBackBufferHeight / 2), null, "square/small"),
                new Button(false, "exit/small", new Vector2(Graphics.PreferredBackBufferWidth * .75f - (int) ButtonSize.Small / 2, Graphics.PreferredBackBufferHeight / 2), (s, e) => Action = Action.EXIT, "square/small"),
            };

            Background = Content.Load<Texture2D>("backgrounds/mainmenu");
        }

        public override void Update(GameTime gt) {
            Buttons.ForEach(x => x.Update(gt));

            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {
            sb.Draw(Background, new Vector2(Graphics.PreferredBackBufferWidth / 2 - Background.Width / 2, Graphics.PreferredBackBufferHeight / 2 - Background.Height / 2), Color.White);
            sb.DrawString(Font90, "Battle Reign", new Vector2((Graphics.PreferredBackBufferWidth / 2) - Font90.MeasureString("Battle Reign").X / 2, (Graphics.PreferredBackBufferHeight / 2) - 250), Color.White);
            Buttons.ForEach(x => x.Draw(sb));
        }

        public List<Button> Buttons { get; set; }
        public Texture2D Background { get; set; }
    }
    public enum ButtonSize
    {
        Tiny = 50,
        Small = 80,
        Medium = 160,
        Large = 240,
    }
}