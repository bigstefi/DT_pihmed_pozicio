using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT_pihmed_pozicio.Data
{
    internal class PolarCoordinates
    {
        #region Member variables
        private List<PolarCoordinate> _polarCoordinates = new List<PolarCoordinate>();
        #endregion

        #region Properties
        public IReadOnlyCollection<PolarCoordinate> Data { get { return _polarCoordinates; } }
        #endregion

        #region Constructor
        public PolarCoordinates(RawData rawData)
        {
            foreach (string line in rawData.Data)
            {
                string[] values = line.Split(',');

                // ToDo: add some error handling to handle malformed data

                _polarCoordinates.Add(new PolarCoordinate(Convert.ToDouble(values[0]), Convert.ToDouble(values[1])));
            }
        }

        public PolarCoordinates(string rawDataFilePath)
            : this(new RawData(rawDataFilePath))
        {

        }
        #endregion

        #region APIs
        public static double ToRadian(double degree)
        {
            // Transform angles from degrees to radian
            double radian = degree * Math.PI / 180;
            
            return radian;
        }
        #endregion
    }
}
