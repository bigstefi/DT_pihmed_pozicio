using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT_pihmed_pozicio.Data
{
    internal class RawData
    {
        #region Member variables
        private List<string> _dataLines = new List<string>();
        #endregion

        #region Properties
        public IReadOnlyCollection<string> Data
        {
            get
            {
                return _dataLines;
            }
        }
        #endregion

        #region Constructor
        public RawData(string rawDataFilePath)
        {
            ReadData(rawDataFilePath);
        }
        #endregion

        #region Helpers
        private void ReadData(string rawDataFilePath) // testable this way, no need for OpenFileDialog, you can directly pass the file full path and see if parsing works correctly
        {
            // ToDo: File.ReadAllLines would just make it. Stream is useful, if someone is continuously pushing data into it
            using (System.IO.StreamReader sr = new System.IO.StreamReader(rawDataFilePath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    _dataLines.Add(line);
                }
            }
        }
        #endregion
    }
}
