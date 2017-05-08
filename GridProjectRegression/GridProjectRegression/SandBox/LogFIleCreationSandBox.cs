using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace GridProjectRegression
{
    [TestClass]
    public class LogFIleCreationSandBox
    {
        [TestInitialize()]
        public void SetupTest()
        {
            
        }



        [TestMethod]
        public void TestMethod1()

        {
            string path = @"c:\temp\MyTest.txt";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Hello");
                    sw.WriteLine("And");
                    sw.WriteLine("Welcome");
                }
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine("Welcome 2");
                }
            }

            // Open the file to read from.
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
        }

    



        [TestCleanup()]
        public void TearDown()
        {



        }
    }
}
