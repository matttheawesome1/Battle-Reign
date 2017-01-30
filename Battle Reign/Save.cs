using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Reign {
    public class Save : GameObject {
        public Save(string name, Point size, Scene parent) {
            Name = name;
            Time = 0;

            Parent = parent;

            TileWidth = 36;

            ExitButton = new Button(true, "exit/small", new Vector2(Camera.Position.X + Graphics.PreferredBackBufferWidth - 90, Camera.Position.Y + 10), (s, e) => Parent.Action = Action.EXIT, "square/small");
            GenerateButton = new Button(true, "refresh/small", new Vector2(Camera.Position.X + Graphics.PreferredBackBufferWidth - 180, Camera.Position.Y + 10), (s, e) => GenerateMap(), "square/small");

            if (size.X < 0) size.X = 0;
            else if (size.X > MaxSize.X) size.X = MaxSize.X;

            if (size.Y < 0) size.Y = 0;
            else if (size.Y > MaxSize.Y) size.Y = MaxSize.Y;

            size = new Point(size.X * TileWidth, size.Y * TileWidth);

            World = new World(size);

            Team[] teams = new Team[3];
            Teams = teams;

            World.GenerateMap(Teams);
            CurrentTeam = Teams[0];
            
        }

        public void Update(GameTime gt) {
            if (Time > TurnTime) {
                Time = 0f;
                SwitchTeam();
            }

            Time += (float) gt.ElapsedGameTime.TotalSeconds;

            World.Update(gt);

            CurrentTeam.Update(gt);

            ExitButton.Position = new Vector2(Camera.Position.X + Graphics.PreferredBackBufferWidth - 90, Camera.Position.Y + 10);
            ExitButton.Update(gt);

            GenerateButton.Position = new Vector2(Camera.Position.X + Graphics.PreferredBackBufferWidth - 180, Camera.Position.Y + 10);
            GenerateButton.Update(gt);
        }

        public void Draw(SpriteBatch sb) {
            World.Draw(sb);

            CurrentTeam.Draw(sb);

            int padding = 20, height = (int) (padding * 8.2);

            sb.Draw(BlankPixel, Camera.Position, new Rectangle(0, 0, Graphics.PreferredBackBufferWidth, height), new Color(62, 6, 6));
            sb.Draw(BlankPixel, new Vector2(Camera.Position.X + padding, Camera.Position.Y + padding), new Rectangle(0, 0, Graphics.PreferredBackBufferWidth - padding * 2, height - 50 - padding * 2), new Color(129, 21, 21));
            sb.Draw(BlankPixel, new Vector2(Camera.Position.X + padding, Camera.Position.Y + padding + height - 50 - padding), new Rectangle(0, 0, Graphics.PreferredBackBufferWidth - padding * 2, 30), new Color(129, 21, 21));

            int left = 0;

            for (int i = 0; i < Teams.Length; i++) {
                sb.Draw(Spritesheet, new Vector2(Camera.Position.X + padding * 2, Camera.Position.Y + padding * 2), new Rectangle(new Point((Teams[i].SpriteCoords.X + 3) * Cell, 0), new Point(Teams[i].SpriteSize.X * Cell, 2 * Cell)), Teams[i].Color);
            }

            ExitButton.Draw(sb);
            //GenerateButton.Draw(sb);
        }

        public void GenerateMap() {
            World.GenerateMap(Teams);
        }

        public void SwitchTeam() {
            int index = Array.IndexOf(Teams, CurrentTeam);

            if (index + 1 > Teams.Length - 1)
                CurrentTeam = Teams[0];
            else
                CurrentTeam = Teams[index + 1];
        }

        public string Name { get; set; }
        
        public float Time { get; set; }
        public float TurnTime {
            get { return 5; }
        }

        public Point MaxSize {
            get { return new Point(60); }
        }

        public Button ExitButton { get; set; }
        public Button GenerateButton { get; set; }

        public World World { get; set; }

        public Scene Parent { get; set; }

        public Team[] Teams { get; set; }

        public Team CurrentTeam { get; set; }
    }
}