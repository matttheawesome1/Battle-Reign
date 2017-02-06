using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Reign {
    public abstract class Tile : GameObject {
        public Tile(Texture2D image, Vector2 position, Tile[,] tiles, Point spriteCoords, Point spriteSize) {
            Level = 3;

            Spritesheet = image;
            Undiscovered = Content.Load<Texture2D>("tiles/undiscovered");
            Position = position;

            SpriteCoords = spriteCoords;
            SpriteSize = spriteSize;

            Discovered = false;
            LeftButtonClicked = false;
            RightButtonClicked = false;
            InView = false;
            Highlight = false;

            Tiles = tiles;

            Coordinates = new Point((int) position.X / TileWidth, (int) position.Y / TileWidth);

            Hitbox = new Rectangle(Position.ToPoint(), new Point(TileWidth));

            LeftButtonClick += (s, e) => Fix();
        }

        public virtual void Update(GameTime gt) {
            Hovering = Hitbox.Intersects(Mouse.Hitbox);
            MouseState state = Mouse.GetState();

            if (Hovering) {
                Mouse.Hovering = true;

                if (state.LeftButton == ButtonState.Pressed) LeftButtonClicked = true;

                if (LeftButtonClicked && LeftButtonClick != null && Hovering && state.LeftButton == ButtonState.Released) {
                    GameObject.ObjectsClicked.Add(this);
                }
                if (LeftButtonClicked && !Hovering && state.LeftButton == ButtonState.Released) {
                    LeftButtonClicked = false;
                }
            }
        }

        public virtual void Draw(SpriteBatch sb) {
            
        }

        public virtual void Draw(SpriteBatch sb, Vector2 origClick) {
            sb.Draw(BlankPixel, new Rectangle(new Vector2(origClick.X - origClick.X % TileWidth, origClick.Y - origClick.Y % TileWidth).ToPoint(), new Point(4, TileWidth)), Color.White);
            sb.Draw(BlankPixel, new Rectangle(new Vector2(origClick.X - origClick.X % TileWidth, origClick.Y - origClick.Y % TileWidth).ToPoint(), new Point(TileWidth, 4)), Color.White);
            sb.Draw(BlankPixel, new Rectangle(new Vector2(origClick.X - origClick.X % TileWidth + TileWidth - 4, origClick.Y - origClick.Y % TileWidth).ToPoint(), new Point(4, TileWidth)), Color.White);
            sb.Draw(BlankPixel, new Rectangle(new Vector2(origClick.X - origClick.X % TileWidth, origClick.Y - origClick.Y % TileWidth + TileWidth - 4).ToPoint(), new Point(TileWidth, 4)), Color.White);
        }

        public virtual void Fix() {

        }

        public override void Click() {
            LeftButtonClick(this, EventArgs);
            LeftButtonClicked = false;

            Fix();
        }

        public void Discover(bool inView) {
            Discovered = true;

            int radius = 4 * TileWidth + TileWidth;

            for (int i = 0; i < Tiles.GetLength(0); i++) {
                for (int j = 0; j < Tiles.GetLength(1); j++) {
                    if (Utilities.Distance(Hitbox.Center, Tiles[i, j].Hitbox.Center) < radius){
                        if (inView) Tiles[i, j].InView = true;

                        Tiles[i, j].Discovered = true;
                    }
                }
            }

            //Tiles.Cast<Tile>().ToList().ForEach(x => x.Fix());
        }

        public bool IsHovering {
            get { return Mouse.Hitbox.Intersects(Hitbox); }
        }

        public delegate void Pressed(object sender, EventArgs e);
        public EventArgs EventArgs;
        public EventHandler LeftButtonClick;

        public bool Discovered { get; set; }
        public bool InView { get; set; }

        public bool LeftButtonClicked { get; set; }
        public bool RightButtonClicked { get; set; }
        public bool Hovering { get; set; }
        public bool Highlight { get; set; }
        
        public Texture2D Undiscovered { get; set; }
        public Vector2 Position { get; set; }

        public Rectangle Hitbox { get; set; }

        public Point SpriteCoords { get; set; }
        public Point SpriteSize { get; set; }
        public Point Coordinates { get; set; }

        public int X {
            get { return Coordinates.X; }
        }
        public int Y {
            get { return Coordinates.Y; }
        }

        public Tile[,] Tiles { get; set; }
    }
}