using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using experitestClient;

namespace Experitest
{
    [TestClass]
    public class EriBankOnIOS
    {
        private string host = "localhost";
        private int port = 8889;
        private string projectBaseDirectory = "C:\\Users\\eyal.neumann\\workspace\\GridProject";
        protected Client client = null;

        [TestInitialize()]
        public void SetupTest()
        {
            client = new Client(host, port, true);
            client.SetProjectBaseDirectory(projectBaseDirectory);
            client.SetReporter("xml", "reports", "EriBankOnIOS");
        }

        [TestMethod]
        public void TestEriBankOnIOS()
        {
            string str0 = client.WaitForDevice("@os='ios'", 300000);
            client.Launch("com.experitest.ExperiBank", true, true);
            client.ElementSendText("NATIVE", "placeholder=Username", 0, "company");
            client.ElementSendText("NATIVE", "placeholder=Password", 0, "company");
            client.Click("NATIVE", "accessibilityLabel=loginButton", 0, 1);
            client.Click("NATIVE", "accessibilityLabel=makePaymentButton", 0, 1);
            client.ElementSendText("NATIVE", "placeholder=Phone", 0, "09785634");
            client.ElementSendText("NATIVE", "placeholder=Name", 0, "Eyal");
            client.ElementSendText("NATIVE", "placeholder=Amount", 0, "-100");
            client.Click("NATIVE", "accessibilityLabel=countryButton", 0, 1);
            client.Click("NATIVE", "xpath=//*[@accessibilityLabel='Greece']", 0, 1);
            client.Click("NATIVE", "accessibilityLabel=sendPaymentButton", 0, 1);
            client.Click("NATIVE", "xpath=//*[@text='Yes']", 0, 1);
            client.Click("NATIVE", "accessibilityLabel=logoutButton", 0, 1);
            client.Click("NATIVE", "placeholder=Username", 0, 1);
            client.Click("NATIVE", "class=UIButton", 0, 1);
            client.Click("NATIVE", "placeholder=Password", 0, 1);
            client.Click("NATIVE", "class=UIButton", 0, 1);
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