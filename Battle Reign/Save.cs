using System;
using System.Collections.Generic;
using System.Linq;
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

            Padding = 12;

            if (size.X < 0) size.X = 0;
            else if (size.X > MaxSize.X) size.X = MaxSize.X;

            if (size.Y < 0) size.Y = 0;
            else if (size.Y > MaxSize.Y) size.Y = MaxSize.Y;

            size = new Point(size.X * TileWidth, size.Y * TileWidth);
            WorldSize = size;

            StartGame();

            EndTurnButton = new Button(true, "END TURN", "silkscreen/small", new Vector2(0), (s, e) => SwitchTeam(), "wide/tiny");
        }
        public void StartGame() {
            World = new World(WorldSize, this, Parent);

            Teams = new Team[3];

            World.GenerateMap(Teams);
            CurrentTeam = Teams[0];

            foreach (Team t in Teams) {
                t.Base.SpawnUnit();
            }

            Teams.Cast<Team>().ToList().ForEach(x => x.TurnTime = TurnTime);
        }

        public void Update(GameTime gt) {
            if (Time >= TurnTime) {
                Time = 0f;
                SwitchTeam();
            }

            Time += (float) gt.ElapsedGameTime.TotalSeconds;

            if (Keyboard.GetState().IsKeyDown(Keys.R) && Mouse.CanPress)
                StartGame();

            World.Update(gt);

            CurrentTeam.Update(gt);

            EndTurnButton.Update(gt);

            if (TeamPopup != null) {
                TeamPopup.Update(gt);

                if (TeamPopup.Finished) {
                    TeamPopup = null;
                }
            }
        }

        public void Draw(SpriteBatch sb) {
            World.Draw(sb);

            CurrentTeam.Draw(sb);
            EndTurnButton.Position = new Vector2(Graphics.PreferredBackBufferWidth - EndTurnButton.Background.Width - Padding, Graphics.PreferredBackBufferHeight - EndTurnButton.Background.Height - Padding);

            EndTurnButton.Draw(sb);

            if (TeamPopup != null) {
                TeamPopup.Draw(sb);
            }
        }

        public void GenerateMap() {
            World.GenerateMap(Teams);
        }

        public void SwitchTeam() {
            CurrentTeam.Increase();

            int index = Array.IndexOf(Teams, CurrentTeam);

            if (index + 1 > Teams.Length - 1)
                CurrentTeam = Teams[0];
            else
                CurrentTeam = Teams[index + 1];

            CurrentTeam.Time = 0;
            Time = 0;
            CurrentTeam.Cards.ForEach(x => x.Position = x.HoverPosition + new Vector2(0, Utilities.Next(0, 60)));

            foreach (Unit u in World.Units) {
                if (u.Team == CurrentTeam)
                    u.ReplenishMoves();
            }

            TeamPopup = new Popup(CurrentTeam.Color, 15, 1);

            Camera.Position = CurrentTeam.Base.Position - new Vector2(Graphics.PreferredBackBufferWidth / 2 - (CurrentTeam.Base.SpriteSize.X * Cell) / 2, Graphics.PreferredBackBufferHeight / 2 - (CurrentTeam.Base.SpriteSize.Y * Cell) / 2);
        }

        public string Name { get; set; }
        
        public int Padding { get; set; }

        public float Time { get; set; }
        public float TurnTime {
            get { return 30; }
        }

        public Button EndTurnButton { get; set; }

        public Point WorldSize { get; set; }
        public Point MaxSize {
            get { return new Point(100); }
        }

        public World World { get; set; }

        public Scene Parent { get; set; }

        public Team[] Teams { get; set; }

        public Team CurrentTeam { get; set; }

        public Popup TeamPopup { get; set; }
    }
}