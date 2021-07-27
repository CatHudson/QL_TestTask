using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace QL_TestTask.PageObjects
{
    class MailPage
    {
        private readonly IWebDriver driver;
        public const string url = "";

        public MailPage (IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//a[@title='Написать письмо']")]
        [CacheLookup]
        public IWebElement writeLetterButton;
    }
}
