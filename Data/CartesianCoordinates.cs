using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT_pihmed_pozicio.Data
{
    internal class CartesianCoordinates
    {
        #region Member variables
        private List<CartesianCoordinate> _cartesianCoordinates = new List<CartesianCoordinate>();
        #endregion

        #region Properties
        public IReadOnlyCollection<CartesianCoordinate> Data { get { return _cartesianCoordinates; } }
        #endregion

        #region Constructor
        public CartesianCoordinates(PolarCoordinates polarCoordinates)
        {
            foreach (PolarCoordinate data in polarCoordinates.Data)
            {
                _cartesianCoordinates.Add(ToCartesian(data));
            }
        }

        public CartesianCoordinates(string rawDataFilePath)
            : this(new PolarCoordinates(rawDataFilePath))
        {

        }
        #endregion

        #region APIs
        public static CartesianCoordinate ToCartesian(PolarCoordinate polarCoordinate)
        {
            // Transform to xy coordinates
            double x = polarCoordinate.Distance * Math.Cos(PolarCoordinates.ToRadian(polarCoordinate.Angle));
            double y = polarCoordinate.Distance * Math.Sin(PolarCoordinates.ToRadian(polarCoordinate.Angle));

            return new CartesianCoordinate(x, y);
        }
        #endregion
    }
}
