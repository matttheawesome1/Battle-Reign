using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Battle_Reign {
    public class GrassTile : Tile {
        public GrassTile(Vector2 position, Tile[,] tiles) : base(Content.Load<Texture2D>("tiles/spritesheet"), position, tiles, Point.Zero, new Point(3, 3)) {
            Values = new TextureSpot[3, 3] { { TextureSpot.TOPLEFT, TextureSpot.TOPMID, TextureSpot.TOPRIGHT }, { TextureSpot.MIDLEFT, TextureSpot.MIDMID, TextureSpot.MIDRIGHT }, { TextureSpot.BOTLEFT, TextureSpot.BOTMID, TextureSpot.BOTRIGHT} };
        }

        public override void Update(GameTime gt) {


            base.Update(gt);
        }

        public void Update (bool a) {
            //LEFT
            if (Tiles[X - 1, Y].GetType() != typeof(WaterTile)) {
                //TOP
                if (Tiles[X, Y - 1].GetType() != typeof(WaterTile)) {
                    if (Tiles[X - 1, Y - 1].GetType() == typeof(WaterTile))
                        Values[0, 0] = TextureSpot.MIDTL;
                    else Values[0, 0] = TextureSpot.MIDMID;
                }

                //BOTTOM
                if (Tiles[X, Y + 1].GetType() != typeof(WaterTile)) {
                    if (Tiles[X - 1, Y + 1].GetType() == typeof(WaterTile))
                        Values[0, 2] = TextureSpot.MIDBL;
                    else
                        Values[0, 2] = TextureSpot.MIDMID;
                }
            }
            //RIGHT
            if (Tiles[X + 1, Y].GetType() != typeof(WaterTile)) {
                //TOP
                if (Tiles[X, Y - 1].GetType() != typeof(WaterTile)) {
                    if (Tiles[X + 1, Y - 1].GetType() == typeof(WaterTile))
                        Values[2, 0] = TextureSpot.MIDTR;
                    else
                        Values[2, 0] = TextureSpot.MIDMID;
                } else {
                    Values[2, 0] = TextureSpot.MIDLEFT;
                }

                //BOTTOM
                if (Tiles[X, Y + 1].GetType() != typeof(WaterTile)) {
                    if (Tiles[X + 1, Y + 1].GetType() == typeof(WaterTile))
                        Values[2, 2] = TextureSpot.MIDBR;
                    else
                        Values[2, 2] = TextureSpot.MIDMID;
                } else {
                    Values[2, 2] = TextureSpot.MIDRIGHT;
                }
            } else {
                if (Tiles[X, Y + 1].GetType() != typeof(WaterTile)) {
                    Values[2, 2] = TextureSpot.BOTMID;
                }
            }

            //TOP
            if (Tiles[X, Y - 1].GetType() != typeof(WaterTile)) {
                //LEFT
                if (Tiles[X - 1, Y].GetType() != typeof(GrassTile)) {
                    Values[0, 0] = TextureSpot.TOPMID;
                }

                if (Tiles[X + 1, Y].GetType() != typeof(GrassTile)) {
                    Values[2, 0] = TextureSpot.BOTMID;
                }
            } else {
                if (Tiles[X + 1, Y].GetType() != typeof(WaterTile)) {
                    Values[2, 0] = TextureSpot.MIDLEFT;
                }
                if (Tiles[X - 1, Y].GetType() != typeof(WaterTile)) {
                    Values[0, 0] = TextureSpot.MIDLEFT;
                }
            }

            //BOTTOM
            if (Tiles[X, Y + 1].GetType() != typeof(WaterTile)) {
                if (Tiles[X - 1, Y].GetType() != typeof(GrassTile)) {
                    Values[0, 2] = TextureSpot.TOPMID;
                }
            } else {
                if (Tiles[X + 1, Y].GetType() != typeof(WaterTile)) {
                    Values[2, 2] = TextureSpot.MIDRIGHT;
                }
                if (Tiles[X - 1, Y].GetType() != typeof(WaterTile)) {
                    Values[0, 2] = TextureSpot.MIDRIGHT;
                }
            }

            if (Tiles[X - 1, Y].GetType() != typeof(WaterTile)) {
                if (Tiles[X - 1, Y].GetType() == typeof(DirtTile)) {
                    Values[0, 0] = TextureSpot.MIDMID;
                    Values[0, 1] = TextureSpot.MIDMID;
                    Values[0, 2] = TextureSpot.MIDMID;
                } else {
                    Values[0, 1] = TextureSpot.MIDMID;
                }
            }
            if (Tiles[X + 1, Y].GetType() != typeof(WaterTile)) {
                if (Tiles[X + 1, Y].GetType() == typeof(DirtTile)) {
                    Values[2, 0] = TextureSpot.MIDMID;
                    Values[2, 1] = TextureSpot.MIDMID;
                    Values[2, 2] = TextureSpot.MIDMID;
                } else {
                    Values[2, 1] = TextureSpot.MIDMID;
                }
            }

            if (Tiles[X, Y - 1].GetType() != typeof(WaterTile)) {
                Values[1, 0] = TextureSpot.MIDMID;
            }
            if (Tiles[X, Y + 1].GetType() != typeof(WaterTile)) {
                Values[1, 2] = TextureSpot.MIDMID;
            }
        }

        public override void Draw(SpriteBatch sb) {
            //sb.Draw(Spritesheet, Position, Color.White);

            for (int i = 0; i < Values.GetLength(0); i++) {
                for (int j = 0; j < Values.GetLength(1); j++) {
                    sb.Draw(Spritesheet, new Vector2(Position.X + (i + 1) * Cell, Position.Y + (j + 1) * Cell), 
                        new Rectangle(new Vector2(Cell * (float) Math.Floor((float) Values[i, j] / 3), Cell * (float) Math.Floor((float) Values[i, j] % 3)).ToPoint(), 
                        new Vector2(Cell).ToPoint()), Color.White, 0f, new Vector2(TileWidth * .33f), 1f, SpriteEffects.None, TileLayer);
                }
            }

            base.Draw(sb);
        }

        public TextureSpot[,] Values { get; set; }
    }

    public enum TextureSpot {
        TOPLEFT = 0,
        TOPMID = 1,
        TOPRIGHT = 2,
        MIDLEFT = 3,
        MIDMID = 4,
        MIDRIGHT = 5,
        BOTLEFT = 6,
        BOTMID = 7,
        BOTRIGHT = 8,
        MIDTL = 9,
        MIDTR = 10,
        MIDBL = 11,
        MIDBR = 12
    }
}