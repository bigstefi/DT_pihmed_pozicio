using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT_pihmed_pozicio.Data
{
    internal class PolarCoordinate // ToDo: could be stored as Point as well, or as Tuple, but is more intuitive this way
    {
        #region Member variables
        private double _angle;
        private double _distance;
        #endregion

        #region Properties
        public double Angle { get { return _angle; } internal set { _angle = value; } }
        public double Radian { get { return PolarCoordinates.ToRadian(_angle); } }
        public double Distance { get { return _distance; } internal set { _distance = value; } }
        #endregion

        #region Constructor
        public PolarCoordinate(double angle, double distance)
        {
            _angle = angle;
            _distance = distance;
        }
        #endregion
    }
}
