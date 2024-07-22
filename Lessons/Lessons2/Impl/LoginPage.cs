namespace Lessons2.Impl
{
    using OpenQA.Selenium;

    public class LoginPage
    {
        internal string url = "https://www.saucedemo.com/";

        IWebDriver webDriver;
        By loginInput = By.XPath("//input[@name='user-name']");
        By passInput = By.XPath("//input[@name='password']");
        By logInButton = By.XPath("//input[@value='Login']");

        //private string headerLogoXpath = "//a[contains(@class, "tm-header__logo")]";

        //    header_search_xpath = '//a[contains(@class, "tm-header-user-menu__search")]'
        //search_input_xpath = '//div[contains(@class, "tm-search__input")]//input'
        //search_button_xpath = '//div[contains(@class, "tm-search__input")]//span'
        //news_block_xpath = '//section[@id="news_block_1"]'


        public LoginPage(WebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        /// <summary>
        /// Методж авторизации
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        internal void Authorization(string login, string pass)
        {
            var l = webDriver.FindElement(loginInput);

            l.SendKeys(login);

            var t = l.FindElement(passInput);
            t.SendKeys(pass);
            webDriver.FindElement(logInButton).Click();
        }

        internal bool IsElementExist(By xpath)
        {
            var element = webDriver.FindElement(xpath);
            return element != null;
        }
    }
}