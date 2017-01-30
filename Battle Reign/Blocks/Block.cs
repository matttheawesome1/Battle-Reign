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
        public Block(Texture2D image, Vector2 position, Point spriteCoords, Point spriteSize) {
            Spritesheet = image;

            Position = position;
            SpriteCoords = spriteCoords;
            SpriteSize = spriteSize;
        }

        public virtual void Update(GameTime gt) {
            
        }

        public virtual void Draw(SpriteBatch sb) {
            sb.Draw(Spritesheet, new Vector2(Position.X, Position.Y + (TileWidth - SpriteSize.Y * (TileWidth / 3))), new Rectangle(new Point(SpriteCoords.X * (TileWidth / 3), SpriteCoords.Y * (TileWidth / 3)), new Point(SpriteSize.X * (TileWidth / 3), SpriteSize.Y * (TileWidth / 3))), Color.White, 0f, new Vector2(0), 1f, SpriteEffects.None, 0f);
        }

        public bool Passable { get; set; }

        public Vector2 Position { get; set; }
        public Point SpriteCoords { get; set; }
        public Point SpriteSize { get; set; }
    }
}
