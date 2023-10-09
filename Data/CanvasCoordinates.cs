using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DT_pihmed_pozicio.Data
{
    internal class CanvasCoordinates
    {
        #region Member variables
        private double _canvasWidth = 0;
        private double _canvasHeight = 0;
        private double _marginLeft = 0;
        private double _marginTop = 0;
        private double _marginRight = 0;
        private double _marginBottom = 0;
        private static double _correction = 0.85; // for testability only
        private List<Point> _canvasCoordinates = null;
        #endregion

        #region Properties
        public IReadOnlyCollection<Point> Data { get { return _canvasCoordinates; } }
        internal static double Correction { set { _correction = value; } }
        #endregion

        #region Constructor
        public CanvasCoordinates(string rawDataFilePath, double canvasWidth, double canvasHeight)
            : this(new CartesianCoordinates(rawDataFilePath), canvasWidth, canvasHeight, 0, 0, 0, 0)
        { }

        public CanvasCoordinates(string rawDataFilePath,
            double canvasWidth, double canvasHeight, double marginLeft, double marginRight, double marginTop, double marginBottom)
            : this(new CartesianCoordinates(rawDataFilePath), canvasWidth, canvasHeight, marginLeft, marginRight, marginTop, marginBottom)
        { }

        public CanvasCoordinates(CartesianCoordinates cartesianCoordinates, 
            double canvasWidth, double canvasHeight, double marginLeft = 0, double marginRight = 0, double marginTop = 0, double marginBottom = 0) 
        {
            // ToDo: I don't know yet why, but without multiplying by 0.85, the right and bottom lines go out of the canvas. Feel free to correct any of the formulas below 
            _canvasWidth = canvasWidth * _correction;
            _canvasHeight = canvasHeight * _correction;
            _marginLeft = marginLeft;
            _marginRight = marginRight;
            _marginTop = marginTop;
            _marginBottom = marginBottom;

            double realWidth = _canvasWidth - _marginLeft - _marginRight;
            double realHeight = _canvasHeight - _marginTop - _marginBottom;
            double coordinatesXMin = cartesianCoordinates.Data.Select(cartesianCoordinate => cartesianCoordinate.X).Min();
            double coordinatesXMax = cartesianCoordinates.Data.Select(cartesianCoordinate => cartesianCoordinate.X).Max();
            double coordinatesYMin = cartesianCoordinates.Data.Select(cartesianCoordinate => cartesianCoordinate.Y).Min();
            double coordinatesYMax = cartesianCoordinates.Data.Select(cartesianCoordinate => cartesianCoordinate.Y).Max();
            double shiftX = _marginLeft;
            double shiftY = _marginTop;
            double zoomX = realWidth / (coordinatesXMax - coordinatesXMin);
            double zoomY = realHeight / (coordinatesYMax - coordinatesYMin);
            double zoom = 0;

            if(zoomX < zoomY)
            {
                zoom = zoomX;

                shiftY += (realHeight - zoom * (coordinatesYMax - coordinatesYMin)) / 2;
            }
            else 
            {
                zoom = zoomY;

                shiftX += (realWidth - zoom * (coordinatesXMax - coordinatesXMin)) / 2;
            }

            //_canvasCoordinates = new List<Point>(cartesianCoordinates.Data.Select(cartesianCoordinate 
            //    => new Point(shiftX + (-coordinatesXMin + cartesianCoordinate.X) * zoom, shiftY + (-coordinatesYMin + cartesianCoordinate.Y) * zoom)));

            _canvasCoordinates = new List<Point>();
            foreach(CartesianCoordinate cartesianCoordinate in cartesianCoordinates.Data)
            {
                Point p = new Point(shiftX + (-coordinatesXMin + cartesianCoordinate.X) * zoom, shiftY + (-coordinatesYMin + cartesianCoordinate.Y) * zoom);

                _canvasCoordinates.Add(p);
            }
        }
        #endregion
    }
}
