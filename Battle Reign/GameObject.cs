using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Battle_Reign {
    public abstract class GameObject {
        public GameObject() {

        }

        public static void Initialize(ContentManager content, GraphicsDeviceManager graphics, Camera2D camera, GameMouse mouse) {
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
            FontXtraLarge = Content.Load<SpriteFont>("fonts/silkscreen/XtraLarge");
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
        public static SpriteFont FontXtraLarge { get; set; }
    }
}
