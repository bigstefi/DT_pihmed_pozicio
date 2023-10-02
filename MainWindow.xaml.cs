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
using Microsoft.Win32;


namespace DT_pihmed_pozicio
{

    public partial class MainWindow : Window
    {
        public List<double> angle = new List<double>();
        public List<double> distance = new List<double>();
        public List<double> x_relative = new List<double>();
        public List<double> y_relative = new List<double>();
        public List<Point> points_real_time = new List<Point>();
        public List<Path> _lines = new List<Path>();

        public MainWindow()
        {
            InitializeComponent();

            read_data_from_file();
            calibration();
            
        }

        public void read_data_from_file()
        {
            // Configure open file dialog box
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";

            // Csv reading
            if (OFD.ShowDialog() == true)
            {
                string filepath = OFD.FileName;
                System.IO.StreamReader sr = new System.IO.StreamReader(filepath);
                angle.Clear();
                distance.Clear();
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var values = line.Split(',');
                    angle.Add(Convert.ToDouble(values[0]));
                    distance.Add(Convert.ToDouble(values[1]));
                }
            }
        }

        public void calibration()
        {
            // Clear lists
            x_relative.Clear();
            y_relative.Clear();
            points_real_time.Clear();
            canvas_real_time.Children.Clear();

            // Transform to xy coordinates
            double x_rel;
            double y_rel;
            for (int i = 0; i < angle.Count(); i++)
            {
                x_rel = distance[i] * Math.Cos(rad(angle[i]));
                x_relative.Add(x_rel);
                y_rel = distance[i] * Math.Sin(rad(angle[i]));
                y_relative.Add(y_rel);
            }
            calibration_drawing();
        }

        private void calibration_drawing()
        {
            // Display - zoom
            double x_max = Math.Max(x_relative.Max(), Math.Abs(x_relative.Min()));
            double y_max = Math.Max(y_relative.Max(), Math.Abs(y_relative.Min()));
            double zoom = Math.Max(x_max / (canvas_real_time.Width - 20) * 2, y_max / (canvas_real_time.Height - 20) * 2);


            // Display - transform of coordinates

            double xc;
            double yc;
            Point _point = new Point();

            for (int i = 0; i < x_relative.Count(); i++)
            {
                xc = x_relative[i] / zoom + canvas_real_time.Width / 2;
                yc = canvas_real_time.Height / 2 - y_relative[i] / zoom;
                _point.X = xc;
                _point.Y = yc;
                points_real_time.Add(_point);
            }

            // Display on canvas
            for (int i = 0; i < points_real_time.Count(); i++)
            {
                DrawPoint(points_real_time[i]);
            }
            UpdateCanvasElements();
        }

        public double rad(double degree)
        {
            // Transform angles from degrees to radian
            double radian = degree * Math.PI / 180;
            return radian;
        }

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

        private void DrawPoint(Point p)
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
            canvas_real_time.Children.Clear();
            foreach (Path line in _lines)
            {
                canvas_real_time.Children.Add(line);
            }
        }

    }
}
