using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Reign {
    public class Unit : Placable {
        public Unit(int visionRange, int moveRange, int maxHealth, Point coords, Point spriteCoords, Point spriteSize, Point hitboxSize, World world) : base(maxHealth, coords.ToVector2() * new Vector2(TileWidth)) {
            VisionRange = visionRange;

            Speed = 5;
            MoveRange = moveRange;
            MovesAvailable = MoveRange;

            SpriteCoords = spriteCoords;
            SpriteSize = spriteSize;
            
            OriginalPosition = Position;

            World = world;

            Team = Save.CurrentTeam;

            HitboxSize = hitboxSize;
            Hitbox = new Rectangle(new Point((int) Position.X, (int) Position.Y), hitboxSize * new Point(Cell));
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

                if (Keyboard.GetState().IsKeyDown(Keys.S) && Mouse.CanType)
                    Health -= MaxHealth / 20;

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

            if (Health < 0)
                Health = 0;
            else if (Health > MaxHealth)
                Health = MaxHealth;

            Hitbox = new Rectangle(new Point((int) Position.X, (int) Position.Y), Hitbox.Size);
        }

        public virtual void Draw(SpriteBatch sb) {
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

            DrawInfo(sb);

            sb.Draw(Spritesheet, Position, new Rectangle(SpriteCoords * new Point(Cell), SpriteSize * new Point(Cell)), Color.White);
        }
        public virtual void Draw(SpriteBatch sb, bool available) {
            DrawHitbox(sb, available ? ColorAvailable : ColorUnavailable);
            
            sb.Draw(Spritesheet, new Vector2(Position.X, Position.Y), new Rectangle(new Point(SpriteCoords.X * Cell, SpriteCoords.Y * Cell), new Point(Cell * SpriteSize.X, Cell * SpriteSize.Y)), Color.White);
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

        public Point Coordinates {
            get { return OriginalPosition.ToPoint() / new Point(TileWidth); }
        }
        public Point HitboxSize { get; set; }
        
        public Vector2 OriginalPosition { get; set; }

        public Rectangle Hitbox { get; set; }

        public World World { get; set; }
        public Save Save {
            get { return World.Save; }
        }

        public Color ColorAvailable {
            get { return Color.FromNonPremultiplied(68, 47, 161, 100); }
        }
        public Color ColorUnavailable {
            get { return Color.FromNonPremultiplied(170, 29, 35, 100); }
        }
    }
}
