using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.IE;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using QL_TestTask.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Interactions;

namespace QL_TestTask
{
    [TestFixture]
    public class Tests
    {
        const string mainPageUrl = "https://mail.ru";

        [Test]
        [TestCase("login", "domain", "password", "addressee", "letterTheme", "letterBody")]
        public void SendLetter_Test(string login, string domain, string password, string addressee, string letterTheme, string letterBody)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Не задан логин либо пароль");
            var driver = initDriver();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            driver.Navigate().GoToUrl(mainPageUrl);
            driver.Manage().Window.Maximize();
            HomePage page = new HomePage(driver);
            SelectElement select = new SelectElement(page.domainField);
            MailPage mailPage = new MailPage(driver);
            try
            {
                select.SelectByText(domain);
                page.loginField.SendKeys(login + Keys.Enter);
                wait.Until(ExpectedConditions.ElementToBeClickable(page.passwordField));
                page.passwordField.SendKeys(password + Keys.Enter);
                wait.Until(ExpectedConditions.ElementToBeClickable(mailPage.writeLetterButton));
            }
            catch (Exception ex) when (ex is WebDriverTimeoutException || ex is NoSuchElementException)
            {
                throw new ArgumentException("Были введены невалидные данные для входа");
            };
            mailPage.writeLetterButton.Click();
            LetterPopUpPage popUp = new LetterPopUpPage(driver);
            wait.Until(ExpectedConditions.ElementToBeClickable(popUp.addresseeField));
            Actions actions = new Actions(driver);
            actions.SendKeys(addressee + Keys.Tab);
            actions.SendKeys(letterTheme + Keys.Tab);
            actions.SendKeys(Keys.Tab);
            actions.SendKeys(letterBody + Keys.Tab);
            actions.SendKeys(Keys.Enter);
            try
            {
                actions.Perform();
                wait.Until(ExpectedConditions.ElementToBeClickable(popUp.letterSentLabel));
            }
            catch (WebDriverTimeoutException) 
            {
                throw new ArgumentException("Были введены некорректные данные для отправки письма");
            };
            Assert.True(popUp.letterSentLabel.Displayed);
            driver.Quit();
        }

        private IWebDriver initDriver (string driverName = "Chrome")
        {
            switch(driverName.ToUpper())
            {
                case "CHROME":
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    return new ChromeDriver();
                case "FIREFOX":
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    return new FirefoxDriver();
                case "EDGE":
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    return new EdgeDriver();
                case "OPERA":
                    new DriverManager().SetUpDriver(new OperaConfig());
                    return new OperaDriver();
                case "IE":
                    new DriverManager().SetUpDriver(new InternetExplorerConfig());
                    return new InternetExplorerDriver();
                default:
                    throw new ArgumentException("Неизвестный или неподдерживаемый браузер");
            }
        }
    }
}