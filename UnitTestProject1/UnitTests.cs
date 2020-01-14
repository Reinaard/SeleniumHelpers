using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumHelpers;

namespace SeleniumHelpers
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void get_web_element_text()
        {
            Assert.IsTrue("Some text from the web element" == SeleniumHelpers.GetWebElementText("valid_id"));
        }
        [TestMethod]
        public void null_when_no_web_element_found()
        {
            Assert.IsTrue(null == SeleniumHelpers.GetWebElementText("invalid id #"));
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Starting unit tests");
        }
    }
}
