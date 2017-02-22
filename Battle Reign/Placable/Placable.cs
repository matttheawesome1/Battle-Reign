using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle_Reign {
    public class Placable : GameObject {
        public Placable(int maxHealth, Vector2 position) {
            MaxHealth = maxHealth;
            Health = MaxHealth;

            Position = position;

            FlagCoords = new Point(SpritesheetSize.X - 3, 0);
            FlagSize = new Point(Cell * 2);

            HealthCoords = new Point(0, SpritesheetSize.Y - 15);
            HealthSize = new Point(Cell);

            BackgroundCoords = new Point(0, SpritesheetSize.Y - 14);
            BackgroundSize = new Point(Cell * 2);
        }

        public void DrawInfo(SpriteBatch sb) {
            Vector2 position = Position + new Vector2(SpriteSize.X * Cell / 2 - BackgroundSize.X / 2, -BackgroundSize.Y - 4 + (TileWidth - SpriteSize.Y * Cell));

            sb.Draw(Spritesheet, position, new Rectangle(BackgroundCoords * new Point(Cell), BackgroundSize), Color.White);
            sb.Draw(Spritesheet, position, new Rectangle(FlagCoords * new Point(Cell), FlagSize), Team.Color);
            sb.Draw(Spritesheet, position + new Vector2(7, 15), new Rectangle((HealthCoords + new Point(10 - (Health > 0 && ((int) (((float) Health / (float) MaxHealth) * 10)) == 0 ? 1 : ((int) (((float) Health / (float) MaxHealth) * 10))), 0)) * new Point(Cell), HealthSize), Color.White);
        }

        public int Health { get; set; }
        public int MaxHealth { get; set; }

        public Point SpriteCoords { get; set; }
        public Point SpriteSize { get; set; }

        public Point FlagCoords { get; set; }
        public Point FlagSize { get; set; }

        public Point HealthCoords { get; set; }
        public Point HealthSize { get; set; }

        public Point BackgroundCoords { get; set; }
        public Point BackgroundSize { get; set; }

        public Vector2 Position { get; set; }

        public Team Team { get; set; }
    }
}
