using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Reign {
    public class World : GameObject {
        public World(Point size) {
            Background = Content.Load<Texture2D>("backgrounds/game");
            
            Size = size;

            Scrolling = false;
            CanPress = true;
            Typing = false;

            Buffer = 2;
            Padding = 216;

            Camera.Position = new Vector2(Size.X / 2 - Graphics.PreferredBackBufferWidth / 2 + TileWidth / 2, Size.Y / 2 - Graphics.PreferredBackBufferHeight / 2);

            Tiles = new Tile[Size.X / TileWidth, Size.Y / TileWidth];
        }

        public void Update(GameTime gt) {
            KeyboardState kState = Keyboard.GetState();
            MouseState mState = Mouse.GetState();

            if (!Typing && kState.GetPressedKeys().Length > 0 && CanPress) {
                CanPress = false;
            } else if (!Typing && kState.GetPressedKeys().Length == 0) {
                CanPress = true;
            }

            if (Mouse.CanPress) {
                if (Mouse.LeftMouseDown && !Scrolling) {
                    Scrolling = true;

                    OriginalClick = Mouse.Position;

                    int x = (int) (OriginalClick.X - OriginalClick.X % TileWidth) / TileWidth,
                        y = (int) (OriginalClick.Y - OriginalClick.Y % TileWidth) / TileWidth;

                    try {
                        Tiles.Cast<Tile>().ToList().ForEach(z => z.Highlight = false);
                        Tiles[x, y].Highlight = true;
                    } catch (Exception) { Console.WriteLine("X: " + ((int) (OriginalClick.X - OriginalClick.X % TileWidth)) + "Y: " + ((int) (OriginalClick.Y - OriginalClick.Y % TileWidth))); }
                }

                if (Scrolling) {
                    Camera.Position = new Vector2(Camera.Position.X + (OriginalClick.X - Mouse.Position.X), Camera.Position.Y + (OriginalClick.Y - Mouse.Position.Y));

                    Camera.Position = new Vector2(Camera.Position.X < 0 - Padding ? 0 - Padding : Camera.Position.X, Camera.Position.Y > Size.Y - Graphics.PreferredBackBufferHeight + Padding ? Size.Y - Graphics.PreferredBackBufferHeight + Padding : Camera.Position.Y);
                    Camera.Position = new Vector2(Camera.Position.X > Size.X - Graphics.PreferredBackBufferWidth + Padding ? Size.X + Padding - Graphics.PreferredBackBufferWidth : Camera.Position.X, Camera.Position.Y < 0 - Padding ? 0 - Padding : Camera.Position.Y);

                    Mouse.CanPress = false;
                }

                Scrolling = Mouse.CanPress ? false : Mouse.LeftMouseDown;
            }

            Tiles.OfType<Tile>().ToList().ForEach(x => x.Update(gt));
        }

        public void Draw(SpriteBatch sb) {
            sb.Draw(Background, Camera.Position, Color.White);

            foreach (Tile t in Tiles.OfType<Tile>().ToList()) {
                if (t.Highlight) {
                    t.Draw(sb);
                    t.Draw(sb, OriginalClick);
                } else
                    t.Draw(sb);
            }
            
        }

        public void GenerateMap(Team[] teams) {
            Camera.Position = new Vector2(Size.X / 2 - Graphics.PreferredBackBufferWidth / 2 + TileWidth / 2, Size.Y / 2 - Graphics.PreferredBackBufferHeight / 2);

            GenerateTiles();
            GenerateBlocks();

            bool cont = true;

            for (int i = 0; i < teams.Length; i++) {
                cont = true;

                teams[i] = new Team("Player " + i, new Vector2(), Tiles);

                for (int x = 0; x < Tiles.GetLength(0) && cont; x++) {
                    for (int y = 0; y < Tiles.GetLength(1) && cont; y++) {
                        if (Utilities.Distance(Tiles[0, 0].Coordinates, Tiles[x, y].Coordinates) < 10) {
                            if (Tiles[x, y].GetType() != typeof(WaterTile) && !Tiles[x, y].HasBuilding && !Tiles[x, y].HasBlock) {
                                teams[i].Base.Position = new Vector2(x * TileWidth, y * TileWidth);
                                Tiles[x, y].Building = teams[i].Base;
                                cont = false;
                                break;
                            }
                        }
                    }

                    if (!cont) break;
                }
            }

            Tiles.OfType<GrassTile>().ToList().ForEach(x => x.Update(true));
            Tiles.OfType<DirtTile>().ToList().ForEach(x => x.Update(true));

            Tiles[Tiles.GetLength(0) / 2, Tiles.GetLength(1) / 2].Discover(true);
        }

        public void GenerateTiles() {
            for (int i = 0; i < Tiles.GetLength(0); i++) {
                for (int j = 0; j < Tiles.GetLength(1); j++) {
                    if (i <= Buffer - 1 || i >= Tiles.GetLength(0) - Buffer || j <= Buffer - 1 || j >= Tiles.GetLength(1) - Buffer) {
                        Tiles[i, j] = new WaterTile(new Vector2(i * TileWidth, j * TileWidth), Tiles);
                    } else {
                        int a = Utilities.Next(0, 10);

                        if (a > 0) Tiles[i, j] = new GrassTile(new Vector2(i * TileWidth, j * TileWidth), Tiles);
                        else Tiles[i, j] = new WaterTile(new Vector2(i * TileWidth, j * TileWidth), Tiles);
                    }
                }
            }

            for (int i = 0; i < Tiles.GetLength(0); i++) {
                for (int j = 0; j < Tiles.GetLength(1); j++) {
                    if (Tiles[i, j].GetType() != typeof(GrassTile)) {
                        if (i <= Buffer - 1 || i >= Tiles.GetLength(0) - Buffer || j <= Buffer - 1 || j >= Tiles.GetLength(1) - Buffer) {

                        } else {
                            if (Tiles[i - 1, j].GetType() != typeof(WaterTile) && Tiles[i, j - 1].GetType() != typeof(WaterTile) && Tiles[i + 1, j].GetType() != typeof(WaterTile) && Tiles[i, j + 1].GetType() != typeof(WaterTile)) {
                                Tiles[i, j] = new GrassTile(new Vector2(i * TileWidth, j * TileWidth), Tiles);
                            }

                            if (Tiles[i - 2, j].GetType() != typeof(GrassTile)) {
                                Tiles[i - 1, j] = new WaterTile(new Vector2((i - 1) * TileWidth, j * TileWidth), Tiles);
                            }
                            if (Tiles[i + 2, j].GetType() != typeof(GrassTile)) {
                                Tiles[i + 1, j] = new WaterTile(new Vector2((i + 1) * TileWidth, j * TileWidth), Tiles);
                            }
                            if (Tiles[i, j + 2].GetType() != typeof(GrassTile)) {
                                Tiles[i, j + 1] = new WaterTile(new Vector2(i * TileWidth, (j + 1) * TileWidth), Tiles);
                            }
                            if (Tiles[i, j - 2].GetType() != typeof(GrassTile)) {
                                Tiles[i, j - 1] = new WaterTile(new Vector2(i * TileWidth, (j - 1) * TileWidth), Tiles);
                            }
                        }
                    }
                }
            }
        }
        public void GenerateBlocks() {
            for (int i = 0; i < Tiles.GetLength(0); i++) {
                for (int j = 0; j < Tiles.GetLength(1); j++) {
                    if (Tiles[i, j].GetType() == typeof(GrassTile)) {
                        if (Tiles[i - 1, j - 1].GetType() != typeof(WaterTile) && Tiles[i, j - 1].GetType() != typeof(WaterTile) && Tiles[i + 1, j - 1].GetType() != typeof(WaterTile) &&
                            Tiles[i - 1, j].GetType() != typeof(WaterTile) && Tiles[i + 1, j].GetType() != typeof(WaterTile) &&
                            Tiles[i - 1, j + 1].GetType() != typeof(WaterTile) && Tiles[i, j + 1].GetType() != typeof(WaterTile) && Tiles[i + 1, j + 1].GetType() != typeof(WaterTile)) {

                            int b = Utilities.Next(0, 30);

                            if (b < 2) {
                                int c = Utilities.Next(0, 20);

                                if (c > 3)
                                    Tiles[i, j].Block = new Tree(new Vector2(i * TileWidth, j * TileWidth));
                                else
                                    Tiles[i, j].Block = new Bush(new Vector2(i * TileWidth, j * TileWidth));
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < Tiles.GetLength(0); i++) {
                for (int j = 0; j < Tiles.GetLength(1); j++) {
                    if (Tiles[i, j].GetType() == typeof(GrassTile)) {
                        bool cont = true;

                        foreach (Tile t in Tiles.Cast<Tile>().ToList()) {
                            if (Utilities.Distance(Tiles[i, j].Coordinates, t.Coordinates) < 3) {
                                if (t.GetType() == typeof(WaterTile)) {
                                    cont = false;
                                    break;
                                }
                            }
                            if (Utilities.Distance(Tiles[i, j].Coordinates, t.Coordinates) < 2) {
                                if (t.HasBlock) {
                                    if (t.Block.GetType() != typeof(Boulder)) {
                                        cont = false;
                                        break;
                                    }
                                }
                            }
                        }

                        if (cont && Utilities.Next(0, 10) < 3) {
                            Tiles[i, j] = new DirtTile(new Vector2(i * TileWidth, j * TileWidth), Tiles);

                            Tiles[i - 1, j - 1] = new DirtTile(new Vector2((i - 1) * TileWidth, (j - 1) * TileWidth), Tiles);
                            Tiles[i, j - 1] = new DirtTile(new Vector2(i * TileWidth, (j - 1) * TileWidth), Tiles);
                            Tiles[i + 1, j - 1] = new DirtTile(new Vector2((i + 1) * TileWidth, (j - 1) * TileWidth), Tiles);
                            Tiles[i - 1, j] = new DirtTile(new Vector2((i - 1) * TileWidth, j * TileWidth), Tiles);
                            Tiles[i + 1, j] = new DirtTile(new Vector2((i + 1) * TileWidth, j * TileWidth), Tiles);
                            Tiles[i - 1, j + 1] = new DirtTile(new Vector2((i - 1) * TileWidth, (j + 1) * TileWidth), Tiles);
                            Tiles[i, j + 1] = new DirtTile(new Vector2(i * TileWidth, (j + 1) * TileWidth), Tiles);
                            Tiles[i + 1, j + 1] = new DirtTile(new Vector2((i + 1) * TileWidth, (j + 1) * TileWidth), Tiles);
                        }
                    }
                }
            }

            for (int i = 0; i < Tiles.GetLength(0); i++) {
                for (int j = 0; j < Tiles.GetLength(1); j++) {
                    if (Tiles[i, j].GetType() == typeof(DirtTile) && Utilities.Next(0, 20) < 3) Tiles[i, j].Block = new Boulder(new Vector2(i * TileWidth, j * TileWidth));
                }
            }
        }
        
        public int Buffer { get; set; }
        public int Padding { get; set; }

        public bool Scrolling { get; set; }
        public bool CanPress { get; set; }
        public bool Typing { get; set; }

        public Texture2D Background { get; set; }

        public Vector2 OriginalClick { get; set; }

        public Point Size { get; set; }

        public Tile[,] Tiles { get; set; }
    }
}
