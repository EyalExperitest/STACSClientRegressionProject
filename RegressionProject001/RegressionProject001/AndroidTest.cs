using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using experitestClient;

namespace Experitest
{
    [TestClass]
    public class AndroidTest
    {
        private string host = "localhost";
        private int port = 8889;
        protected Client client = null;

        [TestInitialize()]
        public void SetupTest()
        {
            client = new Client(host, port, true);
            client.SetReporter("xml", "reports", "Androids");
        }

        [TestMethod]
        public void TestAndroidClient()
        {
            string str0 = client.WaitForDevice("@os='android'", 300000);
            Console.WriteLine(str0);
            client.Launch("com.experitest.ExperiBank/.LoginActivity", true, true);
            if (client.WaitForElement("NATIVE", "xpath=//*[@text='Login']", 0, 30000))
            {
                // If statement
            }
            client.ElementSendText("NATIVE", "xpath=//*[@hint='Username']", 0, "company");
            client.ElementSendText("NATIVE", "xpath=//*[@hint='Password']", 0, "company");
            client.Click("NATIVE", "xpath=//*[@text='Login']", 0, 1);
            client.VerifyElementFound("NATIVE", "xpath=//*[@text='Logout']", 0);
            client.Click("NATIVE", "xpath=//*[@text='Make Payment']", 0, 1);
            client.ElementSendText("NATIVE", "xpath=//*[@hint='Phone']", 0, "0505050505");
            client.ElementSendText("NATIVE", "xpath=//*[@hint='Name']", 0, "user");
            client.ElementSendText("NATIVE", "xpath=//*[@hint='Amount']", 0, "5000");
            client.Click("NATIVE", "xpath=//*[@text='Select']", 0, 1);
            client.ElementListSelect("id=countryList", "text=Iran", 0, true);
            client.Click("NATIVE", "xpath=//*[@text='Send Payment']", 0, 1);
            client.Click("NATIVE", "xpath=//*[@text='Yes']", 0, 1);
            client.VerifyElementFound("NATIVE", "xpath=//*[@text='Expense Report']", 0);
            client.Click("NATIVE", "xpath=//*[@text='Logout']", 0, 1);
            client.VerifyElementFound("NATIVE", "xpath=//*[@text='Login']", 0);
            client.SendText("{HOME}");
            client.Launch("chrome:http://www.google.com", true, true);
            if (client.WaitForElement("WEB", "xpath=//*[@id='lst-ib']", 0, 10000))
            {
                // If statement
            }
            client.Click("WEB", "xpath=//*[@id='lst-ib']", 0, 1);
            client.SendText("seeTest");
            client.Click("WEB", "xpath=//*[@name='btnG']", 0, 1);
            client.VerifyElementFound("WEB", "xpath=//*[@text='Experitest']", 0);
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