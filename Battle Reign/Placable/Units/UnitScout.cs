using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battle_Reign {
    public class UnitScout : Unit {
        public UnitScout(Point coords, World world) : base(6, 5, 75, 25, coords, new Point(SpritesheetSize.X - 12, 3), new Point(3), new Point(3), world) {

        }
    }
}
