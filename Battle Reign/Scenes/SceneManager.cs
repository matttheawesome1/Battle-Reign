using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle_Reign
{
    public sealed class SceneManager : GameObject
    {
        public SceneManager(Game game)
        {
            Game = game;

            Scenes = new List<Scene>() { new SceneMainMenu() };
            CurrentScene = Scenes[0];

            Scenes.ForEach(x => x.Manager = this);
        }
        public SceneManager(Game game, Scene scene)
        {
            Game = game;

            Scenes = new List<Scene>() { scene };
            CurrentScene = Scenes[0];

            Scenes.ForEach(x => x.Manager = this);
        }
        public SceneManager(Game game, List<Scene> scenes)
        {
            Game = game;

            Scenes = scenes;
            CurrentScene = Scenes[0];

            Scenes.ForEach(x => x.Manager = this);
        }

        public void Update(GameTime gt)
        {
            Mouse.Hovering = false;

            CurrentScene.Update(gt);

            switch (CurrentScene.Action)
            {
                case (Action.CHANGESTATE):
                    switch (CurrentScene.State)
                    {
                        case (State.BACK):
                            SwitchScene(false);

                            break;
                        case (State.FORWARD):
                            SwitchScene(true);

                            break;
                        default:
                            break;
                    }

                    break;
                case (Action.EXIT):
                    Game.Exit();

                    break;
                default:
                    break;
            }
        }

        public void Draw(SpriteBatch sb)
        {
            CurrentScene.Draw(sb);
        }

        public Scene GetScene(string name)
        {
            foreach (Scene s in Scenes)
            {
                if (s.Name == name)
                {
                    return s;
                }
            }

            return null;
        }
        public void AddScene(Scene s)
        {
            Scenes.Add(s);
        }
        public void SwitchScene(bool forward)
        {
            //Console.WriteLine("Scenes: " + Scenes.ToString());

            if (forward)
            {
                if (Array.IndexOf(Scenes.ToArray(), CurrentScene) != Scenes.Count - 1)
                {
                    CurrentScene = Scenes[Array.IndexOf(Scenes.ToArray(), CurrentScene) + 1];
                }
            }
            else
            {
                if (Array.IndexOf(Scenes.ToArray(), CurrentScene) != 0)
                {
                    CurrentScene = Scenes[Array.IndexOf(Scenes.ToArray(), CurrentScene) - 1];
                }
            }

            CurrentScene.State = State.IDLE;
        }

        public Scene CurrentScene { get; set; }

        public Game Game { get; set; }

        public List<Scene> Scenes { get; set; }
    }
}