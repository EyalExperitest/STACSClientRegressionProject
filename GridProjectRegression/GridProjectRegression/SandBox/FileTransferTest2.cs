using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using experitestClient;

namespace Experitest
{
    [TestClass]
    public class FileTranferGridTest2
    {
        private int port;
        //private string projectBaseDirectory = "C:\\Users\\eyal.neumann\\workspace\\GridProject";
        private string logPath = @"C:\LogsFromRun\log001.log";
        protected Client client = null;
        protected GridClient grid = null;
        private string reportPath;
        [TestInitialize()]
        public void SetupTest()
        {
            // client = new Client(host, port, true);
            String ipAddress = "192.168.4.63";
            port = 8090;
            String project = "Default";
            grid = new GridClient("eyal", "Experitest2012", project, ipAddress, port, false);
            client = grid.LockDeviceForExecution("Grid File Transfer Test", "", 70, 300000);
            client.Sleep(10000);
            Console.WriteLine("Before Set Project");
            //client.SetProjectBaseDirectory(projectBaseDirectory);
            Console.WriteLine("After Set Project");
            reportPath= client.SetReporter("xml", "reports", "GridTest");
        }

        [TestMethod]
        public void TestFileTranfer()
        {

            client.StartVideoRecord();
            client.StartLoggingDevice(logPath);
            client.Sleep(10000);

            string remoteVideoPath = client.StopVideoRecord();
            string localVideoPath = reportPath+@"\vidoeImported.ogg";
            client.GetRemoteFile(remoteVideoPath, 10000, localVideoPath);

            string localLogPath= reportPath + @"\logImported.log";
            string remoteLogPath = client.GetDeviceLog();

            client.GetRemoteFile(remoteLogPath, 10000, localLogPath);
            string loggingMethodPath = client.StopLoggingDevice();
            Console.WriteLine("loggingMethodPath : " + loggingMethodPath);




        }

        [TestCleanup()]
        public void TearDown()
        {
            // Generates a report of the test case.
            // For more information - https://docs.experitest.com/display/public/SA/Report+Of+Executed+Test
            client.GenerateReport(false);
            // Releases the client so that other clients can approach the agent in the near future. 
            client.ReleaseClient();
        }
    }
}