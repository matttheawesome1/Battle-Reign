using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle_Reign {
    public sealed class SceneManager : GameObject {
        public SceneManager(Game game) {
            Game = game;

            Scenes = new List<Scene>() { new SceneMainMenu() };
            CurrentScene = Scenes[0];
        }
        public SceneManager(Game game, Scene scene) {
            Game = game;

            Scenes = new List<Scene>() { scene };
            CurrentScene = Scenes[0];
        }

        public void Update(GameTime gt) {
            CurrentScene.Update(gt);

            switch (CurrentScene.Action) {
                case (Action.CHANGESTATE):
                    CurrentScene = new SceneGame();

                    break;
                case (Action.EXIT):
                    Game.Exit();

                    break;
                default:
                    break;
            }
        }

        public void Draw(SpriteBatch sb) {
            CurrentScene.Draw(sb);
        }

        public Scene GetScene(string name) {
            foreach(Scene s in Scenes) {
                if (s.Name == name) {
                    return s;
                }
            }

            return null;
        }
        public void AddScene(Scene s) {
            Scenes.Add(s);
        }

        public Scene CurrentScene { get; set; }

        public Game Game { get; set; }

        public List<Scene> Scenes { get; set; }
    }
}