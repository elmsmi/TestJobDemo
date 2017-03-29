using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace TestJobDemo.Tests.tests
{
    [TestClass]
    public class SelTests
    {
        //private readonly IWebDriver _firefox = new FirefoxDriver();
        //private readonly IWebDriver _chrome = new ChromeDriver();
        private readonly IWebDriver _iexplorer = new InternetExplorerDriver();

        [TestMethod]
        public void Can_Create_Account_And_Login()
        {
            var email = string.Join("", Guid.NewGuid().ToString().Take(6)) + "@gmail.com";
            var password = "Ab*?" + string.Join("", Guid.NewGuid().ToString().Take(6));
            CreateUser(_iexplorer, email, password);
            LoginUser(_iexplorer, email, password);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (_iexplorer != null)
                _iexplorer.Quit();
            //if (_firefox != null)
            //    _firefox.Quit();
            //if (_chrome != null)
            //    _chrome.Quit();
        }

        private void CreateUser(IWebDriver driver, string email, string password)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            driver.Navigate().GoToUrl("http://localhost:49710/");
            driver.Manage().Window.Maximize();
            driver.FindElement(By.Id("registerLink")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("Email")));
            driver.FindElement(By.Id("Email")).SendKeys(email);
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys(password);
            driver.FindElement(By.CssSelector(".btn.btn-default")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".nav.navbar-nav.navbar-right")));
            var userWasCreated = driver.FindElement(By.CssSelector(".nav.navbar-nav.navbar-right")).Text.Contains(email);
            Assert.IsTrue(userWasCreated);
            driver.FindElement(By.Id("logoutForm")).Submit();
        }

        private void LoginUser(IWebDriver driver, string email, string password)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            driver.Navigate().GoToUrl("http://localhost:49710/");
            driver.FindElement(By.Id("loginLink")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.Id("Email")));
            driver.FindElement(By.Id("Email")).SendKeys(email);
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.CssSelector(".btn.btn-default")).Click();
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector(".nav.navbar-nav.navbar-right")));
            var userWasCreated = driver.FindElement(By.CssSelector(".nav.navbar-nav.navbar-right")).Text.Contains(email);
            Assert.IsTrue(userWasCreated);
            driver.FindElement(By.Id("logoutForm")).Submit();
        }
    }
}



