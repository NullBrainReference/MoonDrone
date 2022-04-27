using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoonDrone
{
    public enum CellType { Vulcane, Terrain }
    internal abstract class PlanetCell
    {
        public CellType cellType;
        public double Temperature 
        { 
            get { return temperature; 
            } 
            set { temperature = value; } 
        }
        private double temperature;
        public Button parent;
        public PlanetCell()
        {

        }
        public PlanetCell(double temperature)
        {
            this.temperature = temperature;
        }
        public virtual void EditTemp(double t1, double t2, double t3, double t4)
        {
            this.temperature = (t1 + t2 + t3 + t4)/4;
        }
    }
}
