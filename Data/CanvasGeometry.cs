using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DT_pihmed_pozicio.Data
{
    internal class CanvasGeometry
    {
        private List<Path> _lines = new List<Path>();
        private Color _lineColor = Color.FromRgb(0, 0, 0);
        private Brush _brush = null;

        public IEnumerable<Path> Data { get { return _lines; } }

        public CanvasGeometry(IEnumerable<Point> points)
        {
            _lineColor = Color.FromRgb(0, 0, 0);
            _brush = new SolidColorBrush(_lineColor);

            foreach (Point p in points)
            {
                CreatePointGeometry(p);
            }
        }

        private void CreatePointGeometry(Point p) // ToDo: you are cheating with the method name, because actually you are drawing a +
        {
            double dxy = 2;
            Point p1 = new Point();
            Point p2 = new Point();
            Point p3 = new Point();
            Point p4 = new Point();

            p1.X = p.X + dxy;
            p1.Y = p.Y;
            p2.X = p.X - dxy;
            p2.Y = p.Y;
            p3.X = p.X;
            p3.Y = p.Y + dxy;
            p4.X = p.X;
            p4.Y = p.Y - dxy;

            CreateLineGeometry(p1, p2);
            CreateLineGeometry(p3, p4);
        }

        [Obsolete("Use the one with 2 parameters")]
        private void CreateLineGeometry(Point start, Point end, Color lineColor)
        {
            var lineGeom = new LineGeometry { StartPoint = start, EndPoint = end };

            Path linePath = new Path
            {
                Stroke = new SolidColorBrush(lineColor),
                StrokeThickness = 1,
                Data = lineGeom
            };

            _lines.Add(linePath);
        }

        private void CreateLineGeometry(Point start, Point end)
        {
            var lineGeom = new LineGeometry { StartPoint = start, EndPoint = end };

            Path linePath = new Path
            {
                Stroke = _brush,
                StrokeThickness = 1,
                Data = lineGeom
            };

            _lines.Add(linePath);
        }
    }
}
