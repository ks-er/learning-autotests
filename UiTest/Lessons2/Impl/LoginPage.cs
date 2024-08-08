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

        public LoginPage(WebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public void NavigateAuth(string login, string pass)
        {
            this.webDriver.Navigate().GoToUrl(this.url);
            this.Authorization(login, pass);
        }

        /// <summary>
        /// Проверка на наличие элемента на странице
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        internal bool IsElementExist(By xpath)
        {
            var element = webDriver.FindElement(xpath);
            return element != null;
        }

        /// <summary>
        /// Метод авторизации
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        private void Authorization(string login, string pass)
        {
            var loginEl = webDriver.FindElement(loginInput);

            loginEl.SendKeys(login);

            var passEl = loginEl.FindElement(passInput);
            passEl.SendKeys(pass);
            webDriver.FindElement(logInButton).Click();
        }
    }
}