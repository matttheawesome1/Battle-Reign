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
        public CardFarm(int tileWidth, int padding, List<Card> hand, Tile[,] tiles) : base("FARM", "Farm crops", "", "g100", tileWidth, padding, hand, tiles, DefaultCoords, DefaultSize) {

        }

        public override void Update(GameTime gt, bool hovering) {


            base.Update(gt, hovering);
        }

        public override void Draw(SpriteBatch sb) {


            base.Draw(sb);
        }
    }
}
