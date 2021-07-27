using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace QL_TestTask.PageObjects
{
    class HomePage
    {
        private readonly IWebDriver driver;

        public HomePage (IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Name, Using = "login")]
        [CacheLookup]
        public IWebElement loginField;

        [FindsBy(How = How.Name, Using = "password")]
        [CacheLookup]
        public IWebElement passwordField;

        [FindsBy(How = How.Name, Using = "domain")]
        [CacheLookup]
        public IWebElement domainField;

        [FindsBy(How = How.Id, Using = "q")]
        [CacheLookup]
        public IWebElement searchField;

    }
}
