using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Battle_Reign {
    public class TabManager : GameObject {
        public TabManager(List<Tab> tabs) {
            Tabs = tabs;

            SelectedTab = Tabs[0];
        }
        public TabManager() {
            Tabs = new List<Tab>();
        }

        public void Update(GameTime gt) {
            Tabs.ForEach(x => x.Update(gt));

            foreach (Tab t in Tabs)
                if (t.Selected)
                    SelectedTab = t;
        }

        public void Draw(SpriteBatch sb) {
            SelectedTab.Draw(sb);

            Tabs.ForEach(x => x.DrawButton(sb));
        }

        public void SetTabs (List<Tab> tabs) {
            Tabs = tabs;
            SelectedTab = Tabs[0];
        }

        public List<Tab> Tabs { get; set; }
        public Tab SelectedTab { get; set; }
    }
}
