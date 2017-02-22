using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Battle_Reign {
    public static class SoundManager {
        public static SoundEffectInstance musicBackground;

        public static void Initialize(ContentManager c) {
            SoundEffect se = c.Load<SoundEffect>("music/musicBackground");

            musicBackground = se.CreateInstance();
            musicBackground.Volume = 1f;
            musicBackground.Pan = 0f;
            musicBackground.Pitch = 0f;
        }

        public static void PlayBackgroundMusic(Slider slider) {
            musicBackground.Play();
        }
    }
}
