using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using experitestClient;

namespace Experitest
{
    [TestClass]
    public class SupportDataTest
    {
        private int port;
        protected Client client = null;
        protected GridClient grid = null;
        private string reportPath;
        [TestInitialize()]
        public void SetupTest()
        {
            // client = new Client(host, port, true);
            String ipAddress = "192.168.4.63";
            port = 8091;
            String project = "Default";
            grid = new GridClient("eyal", "Experitest2012", project, ipAddress, port, false);
            client = grid.LockDeviceForExecution("Support Data Test", "", 70, 300000);
            client.Sleep(10000);
            reportPath= client.SetReporter("xml", "reports", "Support Data Test");
            System.Diagnostics.Debug.WriteLine(reportPath);

        }

        [TestMethod]
        public void TestSupportData()
        {
            string appPath = @"C:\MobileApps\apk\AndroidUICatlog.apk";
            string deviceName = client.GetDeviceProperty("device.name");
            Console.WriteLine("Device Name : " + deviceName);




           /*
            try
            {

                client.CollectSupportData(reportPath, "", "", "Support Data", "Expected", "Result", false, false);

            }
            catch (Exception e)
            {
                Console.WriteLine("Support Data C:  " + " Failed");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            

             try
            {
                client.CollectSupportData("", "", "", "Support Data", "Expected", "Result", false, false);

            }
            catch (Exception e)
            {
                Console.WriteLine("Support Data Default Path " + " Failed");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            */



            try
            {
                client.CollectSupportData(reportPath + @"\SupportDataBasic.zip", "", "", "Support Data", "Expected", "Result", false, false);

            }
            catch (Exception e)
            {
                Console.WriteLine("Support Data" + " Failed");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

            try
            {
                client.CollectSupportData(reportPath + @"\SupportDataDevice.zip", "", deviceName, "Support Data with Device Name", "Expected", "Result", false, false);

            }
            catch (Exception e)
            {
                Console.WriteLine("Support Data with Device Name" + " Failed");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

            try
            {
                client.CollectSupportData(reportPath + @"\SupportDataApp.zip", appPath, "", "Support Data with App Path", "Expected", "Result", false, false);

            }
            catch (Exception e)
            {
                Console.WriteLine("Support Data with App Path" + " Failed");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

            try
            {
                client.CollectSupportData(reportPath + @"\SupportDataCloud.zip", "", "", "Support Data with Cloud", "Expected", "Result", true, false);

            }
            catch (Exception e)
            {
                Console.WriteLine("Support Data with Cloud"+" Failed");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            
            client.Sleep(10000);
        }

        [TestCleanup()]
        public void TearDown()
        {
            // Generates a report of the test case.
            // For more information - https://docs.experitest.com/display/public/SA/Report+Of+Executed+Test
            System.Diagnostics.Debug.WriteLine(reportPath);
            client.GenerateReport(false);
            // Releases the client so that other clients can approach the agent in the near future. 
            client.ReleaseClient();
        }
    }
}