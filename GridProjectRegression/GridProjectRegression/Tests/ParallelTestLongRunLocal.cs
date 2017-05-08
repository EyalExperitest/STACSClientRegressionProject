using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using experitestClient;
using GridProjectRegression;
using System.Threading;

namespace Experitest
{
    [TestClass]
    public class ParallelTestLongRunLocal

    {
        private const int NUMBER_OF_CLIENTS= 2;
        private const int HOUERS = 0;
        private const int MINUTES = 30;
        private const int SECONDS = 0;

        private TimeSpan testTime = new TimeSpan(HOUERS, MINUTES, SECONDS);

        private ClientTestLongRun[] clientTests = new ClientTestLongRun[NUMBER_OF_CLIENTS];


        [TestInitialize()]
        public void SetupTest()
        {
            //public ClientTest(String testName, String query, String ipAddress, int port, String user, String password, String project, bool isSecured)
            /*
            clientTests[0] = new ClientTestLongRun("Andorid Dynamic Test","@os='android'", new RunEriBankPayOnAndroid(), testTime);
            clientTests[1] = new ClientTestLongRun("Andorid Repository Test", "@os='android'",new RunEriBankPayOnAndroidRepository(), testTime);
            clientTests[2] = new ClientTestLongRun("Andorid Web Test", "@os='android'", new WikipediaTest(), testTime);
            clientTests[3] = new ClientTestLongRun("iOS Dynamic Test", "@os='ios'", new EriBankTestOnIOS(), testTime);
            clientTests[4] = new ClientTestLongRun("iOS Repository Test", "@os='ios'", new EriBankTestOnIOSRepository(), testTime);
            clientTests[5] = new ClientTestLongRun("iOS Web Test", "@os='ios'", new WikipediaTest(), testTime);
            */
            /*
            clientTests[0] = new ClientTestLongRun("Android Device Test 1", "@os='android'", new GetDeviceAndProperties(), testTime);
            clientTests[1] = new ClientTestLongRun("Andorid Device Test 2", "@os='android'", new GetDeviceAndProperties(), testTime);
            clientTests[2] = new ClientTestLongRun("Andorid Device Test 3", "@os='android'", new GetDeviceAndProperties(), testTime);
            clientTests[3] = new ClientTestLongRun("iOS Device Test 1", "@os='ios'", new GetDeviceAndProperties(), testTime);
            clientTests[4] = new ClientTestLongRun("iOS Device Test 2", "@os='ios'", new GetDeviceAndProperties(), testTime);
            clientTests[5] = new ClientTestLongRun("iOS Device Test 3", "@os='ios'", new GetDeviceAndProperties(), testTime);
           

            clientTests[0] = new ClientTestLongRun("Andorid Web Test 1", "@os='android'", new WikipediaTest(), testTime);
            clientTests[1] = new ClientTestLongRun("Andorid Web Test 2", "@os='android'", new WikipediaTest(), testTime);
            clientTests[2] = new ClientTestLongRun("Andorid Web Test 3", "@os='android'", new WikipediaTest(), testTime);
            clientTests[3] = new ClientTestLongRun("iOS Web Test 1", "@os='ios'", new WikipediaTest(), testTime);
            //clientTests[4] = new ClientTestLongRun("iOS Web Test 2", "@os='ios'", new WikipediaTest(), testTime);
            //clientTests[5] = new ClientTestLongRun("iOS Web Test 3", "@os='ios'", new WikipediaTest(), testTime);
            */
            clientTests[0] = new ClientTestLongRun("Andorid Rail  Test", "@os='android'", new AndroidRailTest(), testTime);
            clientTests[1] = new ClientTestLongRun("iOS Rail Test", "@os='ios'", new IOSRailTest(), testTime);
            //clientTests[2] = new ClientTestLongRun("Andorid Web Test", "@os='android'", new WikipediaTest(), testTime);
            //clientTests[3] = new ClientTestLongRun("iOS Dynamic Test", "@os='ios'", new EriBankTestOnIOS(), testTime);

        }

        [TestMethod]
        public void TestLongRunParallelTest()
        {
            Thread[] runningThreads = new Thread[NUMBER_OF_CLIENTS];


            for (int i = 0; i < NUMBER_OF_CLIENTS; i++)
            {
                runningThreads[i] = new Thread(clientTests[i].LocalLongRunTest);
                runningThreads[i].Name = "Long Run  " + i;

            }
            for (int i = 0; i < NUMBER_OF_CLIENTS; i++)
            {
                runningThreads[i].Start();

            }
            for (int i = 0; i < NUMBER_OF_CLIENTS; i++)
            {
                runningThreads[i].Join();

            }

        }

        [TestCleanup()]
        public void TearDown()
        {



        }
    }
}