using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle_Reign {
    public abstract class Scene : GameObject {
        public Scene(string name) {
            Name = name;
        }

        public virtual void Update(GameTime gt) {
            ClickObject();
        }

        public virtual void Draw(SpriteBatch sb) {

        }

        public string Name { get; set; }

        public Action Action { get; set; }
    }

    public enum Action {
        IDLE,
        CHANGESTATE,
        EXIT
    }
}
