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
                new Button(false, "play/small", new Vector2(50), (s, e) => Action = Action.CHANGESTATE, "square/small"),
                new Button(false, "settings/small", new Vector2(150, 50), null, "square/small"),
                new Button(false, "exit/small", new Vector2(250, 50), (s, e) => Action = Action.EXIT, "square/small"),
            };
        }
        public void ChangeState(Action action, State state) {
            Action = action;
            State = state;            
        }

        public override void Update(GameTime gt) {
            Buttons.ForEach(x => x.Update(gt));

            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {
            Buttons.ForEach(x => x.Draw(sb));

            base.Draw(sb);
        }

        public List<Button> Buttons { get; set; }
    }
}