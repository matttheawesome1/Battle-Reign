using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle_Reign {
    public class UnitWorker : Unit {
        public UnitWorker(Point coordinates, World world) : base(4, 3, coordinates, new Point(SpritesheetSize.X - 12, 6), new Point(3), new Point(3), world) {

        }
    }
}