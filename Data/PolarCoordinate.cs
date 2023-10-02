using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT_pihmed_pozicio.Data
{
    internal class PolarCoordinate // ToDo: could be stored as Point as well, or as Tuple, but is more intuitive this way
    {
        private double _angle;
        private double _distance;

        public double Angle { get { return _angle; } internal set { _angle = value; } }
        public double Distance { get { return _distance; } internal set { _distance = value; } }

        public PolarCoordinate(double angle, double distance)
        {
            _angle = angle;
            _distance = distance;
        }
    }
}
