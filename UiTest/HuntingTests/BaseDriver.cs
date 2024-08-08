namespace HuntingTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Remote;
    using OpenQA.Selenium.Support.Extensions;
    using OpenQA.Selenium.Support.UI;
    using SeleniumExtras.WaitHelpers;

    /// <summary>
    /// Базовый класс с обертками selenium
    /// </summary>
    public class BaseDriver
    {
        /// <summary>
        /// Переменная для хранения информации о браузере
        /// </summary>
        private IWebDriver driver;

        private Actions actions;

        private string remoteWd = "http://localhost:4444/";

        /// <summary>
        /// Инициализация браузера
        /// </summary>
        public BaseDriver()
        {
            this.driver = this.StartRemoteBrowser();
            this.actions = new Actions(this.driver);
        }

        /// <summary>
        /// Метод создания браузера
        /// </summary>
        /// <returns></returns>
        private WebDriver StartBrowser()
        { 
            var driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            return driver;
        }

        private RemoteWebDriver StartRemoteBrowser()
        {
            var options = new ChromeOptions();

            var selenoidOptions = new Dictionary<string, object>();
            selenoidOptions.Add("enableVNC", true);
            selenoidOptions.Add("browser", "chrome");
            selenoidOptions.Add("version", "126.0");

            options.AddAdditionalOption("selenoid:options", selenoidOptions);
            options.AddArgument("start-maximized");

            var driver = new RemoteWebDriver(
                new Uri(this.remoteWd + "wd" + Path.DirectorySeparatorChar + "hub"), options.ToCapabilities());

            return driver;
        }

        /// <summary>
        /// Выход из браузера
        /// </summary>
        public void Queit()
        {
            this.driver.Quit();
        }

        /// <summary>
        /// Переход по url
        /// </summary>
        /// <param name="url"></param>
        public void GoToUrl(string url = "")
        {
            driver.Url = Constants.huntingUrl + url;
            driver.Navigate();//.Refresh();
        }

        /// <summary>
        /// Возвращает элемент
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        public IWebElement GetElement(By locator)
        {
            this.WaitUntilElementExists(locator);
            return driver.FindElement(locator);
        }

        /// <summary>
        /// Возвращает список элементов
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        public List<IWebElement> GetElements(By locator)
        {
            return driver.FindElements(locator).ToList();
        }

        /// <summary>
        /// Наводит курсор и нажимает на элемент
        /// TODO ContextClick ?
        /// </summary>
        /// <param name="locator"></param>
        public void MoveToElementAndClick(By locator, TimeSpan timeSpan = default)
        {
            var webElement = this.GetElement(locator);
            this.WaitUntilElementExists(locator, timeSpan);
            this.actions.MoveToElement(webElement).Click().Perform();
        }

        /// <summary>
        /// Нажимает на элемент
        /// </summary>
        /// <param name="locator"></param>
        public void Click(By locator, TimeSpan timeSpan = default)
        {
            var elementToClick = this.GetElement(locator);
            this.Click(elementToClick, locator, timeSpan);
        }

        /// <summary>
        /// Нажимает на элемент
        /// </summary>
        /// <param name="locator"></param>
        public void Click(
            IWebElement elementToClick,
            By locator,
            TimeSpan timeSpan = default)
        {
            this.WaitUntilElementClicable(locator, timeSpan);
            elementToClick.Click();
        }

        /// <summary>
        /// Отправка значений в элемент
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="value"></param>
        /// <param name="pressEnter"></param>
        public void SendKeys(By locator, string value, bool pressEnter = false)
        {
            var elementToSendKey = GetElement(locator);

            this.WaitUntilElementClicable(locator);

            elementToSendKey.SendKeys(value);

            if (pressEnter)
            {
                elementToSendKey.SendKeys(Keys.Enter);
            }
        }

        public void ScrollIntoElement(IWebElement webElement, string startValue)
        {
            var js = (IJavaScriptExecutor)driver;
            var scrollValue = startValue != null
                ? startValue
                : "arguments[0].scrollWidth";
            
            js.ExecuteScript($"arguments[0].scrollTo({scrollValue}, arguments[0].scrollWidth);", webElement);
        }

        /// <summary>
        /// Проверка на наличие элемента на странице
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public bool IsElementExist(By xpath)
        {
            var elements = this.driver.FindElements(xpath);
            return elements.Count() == 1;
        }

        public void WaitGridStore(By locator, TimeSpan timeSpan = default)
        {
            if (timeSpan == default)
            {
                timeSpan = TimeSpan.FromSeconds(30);
            }

            var wait = new WebDriverWait(driver, timeSpan);
            wait.Until(ExpectedConditions.ElementExists(locator));
        }

        /// <summary>
        /// Проверяет на существование элемента
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="timeSpan"></param>
        /// <exception cref="Exception"></exception>
        private void WaitUntilElementExists(By locator, TimeSpan timeSpan = default)
        {
            try
            {
                if (timeSpan == default)
                {
                    timeSpan = TimeSpan.FromSeconds(10);
                }

                var wait = new WebDriverWait(driver, timeSpan);
                wait.Until(ExpectedConditions.ElementExists(locator));
            }
            catch (Exception ex)
            {
                throw new Exception($"{locator} не существует элемента");
            }

        }

        /// <summary>
        /// Проверяет на кликабельность элемента
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="timeSpan"></param>
        /// <exception cref="Exception"></exception>
        private void WaitUntilElementClicable(By locator, TimeSpan timeSpan = default)
        {
            try
            {
                if (timeSpan == default)
                {
                    timeSpan = TimeSpan.FromSeconds(10);
                }

                var wait = new WebDriverWait(driver, timeSpan);
                wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            }
            catch (Exception ex)
            {
                throw new Exception($"{locator} не кликабелен");
            }
        }
    }
}
