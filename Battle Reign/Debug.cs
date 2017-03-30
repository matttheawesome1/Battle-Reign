using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Battle_Reign {
    public static class Debug {
        public static void Initialize() {
            CPU = new PerformanceCounter("Process", "% Processor Time", Process.GetCurrentProcess().ProcessName);
            CPU.BeginInit();
        }

        public static void Update(GameTime gt, GameMouse mouse) {
            KeyboardState kState = Keyboard.GetState();

            if (mouse.CanType) {
                if (kState.IsKeyDown(Keys.F3)) Debug.DebugActive = !Debug.DebugActive;

                if (Debug.DebugActive) {
                    if (kState.IsKeyDown(Keys.G)) Debug.DrawGUI = !Debug.DrawGUI;
                    if (kState.IsKeyDown(Keys.F)) Debug.DrawFOW = !Debug.DrawFOW;
                    if (kState.IsKeyDown(Keys.D)) Debug.DiscoverOnClick = !Debug.DiscoverOnClick;
                }
            }

            Time += (float) gt.ElapsedGameTime.TotalSeconds;

            if (Time > RefreshTime) {
                //CPUMessage = (Process.GetCurrentProcess().).ToString();

                Time = 0;
            }
        }

        public static string DebugMenu {
            get {
                int width = 18;

                return string.Format("{0,0} - {1,-" + width + "} : {2,-5} \n", "G", "DRAW GUI", DrawGUI) + 
                    string.Format("{0,0} - {1,-" + width + "} : {2,-5} \n", "F", "DRAW FOG OF WAR", DrawFOW) +
                    string.Format("{0,0} - {1,-" + width + "} : {2,-5} \n", "D", "DISCOVER ON CLICK", DiscoverOnClick) +
                    string.Format("{0,0} - {1,-" + width + "} : {2,-5} \n", "P", "PERFORMANCE", CPUMessage);
            }
        }

        public static PerformanceCounter CPU { get; set; }

        public static string CPUMessage { get; set; }

        public static float Time { get; set; }
        public static float RefreshTime {
            get { return 1; }
        }

        public static bool DebugActive { get; set; }

        public static bool DrawGUI { get; set; } = true;
        public static bool DiscoverOnClick { get; set; }
        public static bool DrawFOW { get; set; } = true;
        public static bool DrawPerformance { get; set; }
    }
}
