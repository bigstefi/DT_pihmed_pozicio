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
        double _canvasWidth = 0;
        double _canvasHeight = 0;
        double _marginLeft = 0;
        double _marginTop = 0;
        double _marginRight = 0;
        double _marginBottom = 0;
        private List<Point> _canvasCoordinates = null;

        public IReadOnlyCollection<Point> Data { get { return _canvasCoordinates; } }

        public CanvasCoordinates(string rawDataFilePath,
            double canvasWidth, double canvasHeight, double marginLeft = 0, double marginRight = 0, double marginTop = 0, double marginBottom = 0)
            : this(new CartesianCoordinates(rawDataFilePath), canvasWidth, canvasHeight, marginLeft, marginRight, marginTop, marginBottom)
        { }

        public CanvasCoordinates(CartesianCoordinates cartesianCoordinates, 
            double canvasWidth, double canvasHeight, double marginLeft = 0, double marginRight = 0, double marginTop = 0, double marginBottom = 0) 
        {
            // ToDo: I don't know yet why, but without multiplying by 0.85, the right and bottom lines go out of the canvas. Feel free to correct any of the formulas below 
            _canvasWidth = canvasWidth * 0.85;
            _canvasHeight = canvasHeight * 0.85;
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
            //    => new Point(shiftX + (-coordinatesXMin + cartesianCoordinate.X) * zoom,  shiftY + (-coordinatesYMin + cartesianCoordinate.Y) * zoom)));

            _canvasCoordinates = new List<Point>();
            foreach(CartesianCoordinate cartesianCoordinate in cartesianCoordinates.Data)
            {
                Point p = new Point(shiftX + (-coordinatesXMin + cartesianCoordinate.X) * zoom, shiftY + (-coordinatesYMin + cartesianCoordinate.Y) * zoom);

                _canvasCoordinates.Add(p);
            }
        }
    }
}
