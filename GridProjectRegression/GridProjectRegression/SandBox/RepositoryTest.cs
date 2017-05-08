using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using experitestClient;

namespace Experitest
{
    [TestClass]
    public class RepositoryTest
    {
        private int port = 8090;
        private string projectBaseDirectory = @"C:\CSGridTestRail\GridProject";//"C:\\Users\\eyal.neumann\\workspace\\GridProject";

        //C:\CSGridTestRail\GridProject
        protected Client client = null;
        protected GridClient grid = null;

        [TestInitialize()]
        public void SetupTest()
        {
            String ipAddress = "192.168.2.135";
            port = 443;
            String project = "Default";
            grid = new GridClient("eyal", "Experitest2012", project, ipAddress, port, false);
            client = grid.LockDeviceForExecution("Grid Repository Test", "@os='android'", 70, 300000); client.SetProjectBaseDirectory(projectBaseDirectory);

            client.SetReporter("xml", "reports", "EriBankAndroid");
        }

        [TestMethod]
        public void TestEriBankAndroid()
        {
            client.DeviceAction("Unlock");
            //client.Uninstall("com.experitest.ExperiBank");
            //client.Install("C:\\MobileApps\\apk\\eribank.apk", true, false);
            
            client.Launch("cloud:com.experitest.ExperiBank/.LoginActivity", true, true);
            client.ElementSendText("EriBankAndroid", "Username", 0, "company");
            client.ElementSendText("EriBankAndroid", "Password", 0, "company");
            client.Click("EriBankAndroid", "Login", 0, 1);
            client.Click("EriBankAndroid", "Make Payment", 0, 1);
            client.ElementSendText("EriBankAndroid", "Phone", 0, "098967");
            client.ElementSendText("EriBankAndroid", "Name", 0, "Name");
            client.ElementSendText("EriBankAndroid", "Amount", 0, "Amount");
            if (client.WaitForElement("EriBankAndroid", "Select", 0, 10000))
            {
                // If statement
            }
            client.Click("EriBankAndroid", "Select", 0, 1);
            client.Click("EriBankAndroid", "Japan", 0, 1);
            client.Click("EriBankAndroid", "Send Payment", 0, 1);
            client.Click("EriBankAndroid", "Yes", 0, 1);
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