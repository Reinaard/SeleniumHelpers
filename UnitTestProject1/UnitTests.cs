using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumHelpers;

namespace SeleniumHelpers
{
    [TestClass]
    public class UnitTests
    {
        public static Mock<IWebElement> webElement;
        public static Mock<IWebDriver> webdriver;

        [ClassInitialize]
        public static void MockStart(TestContext context)
        {
            webElement = new Mock<IWebElement>(MockBehavior.Strict);
            webElement.Setup(e => e.Text).Returns("Some text from the web element");
            webElement.Setup(e => e.Enabled).Returns(true);
            webElement.Setup(e => e.Displayed).Returns(true);
            webElement.Setup(e => e.Click());
            webElement.Setup(e => e.Clear());
            webElement.Setup(e => e.SendKeys("hello world"));
            webElement.Setup(e => e.SendKeys(Keys.Tab));
            webElement.Setup(e => e.TagName).Returns("select");
            webElement.Setup(e => e.GetAttribute("multiple")).Returns("blahjhh");

            webdriver = new Mock<IWebDriver>();
            webdriver.Setup(e => e.Manage().Timeouts().ImplicitWait).Returns(TimeSpan.FromMilliseconds(5000));
            webdriver.Setup(e => e.FindElement(By.Id("valid_id"))).Returns(webElement.Object);
            webdriver.Setup(e => e.FindElement(By.XPath("//*[text()='Some text from the web element']"))).Returns(webElement.Object);
            SeleniumHelpers.Setup(webdriver.Object);
        }

        [TestMethod]
        public void ElementIsValid_positive_scenarios()
        {
            Assert.IsTrue(SeleniumHelpers.ElementIsValid(webElement.Object));
        }

        [TestMethod]
        public void ElementIsValid_negative_scenarios()
        {
            webElement.Setup(e => e.Enabled).Returns(false);
            Assert.IsFalse(SeleniumHelpers.ElementIsValid(webElement.Object));
            webElement.Setup(e => e.Enabled).Returns(true);
            webElement.Setup(e => e.Displayed).Returns(false);
            Assert.IsFalse(SeleniumHelpers.ElementIsValid(webElement.Object));
            webElement.Setup(e => e.Enabled).Returns(false);
            Assert.IsFalse(SeleniumHelpers.ElementIsValid(webElement.Object));
            Assert.IsFalse(SeleniumHelpers.ElementIsValid(null));
        }

        [TestMethod]
        public void GetWebElement_should_find_web_element_by_id_or_null()
        {
            IWebElement element = SeleniumHelpers.GetWebElement("valid_id");
            Assert.IsTrue(SeleniumHelpers.ElementIsValid(element));
            Assert.IsTrue(null == SeleniumHelpers.GetWebElement("invalid id #"));
        }

        [TestMethod]
        public void GetWebElementText_should_find_web_element_text()
        {
            Assert.IsTrue("Some text from the web element" == SeleniumHelpers.GetWebElementText("valid_id"));
        }

        [TestMethod]
        public void Click_web_element_is_possible()
        {
            SeleniumHelpers.ClickWebElement(webElement.Object);
        }
        [TestMethod]
        public void Click_web_element_null_not_possible()
        {
            SeleniumHelpers.ClickWebElement(null);
        }

        [TestMethod]
        public void Fill_input_with_text()
        {
            SeleniumHelpers.FillInputField(webElement.Object, "hello world");
        }

        [TestMethod]
        public void GetElementByText_should_find_element_or_null()
        {
            Assert.IsTrue(null != SeleniumHelpers.GetWebElementByText("Some text from the web element"));
            Assert.IsTrue(null == SeleniumHelpers.GetWebElementByText("non-existing text"));
        }

        [TestMethod]
        public void SelectDropdownValue_should_work()
        {
            SeleniumHelpers.SelectDropdownValue("valid_id", "dropdown_value");
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Starting unit tests");
        }
    }
}
