using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Reign {
    public abstract class Block : Placable {
        public Block(int maxHealth, Texture2D image, Point coords, Point spriteCoords, Point spriteSize) : base(maxHealth, coords.ToVector2() * new Vector2(TileWidth)) {
            Spritesheet = image;
            
            Coordinates = coords;
            SpriteCoords = spriteCoords;
            SpriteSize = spriteSize;

            Hitbox = new Rectangle(Position.ToPoint(), SpriteSize);
        }

        public virtual void Update(GameTime gt) {
            Hitbox = new Rectangle(Position.ToPoint(), SpriteSize);
        }

        public virtual void Draw(SpriteBatch sb) {
            sb.Draw(Spritesheet, new Vector2(Position.X, Position.Y + (TileWidth - SpriteSize.Y * Cell)), new Rectangle(new Point(SpriteCoords.X * Cell, SpriteCoords.Y * Cell), new Point(SpriteSize.X * Cell, SpriteSize.Y * Cell)), Color.White, 0f, new Vector2(0), 1f, SpriteEffects.None, 1);
        }

        public bool Passable { get; set; }
        
        public Point Coordinates { get; set; }

        public Rectangle Hitbox { get; set; }
    }
}
