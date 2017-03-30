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
            SettingsButtons = new List<Button>()
            {
                new Button(true, )
            };
        }

        public override void Update(GameTime gt)
        {

        }

        public override void Draw(SpriteBatch sb)
        {

        }

    public List<Button> SettingsButtons { get; set; }
    }
}