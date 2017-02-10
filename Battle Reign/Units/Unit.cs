using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Battle_Reign {
    public class Unit : GameObject {
        public Unit(int visionRange, int moveRange, int maxHealth, Point coords, Point spriteCoords, Point spriteSize, Point hitboxSize, World world) {
            VisionRange = visionRange;

            Speed = 5;
            MoveRange = moveRange;
            MovesAvailable = MoveRange;

            MaxHealth = maxHealth;
            Health = MaxHealth;

            //Coordinates = coords;
            SpriteCoords = spriteCoords;
            SpriteSize = spriteSize;

            Position = coords.ToVector2() * new Vector2(TileWidth);
            OriginalPosition = Position;

            World = world;

            Team = Save.CurrentTeam;

            //GenerateGrid();

            HitboxSize = hitboxSize;
            Hitbox = new Rectangle(new Point((int) Position.X, (int) Position.Y), hitboxSize * new Point(Cell));
        }
        public void GenerateGrid() {
            Point gridSize = World.Size / new Point(TileWidth);

            Grid = new MyPathNode[gridSize.X, gridSize.Y];

            for (int x = 0; x < gridSize.X; x++) {
                for (int y = 0; y < gridSize.Y; y++) {
                    Grid[x, y] = new MyPathNode() {
                        IsWall = false,
                        X = x,
                        Y = y,
                    };
                }
            }

            UpdateGrid();

            AStar = new SpatialAStar<MyPathNode, Object>(Grid);

            Path = null;
        }
        public void UpdateGrid() {
            Grid.Cast<MyPathNode>().ToList().ForEach(x => x.IsWall = false);

            foreach (Point p in World.GetWalls(World.GetCoordinates(Position), MoveRange)) {
                Grid[p.X, p.Y] = new MyPathNode() {
                    IsWall = true,
                    X = p.X,
                    Y = p.Y,
                };
            }
        }
        public void Move(Point destination) {
            Selected = false;
            Moving = true;

            float distance = Utilities.Distance(MouseToCoordinates(), World.GetCoordinates(OriginalPosition));

            if (distance <= MoveRange && distance <= MovesAvailable) {
                bool available = true;

                foreach(Point p in World.GetWalls(World.GetCoordinates(OriginalPosition), MovesAvailable)) {
                    if (destination == p) {
                        available = false;
                        break;
                    }
                }

                if (available) {
                    OriginalPosition = destination.ToVector2() * TileWidth;

                    MovesAvailable -= distance;

                    Team.Discover(VisionRange);
                }
            }
        }
        public Point MouseToCoordinates() {
            return new Point((int) (Mouse.Position.X - Mouse.Position.X % TileWidth) / TileWidth, (int) (Mouse.Position.Y - Mouse.Position.Y % TileWidth) / TileWidth);
        }
        public void ReplenishMoves() {
            MovesAvailable = MoveRange;
        }
        public void Place() {
            Team.Discover(VisionRange);

            Placed = true;
        }

        public void Update(GameTime gt) {
            if (Save.CurrentTeam == Team) {
                float buffer = 1;

                if (CanMove && Placed) {
                    T = (T + Increment) % 360;
                    Position = new Vector2(Position.X, Position.Y + Amplitude * (float) Math.Sin(T) * (float) gt.ElapsedGameTime.TotalSeconds);
                }

                if (Mouse.LeftMouseDown && Mouse.CanPress && Placed) {
                    if (Selected) {
                        Move(MouseToCoordinates());
                    } else if (Mouse.Hitbox.Intersects(Hitbox)) {
                        Selected = !Selected;
                    }
                }

                if (Moving) {
                    if (Utilities.Distance(OriginalPosition, Position) > buffer) {
                        Position -= (Position - OriginalPosition) * Speed * (float) gt.ElapsedGameTime.TotalSeconds;
                    } else {
                        Position = OriginalPosition;
                        Moving = false;
                    }
                }
            }

            Hitbox = new Rectangle(new Point((int) Position.X, (int) Position.Y), Hitbox.Size);
        }

        public virtual void Draw(SpriteBatch sb) {
            sb.Draw(Spritesheet, Position + new Vector2(SpriteSize.X * Cell / 2 - 12, -15), new Rectangle(new Point(SpritesheetSize.X - 3, 10) * new Point(Cell), new Point(2 * Cell)), Team.Color, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            sb.Draw(Spritesheet, Position + new Vector2(SpriteSize.X * Cell / 2 - 12, -15), new Rectangle(new Point(SpritesheetSize.X - 3, 0) * new Point(Cell), new Point(2 * Cell)), Team.Color, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

            if (Selected) {
                foreach (Tile t in World.Tiles) {
                    if (Utilities.Distance(t.Coordinates, World.GetCoordinates(OriginalPosition)) <= MovesAvailable) {
                        sb.Draw(BlankPixel, new Rectangle(t.Position.ToPoint(), new Point(TileWidth)), ColorAvailable);
                    }
                }

                foreach (Point p in World.GetWalls(World.GetCoordinates(OriginalPosition), MovesAvailable)) {
                    if (p != World.GetCoordinates(Position)) sb.Draw(BlankPixel, new Rectangle(p * new Point(TileWidth), new Point(TileWidth)), ColorUnavailable);
                }
            }

            //DrawHitbox(sb, Color.Blue);

            sb.Draw(Spritesheet, Position, new Rectangle(SpriteCoords * new Point(Cell), SpriteSize * new Point(Cell)), Color.White, 0f, new Vector2(0), 1f, SpriteEffects.None, UnitLayer);
        }
        public virtual void Draw(SpriteBatch sb, bool available) {
            DrawHitbox(sb, available ? ColorAvailable : ColorUnavailable);
            
            sb.Draw(Spritesheet, new Vector2(Position.X, Position.Y), new Rectangle(new Point(SpriteCoords.X * Cell, SpriteCoords.Y * Cell), new Point(Cell * SpriteSize.X, Cell * SpriteSize.Y)), Color.White, 0f, new Vector2(0), 1f, SpriteEffects.None, UnitLayer);
        }
        public void DrawHitbox(SpriteBatch sb, Color color) {
            int width = 2;

            sb.Draw(BlankPixel, new Rectangle(Hitbox.Location, new Point(Hitbox.Width + width, width)), color);
            sb.Draw(BlankPixel, new Rectangle(Hitbox.Location, new Point(width, Hitbox.Height + width)), color);
            sb.Draw(BlankPixel, new Rectangle(new Point(Hitbox.X, Hitbox.Y + Hitbox.Height), new Point(Hitbox.Width, width)), color);
            sb.Draw(BlankPixel, new Rectangle(new Point(Hitbox.X + Hitbox.Width, Hitbox.Y), new Point(width, Hitbox.Height + width)), color);
        }
        public void DrawOutline(SpriteBatch sb) {
            int width = 4;

            sb.Draw(BlankPixel, new Rectangle(Moving ? Position.ToPoint() : OriginalPosition.ToPoint(), new Point(width, TileWidth)), Team.Color);
            sb.Draw(BlankPixel, new Rectangle(Moving ? Position.ToPoint() : OriginalPosition.ToPoint(), new Point(TileWidth, width)), Team.Color);
            sb.Draw(BlankPixel, new Rectangle(Moving ? Position.ToPoint() : OriginalPosition.ToPoint() + new Point(SpriteSize.X * Cell - width, 0), new Point(width, TileWidth)), Team.Color);
            sb.Draw(BlankPixel, new Rectangle(Moving ? Position.ToPoint() : OriginalPosition.ToPoint() + new Point(0, SpriteSize.Y * Cell - width), new Point(TileWidth, width)), Team.Color);
        }

        public override string ToString() {
            return "(" + Coordinates.X + ", " + Coordinates.Y + ")";
        }

        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int VisionRange { get; set; }
        public int MoveRange { get; set; }
        public int Amplitude {
            get { return 20; }
        }

        public bool Placed { get; set; }
        public bool Selected { get; set; }
        public bool Moving { get; set; }
        public bool CanMove {
            get { return MovesAvailable >= 1; }
        }

        public float Speed { get; set; }
        public float T { get; set; }
        public float MovesAvailable { get; set; }
        public float Increment {
            get { return .1f; }
        }

        public Point SpriteCoords { get; set; }
        public Point SpriteSize { get; set; }
        public Point Coordinates {
            get { return OriginalPosition.ToPoint() / new Point(TileWidth); }
        }
        public Point HitboxSize { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 OriginalPosition { get; set; }

        public Rectangle Hitbox { get; set; }

        public World World { get; set; }
        public Save Save {
            get { return World.Save; }
        }

        public Team Team { get; set; }

        public SpatialAStar<MyPathNode, Object> AStar { get; set; }
        public LinkedList<MyPathNode> Path { get; set; }
        public MyPathNode[,] Grid { get; set; }

        public Color ColorAvailable {
            get { return Color.FromNonPremultiplied(68, 47, 161, 100); }
        }
        public Color ColorUnavailable {
            get { return Color.FromNonPremultiplied(170, 29, 35, 100); }
        }
    }
}
