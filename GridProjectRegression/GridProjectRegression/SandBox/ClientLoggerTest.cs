using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace GridProjectRegression
{
    [TestClass]
    public class ClientLoggerTest
    {

        private ClientLogger clientLogger = null;



        [TestInitialize()]
        public void SetupTest()
        {
            string path = @"c:\temp\ClientLoggerTest.txt";

            clientLogger = new ClientLogger(path, "********* Client Logger Test ************");
        }

        [TestMethod]
        public void TestMethod1()
        {
            Thread.Sleep(5000);
            clientLogger.WriteToLog("****1*****");
            Thread.Sleep(5000);
            clientLogger.WriteToLog("****2*****");
            Thread.Sleep(5000);
            clientLogger.WriteToLog("****3*****");
            Thread.Sleep(5000);
            clientLogger.WriteToLog("****4*****");



        }

        [TestCleanup()]
        public void TearDown()
        {



        }
    }
}
