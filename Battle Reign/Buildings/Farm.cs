using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Reign {
    public class Farm : Building {
        public Farm(Point coords) : base(Spritesheet, coords, new Point(SpritesheetSize.X - 9, 6), new Point(3, 4), new Point(3), true) {

        }

        public override void Update(GameTime gt) {
            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {
            base.Draw(sb);
        }
    }
}
