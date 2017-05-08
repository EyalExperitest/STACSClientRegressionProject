using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading;

namespace GridProjectRegression
{
    [TestClass]
    public class StopWatchSandBox
    {
        [TestMethod]
        public void TestStopWatch()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            Thread.Sleep(10000);
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("ts " + ts);
   
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            stopWatch.Stop();



        }
    }
}
