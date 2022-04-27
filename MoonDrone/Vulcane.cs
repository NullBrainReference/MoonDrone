using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MoonDrone
{
    internal class Vulcane: PlanetCell
    {
        public double lifeTime;

        public Vulcane(int lifeTime)
        {
            this.cellType = CellType.Vulcane;
            this.Temperature = 30;
            this.lifeTime = lifeTime;
        }

        public PlanetCell ShutDown()
        {
            lifeTime -= 1;
            if (lifeTime <= 0) { 
            Terrain terrain = new Terrain();
                terrain.parent = this.parent;
                terrain.parent.BackColor = Color.LightGray;
                return terrain;
            }
            else
                return this;
        }
        public override void EditTemp(double t1, double t2, double t3, double t4)
        {
            
        }
    }
}
