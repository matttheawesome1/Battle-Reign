using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Reign {
    public class Base : Building {
        public Base(Point coords, Point spriteCoords, Team team) : base(150, Spritesheet, coords, spriteCoords, new Point(3, 4), new Point(3), true) {
            Team = team;
            Color = team.Color;
        }
        public void SpawnUnit() {
            int radius = 1;
            bool found = false;

            while (!found) {
                for (int a = Coordinates.X - radius - 1; a < Coordinates.X + radius + 1 && !found; a++) {
                    for (int b = Coordinates.Y - radius - 1; b < Coordinates.Y + radius + 1 && !found; b++) {
                        if (Utilities.Distance(new Point(a, b), Coordinates) < radius) {
                            if (Team.World.IsTileAvailable(new Point(a, b))) {
                                found = true;

                                Unit unit = new UnitScout(new Point(a, b), Team.World);
                                unit.Team = Team;

                                unit.Place();
                                Team.World.Units.Add(unit);
                                Team.Discover(unit.Coordinates, unit.VisionRange);
                            }
                        }
                    }
                }

                radius++;
            }
        }

        public override void Update(GameTime gt) {
            base.Update(gt);
        }

        public override void Draw(SpriteBatch sb) {
            base.Draw(sb);

            sb.Draw(Spritesheet, new Vector2(Position.X, Position.Y + (TileWidth - SpriteSize.Y * Cell)), new Rectangle(new Point((SpriteCoords.X + 3) * Cell, SpriteCoords.Y * Cell), new Point(Cell * SpriteSize.X, Cell * SpriteSize.Y)), Color, 0f, new Vector2(0), 1f, SpriteEffects.None, 0f);
        }

        public Color Color { get; set; }
    }
}
