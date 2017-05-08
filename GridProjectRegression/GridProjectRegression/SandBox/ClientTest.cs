using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using experitestClient;


namespace GridProjectRegression
{
    class ClientTest
    {
        private string ipAddress= "192.168.2.135";
        private int port = 443;
        private string user = "eyal";
        private string password = "Experitest2012";
        private string project = "Default";
        private string testName = "Test";
        private string query = "@os='android'";
        private bool isSecured = true;

        private ClientStrategy clientStrtegy = null;
        protected Client client = null;
        protected GridClient grid = null;


        private string projectBaseDirectory = @"C:\Users\eyal.neumann\workspace\GridProject";
        private string reporterPath = @"C:\Users\eyal.neumann\seetest-reports\reports";



        public ClientTest(String testName, String query, String ipAddress, int port, String user, String password, String project, bool isSecured)
        {

            this.testName = testName;
            this.query = query;
            this.ipAddress = ipAddress;
            this.port = port;
            this.user = user;
            this.password = password;
            this.project = project;
            this.isSecured = isSecured;
        }
        public ClientTest(String testName)
        {
            this.testName = testName;
        }
        public ClientTest(String testName, String query)
        {
            this.testName = testName;
            this.query = query;

        }

        public string ProjectBaseDirectory
        {
            get
            {
                return projectBaseDirectory;
            }

            set
            {
                projectBaseDirectory = value;
            }
        }

        public string ReporterPath
        {
            get
            {
                return reporterPath;
            }

            set
            {
                reporterPath = value;
            }
        }

        internal ClientStrategy ClientStrtegy
        {
            get
            {
                return clientStrtegy;
            }

            set
            {
                clientStrtegy = value;
                clientStrtegy.Client = this.client;
            }
        }
        public void InitiateLocalClient()
        {
            string host = "localhost";
            int port = 8889;
            this.client = new Client(host, port, true);
            client.WaitForDevice(query, 300000);

            System.Diagnostics.Debug.WriteLine("Initiation  Test " + testName);
            client.SetProjectBaseDirectory(projectBaseDirectory);
            client.SetReporter("xml", ReporterPath, testName);
        }
        public void InitiateGridClient()
        {
            grid = new GridClient(user, password, project,ipAddress, port, isSecured);
            client = grid.LockDeviceForExecution(testName, query, 70, 300000);
            System.Diagnostics.Debug.WriteLine("Initiation  Test "+ testName);

            client.SetProjectBaseDirectory(projectBaseDirectory);
            client.SetReporter("xml", ReporterPath, testName);
        }
        public void CloseGridClient()
        {
            System.Diagnostics.Debug.WriteLine("Close Test : " + testName);

            String reportPathReturned = client.GenerateReport(false);
            Console.WriteLine("Report in : "+ reportPathReturned);
            client.ReleaseClient();
        }

        public void RunTest()
        {
            System.Diagnostics.Debug.WriteLine("Run Test : " + testName);

            if (ClientStrtegy != null)
            {
                ClientStrtegy.RunStartegy();
            }

        }







    }
}
