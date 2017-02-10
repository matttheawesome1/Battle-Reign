using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Reign {
    public abstract class Block : GameObject {
        public Block(Texture2D image, Point coords, Point spriteCoords, Point spriteSize) {
            Spritesheet = image;

            Position = coords.ToVector2() * new Vector2(TileWidth);
            Coordinates = coords;
            SpriteCoords = spriteCoords;
            SpriteSize = spriteSize;

            Hitbox = new Rectangle(Position.ToPoint(), SpriteSize);
        }

        public virtual void Update(GameTime gt) {
            Hitbox = new Rectangle(Position.ToPoint(), SpriteSize);
        }

        public virtual void Draw(SpriteBatch sb) {
            sb.Draw(Spritesheet, new Vector2(Position.X, Position.Y + (TileWidth - SpriteSize.Y * Cell)), new Rectangle(new Point(SpriteCoords.X * Cell, SpriteCoords.Y * Cell), new Point(SpriteSize.X * Cell, SpriteSize.Y * Cell)), Color.White, 0f, new Vector2(0), 1f, SpriteEffects.None, BuildingLayer);
        }

        public bool Passable { get; set; }

        public Vector2 Position { get; set; }

        public Point SpriteCoords { get; set; }
        public Point SpriteSize { get; set; }
        public Point Coordinates { get; set; }

        public Rectangle Hitbox { get; set; }
    }
}
