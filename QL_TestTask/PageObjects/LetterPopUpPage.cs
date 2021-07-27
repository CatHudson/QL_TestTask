using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace QL_TestTask.PageObjects
{
    class LetterPopUpPage
    {
        private readonly IWebDriver driver;

        public LetterPopUpPage (IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//input[@type='text' and @tabindex='100']")]
        [CacheLookup]
        public IWebElement addresseeField;

        [FindsBy(How = How.XPath, Using = "//input[@type='text' and @name='Subject' and @tabindex='400']")]
        [CacheLookup]
        public IWebElement letterThemeField;

        [FindsBy(How = How.XPath, Using = "//span[@class='button2__txt' and text()='Отправить']")]
        [CacheLookup]
        public IWebElement sendLetterButton;

        [FindsBy(How = How.XPath, Using = "//span[@class='button2__txt' and text()='Сохранить']")]
        [CacheLookup]
        public IWebElement saveLetterButton;

        [FindsBy(How = How.XPath, Using = "//span[@class='button2__txt' and text()='Отменить']")]
        [CacheLookup]
        public IWebElement closeLetterButton;

        [FindsBy(How = How.XPath, Using = "//a[text()='Письмо отправлено']")]
        [CacheLookup]
        public IWebElement letterSentLabel;
    }
}
