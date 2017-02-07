using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Reign {
    public class CardQuarry : Card {
        public CardQuarry(int padding, int spacing, List<Card> hand, Team team, World world) : base("QUARRY", "Mines for stone", "", "g150", padding, spacing, hand, world, team, new Point(0, SpritesheetSize.Y - DefaultSize.Y), DefaultSize, BuildingColor, CardType.BUILDING) {

        }

        public override void Update(GameTime gt) {


            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {


            base.Draw(sb);
        }
    }
}
