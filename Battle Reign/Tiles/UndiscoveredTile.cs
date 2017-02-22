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

            if (Static)
                Values = new TextureSpot[3, 3] { { TextureSpot.MIDMID, TextureSpot.MIDMID, TextureSpot.MIDMID }, { TextureSpot.MIDMID, TextureSpot.MIDMID, TextureSpot.MIDMID }, { TextureSpot.MIDMID, TextureSpot.MIDMID, TextureSpot.MIDMID } };
            else
                Values = new TextureSpot[3, 3] { { TextureSpot.TOPLEFT, TextureSpot.TOPMID, TextureSpot.TOPRIGHT }, { TextureSpot.MIDLEFT, TextureSpot.MIDMID, TextureSpot.MIDRIGHT }, { TextureSpot.BOTLEFT, TextureSpot.BOTMID, TextureSpot.BOTRIGHT } };
        }

        public override void Click() {
            base.Click();

            Tiles.Cast<Tile>().ToList().ForEach(x => x.Fix());
        }

        public override void Fix() {
            if (!Static) {
                //LEFT
                if (!Tiles[X - 1, Y].Discovered) {
                    Values[0, 1] = TextureSpot.MIDMID;

                    if (!Tiles[X, Y - 1].Discovered) {
                        Values[0, 0] = TextureSpot.MIDMID;
                    } else {
                        Values[0, 0] = TextureSpot.MIDLEFT;
                    }
                    if (!Tiles[X, Y + 1].Discovered) {
                        Values[0, 2] = TextureSpot.MIDMID;
                    }
                } else {
                    Values[0, 1] = TextureSpot.TOPMID;
                }

                //RIGHT
                if (!Tiles[X + 1, Y].Discovered) {
                    Values[2, 1] = TextureSpot.MIDMID;

                    if (!Tiles[X, Y - 1].Discovered) {
                        Values[2, 0] = TextureSpot.MIDMID;
                    } else {
                        Values[2, 0] = TextureSpot.MIDLEFT;
                    }
                    if (!Tiles[X, Y + 1].Discovered) {
                        Values[2, 2] = TextureSpot.MIDMID;
                    }
                } else {
                    Values[2, 1] = TextureSpot.BOTMID;

                    if (!Tiles[X, Y + 1].Discovered) {
                        Values[2, 2] = TextureSpot.BOTMID;
                    }
                    if (!Tiles[X, Y - 1].Discovered) {
                        Values[2, 0] = TextureSpot.BOTMID;
                    }
                }

                //TOP
                if (!Tiles[X, Y - 1].Discovered) {
                    Values[1, 0] = TextureSpot.MIDMID;

                    if (Tiles[X - 1, Y].Discovered) {
                        Values[0, 0] = TextureSpot.TOPMID;
                    }
                } else {
                    Values[1, 0] = TextureSpot.MIDLEFT;

                    if (Tiles[X - 1, Y].Discovered) {
                        Values[0, 0] = TextureSpot.TOPLEFT;
                    }
                    if (Tiles[X + 1, Y].Discovered) {
                        Values[2, 0] = TextureSpot.BOTLEFT;
                    }
                }

                //BOTTOM
                if (!Tiles[X, Y + 1].Discovered) {
                    Values[1, 2] = TextureSpot.MIDMID;

                    if (Tiles[X - 1, Y].Discovered) {
                        Values[0, 2] = TextureSpot.TOPMID;
                    }
                } else {
                    Values[1, 2] = TextureSpot.MIDRIGHT;

                    if (!Tiles[X + 1, Y].Discovered) {
                        Values[2, 2] = TextureSpot.MIDRIGHT;
                    }
                    if (!Tiles[X - 1, Y].Discovered) {
                        Values[0, 2] = TextureSpot.MIDRIGHT;
                    }

                    if (Tiles[X - 1, Y].Discovered) {
                        Values[0, 2] = TextureSpot.TOPRIGHT;
                    }
                    if (Tiles[X + 1, Y].Discovered) {
                        Values[2, 2] = TextureSpot.BOTRIGHT;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch sb) {
            if (!Discovered && Debug.DrawFOW) {
                for (int i = 0; i < Values.GetLength(0); i++) {
                    for (int j = 0; j < Values.GetLength(1); j++) {
                        sb.Draw(Spritesheet, new Vector2(Position.X + (i + 1) * Cell, Position.Y + (j + 1) * Cell), new Rectangle(new Vector2((Cell * SpriteCoords.X) + (Cell * (float) Math.Floor((float) Values[i, j] / 3)), (Cell * SpriteCoords.Y) + (Cell * (float) Math.Floor((float) Values[i, j] % 3))).ToPoint(), new Point(Cell)), Color.White, 0f, new Vector2(Cell), 1f, SpriteEffects.None, 1);
                    }
                }
            }

            //base.Draw(sb);
        }

        public bool Static { get; set; }

        public TextureSpot[,] Values { get; set; }
    }
}