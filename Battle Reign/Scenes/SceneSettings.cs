using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_Reign
{
    public class SceneSettings : Scene
    {
        public SceneSettings() : base("Settings")
        {

            //SettingsButtons = new List<Button>()
            //{
            //    //new Button(false, "icons/sound", new Vector2(1), (s, e) => , "square/small"),
            //    //new Button(false, "icons/video", new Vector2(2), (s, e) => , "square/small"),
            //    //new Button(false, "icons/video", new Vector2(3), (s, e) =>, "square/small")
            //};

            //TabManager Manager = new TabManager();
            //List<Option> videoOptions = new List<Option>
            //{
            //    new Slider("")
            //}
        }

        public override void Update(GameTime gt)
        {

        }

        public override void Draw(SpriteBatch sb)
        {

        }

        public Button SettingsButtons { get; set; }
    }
}