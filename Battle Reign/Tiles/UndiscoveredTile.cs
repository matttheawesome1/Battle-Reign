using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Battle_Reign {
    public class UndiscoveredTile : Tile {
        public UndiscoveredTile(bool stat, Vector2 position, Tile[,] tiles) : base(Spritesheet, position, tiles, new Point(SpritesheetSize.X - 3, SpritesheetSize.Y - 3), new Point(3)) {
            Static = stat;

            //if (Static)
                Values = new TextureSpot[3, 3] { { TextureSpot.MIDMID, TextureSpot.MIDMID, TextureSpot.MIDMID }, { TextureSpot.MIDMID, TextureSpot.MIDMID, TextureSpot.MIDMID }, { TextureSpot.MIDMID, TextureSpot.MIDMID, TextureSpot.MIDMID } };
            //else
               // Values = new TextureSpot[3, 3] { { TextureSpot.TOPLEFT, TextureSpot.TOPMID, TextureSpot.TOPRIGHT }, { TextureSpot.MIDLEFT, TextureSpot.MIDMID, TextureSpot.MIDRIGHT }, { TextureSpot.BOTLEFT, TextureSpot.BOTMID, TextureSpot.BOTRIGHT } };
        }

        public override void Update(GameTime gt) {
            if (Mouse.Hitbox.Intersects(Hitbox) && Mouse.LeftMouseDown) {
                Discover(false);
            }

            base.Update(gt);
        }

        public override void Fix() {
            
        }

        public override void Draw(SpriteBatch sb) {
            if (!Discovered) {
                for (int i = 0; i < Values.GetLength(0); i++) {
                    for (int j = 0; j < Values.GetLength(1); j++) {
                        sb.Draw(Spritesheet, new Vector2(Position.X + (i + 1) * (TileWidth * .33f), Position.Y + (j + 1) * (TileWidth * .33f)),
                            new Rectangle(new Vector2((TileWidth / 3 * SpriteCoords.X) + ((TileWidth * .33f) * (float) Math.Floor((float) Values[i, j] / 3)), (TileWidth / 3 * SpriteCoords.Y) + ((TileWidth * .33f) * (float) Math.Floor((float) Values[i, j] % 3))).ToPoint(),
                            new Vector2(TileWidth / 3).ToPoint()), Color.White, 0f, new Vector2(TileWidth / 3), 1f, SpriteEffects.None, 0f);
                    }
                }
            }

            //base.Draw(sb);
        }

        public bool Static { get; set; }

        public TextureSpot[,] Values { get; set; }
    }
}