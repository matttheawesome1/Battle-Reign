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
            sb.Draw(Spritesheet, Position, new Rectangle(new Point(SpriteCoords.X * (TileWidth / 3), SpriteCoords.Y * (TileWidth / 3)), new Point((TileWidth / 3) * SpriteSize.X, (TileWidth / 3) * SpriteSize.Y)), Color.White, 0f, new Vector2(0), 1f, SpriteEffects.None, 0f);

            base.Draw(sb);
        }
    }
}
