using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle_Reign {
    public static class Debug {
        public static void Initialize() {
            DrawGUI = true;
            DrawFOW = true;
        }

        public static void Update(GameMouse mouse) {
            KeyboardState kState = Keyboard.GetState();

            if (mouse.CanType) {
                if (kState.IsKeyDown(Keys.F3)) Debug.DebugActive = !Debug.DebugActive;

                if (Debug.DebugActive) {
                    if (kState.IsKeyDown(Keys.G)) Debug.DrawGUI = !Debug.DrawGUI;
                    if (kState.IsKeyDown(Keys.F)) Debug.DrawFOW = !Debug.DrawFOW;
                    if (kState.IsKeyDown(Keys.D)) Debug.DiscoverOnClick = !Debug.DiscoverOnClick;
                }
            }
        }

        public static string DebugMenu {
            get {
                int width = 18;

                return String.Format("{0,0} - {1,-" + width + "} : {2,-5} \n", "G", "DRAW GUI", DrawGUI) + 
                    String.Format("{0,0} - {1,-" + width + "} : {2,-5} \n", "F", "DRAW FOG OF WAR", DrawFOW) +
                    String.Format("{0,0} - {1,-" + width + "} : {2,-5} \n", "D", "DISCOVER ON CLICK", DiscoverOnClick);
            }
        }

        public static bool DebugActive { get; set; }

        public static bool DrawGUI { get; set; }
        public static bool DiscoverOnClick { get; set; }
        public static bool DrawFOW { get; set; }
    }
}
