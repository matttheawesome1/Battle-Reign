using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Reign {
    public class Bush : Block {
        public Bush (Point coords) : base(Spritesheet, coords, new Point(24, Utilities.Next(0, 2) * 3), new Point(3, 3)) {

        }

        public override void Update(GameTime gt) {


            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {


            base.Draw(sb);
        }
    }
}
