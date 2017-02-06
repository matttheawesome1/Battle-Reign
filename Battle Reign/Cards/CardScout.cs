using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_Reign {
    public class CardScout : Card {
        public CardScout(int padding, int spacing, List<Card> hand, Team team, World world, Save save) : base("SCOUT", "Discover the land quickly", "", "g75", padding, spacing, hand, world, save, team, new Point(2, SpritesheetSize.Y - DefaultSize.Y), DefaultSize, UnitColor, CardType.UNIT) {
            
        }

        public override void Update(GameTime gt) {


            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {


            base.Draw(sb);
        }
    }
}
