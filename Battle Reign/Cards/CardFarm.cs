using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Reign {
    public class CardFarm : Card {
        public CardFarm(int padding, int spacing, List<Card> hand, Team team, World world) : base("FARM", "Farm crops", "", "g100", padding, spacing, hand, world, team, new Point(0, SpritesheetSize.Y - DefaultSize.Y), DefaultSize, BuildingColor, CardType.BUILDING) {

        }

        public override void Update(GameTime gt) {


            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {


            base.Draw(sb);
        }
    }
}
