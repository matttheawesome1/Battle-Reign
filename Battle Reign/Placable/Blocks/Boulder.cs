using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Reign {
    public class Boulder : Block {
        public Boulder(Point coords) : base(50, Spritesheet, coords, new Point(21, Utilities.Next(0, 2) * 3), new Point(3, 3)) {
            Type = (BoulderType) Utilities.Next(0, Enum.GetNames(typeof(BoulderType)).Length);
            SpriteCoords = new Point(SpriteCoords.X, ((int) Type) * SpriteSize.Y);
        }

        public override void Update(GameTime gt) {


            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {


            base.Draw(sb);
        }

        public BoulderType Type { get; set; }
    }

    public enum BoulderType {
        NORMAL = 0,
        GOLD = 1,
        IRON = 2,
        COAL = 3,
        COPPER = 4
    }
}