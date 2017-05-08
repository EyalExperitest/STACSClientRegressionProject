using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using experitestClient;

namespace Experitest
{
    [TestClass]
    public class setDevice
    {
        private string host = "localhost";
        private int port = 8889;
        private string projectBaseDirectory = "C:\\Users\\eyal.neumann\\workspace\\GridProject";
        protected Client client = null;
        private string reportPath;


        [TestInitialize()]
        public void SetupTest()
        {
            client = new Client(host, port, true);
            client.SetProjectBaseDirectory(projectBaseDirectory);
            reportPath = client.SetReporter("xml", "reports", "setDevice");
        }

        [TestMethod]
        public void TestsetDevice()
        {
            client.SetDevice("ios_app:iPad nevo");
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
            // 
        }

        [TestCleanup()]
        public void TearDown()
        {
            // Generates a report of the test case.
            // For more information - https://docs.experitest.com/display/public/SA/Report+Of+Executed+Test

            Console.WriteLine(client.GenerateReport(false));
            // Releases the client so that other clients can approach the agent in the near future. 
            client.ReleaseClient();
        }
    }
}