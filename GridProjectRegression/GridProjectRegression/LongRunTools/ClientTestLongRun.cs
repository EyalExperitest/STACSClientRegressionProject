using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using experitestClient;
using System.Diagnostics;
using System.IO;

namespace GridProjectRegression
{
    class ClientTestLongRun
    {
        
        
         
        private string ipAddress= "eyalneumann.experitest.local";
        private int port = 8090;
        private string user = "admin";
        private string password = "Experitest2012";
        private string project = "Default";
        private string testName = "Test";
        private string query = "@os='android'";
        private bool isSecured = false;

        private ClientStrategy clientStrategy = null;
        protected Client client = null;
        protected GridClient grid = null;


        private string projectBaseDirectory = @"C:\Users\eyal.neumann\workspace\GridProject";
        private string reporterPath = @"C:\Users\eyal.neumann\seetest-reports\LocalReports\Batch001";
        private string reportsPath="";
        private TimeSpan testTime=new TimeSpan(0,30,0);

        private ClientLogger clientLogger = null;
        private string rootPath;
        private LongRunObserver longRunObserver;
        private Boolean successFlag = true;
        private int counter;
        private String deviceName = "?????";


        public ClientTestLongRun(String testName)
        {
            this.testName = testName;
            rootPath = this.reporterPath;
            longRunObserver = LongRunObserver.LongRunObserverFactory(rootPath);

            this.reporterPath = reporterPath + @"\" + testName;
            if (!Directory.Exists(this.reporterPath)) Directory.CreateDirectory(this.reporterPath);

            clientLogger = new ClientLogger(this.reporterPath + @"\Test.log", testName);

            clientLogger.WriteToLog("query = " + query);
            clientLogger.WriteToLog("ipAddress = " + ipAddress);
            clientLogger.WriteToLog("port = " + port);
            clientLogger.WriteToLog("user = " + user);
            clientLogger.WriteToLog("project = " + project);
            clientLogger.WriteToLog("isSecured = " + isSecured);

        }
        public ClientTestLongRun(String testName, String query, String ipAddress, int port, String user, String password, String project, bool isSecured)
        {

            this.testName = testName;
            rootPath = this.reporterPath;
            longRunObserver = LongRunObserver.LongRunObserverFactory(rootPath);

            this.reporterPath = reporterPath + @"\" + testName;
            if (!Directory.Exists(this.reporterPath)) Directory.CreateDirectory(this.reporterPath);



            clientLogger = new ClientLogger(this.reporterPath + @"\Test.log", testName);

            this.query = query;
            this.ipAddress = ipAddress;
            this.port = port;
            this.user = user;
            this.password = password;
            this.project = project;
            this.isSecured = isSecured;

            clientLogger.WriteToLog("query = " + query);
            clientLogger.WriteToLog("ipAddress = " + ipAddress);
            clientLogger.WriteToLog("port = " + port);
            clientLogger.WriteToLog("user = " + user);
            clientLogger.WriteToLog("project = " + project);
            clientLogger.WriteToLog("isSecured = " + isSecured);

        }


        public ClientTestLongRun(String testName, String query, ClientStrategy clientStrategy, TimeSpan testTime)
        {
            this.testName = testName;
            rootPath = this.reporterPath;
            longRunObserver= LongRunObserver.LongRunObserverFactory(rootPath);

            this.reporterPath = reporterPath + @"\" + testName;
            if (!Directory.Exists(this.reporterPath)) Directory.CreateDirectory(this.reporterPath);
            
            clientLogger = new ClientLogger(this.reporterPath + @"\Test.log", testName);

            this.query = query;
            this.clientStrategy = clientStrategy;
            this.testTime = testTime;

            clientLogger.WriteToLog("query = " + query);
            clientLogger.WriteToLog("ipAddress = " + ipAddress);
            clientLogger.WriteToLog("port = " + port);
            clientLogger.WriteToLog("user = " + user);
            clientLogger.WriteToLog("project = " + project);
            clientLogger.WriteToLog("isSecured = " + isSecured);




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

        public void GridLongRunTest()
        {
            longRunObserver.AddTest(this.testName);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            bool continueFlag = true;
            counter = 0;
            while (continueFlag)
            {

                String toLog = "\n******************************************  Test "+testName + counter + " ******************************************";
                clientLogger.WriteToLog(toLog);
                longRunObserver.WriteToLog(toLog);
                successFlag = true;

                this.InitiateGridClient();

                if (this.client != null)
                {
                    this.clientStrategy.Client = this.client;
                    try
                    {
                         deviceName = client.GetDeviceProperty("device.name");

                    }
                    catch (Exception e)
                    {
                        longRunObserver.addException("Device Name  Exception : " + e.Message);

                        deviceName = "?????";

                        toLog = "\n$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$";
                        longRunObserver.WriteToLog(toLog);
                        clientLogger.WriteToLog(toLog);

                        toLog = "Exception when trying to get  Device Property for " + " "+ testName + " " + counter + " : \n" +e.Message+"\n"+ e.StackTrace;
                        longRunObserver.WriteToLog(toLog);
                        clientLogger.WriteToLog(toLog);

                        toLog = "\n###########################################################################";
                        clientLogger.WriteToLog(toLog);
                        longRunObserver.WriteToLog(toLog);


                    }
                    toLog = "  " + testName + " " + counter + " -> Device : " + deviceName;
                    longRunObserver.AddDevice(deviceName);
                    clientLogger.WriteToLog(toLog);
                    longRunObserver.WriteToLog(toLog);

                }
                else
                {
                    deviceName = "?????";
                    longRunObserver.AddDevice(deviceName);

                }


                this.RunTest();

                this.CloseGridClient();

                longRunObserver.UpdateTest(testName, successFlag);
                longRunObserver.UpdateDevice(deviceName, successFlag);

                TimeSpan timeSpan = stopWatch.Elapsed;
                continueFlag = (timeSpan.CompareTo(testTime) < 1);
                clientLogger.WriteToLog(String.Format("Run Time So far : {0:00}:{1:00}:{2:00}.{3:000}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds ));

                String successString = successFlag ? " Success" : " Failure";
                toLog = "\n****************************************** Ending " + testName + counter + successString+" ******************************************";
                clientLogger.WriteToLog(toLog);
                longRunObserver.WriteToLog(toLog);

                counter++;

            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            clientLogger.WriteToLog(this.testName+" RunTime " + elapsedTime);
            clientLogger.WriteToLog("\n**********************************************************************");

        }
        public void LocalLongRunTest()
        {
            longRunObserver.AddTest(this.testName);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            bool continueFlag = true;
            counter = 0;
            while (continueFlag)
            {

                String toLog = "\n******************************************  Test " + testName + counter + " ******************************************";
                clientLogger.WriteToLog(toLog);
                longRunObserver.WriteToLog(toLog);
                successFlag = true;

                this.InitiateLocalClient();

                if (this.client != null)
                {
                    this.clientStrategy.Client = this.client;
                    try
                    {
                        deviceName = client.GetDeviceProperty("device.name");

                    }
                    catch (Exception e)
                    {
                        longRunObserver.addException("Device Name Exception"+e.Message);

                        deviceName = "?????";

                        toLog = "\n$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$";
                        longRunObserver.WriteToLog(toLog);
                        clientLogger.WriteToLog(toLog);

                        toLog = "Exception when trying to get  Device Property for " + " " + testName + " " + counter + " : \n" + e.Message + "\n" + e.StackTrace;
                        longRunObserver.WriteToLog(toLog);
                        clientLogger.WriteToLog(toLog);

                        toLog = "\n###########################################################################";
                        clientLogger.WriteToLog(toLog);
                        longRunObserver.WriteToLog(toLog);


                    }
                    toLog = "  " + testName + " " + counter + " -> Device : " + deviceName;
                    longRunObserver.AddDevice(deviceName);
                    clientLogger.WriteToLog(toLog);
                    longRunObserver.WriteToLog(toLog);

                }
                else
                {
                    deviceName = "?????";
                    longRunObserver.AddDevice(deviceName);

                }


                this.RunTest();

                this.CloseGridClient();

                longRunObserver.UpdateTest(testName, successFlag);
                longRunObserver.UpdateDevice(deviceName, successFlag);

                TimeSpan timeSpan = stopWatch.Elapsed;
                continueFlag = (timeSpan.CompareTo(testTime) < 1);
                clientLogger.WriteToLog(String.Format("Run Time So far : {0:00}:{1:00}:{2:00}.{3:000}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds));

                String successString = successFlag ? " Success" : " Failure";
                toLog = "\n****************************************** Ending " + testName + counter + successString + " ******************************************";
                clientLogger.WriteToLog(toLog);
                longRunObserver.WriteToLog(toLog);

                counter++;

            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            clientLogger.WriteToLog(this.testName + " RunTime " + elapsedTime);
            clientLogger.WriteToLog("\n**********************************************************************");

        }






        public void InitiateLocalClient()
        {
            string host = "localhost";
            int port = 8889;

            try
            {
                this.client = new Client(host, port, true);
                client.WaitForDevice(query, 300000);
            }
            catch (Exception e)
            {

                longRunObserver.addException("Initiation Exception : "+e.Message);
                successFlag = false;
                String toLog;
                toLog = "\n$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$";
                clientLogger.WriteToLog(toLog);
                toLog = "Exception when Locking Device for  " + testName + " " + counter + " : \n" + e.Message + "\n" + e.StackTrace;
                clientLogger.WriteToLog(toLog);
                longRunObserver.WriteToLog(toLog);
            }

            clientLogger.WriteToLog("Initiation  Test " + testName+" "+counter);
            client.SetProjectBaseDirectory(projectBaseDirectory);
            reportsPath=client.SetReporter("xml", ReporterPath, testName);
            client.SetShowPassImageInReport(true);
            
            clientLogger.WriteToLog("Reporter Path is" + reportsPath);

        }
        public void InitiateGridClient()
        {
            try
            {
                grid = new GridClient(user, password, project, ipAddress, port, isSecured);
                client = grid.LockDeviceForExecution(testName, query, 70, 300000);
                clientLogger.WriteToLog("Initiation  " + testName+" "+counter);

                client.SetProjectBaseDirectory(projectBaseDirectory);
                clientLogger.WriteToLog("Setting Project Direectory to " + projectBaseDirectory);

                reportsPath = client.SetReporter("xml", ReporterPath, testName);
                clientLogger.WriteToLog("Reporter Path is" + reportsPath);

            }
            catch (Exception e)
            {
                longRunObserver.addException("Initiation Exception : " + e.Message);
                successFlag = false;
                String toLog;
                toLog = "\n$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$";
                clientLogger.WriteToLog(toLog);
                toLog = "Exception when Locking Device for  "+ testName + " " + counter+ " : \n" + e.Message + "\n" + e.StackTrace;
                clientLogger.WriteToLog(toLog);
                longRunObserver.WriteToLog(toLog);

                if (client != null)
                {

                    try
                    {
                        String zipPath = @"C:\SupportData\SupportDataLocking_" + testName + ".zip";
                        longRunObserver.WriteToLog(zipPath);
                        client.CollectSupportData(zipPath, "", "", testName, "Success", e.Message + "\n"+e.StackTrace);
                        toLog = "\n###########################################################################";
                        clientLogger.WriteToLog(toLog);
                        longRunObserver.WriteToLog(toLog);


                    }
                    catch (Exception e2)
                    {
                        longRunObserver.addException("Initiation Support Data Exception : " + e2.Message);

                        toLog = "Exception when Collecting Support Data for Locking Device for Test : " + testName + " " + counter  + " : \n" + e2.Message + "\n" + e2.StackTrace;
                        clientLogger.WriteToLog(toLog);
                        longRunObserver.WriteToLog(toLog);
                        toLog = "\n###########################################################################";
                        clientLogger.WriteToLog(toLog);
                        longRunObserver.WriteToLog(toLog);

                    }
                    finally
                    {
                        client = null;
                    }
                    
                }
                
            }
        }
        public void CloseGridClient()
        {
            try
            {
                if (client != null)
                {



                    clientLogger.WriteToLog("Close  : " + testName);
                    clientLogger.WriteToLog("Reporter Path is" +reportsPath);

                    String reportPathReturned = client.GenerateReport(false);
                    clientLogger.WriteToLog("Report in : " + reportPathReturned);
                    client.ReleaseClient();
                }
            }
            catch (Exception e)
            {
                longRunObserver.addException("Closing Exception : " + e.Message);
                successFlag = false;
                String toLog;
                toLog = "\n$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$";
                clientLogger.WriteToLog(toLog);
                longRunObserver.WriteToLog(toLog);
                toLog = "Exception when closing   : " + testName + " " + counter + " : \n" + e.Message + "\n" + e.Message + "\n" + e.StackTrace;
                clientLogger.WriteToLog(toLog);
                longRunObserver.WriteToLog(toLog);


                try
                {
                    String zipPath = reportsPath + @"\SupportDataClosing.zip";
                    longRunObserver.WriteToLog(zipPath);
                    client.CollectSupportData(zipPath, "", "", "Closing " + testName, "Success", e.Message + "\n" + e.StackTrace);
                    toLog = "\n###########################################################################";
                    clientLogger.WriteToLog(toLog);
                    longRunObserver.WriteToLog(toLog);

                }
                catch (Exception e2)
                {
                    longRunObserver.addException("Closing Support Data Exception : " + e2.Message);

                    toLog = "Exception when Collecting Support Data for Closing Test : " + testName + " " + counter + " : \n" + e2.Message + "\n" + e2.StackTrace;
                    clientLogger.WriteToLog(toLog);
                    longRunObserver.WriteToLog(toLog);
                    toLog = "\n###########################################################################";
                    clientLogger.WriteToLog(toLog);
                    longRunObserver.WriteToLog(toLog);

                }
            }
        }

        public void RunTest()
        {
            if (client != null)
            {
                clientLogger.WriteToLog("Run Test : " + testName + " " + counter);
                try
                {
                    if (clientStrategy != null)
                    {
                        clientStrategy.RunStartegy();
                    }
                }
                catch (Exception e)
                {
                    longRunObserver.addException("Running Exception : " + e.Message);

                    successFlag = false;

                    String toLog;
                    toLog = "\n$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$";
                    clientLogger.WriteToLog(toLog);
                    longRunObserver.WriteToLog(toLog);

                    toLog = "Exception in  Test : " + testName + " " + counter+ " : \n" + e.Message + "\n" + e.StackTrace;
                    clientLogger.WriteToLog(toLog);
                    longRunObserver.WriteToLog(toLog);

                    try
                    {
                        String zipPath = reportsPath + @"\SupportData.zip";
                        longRunObserver.WriteToLog(zipPath);
                        client.CollectSupportData(zipPath, "", "", testName, "Success", e.Message + "\n" + e.StackTrace);
                        toLog = "\n###########################################################################";
                        clientLogger.WriteToLog(toLog);
                        longRunObserver.WriteToLog(toLog);

                    }
                    catch (Exception e2)
                    {
                        longRunObserver.addException("Running Support Data Exception : " + e2.Message);

                        toLog = "Exception when Collecting Support Data for Running  Test : " + testName + " " + counter + " : \n" + e2.Message + "\n" + e2.StackTrace;
                        clientLogger.WriteToLog(toLog);
                        longRunObserver.WriteToLog(toLog);
                        toLog = "\n###########################################################################";
                        clientLogger.WriteToLog(toLog);
                        longRunObserver.WriteToLog(toLog);
                    }
                }
            }

        }







    }
}
