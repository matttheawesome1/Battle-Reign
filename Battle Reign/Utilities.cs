using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Battle_Reign {
    public static class Utilities {
        public static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

        static char[] consonantsList = { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };
        static char[] vowelsList = { 'a', 'e', 'i', 'o', 'u' };
        static string[] methods = { "V1c1v1c1v1c1v1", "C1c1v1c1v1", "C1v1c2v2", "C1v1c1v1c1v1", "V1c1v2", "C1v1c2v2c1", "C1v1c2v1c2v1c1", "C1c1v1c1v2c1", "C1v1c2v1c2", "C1c1v1c2 C1v2c1v1c1" };

        static Dictionary<char, int> consonants = new Dictionary<char, int>() {
            { 'b', 9 },
            { 'c', 9 },
            { 'd', 9 },
            { 'f', 9 },
            { 'g', 9 },
            { 'h', 9 },
            { 'j', 8 },
            { 'k', 8 },
            { 'l', 9 },
            { 'm', 9 },
            { 'n', 9 },
            { 'p', 9 },
            { 'q', 5 },
            { 'r', 8 },
            { 's', 9 },
            { 't', 9 },
            { 'v', 4 },
            { 'w', 4 },
            { 'x', 3 },
            { 'y', 7 },
            { 'z', 5 }
        };

        static Dictionary<char, int> consonantsReset;

        public static IEnumerable<String> SplitInParts(this String s, Int32 partLength) {
            if (s == null)
                throw new ArgumentNullException("s");
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", "partLength");

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

        public static Int32 Next(Int32 min, Int32 max) {
            uint scale = uint.MaxValue;

            while (scale == uint.MaxValue) {
                byte[] four_bytes = new byte[4];
                rand.GetBytes(four_bytes);

                scale = BitConverter.ToUInt32(four_bytes, 0);
            }

            return (int) (min + (max - min) * (scale / (double) uint.MaxValue));
        }

        public static float Next(float min, float max) {
            uint scale = uint.MaxValue;

            while (scale == uint.MaxValue) {
                byte[] four_bytes = new byte[4];
                rand.GetBytes(four_bytes);

                scale = BitConverter.ToUInt32(four_bytes, 0);
            }

            return (min + (max - min) * (scale / (float) uint.MaxValue));
        }

        public static float Distance(Point p1, Point p2) {
            return (float) Math.Abs(Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2)));
        }

        public static string RandomName() {
            string name = "", method = methods[Next(0, methods.Length)];
            int t;

            for (int i = 0; i < method.Length; i++) {
                if (method[i] == ' ') {
                    name += " ";
                } else {
                    for (int j = 0; i + 1 < method.Length && Int32.TryParse(method[i + 1].ToString(), out t) && j < Convert.ToInt32(method[i + 1].ToString()); j++) {
                        if (method[i].ToString().ToUpper() == "C") {
                            while (true) {
                                char temp = consonantsList[Next(0, consonants.Count)];

                                try {
                                    if (consonants[temp] > Next(0, 10)) {
                                        name += method[i].ToString().ToUpper() == method[i].ToString() ? temp.ToString().ToUpper() : temp.ToString();

                                        break;
                                    }
                                } catch (Exception) { }
                            }
                        } else if (method[i].ToString().ToUpper() == "V") {
                            name += method[i].ToString().ToUpper() == method[i].ToString() ? vowelsList[Next(0, vowelsList.Length)].ToString().ToUpper() : vowelsList[Next(0, vowelsList.Length)].ToString();
                        }
                    }
                }
            }

            return name;
        }
    }
}
