using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle_Reign {
    public abstract class Scene : GameObject {
        public Scene(string name) {
            Name = name;

            State = State.FORWARD;
        }

        public virtual void Update(GameTime gt) {
            ClickObject();
        }

        public virtual void Draw(SpriteBatch sb) {

        }

        public string Name { get; set; }

        public Action Action { get; set; }
        public State State { get; set; }
    }

    public enum Action {
        IDLE,
        CHANGESTATE,
        EXIT
    }
    public enum State {
        MAINMENU,
        GAME,
        SETTINGS,
        BACK,
        FORWARD
    }
}
