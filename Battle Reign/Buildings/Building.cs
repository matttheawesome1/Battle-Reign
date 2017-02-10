using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Battle_Reign {
    public class Building : GameObject {
        public Building(Texture2D image, Point coords, Point spriteCoords, Point spriteSize, Point hitboxSize, bool extend) {
            Image = image;

            Extend = extend;

            Coordinates = coords;
            SpriteCoords = spriteCoords;
            SpriteSize = spriteSize;

            Position = coords.ToVector2() * new Vector2(TileWidth);

            HitboxSize = hitboxSize;
            Hitbox = new Rectangle(new Point((int) Position.X, (int) Position.Y), hitboxSize * new Point(Cell));
        }

        public virtual void Update(GameTime gt) {
            Hitbox = new Rectangle(new Point((int) Position.X, (int) Position.Y), HitboxSize * new Point(Cell));
        }

        public virtual void Draw(SpriteBatch sb) {
            sb.Draw(Spritesheet, new Vector2(Position.X, Extend ? Position.Y + (TileWidth - SpriteSize.Y * Cell) : Position.Y), new Rectangle(new Point(SpriteCoords.X * Cell, SpriteCoords.Y * Cell), new Point(Cell * SpriteSize.X, Cell * SpriteSize.Y)), Color.White, 0f, new Vector2(0), 1f, SpriteEffects.None, 0f);
        }
        public virtual void Draw(SpriteBatch sb, bool available) {
            //sb.Draw(BlankPixel, Hitbox, available ? ColorAvailable : ColorUnavailable);
            DrawHitbox(sb, available ? ColorAvailable : ColorUnavailable);

            sb.Draw(Spritesheet, new Vector2(Position.X, Extend ? Position.Y + (TileWidth - SpriteSize.Y * Cell) : Position.Y), new Rectangle(new Point(SpriteCoords.X * Cell, SpriteCoords.Y * Cell), new Point(Cell * SpriteSize.X, Cell * SpriteSize.Y)), Color.White, 0f, new Vector2(0), 1f, SpriteEffects.None, 0f);
        }
        public void DrawHitbox(SpriteBatch sb, Color color) {
            int width = 2;

            sb.Draw(BlankPixel, new Rectangle(Hitbox.Location, new Point(Hitbox.Width + width, width)), color);
            sb.Draw(BlankPixel, new Rectangle(Hitbox.Location, new Point(width, Hitbox.Height + width)), color);
            sb.Draw(BlankPixel, new Rectangle(new Point(Hitbox.X, Hitbox.Y + Hitbox.Height), new Point(Hitbox.Width, width)), color);
            sb.Draw(BlankPixel, new Rectangle(new Point(Hitbox.X + Hitbox.Width, Hitbox.Y), new Point(width, Hitbox.Height + width)), color);
        }

        public static int VisionRange {
            get { return 4; }
        }

        public bool Extend { get; set; }

        public Point SpriteCoords { get; set; }
        public Point SpriteSize { get; set; }
        public Point Coordinates { get; set; }
        public Point HitboxSize { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Hitbox { get; set; }

        public Texture2D Image { get; set; }

        public Color ColorAvailable {
            get { return Color.FromNonPremultiplied(68, 161, 47, 255); }
        }
        public Color ColorUnavailable {
            get { return Color.FromNonPremultiplied(170, 29, 35, 255); }
        }
    }
}
