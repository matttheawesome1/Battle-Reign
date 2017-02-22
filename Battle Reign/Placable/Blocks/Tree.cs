 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Reign {
    public class Tree : Block {
        public Tree(Point coords) : base(100, Spritesheet, coords, new Point(18, 0), new Point(3, 4)) {

        }

        public override void Update(GameTime gt) {


            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {


            base.Draw(sb);
        }
    }
}
