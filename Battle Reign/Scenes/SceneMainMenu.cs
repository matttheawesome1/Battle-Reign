using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Battle_Reign {
    public class SceneMainMenu : Scene {
        public SceneMainMenu() : base("Main Menu") {
            //Set up the buttons for the main menu
            //The vector 2 will scale with resolution and place them in the left, middle, and right sides of the screen.
            Buttons = new List<Button>() {
                new Button(false, "play/small", new Vector2(Graphics.PreferredBackBufferWidth * .25f - (int) SquareButtonSize.Small / 2, Graphics.PreferredBackBufferHeight / 2), (s, e) => ChangeState(new SceneGame()), "square/small"),
                new Button(false, "settings/small", new Vector2(Graphics.PreferredBackBufferWidth * .5f - (int) SquareButtonSize.Small / 2, Graphics.PreferredBackBufferHeight / 2), null, "square/small"),
                new Button(false, "exit/small", new Vector2(Graphics.PreferredBackBufferWidth * .75f - (int) SquareButtonSize.Small / 2, Graphics.PreferredBackBufferHeight / 2), (s, e) => Action = Action.EXIT, "square/small"),
            };

            //Load the background picture for the game.
            Background = Content.Load<Texture2D>("backgrounds/mainmenu");
        }

        //Change the state of the game, i.e. if the user selects the play button, go to the game scene.
        public void ChangeState(Action action, State state)
        {
            Action = action;
            State = state;
        }
        public void ChangeState(Scene scene)
        {
            Manager.Scenes.Insert(Manager.Scenes.IndexOf(Manager.CurrentScene) + 1, scene);

            Action = Action.CHANGESTATE;
            State = State.FORWARD;
        }

        //Update the mainmenu, continues to draw to the screen.
        public override void Update(GameTime gt) {
            Buttons.ForEach(x => x.Update(gt));

            base.Update(gt);
        }

        //Draw the background first.
        //The text second.
        //And lastly the list of buttons.
        public override void Draw(SpriteBatch sb) {
            sb.Draw(Background, new Vector2(Graphics.PreferredBackBufferWidth / 2 - Background.Width / 2, Graphics.PreferredBackBufferHeight / 2 - Background.Height / 2), Color.White);
            sb.DrawString(Font90, "Battle Reign", new Vector2((Graphics.PreferredBackBufferWidth / 2) - Font90.MeasureString("Battle Reign").X / 2, (Graphics.PreferredBackBufferHeight / 2) - 250), Color.White);
            Buttons.ForEach(x => x.Draw(sb));
        }

        //List of buttons for the user to interact with
        public List<Button> Buttons { get; set; }

        //The image file for the background.
        public Texture2D Background { get; set; }
    }

    //Gets the size of buttons, these are for the square buttons so the sides are all equal.
    public enum SquareButtonSize
    {
        Tiny = 50,
        Small = 80,
        Medium = 160,
        Large = 240,
    }
}