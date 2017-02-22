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
        public World(Point size, Save save, Scene scene) {
            Background = Content.Load<Texture2D>("backgrounds/game");

            Scene = scene;
            Save = save;

            Size = size;

            Scrolling = false;
            CanPress = true;
            Typing = false;

            Buffer = 6;
            Padding = TileWidth * 6;

            Camera.Position = new Vector2(Size.X / 2 - Graphics.PreferredBackBufferWidth / 2 + TileWidth / 2, Size.Y / 2 - Graphics.PreferredBackBufferHeight / 2);

            Tiles = new Tile[Size.X / TileWidth, Size.Y / TileWidth];
        }
        public List<Point> GetWalls(Point position, float radius) {
            List<Point> points = new List<Point>();

            foreach(Block b in Blocks) {
                if (Utilities.Distance(GetCoordinates(b.Position), position) <= radius) {
                    points.Add(GetCoordinates(b.Position));
                }
            }
            foreach (Building b in Buildings) {
                if (Utilities.Distance(GetCoordinates(b.Position), position) <= radius) {
                    points.Add(GetCoordinates(b.Position));
                }
            }
            foreach(Unit u in Units) {
                if (Utilities.Distance(GetCoordinates(u.OriginalPosition), position) <= radius) {
                    points.Add(GetCoordinates(u.OriginalPosition));
                }
            }
            foreach(Tile t in Tiles.OfType<WaterTile>()) {
                if (Utilities.Distance(GetCoordinates(t.Position), position) <= radius) {
                    points.Add(GetCoordinates(t.Position));
                }
            }

            return points;
        }
        public Point GetCoordinates(Vector2 position) {
            return new Point((int) (position.X - position.X % TileWidth) / TileWidth, (int) (position.Y - position.Y % TileWidth) / TileWidth);
        }

        public void Update(GameTime gt) {
            KeyboardState kState = Keyboard.GetState();
            MouseState mState = Mouse.GetState();

            if (kState.IsKeyDown(Keys.R) && Mouse.CanPress)
                GenerateMap(Save.Teams);

            if (!Typing && kState.GetPressedKeys().Length > 0 && CanPress) {
                CanPress = false;
            } else if (!Typing && kState.GetPressedKeys().Length == 0) {
                CanPress = true;
            }
            
            if (Mouse.RightMouseDown && !Scrolling) {
                Scrolling = true;

                OriginalClick = Mouse.Position;
            } else if (Mouse.LeftMouseDown && !Scrolling) {
                OriginalClick = Mouse.Position;

                int x = (int) (OriginalClick.X - OriginalClick.X % TileWidth) / TileWidth,
                    y = (int) (OriginalClick.Y - OriginalClick.Y % TileWidth) / TileWidth;

                try {
                    Tiles.Cast<Tile>().ToList().ForEach(z => z.Highlight = false);
                    Tiles[x, y].Highlight = true;
                } catch (Exception) { }
            }

            if (Scrolling) {
                Camera.Position = new Vector2(Camera.Position.X + (OriginalClick.X - Mouse.Position.X), Camera.Position.Y + (OriginalClick.Y - Mouse.Position.Y));

                //Camera.Position = new Vector2(Camera.Position.X < 0 - Padding ? 0 - Padding : Camera.Position.X, Camera.Position.Y > Size.Y - Graphics.PreferredBackBufferHeight + Padding ? Size.Y - Graphics.PreferredBackBufferHeight + Padding : Camera.Position.Y);
                //Camera.Position = new Vector2(Camera.Position.X > Size.X - Graphics.PreferredBackBufferWidth + Padding ? Size.X + Padding - Graphics.PreferredBackBufferWidth : Camera.Position.X, Camera.Position.Y < 0 - Padding ? 0 - Padding : Camera.Position.Y);

                Mouse.CanPress = false;
            }

            Scrolling = Mouse.CanPress ? false : Mouse.RightMouseDown;

            //Tiles.OfType<Tile>().ToList().ForEach(x => x.Update(gt));
            Blocks.ForEach(x => x.Update(gt));
            Buildings.ForEach(x => x.Update(gt));
            Units.ForEach(x => x.Update(gt));
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

            Units.ForEach(x => x.Draw(sb));
            Buildings.ForEach(x => x.Draw(sb));
            Blocks.ForEach(x => x.Draw(sb));
        }
        
        public bool CanPlaceBuilding(Building b) {
            bool available = true;

            foreach (Building bu in Buildings) {
                if (b.Hitbox.Intersects(bu.Hitbox)) {
                    available = false;

                    break;
                }
            }

            if (available) {
                foreach (Unit u in Units) {
                    if (u.Hitbox.Intersects(b.Hitbox)) {
                        available = false;

                        break;
                    }
                }
            }

            if (available) {
                foreach (Block bl in Blocks) {
                    if (b.Hitbox.Intersects(bl.Hitbox)) {
                        available = false;

                        break;
                    }
                }
            }

            if (available) {
                List<WaterTile> tiles = Tiles.OfType<WaterTile>().Cast<WaterTile>().ToList();

                foreach(WaterTile t in tiles) {
                    if (t.Hitbox.Intersects(b.Hitbox)) {
                        available = false;

                        break;
                    }
                }
            }

            return available;
        }
        public bool CanPlaceUnit(Unit u) {
            bool available = true;

            foreach (Building bu in Buildings) {
                if (u.Hitbox.Intersects(bu.Hitbox)) {
                    available = false;

                    break;
                }
            }

            if (available) {
                foreach (Unit un in Units) {
                    if (un.Hitbox.Intersects(u.Hitbox)) {
                        available = false;

                        break;
                    }
                }
            }

            if (available) {
                foreach (Block bl in Blocks) {
                    if (u.Hitbox.Intersects(bl.Hitbox)) {
                        available = false;

                        break;
                    }
                }
            }

            if (available) {
                List<WaterTile> tiles = Tiles.OfType<WaterTile>().Cast<WaterTile>().ToList();

                foreach (WaterTile t in tiles) {
                    if (t.Hitbox.Intersects(u.Hitbox)) {
                        available = false;

                        break;
                    }
                }
            }

            return available;
        }
        public bool IsTileAvailable(Point coords) {
            bool available = true;

            if (available) {
                foreach (Block b in Blocks) {
                    if (b.Coordinates == coords) {
                        available = false;

                        break;
                    } 
                }
            }
            if (available) {
                foreach (Building b in Buildings) {
                    if (b.Coordinates == coords) {
                        available = false;

                        break;
                    }
                }
            }
            if (available) {
                foreach (Unit u in Units) {
                    if (u.Coordinates == coords) {
                        available = false;

                        break;
                    }
                }
            }
            if (available) {
                foreach (WaterTile t in Tiles.OfType<WaterTile>()) {
                    if (t.Coordinates == coords) {
                        available = false;

                        break;
                    }
                }
            }

            return available;
        }
        public bool IsTileAvailable(int x, int y) {
            bool available = true;

            if (available) {
                foreach (Block b in Blocks) {
                    if (b.Coordinates == new Point(x, y)) {
                        available = false;

                        break;
                    }
                }
            }
            if (available) {
                foreach (Building b in Buildings) {
                    if (b.Coordinates == new Point(x, y)) {
                        available = false;

                        break;
                    }
                }
            }

            return available;
        }
        public Block GetBlock(Point coords) {
            foreach (Block b in Blocks)
                if (coords == b.Coordinates)
                    return b;

            return null;
        }
        public Block GetBlock(int x, int y) {
            foreach (Block b in Blocks)
                if (new Point(x, y) == b.Coordinates)
                    return b;

            return null;
        }    

        public void GenerateMap(Team[] teams) {
            Blocks = new List<Block>();
            Units = new List<Unit>();
            Buildings = new List<Building>();

            Camera.Position = new Vector2(Size.X / 2 - Graphics.PreferredBackBufferWidth / 2 + TileWidth / 2, Size.Y / 2 - Graphics.PreferredBackBufferHeight / 2);

            GenerateTiles();
            GenerateBlocks();

            for (int i = 0; i < teams.Length; i++) {

                teams[i] = new Team("Player " + i, 0, new Point(), this, Save, Scene);

                while (true) {
                    bool quit = false;

                    int x = Utilities.Next(0, Tiles.GetLength(0) - 1), y = Utilities.Next(0, Tiles.GetLength(1) - 1);

                    foreach(Team t in teams) {
                        if (Tiles[x, y] != null & IsTileAvailable(new Point(x, y)) && Utilities.Distance(Tiles[x, y].Coordinates, t.Base.Coordinates) > 5) {
                            teams[i].Base.Position = new Vector2(x * TileWidth, y * TileWidth);
                            teams[i].Base.Coordinates = teams[i].Base.Position.ToPoint() / new Point(TileWidth);
                            Buildings.Add(teams[i].Base);

                            quit = true;

                            break;
                        }
                    }

                    if (quit)
                        break;
                }
            }

            Camera.Position = teams[0].Base.Position - new Vector2(Graphics.PreferredBackBufferWidth / 2 - (teams[0].Base.SpriteSize.X * Cell) / 2, Graphics.PreferredBackBufferHeight / 2 - (teams[0].Base.SpriteSize.Y * Cell) / 2);

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
                        int a = Utilities.Next(0, 70);

                        if (a > 0)
                            Tiles[i, j] = new GrassTile(new Vector2(i * TileWidth, j * TileWidth), Tiles);
                        else {
                            Tiles[i, j] = new WaterTile(new Vector2(i * TileWidth, j * TileWidth), Tiles);
                        }
                    }
                }
            }

            foreach (WaterTile t in Tiles.OfType<WaterTile>().ToList()) {
                if (!(t.X <= Buffer - 1 || t.X >= Tiles.GetLength(0) - Buffer || t.Y <= Buffer - 1 || t.Y >= Tiles.GetLength(1) - Buffer)) {
                    int radius = Utilities.Next(0, 5);

                    for (int x = t.X - radius - Utilities.Next(-1, 0); x < t.X + radius + Utilities.Next(0, 1); x++) {
                        for (int y = t.Y - radius - Utilities.Next(1, 2); y < t.Y + radius + Utilities.Next(1, 2); y++) {
                            if (!(x <= Buffer - 1 || x >= Tiles.GetLength(0) - Buffer || y <= Buffer - 1 || y >= Tiles.GetLength(1) - Buffer) && Utilities.Distance(Tiles[x, y].Coordinates, t.Coordinates) <= radius) {
                                Tiles[x, y] = new WaterTile(new Vector2(x, y) * TileWidth, Tiles);
                            }
                        }
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
                                    Blocks.Add(new Tree(new Point(i, j)));
                                else
                                   Blocks.Add(new Bush(new Point(i, j)));
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
                                if (IsTileAvailable(t.Coordinates)) {
                                    /*if (GetBlock(t.Coordinates).GetType() != typeof(Boulder)) {
                                        cont = false;
                                        break;
                                    }*/
                                }
                            }
                        }

                        if (cont && Utilities.Next(0, 10) < 2) {
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
                    if (Tiles[i, j].GetType() == typeof(DirtTile) && Utilities.Next(0, 20) < 3 && IsTileAvailable(i, j)) Blocks.Add(new Boulder(new Point(i, j)));
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

        public Scene Scene { get; set; }

        public Save Save { get; set; }

        public Tile[,] Tiles { get; set; }
        
        public List<Block> Blocks { get; set; }
        public List<Unit> Units { get; set; }
        public List<Building> Buildings { get; set; }
    }
}
