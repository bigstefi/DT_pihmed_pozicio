using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT_pihmed_pozicio.Data
{
    internal class CartesianCoordinates
    {
        private List<CartesianCoordinate> _cartesianCoordinates = new List<CartesianCoordinate>();

        public IReadOnlyCollection<CartesianCoordinate> Data { get { return _cartesianCoordinates; } }

        public CartesianCoordinates(string rawDataFilePath)
            : this(new PolarCoordinates(rawDataFilePath))
        {

        }

        public CartesianCoordinates(PolarCoordinates polarCoordinates)
        {
            foreach (PolarCoordinate data in polarCoordinates.Data)
            {
                _cartesianCoordinates.Add(ConvertCircleGeometryToCoordinateGeometry(data));
            }
        }

        private CartesianCoordinate ConvertCircleGeometryToCoordinateGeometry(PolarCoordinate polarCoordinate) // ToDo: would be enough if taking care only of the Xs and Ys, canvas data should not be here
        {
            // Transform to xy coordinates
            double x_rel = polarCoordinate.Distance * Math.Cos(PolarCoordinates.ToRadian(polarCoordinate.Angle));
            double y_rel = polarCoordinate.Distance * Math.Sin(PolarCoordinates.ToRadian(polarCoordinate.Angle));

            return new CartesianCoordinate(x_rel, y_rel);
        }
    }
}
