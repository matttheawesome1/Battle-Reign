using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Battle_Reign {
    public class DirtTile : Tile {
        public DirtTile(Vector2 position, Tile[,] tiles) : base(Spritesheet, position, tiles, new Point(6, 0), new Point(3)) {
            Values = new TextureSpot[3, 3] { { TextureSpot.TOPLEFT, TextureSpot.TOPMID, TextureSpot.TOPRIGHT }, { TextureSpot.MIDLEFT, TextureSpot.MIDMID, TextureSpot.MIDRIGHT }, { TextureSpot.BOTLEFT, TextureSpot.BOTMID, TextureSpot.BOTRIGHT } };
        }

        public override void Update(GameTime gt) {
            base.Update(gt);
        }

        public override void Fix() {
            for (int i = X - 1; i < X + 1; i++) {
                for (int j = Y - 1; j < Y + 1; j++) {
                    if ((i != X && j != Y) && Tiles[i, j].GetType() == typeof(DirtTile)) {
                        Tiles[i, j] = new DirtTile(Tiles[i, j].Position, Tiles[i, j].Tiles);

                        DirtTile t = Tiles[i, j] as DirtTile;
                        t.Update(true);

                        Tiles[i, j] = t;
                    }
                }
            }
        }

        public void Update(bool a) {
            if (Tiles[X - 1, Y].GetType() == typeof(DirtTile)) {
                Values[0, 1] = TextureSpot.MIDMID;
            }
            if (Tiles[X + 1, Y].GetType() == typeof(DirtTile)) {
                Values[2, 1] = TextureSpot.MIDMID;
            }

            if (Tiles[X, Y - 1].GetType() == typeof(DirtTile)) {
                Values[1, 0] = TextureSpot.MIDMID;
            }
            if (Tiles[X, Y + 1].GetType() == typeof(DirtTile)) {
                Values[1, 2] = TextureSpot.MIDMID;
            }
            
            
            //LEFT
            if (Tiles[X - 1, Y].GetType() == typeof(DirtTile)) {
                //TOP
                if (Tiles[X, Y - 1].GetType() == typeof(DirtTile)) {
                    Values[0, 0] = TextureSpot.MIDMID;
                }

                //BOTTOM
                if (Tiles[X, Y + 1].GetType() == typeof(DirtTile)) {
                    Values[0, 2] = TextureSpot.MIDMID;
                }
            }
            //RIGHT
            if (Tiles[X + 1, Y].GetType() == typeof(DirtTile)) {
                //TOP
                if (Tiles[X, Y - 1].GetType() == typeof(DirtTile)) {
                    Values[2, 0] = TextureSpot.MIDMID;
                } else {
                    Values[2, 0] = TextureSpot.MIDLEFT;
                }

                //BOTTOM
                if (Tiles[X, Y + 1].GetType() == typeof(DirtTile)) {
                    Values[2, 2] = TextureSpot.MIDMID;
                } else {
                    Values[2, 2] = TextureSpot.MIDRIGHT;
                }
            } else {
                if (Tiles[X, Y + 1].GetType() == typeof(DirtTile)) {
                    Values[2, 2] = TextureSpot.BOTMID;
                }
            }

            //TOP
            if (Tiles[X, Y - 1].GetType() == typeof(DirtTile)) {
                //LEFT
                if (Tiles[X - 1, Y].GetType() != typeof(DirtTile)) {
                    Values[0, 0] = TextureSpot.TOPMID;
                }

                if (Tiles[X + 1, Y].GetType() != typeof(DirtTile)) {
                    Values[2, 0] = TextureSpot.BOTMID;
                }
            } else {
                if (Tiles[X + 1, Y].GetType() == typeof(DirtTile)) {
                    Values[2, 0] = TextureSpot.MIDLEFT;
                }
                if (Tiles[X - 1, Y].GetType() == typeof(DirtTile)) {
                    Values[0, 0] = TextureSpot.MIDLEFT;
                }
            }

            //BOTTOM
            if (Tiles[X, Y + 1].GetType() == typeof(DirtTile)) {
                if (Tiles[X - 1, Y].GetType() != typeof(DirtTile)) {
                    Values[0, 2] = TextureSpot.TOPMID;
                }
            } else {
                if (Tiles[X + 1, Y].GetType() == typeof(DirtTile)) {
                    Values[2, 2] = TextureSpot.MIDRIGHT;
                }
                if (Tiles[X - 1, Y].GetType() == typeof(DirtTile)) {
                    Values[0, 2] = TextureSpot.MIDRIGHT;
                }
            }
            
        }

        public override void Draw(SpriteBatch sb) {
            for (int i = 0; i < Values.GetLength(0); i++) {
                for (int j = 0; j < Values.GetLength(1); j++) {
                    sb.Draw(Spritesheet, new Vector2(Position.X + (i + 1) * (TileWidth * .33f), Position.Y + (j + 1) * (TileWidth * .33f)),
                        new Rectangle(new Vector2((TileWidth / 3 * SpriteCoords.X) + ((TileWidth * .33f) * (float) Math.Floor((float) Values[i, j] / 3)), (TileWidth / 3 * SpriteCoords.Y) + ((TileWidth * .33f) * (float) Math.Floor((float) Values[i, j] % 3))).ToPoint(),
                        new Vector2(TileWidth / 3).ToPoint()), Color.White, 0f, new Vector2(TileWidth / 3), 1f, SpriteEffects.None, TileLayer);
                }
            }

            base.Draw(sb);
        }

        public TextureSpot[,] Values { get; set; }
    }
}