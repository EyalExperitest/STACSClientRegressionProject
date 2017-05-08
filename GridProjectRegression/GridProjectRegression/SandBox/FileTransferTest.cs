using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using experitestClient;

namespace Experitest
{
    [TestClass]
    public class FileTranferGridTest
    {
        // private string host = "192.168.4.63";
        private int port = 8090;
        //private string projectBaseDirectory = "C:\\Users\\eyal.neumann\\workspace\\GridProject";
        private string logPath = @"C:\LogsFromRun\log001.log";
        protected Client client = null;
        protected GridClient grid = null;
        private string reportPath;
        [TestInitialize()]
        public void SetupTest()
        {
            // client = new Client(host, port, true);
            String ipAddress = "192.168.1.210";
            port = 443;
            String project = "Default";
            grid = new GridClient("eyal", "Experitest2012", project, ipAddress, port, false);
            //grid = new GridClient("eyal", "Experitest2012", project, "");
            client = grid.LockDeviceForExecution("Grid File Transfer Test", "@os='ios'", 70, 300000);
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
            client.Launch("cloud:com.experitest.ExperiBank", true, true);
            client.ElementSendText("NATIVE", "placeholder=Username", 0, "company");
            client.ElementSendText("NATIVE", "placeholder=Password", 0, "company");
            client.Click("NATIVE", "accessibilityLabel=loginButton", 0, 1);
            client.Click("NATIVE", "accessibilityLabel=makePaymentButton", 0, 1);
            client.ElementSendText("NATIVE", "placeholder=Phone", 0, "0867564856478");
            client.ElementSendText("NATIVE", "placeholder=Name", 0, "Name");
            client.ElementSendText("NATIVE", "accessibilityLabel=Amount", 0, "100");
            client.Click("NATIVE", "accessibilityLabel=countryButton", 0, 1);
            client.ElementListSelect("", "text=United Kingdom", 0, false);
            client.Click("NATIVE", "xpath=//*[@accessibilityLabel='United Kingdom']", 0, 1);
            client.Click("NATIVE", "accessibilityLabel=sendPaymentButton", 0, 1);
            client.Click("NATIVE", "xpath=//*[@text='Yes']", 0, 1);
            client.Click("NATIVE", "accessibilityLabel=logoutButton", 0, 1);
            client.Click("NATIVE", "placeholder=Username", 0, 1);
            client.Click("NATIVE", "class=UIButton", 0, 1);
            client.Click("NATIVE", "placeholder=Password", 0, 1);
            client.Click("NATIVE", "class=UIButton", 0, 1);

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