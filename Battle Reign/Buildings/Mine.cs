using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Reign {
    public class Mine : Building {
        public Mine(Point coords) : base(Spritesheet, coords, new Point(SpritesheetSize.X - 9, 3), new Point(3), new Point(3), false) {

        }

        public override void Update(GameTime gt) {
            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {
            base.Draw(sb);
        }
    }
}
