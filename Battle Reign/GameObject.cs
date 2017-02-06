using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Battle_Reign {
    public abstract class GameObject {
        public GameObject() {

        }

        public static void Initialize(ContentManager content, GraphicsDeviceManager graphics, Camera2D camera, GameMouse mouse) {
            ObjectsClicked = new List<GameObject>();
            Types = new List<Type>() { typeof(Button), typeof(Card), typeof(CardFarm), typeof(CardMine), typeof(CardQuarry), typeof(Tile), typeof(GrassTile), typeof(WaterTile), typeof(UndiscoveredTile), typeof(DirtTile), };

            Content = content;
            Graphics = graphics;
            Camera = camera;
            Mouse = mouse;

            BlankPixel = Content.Load<Texture2D>("buttons/blankPixel");
            Spritesheet = Content.Load<Texture2D>("tiles/spritesheet");

            FontSmall = Content.Load<SpriteFont>("fonts/silkscreen/small");
            FontMedium = Content.Load<SpriteFont>("fonts/silkscreen/medium");
            FontLarge = Content.Load<SpriteFont>("fonts/silkscreen/large");
            FontTiny = Content.Load<SpriteFont>("fonts/silkscreen/tiny");
        }

        public virtual void Click() {

        }

        public static void ClickObject() {
            if (ObjectsClicked.Count > 0) {
                List<GameObject> inOrder = new List<GameObject>();

                inOrder = ObjectsClicked.OrderBy(x => x.Level).ToList();

                for (int i = 0; i < Types.Count; i++) {
                    for (int j = 0; j < ObjectsClicked.Count; j++) {
                        if (ObjectsClicked[j].GetType() == Types[i]) {
                            inOrder.Add(ObjectsClicked[j]);
                        }
                    }
                }

                if (inOrder.Count > 0) {
                    foreach (GameObject g in inOrder) {
                        //Console.WriteLine(g.GetType());
                    }

                    inOrder[0].Click();
                }
            }

            ObjectsClicked = new List<GameObject>();
        }

        public int Level { get; set; }

        public static float TileLayer {
            get { return .1f; }
        }
        public static float BuildingLayer {
            get { return .2f; }
        }
        public static float UnitLayer {
            get { return .11f; }
        }

        public static int TileWidth { get; set; }
        public static int Cell {
            get { return TileWidth / 3; }
        }

        public static Point SpritesheetSize {
            get { return new Point(Spritesheet.Width / (TileWidth / 3), Spritesheet.Height / (TileWidth / 3)); }
        }

        public static ContentManager Content { get; set; }
        public static GraphicsDeviceManager Graphics { get; set; }
        public static Camera2D Camera { get; set; }
        public static GameMouse Mouse { get; set; }

        public static Texture2D BlankPixel { get; set; }
        public static Texture2D Spritesheet { get; set; }

        public static SpriteFont FontLarge { get; set; }
        public static SpriteFont FontMedium { get; set; }
        public static SpriteFont FontSmall { get; set; }
        public static SpriteFont FontTiny { get; set; }

        public static List<GameObject> ObjectsClicked { get; set; }
        public static List<Type> Types { get; set; }
    }
}
