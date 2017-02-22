using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Reign {
    public class Quarry : Building {
        public Quarry(Point coords) : base(50, Spritesheet, coords, new Point(SpritesheetSize.X - 9, 0), new Point(3), new Point(3), false) {

        }

        public override void Update(GameTime gt) {
            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {
            base.Draw(sb);
        }
    }
}
