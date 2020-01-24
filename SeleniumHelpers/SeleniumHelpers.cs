﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace SeleniumHelpers
{
    public static class SeleniumHelpers
    { 
        public static IWebDriver Webdriver { get; set;}

        public static void Setup(IWebDriver driver)
        {
            Webdriver = driver;
        }

        public static void TurnOnWait(int maxTimeout = 5000)
        {
            Webdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(maxTimeout);
        }

        public static bool ElementIsValid(IWebElement element)
        {
            return element != null && element.Enabled && element.Displayed;
        }

 
        public static void ClickWebElement(this IWebElement element)
        {
            if (ElementIsValid(element))
            {
                element.Click();
            }
        }

        public static void FillInputField(this IWebElement element, string value)
        {
            if (ElementIsValid(element))
            {
                element.Click();
                element.Clear();
                element.SendKeys(value);
                element.SendKeys(Keys.Tab);
            }
        }

        public static void SelectDropdownValue(string dropdownId, string valueToSelect)
        {
            var selectElement = new SelectElement(Webdriver.FindElement(By.Id(dropdownId)));
            selectElement.SelectByText(valueToSelect);
        }

        public static IWebElement GetWebElement(string identifier, int timeout = 3000)
        {
            TurnOnWait(timeout);
            IWebElement result = null;
            By by = IsXpath(identifier) ? By.XPath(identifier) : By.Id(identifier);
            try
            {
                result = Webdriver.FindElement(by);
            }
            catch (NoSuchElementException) { }
            TurnOnWait();
            return result;
        }

        public static IWebElement GetWebElementByText(string identifier, int timeout = 3000)
        {
            string elementXpath = $"//*[text()='{identifier}']";
            return GetWebElement(elementXpath, timeout);
        }

        public static string GetWebElementText(string identifier)
        {
            return GetWebElement(identifier).Text;
        }

        public static IList<IWebElement> GetWebElementList(string identifier, int timeout = 3000)
        {
            TurnOnWait(timeout);
            IList<IWebElement> result = null;
            By by = IsXpath(identifier) ? By.XPath(identifier) : By.Id(identifier);
            try
            {
                result = Webdriver.FindElements(by);
            }
            catch (NoSuchElementException) { }
            TurnOnWait();
            return result;
        }

        public static bool IsXpath(string identifier)
        {
            return identifier.StartsWith("//") || identifier.StartsWith(".//");
        }
    }
}
