using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle_Reign {
    public class CardWorker : Card {
        public CardWorker(int padding, int spacing, List<Card> hand, Team team, World world) : base("WORKER", "Mine resources", "", "g100", padding, spacing, hand, world, team, new Point(), new Point(), UnitColor, CardType.UNIT) {
            
        }
    }
}
