using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DT_pihmed_pozicio.Data;
using Microsoft.Win32;


namespace DT_pihmed_pozicio
{

    public partial class MainWindow : Window
    {
        #region Member variables
        public List<double> _angles = new List<double>();
        public List<double> _distances = new List<double>();
        public List<double> _relativeXs = new List<double>();
        public List<double> _relativeYs = new List<double>();
        public List<Point> _pointsRealTime = new List<Point>();
        public List<Path> _lines = new List<Path>();
        private CanvasCoordinates _canvasCoordinates = null;
        private CartesianCoordinates _cartesianCoordinates = null;
        private string _filePath = string.Empty;
        CanvasGeometry _canvasGeometry = null;
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();

            this.SizeChanged += OnSizeChanged;

            _filePath = ReadData();
            //ConvertCircleGeometryToCoordinateGeometry();
            //ConvertCoordinateGeometryToCanvasGeometry();

            _cartesianCoordinates = new CartesianCoordinates(_filePath);
            _canvasCoordinates = new CanvasCoordinates(_cartesianCoordinates, _canvasRealTime.Width, _canvasRealTime.Height);
            _canvasGeometry = new CanvasGeometry(_canvasCoordinates.Data);
            UpdateCanvasElements(_canvasGeometry.Data);
        }
        #endregion

        #region UI event handlers
        private void OnSizeChanged(object sender, SizeChangedEventArgs args)
        {
            _canvasRealTime.Width = args.NewSize.Width;
            _canvasRealTime.Height = args.NewSize.Height;

            //ConvertCoordinateGeometryToCanvasGeometry();
            _canvasCoordinates = new CanvasCoordinates(_cartesianCoordinates, _canvasRealTime.Width, _canvasRealTime.Height);
            _canvasGeometry = new CanvasGeometry(_canvasCoordinates.Data);
            UpdateCanvasElements(_canvasGeometry.Data);
        }
        #endregion

        #region Test data helpers
        private string ReadData()
        {
            // Configure open file dialog box
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, "TestData");
            openFileDialog.InitialDirectory = filePath;
            openFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";

            // Csv reading
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;

                ReadData(filePath);
            }

            return filePath;
        }

        private void ReadData(string filePath) // testable this way, no need for OpenFileDialog, you can directly pass the file full path and see if parsing works correctly
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
            {
                _angles.Clear();
                _distances.Clear();

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] values = line.Split(',');

                    _angles.Add(Convert.ToDouble(values[0]));
                    _distances.Add(Convert.ToDouble(values[1]));
                }
            }
        }
        #endregion

        #region Domain logic
        private void ConvertCircleGeometryToCoordinateGeometry() // ToDo: would be enough if taking care only of the Xs and Ys, canvas data should not be here
        {
            // Clear lists
            _relativeXs.Clear();
            _relativeYs.Clear();

            // Transform to xy coordinates
            double x_rel;
            double y_rel;
            for (int i = 0; i < _angles.Count(); i++)
            {
                x_rel = _distances[i] * Math.Cos(ToRadian(_angles[i]));
                _relativeXs.Add(x_rel);
                y_rel = _distances[i] * Math.Sin(ToRadian(_angles[i]));
                _relativeYs.Add(y_rel);
            }
        }
        #endregion

        private void ConvertCoordinateGeometryToCanvasGeometry()
        {
            _pointsRealTime.Clear();
            _lines.Clear();
            //_canvasRealTime.Children.Clear();

            // Display - zoom
            double xMax = Math.Max(_relativeXs.Max(), Math.Abs(_relativeXs.Min()));
            double yMax = Math.Max(_relativeYs.Max(), Math.Abs(_relativeYs.Min()));
            double zoom = Math.Max(xMax / (_canvasRealTime.Width - 20) * 2, yMax / (_canvasRealTime.Height - 20) * 2); // ToDo: I would not work here with margins, that is defined in the xaml
            // ToDo: decreasing width and height ends up in a multiplier which makes the image larger than the canvas itself (just think of having a pool of the canvas size)
            //       I would simply adjust the larger direction to the canvas size of that direction

            // Display - transform of coordinates
            double xc;
            double yc;
            Point point = new Point();

            for (int i = 0; i < _relativeXs.Count(); i++)
            {
                // ToDo: 0 of canvas should be xMin --> calculation should be done by a shift to 0 (by xMin) and then multiplied by the zoom factor,
                //       which should end up in canvas dimensions, so zoom above should be calculated by canvas dimensions/coordinate dimensions.
                //       Same vertically
                xc = _relativeXs[i] / zoom + _canvasRealTime.Width / 2; 
                yc = _canvasRealTime.Height / 2 - _relativeYs[i] / zoom;
                point.X = xc;
                point.Y = yc;
                _pointsRealTime.Add(point);
            }

            // Display on canvas
            for (int i = 0; i < _pointsRealTime.Count(); i++)
            {
                DrawPoint(_pointsRealTime[i]);
            }

            UpdateCanvasElements();
        }

        public double ToRadian(double degree)
        {
            // Transform angles from degrees to radian
            double radian = degree * Math.PI / 180;
            return radian;
        }

        // ToDo: you will not change the color for every point, so a member variable would make it (no need for Color.FromRgb calculation all the time)
        private void DrawLine(Point start, Point end, Color lineColor) 
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

        private void DrawPoint(Point p) // ToDo: you are cheating with the method name, because actually you are drawing a +
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

            DrawLine(p1, p2, Color.FromRgb(0, 0, 0));
            DrawLine(p3, p4, Color.FromRgb(0, 0, 0));
        }

        private void UpdateCanvasElements()
        {
            _canvasRealTime.Children.Clear();

            foreach (Path line in _lines)
            {
                _canvasRealTime.Children.Add(line);
            }
        }

        private void UpdateCanvasElements(IEnumerable<Path> lines)
        {
            _canvasRealTime.Children.Clear();

            foreach (Path line in lines)
            {
                _canvasRealTime.Children.Add(line);
            }
        }
    }
}
