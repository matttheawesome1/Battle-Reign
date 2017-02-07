using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle_Reign {
    public class UnitWarrior : Unit {
        public UnitWarrior(Point coords, World world) : base(4, 3, coords, new Point(SpritesheetSize.X - 12, 0), new Point(3), new Point(3), world) {

        }
    }
}