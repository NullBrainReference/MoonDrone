using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonDrone
{
    internal class Terrain: PlanetCell
    {
        public Terrain()
        {
            this.cellType = CellType.Terrain;
        }
    }
}
