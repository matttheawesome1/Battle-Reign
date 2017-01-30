using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Battle_Reign {
    public class Unit {
        public Unit(int visionRange) {
            VisionRange = visionRange;
        }

        public int VisionRange { get; set; }
    }
}
