using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle_Reign {
    public class CardWarrior : Card {
        public CardWarrior(int padding, int spacing, List<Card> hand, Team team, World world) : base("WARRIOR", "Attack enemies", "", "g150", padding, spacing, hand, world, team, new Point(1, SpritesheetSize.Y - DefaultSize.Y), DefaultSize, UnitColor, CardType.UNIT) {

        }

        public override void Update(GameTime gt) {


            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {


            base.Draw(sb);
        }
    }
}