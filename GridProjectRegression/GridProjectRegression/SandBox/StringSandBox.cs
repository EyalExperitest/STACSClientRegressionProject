using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GridProjectRegression
{
    [TestClass]
    public class StringSandBox
    {
        [TestMethod]
        public void TestMethod1()
        {
            Console.WriteLine("A".PadRight(10,' ')+ "|");
            Console.WriteLine("012345678");



        }
    }
}
