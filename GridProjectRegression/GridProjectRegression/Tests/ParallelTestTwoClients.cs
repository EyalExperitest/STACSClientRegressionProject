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
    public class ParallelTestTwoClients
    {
        private ClientTest clientTest0 = null;
        private ClientTest clientTest1 = null;


        [TestInitialize()]
        public void SetupTest()
        {
            clientTest0 = new ClientTest("Grid Test 1");
            clientTest1 = new ClientTest("Grid Test 1");

            //Thread initiationThread0 = new Thread(clientTest0.InitiateLocalClient);
            //Thread initiationThread1 = new Thread(clientTest1.InitiateLocalClient);

            Thread initiationThread0 = new Thread(clientTest0.InitiateGridClient);
            Thread initiationThread1 = new Thread(clientTest1.InitiateGridClient);


            initiationThread0.Name = "Init 0";
            initiationThread1.Name = "Init 1";

            initiationThread0.Start();
            initiationThread1.Start();

            initiationThread0.Join();
            initiationThread1.Join();

            clientTest0.ClientStrtegy = new RunEriBankPayOnAndroid();
            clientTest1.ClientStrtegy = new RunEriBankPayOnAndroid();


        }

        [TestMethod]
        public void TestPTest2()
        {
            Thread runningThread0 = new Thread(clientTest0.RunTest);
            Thread runningThread1 = new Thread(clientTest1.RunTest);

            runningThread0.Name = "Run 0";
            runningThread1.Name = "Run 1";

            runningThread0.Start();
            runningThread1.Start();

            runningThread0.Join();
            runningThread1.Join();

        }

        [TestCleanup()]
        public void TearDown()
        {
            Thread releasingThread0 = new Thread(clientTest0.CloseGridClient);
            Thread releasingThread1 = new Thread(clientTest1.CloseGridClient);

            releasingThread0.Name = "Close 0";
            releasingThread1.Name = "Close 1";

            releasingThread0.Start();
            releasingThread1.Start();

            releasingThread0.Join();
            releasingThread1.Join();

        }
    }
}