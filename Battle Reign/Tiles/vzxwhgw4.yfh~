using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Battle_Reign {
    public class WaterTile : Tile {
        public WaterTile(Vector2 position, Tile[,] tiles) : base(Content.Load<Texture2D>("tiles/spritesheet"), position, tiles, new Point(9, 0), new Point(3)) {
            
        }

        public override void Update(GameTime gt) {

            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {
            //sb.Draw(Spritesheet, Position, Color.White);
            //sb.Draw(Spritesheet, new Rectangle(Position.ToPoint(), new Point(TileWidth)), null, Color.White);
            sb.Draw(Spritesheet, Position, new Rectangle(new Point(SpriteCoords.X * (Cell), SpriteCoords.Y * (Cell)), new Point((Cell) * SpriteSize.X, (Cell) * SpriteSize.Y)), Color.White, 0f, new Vector2(0), 1f, SpriteEffects.None, 1);

            base.Draw(sb);
        }
    }
}
