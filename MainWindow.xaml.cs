using System;
using System.IO;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<double> angle = new List<double>();
        public List<double> distance = new List<double>();

        public MainWindow()
        {
            InitializeComponent();

            read_data_from_file();
            
        }

        public void read_data_from_file()
        {
            // Configure open file dialog box
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            Label1.Content = "";
            Label2.Content = "";
            if (OFD.ShowDialog() == true)
            {
                string filepath = OFD.FileName;
                StreamReader sr = new StreamReader(filepath);
                
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
            Label1.Content = Convert.ToString(angle.Count());
            Label2.Content = Convert.ToString(distance.Count());





        }
    }
}
