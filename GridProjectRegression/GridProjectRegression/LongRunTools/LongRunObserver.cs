using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GridProjectRegression
{
    class LongRunObserver
    {

        private static String locker = "lock";
        private static LongRunObserver longRunObserver = null;
        private string path;
        private string logFilePath;
        private string monitorFilePath;
        private DateTime startingDateTime;
        private ConcurrentDictionary<String, int[]> resultMap = null;
        private ConcurrentDictionary<String, String> statusMap = null;
        private ConcurrentDictionary<String, int[]> deviceMap = null;
        private ConcurrentDictionary<String,int> exceptionMap = null;
        private  int totalTests=0;
        private int totalPassingTests = 0;



        public static LongRunObserver LongRunObserverFactory(String path)
        {
            lock(locker)
            {
                if (LongRunObserver.longRunObserver == null)
                {
                    LongRunObserver.longRunObserver = new LongRunObserver(path);
                }
                else
                {
                    if (!LongRunObserver.longRunObserver.path.Equals(path))
                    {
                        System.Diagnostics.Debug.WriteLine(String.Format("{2} : Changing Observer Path from {0} to {1} ", LongRunObserver.longRunObserver.path, path, DateTime.Now.ToString()));
                        LongRunObserver.longRunObserver = new LongRunObserver(path);

                    }
                }
            }

            return LongRunObserver.longRunObserver;
        }
        public static LongRunObserver getLongRunObserver()
        {
            return LongRunObserver.longRunObserver;
        }



        private LongRunObserver(String path)
        {
            this.path = path;
            if (!Directory.Exists(this.path)) Directory.CreateDirectory(this.path);
            this.logFilePath = path + @"\tests.log";
            resultMap = new ConcurrentDictionary<String, int[]>();
            statusMap = new ConcurrentDictionary<string, string>();
            deviceMap = new ConcurrentDictionary<String, int[]>();
            exceptionMap = new ConcurrentDictionary<String, int>();
            startingDateTime = DateTime.Now;


            using (StreamWriter streamWriter = File.CreateText(logFilePath))
            {
                String title = String.Format("{0} : {1} ", startingDateTime.ToString(), "********** Starting Long Run Test **************");
                streamWriter.WriteLine(title);
                System.Diagnostics.Debug.WriteLine(title);
            }

            this.monitorFilePath= path + @"\monitor.txt";
            using (StreamWriter streamWriter = File.CreateText(monitorFilePath))
            {
                streamWriter.WriteLine("\n*************************************************************************");
                streamWriter.WriteLine(String.Format("Starting At {0} ", startingDateTime.ToString()));
                streamWriter.WriteLine("*          Wait for statistics");

                streamWriter.WriteLine("\n*************************************************************************");

            }




        }
        public void addException(String message)
        {
            if (exceptionMap.ContainsKey(message))
            {
                exceptionMap[message]++;
            }
            else
            {
                exceptionMap.TryAdd(message, 1);
            }



        }




        public void AddTest(String testName)
        {
            int[] passAll = new int[2];
            passAll[0] = 0;
            passAll[1] = 0;
            resultMap.TryAdd(testName, passAll);
            statusMap.TryAdd(testName, "Running");
            //need to add , result Logging

        }
        public void AddDevice(String deviceName)
        {
            int[] passAll = new int[2];
            passAll[0] = 0;
            passAll[1] = 0;
            deviceMap.TryAdd(deviceName, passAll);
        }



        public void UpdateTest(String testName,Boolean success)
        {
            int[] passAll = resultMap[testName];
            passAll[0] += (success) ? 1 : 0;
            passAll[1] += 1;
            resultMap[testName] = passAll;
            printTestStatistics(testName);
            updateMonitor();

        }
        public void UpdateDevice(String deviceName, Boolean success)
        {
            int[] passAll = deviceMap[deviceName];
            passAll[0] += (success) ? 1 : 0;
            passAll[1] += 1;
            Interlocked.Increment(ref totalTests);
            if (success)
            {
                Interlocked.Increment(ref totalPassingTests);

            }
            deviceMap[deviceName] = passAll;
            updateMonitor();

        }





        public void EndTest(String testName)
        {
            statusMap[testName] = "Finished";
            //need to add , result Logging , if all are finished 

            Boolean allDone = true;
            List<String> tests = resultMap.Keys.ToList();
            foreach (String test in tests)
            {
                if (!statusMap[testName].Equals("Finished"))
                {
                    allDone = false;
                }
            }
            if (allDone)
            {
                printAllResults();
                WriteToLog("All tests are complete");
            }




        }


        private void printAllResults()
        {
            List<String> tests = resultMap.Keys.ToList();

            foreach(String testName in tests )
            {
                printTestStatistics(testName);
            }



        }

        private void printTestStatistics(string testName)
        {
            int[] passAll = resultMap[testName];
            String line = String.Format("* {0} : {1} Successes out of {2}  *", testName, passAll[0], passAll[1]);
            WriteToLog(line);
        }

        public void WriteToLog(String line)
        {
            lock (this)
            {
                using (StreamWriter streamWriter = File.AppendText(logFilePath))
                {
                    String output = String.Format("* {0} | {1} ", DateTime.Now.ToString(), line);
                    streamWriter.WriteLine(output);
                    System.Diagnostics.Debug.WriteLine(output);
                }

            }



        }
        private void updateMonitor()
        {
            lock (this)
            {
                using (StreamWriter streamWriter = File.CreateText(monitorFilePath))
                {
                    streamWriter.WriteLine("*************************************************************************");
                    streamWriter.WriteLine(String.Format("Starting At {0} ", startingDateTime.ToString()));
                    streamWriter.WriteLine("\n");

                    List<String> tests = resultMap.Keys.ToList();
                    int maxSize = 0;
                    foreach (String testName in tests)
                    {
                        if (testName.Length> maxSize)
                        {
                            maxSize = testName.Length;
                        }
                    }
                    List<String> devices = deviceMap.Keys.ToList();
                    foreach (String deviceName in devices)
                    {
                        if (deviceName.Length > maxSize)
                        {
                            maxSize = deviceName.Length;
                        }
                    }

                    String line;
                    foreach (String testName in tests)
                    {
                        int[] passAll = resultMap[testName];
                         line = String.Format("*{0} | {1} Successes out of {2} Tests ", testName.PadRight(maxSize,' '), passAll[0], passAll[1]);
                        streamWriter.WriteLine(line);

                    }

                    streamWriter.WriteLine("\n*************************************************************************\n");

                    streamWriter.WriteLine("Success  per Device :");

                    foreach (String deviceName in devices)
                    {
                        int[] passAll = deviceMap[deviceName];
                         line = String.Format("*{0} | {1} Successes out of {2}  Tests", deviceName.PadRight(maxSize, ' '), passAll[0], passAll[1]);
                        streamWriter.WriteLine(line);

                    }
                    streamWriter.WriteLine("\n");
                     line = String.Format("*{0} | {1} Successes out of {2}  Tests", "Total ", totalPassingTests, totalTests);
                    streamWriter.WriteLine(line);
                    streamWriter.WriteLine("*************************************************************************");
                    streamWriter.WriteLine("Exceptions (per message) ");

                    List<String> messages = exceptionMap.Keys.ToList();
                    foreach (String message in messages)
                    {
                        line = String.Format("*{0} | has occured {1} times", message, exceptionMap[message]);
                        streamWriter.WriteLine(line);
                    }




                    streamWriter.WriteLine(String.Format("\nUpdated  At {0} ", DateTime.Now.ToString()));
                    streamWriter.WriteLine("*************************************************************************");
                }

            }
        }





    }
}
