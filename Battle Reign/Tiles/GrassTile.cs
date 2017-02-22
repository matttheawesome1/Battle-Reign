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

            Texture = new Texture2D(Graphics.GraphicsDevice, TileWidth, TileWidth);

            Color[] ssData = new Color[Spritesheet.Width * Spritesheet.Height];
            Spritesheet.GetData<Color>(ssData);

            Rectangle rectangle = new Rectangle(SpriteCoords * new Point(Cell), new Point(TileWidth));

            Color[] data = new Color[rectangle.Width * rectangle.Height];
            Spritesheet.GetData(0, rectangle, data, 0, data.Length);
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

            /*for (int i = 0; i < Values.GetLength(0); i++) {
                for (int j = 0; j < Values.GetLength(1); j++) {
                    for (int x = 0; x < Cell; x++) {
                        for (int y = 0; y < Cell; y++) {
                            data[(x + (i * Cell)) * (y + (j * Cell))] = ssData[((SpriteCoords.X * Cell) + (x + (i * Cell))) * ((SpriteCoords.Y * Cell) * (y + (j * Cell)))];
                            //Console.WriteLine((SpriteCoords.Y * Cell) + " + " + (y + (Cell * j)) + " = " + (SpriteCoords.Y * Cell + ((y + (Cell * j)))) + " at (" + (x + (Cell * i)) + ", " + (y + (Cell * j)) + ")");
                        }
                    }
                }
            }*/

            // DO ^^^^^^^^ WITH CURRENT TEXTURE

            /*Rectangle rectangle = new Rectangle(SpriteCoords * new Point(Cell), new Point(TileWidth));
            
            Color[] data = new Color[rectangle.Width * rectangle.Height];
            Texture.GetData(0, rectangle, data, 0, data.Length);

            Color[] ssData = new Color[rectangle.Width * rectangle.Height];
            Spritesheet.GetData(ssData);

            for (int i = 0; i < Values.GetLength(0); i++) {
                for (int j = 0; j < Values.GetLength(1); j++) {
                    for (int x = 0; x < Cell; x++) {
                        for (int y = 0; y < Cell; y++) {
                            //data[(Cell * i + x) * y] = ssData[(((((int) Values[i, j] * Cell) / 3)) + x) * (((int) Values[i, j] % 3) * 3 + y)];

                            //Console.WriteLine(((Cell * i + x) * y));
                        }
                    }
                }
            }
            
            Texture.SetData(data);*/
        }

        public override void Draw(SpriteBatch sb) {
            //sb.Draw(Texture, Position, Color.White);

            for (int i = 0; i < Values.GetLength(0); i++) {
                for (int j = 0; j < Values.GetLength(1); j++) {
                    sb.Draw(Spritesheet, new Vector2(Position.X + (i + 1) * Cell, Position.Y + (j + 1) * Cell), 
                        new Rectangle(new Vector2(Cell * (float) Math.Floor((float) Values[i, j] / 3), Cell * (float) Math.Floor((float) Values[i, j] % 3)).ToPoint(), 
                        new Vector2(Cell).ToPoint()), Color.White, 0f, new Vector2(Cell), 1f, SpriteEffects.None, 1);
                }
            }

            base.Draw(sb);
        }

        public Texture2D DefaultTexture { get; set; }
        public Texture2D Texture { get; set; }

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