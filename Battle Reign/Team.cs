using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Battle_Reign {
    public class Team : GameObject {
        public Team(string name, Vector2 basePos, Tile[,] tiles) {
            Name = name;

            Gold = 100;
            GoldIncrease = 5;
            Food = 100;
            GoldIncrease = 0;

            Cards = new List<Card>(7);

            Tiles = tiles;
            UndiscoveredTiles = new Tile[tiles.GetLength(0), Tiles.GetLength(1)];

            SpriteSize = new Point(3, 4);
            SpriteCoords = new Point(SpritesheetSize.X - 6, 0);

            Color = new Color(Utilities.Next(0, 255), Utilities.Next(0, 255), Utilities.Next(0, 255));
            Console.WriteLine(Color.ToString());

            Base = new Base(basePos, SpriteCoords, this);

            for (int i = 0; i < (StartingCards > Cards.Capacity ? Cards.Capacity : StartingCards); i++) {
                int a = Utilities.Next(0, 3);

                if (a == 1)
                    Cards.Add(new CardMine(TileWidth, 35, Cards, tiles));
                else if (a == 2)
                    Cards.Add(new CardFarm(TileWidth, 35, Cards, tiles));
                else
                    Cards.Add(new CardQuarry(TileWidth, 35, Cards, tiles));
            }

            GenerateFOW();
        }

        public void Update(GameTime gt) {
            List<Card> hoveringCards = new List<Card>();

            Cards.ForEach(x => x.CheckHovering(hoveringCards));

            hoveringCards = hoveringCards.OrderBy(x => x.Index).ToList();
            Console.WriteLine();

            if (hoveringCards.Count > 0) {
                if (HoveringCard != null) {
                    Cards.ForEach(x => x.Update(gt, false));
                    hoveringCards[hoveringCards.Count - 1].Update(gt, true);
                } else {
                    Cards.ForEach(x => x.Update(gt, hoveringCards[hoveringCards.Count - 1] == x));
                }

                HoveringCard = hoveringCards[hoveringCards.Count - 1];
            } else {
                Cards.ForEach(x => x.Update(gt, false));
                HoveringCard = null;
            }

            UndiscoveredTiles.Cast<Tile>().ToList().ForEach(x => x.Update(gt));
        }

        public void Draw(SpriteBatch sb) {
            foreach (Tile t in UndiscoveredTiles) {
                t.Draw(sb);
            }

            int offset = 3, padding = 35;

            sb.Draw(Spritesheet, new Vector2(Camera.Position.X + padding, Camera.Position.Y + padding), new Rectangle(new Point(0, (SpritesheetSize.Y - 16) * Cell), new Point(Cell)), Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);

            sb.DrawString(FontLarge, "GOLD: " + Gold + "(+" + GoldIncrease + ") // FOOD: " + Food + "(+" + FoodIncrease + ")", new Vector2(Camera.Position.X + padding + Cell * 3, Camera.Position.Y + padding), Color.Black);
            sb.DrawString(FontLarge, "GOLD: " + Gold + "(+" + GoldIncrease + ") // FOOD: " + Food + "(+" + FoodIncrease + ")", new Vector2(Camera.Position.X + padding - offset + Cell * 3, Camera.Position.Y + padding - offset), Color.White);

            Cards.ForEach(x => x.Draw(sb));
            if (HoveringCard != null) HoveringCard.Draw(sb);
        }

        public void GenerateFOW() {
            for (int i = 0; i < UndiscoveredTiles.GetLength(0); i++) {
                for (int j = 0; j < UndiscoveredTiles.GetLength(1); j++) {
                    if (i <= Buffer - 1 || i >= Tiles.GetLength(0) - Buffer || j <= Buffer - 1 || j >= Tiles.GetLength(1) - Buffer) {
                        UndiscoveredTiles[i, j] = new UndiscoveredTile(true, new Vector2(i * TileWidth, j * TileWidth), UndiscoveredTiles);
                    } else {
                        UndiscoveredTiles[i, j] = new UndiscoveredTile(false, new Vector2(i * TileWidth, j * TileWidth), UndiscoveredTiles);
                    }
                }
            }

            UndiscoveredTiles.Cast<UndiscoveredTile>().ToList().ForEach(x => x.Fix());
        }

        public string Name { get; set; }

        public int Gold { get; set; }
        public int GoldIncrease { get; set; }
        public int Food { get; set; }
        public int FoodIncrease { get; set; }
        public int Buffer {
            get { return 2; }
        }

        public static int StartingCards {
            get { return 15; }
        }

        public bool Turn { get; set; }

        public Texture2D BaseImage { get; set; }

        public Point SpriteCoords { get; set; }
        public Point SpriteSize { get; set; }

        public Color Color { get; set; }

        public Card HoveringCard { get; set; }

        public Base Base { get; set; }

        public List<Card> Cards { get; set; }

        public Tile[,] Tiles { get; set; }
        public Tile[,] UndiscoveredTiles { get; set; }
    }
}