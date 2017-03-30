using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Battle_Reign {
    public class Option : GameObject {
        public Option() {

        }

        public virtual void Update(GameTime gt) {

        }

        public virtual void Draw(SpriteBatch sb) {

        }

        public int Width {
            get { return Size.X; }
        }
        public int Height {
            get { return Size.Y; }
        }

        public Point Size { get; set; }
    }
}
