using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Battle_Reign {
    public class SceneGame : Scene { 
        public SceneGame() : base("Game") {
            Save = new Save("Nick", new Point(40), this);
            Camera.Zoom = 1f;
        }

        public override void Update(GameTime gt) {
            Save.Update(gt);

            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {
            Save.Draw(sb);

            base.Draw(sb);
        }

        public Save Save { get; set; }
    }
}
