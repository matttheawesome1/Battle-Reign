using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Reign {
    public class Base : Building {
        public Base(Vector2 position, Point spriteCoords, Team team) : base(Spritesheet, position, spriteCoords, new Point(3, 4)) {
            Team = team;
            Color = team.Color;
        }

        public override void Update(GameTime gt) {
            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {
            base.Draw(sb);

            sb.Draw(Spritesheet, new Vector2(Position.X, Position.Y + (TileWidth - SpriteSize.Y * Cell)), new Rectangle(new Point((SpriteCoords.X + 3) * Cell, SpriteCoords.Y * Cell), new Point(Cell * SpriteSize.X, Cell * SpriteSize.Y)), Color, 0f, new Vector2(0), 1f, SpriteEffects.None, 0f);
        }

        public Color Color { get; set; }

        public Team Team { get; set; }
    }
}
