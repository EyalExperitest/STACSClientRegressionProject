using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridProjectRegression
{
    class ClientLogger
    {
        private string path;

        public ClientLogger(String path, String name)
        {

            this.path = path;
            if (!File.Exists(path))
            {
                using (StreamWriter streamWriter = File.CreateText(path))
                {
                    String title = String.Format("{0} : {1} ", DateTime.Now.ToString(), name);
                    streamWriter.WriteLine(title);
                    System.Diagnostics.Debug.WriteLine(title);
                }
            }
            else
            {
                using (StreamWriter streamWriter = File.AppendText(path))
                {
                    String title = String.Format("{0} : {1} ", DateTime.Now.ToString(), name);
                    streamWriter.WriteLine(title);
                    System.Diagnostics.Debug.WriteLine(title);
                }
            }
        }
        public void WriteToLog(String logLine)
        {
            using (StreamWriter streamWriter = File.AppendText(path))
            {
                String line = String.Format("{0} : {1} ", DateTime.Now.ToString(), logLine);
                streamWriter.WriteLine(line);
                System.Diagnostics.Debug.WriteLine(line);

            }
        }






    }
}
