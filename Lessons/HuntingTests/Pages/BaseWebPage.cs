namespace HuntingTests.Pages
{
    using Allure.Commons;
    using NUnit.Allure.Core;
    using OpenQA.Selenium;

    /// <summary>
    /// Основной класс с тестовыми методами, которыемогут быть применены в реестре
    /// </summary>
    public abstract class BaseWebPage
    {
        protected string path;
        public BaseDriver driver { get; private set; }

        public BaseWebPage(BaseDriver baseDriver, string path)
        {
            driver = baseDriver;
            this.path = path;
        }

        /// <summary>
        /// Открытие страницы
        /// </summary>
        public virtual void OpenPage()
        {
            AllureLifecycle.Instance.WrapInStep(() =>
            {
                this.driver.GoToUrl(path);
            }, $"Открытие страницы по {Constants.huntingUrl}{path}");
        }

        /// <summary>
        /// Нажимает на раздел
        /// </summary>
        /// <param name="sectionName"></param>
        public void ClickToSection(string sectionName)
        {
            AllureLifecycle.Instance.WrapInStep(() =>
            {
                this.driver.Click(By.XPath($"//div[contains(text(), '{sectionName}')]"));
            }, $"Нажатие на раздел: {sectionName}");
        }

        public IWebElement GetColumnHeader(string columnName)
        {
            IWebElement columHeader = null;

            AllureLifecycle.Instance.WrapInStep(() =>
            {
                columHeader = this.driver.GetElement(
                    By.XPath($"//div[@class = 'x-column-header-inner']//span[text() = '{columnName}']"));

            }, $"Получение элемента header по названияю столбца '{columnName}'");

            return columHeader;
        }

        public string GetColumnId(IWebElement webElement)
        {
            var id = this.GetAttribute(webElement, "id");
              return id.Split('-')[1];
        }

        public string GetAttribute(IWebElement elem, string attrName)
        {
            return elem.GetAttribute(attrName);
        }

        public void Logout()
        {
            AllureLifecycle.Instance.WrapInStep(() =>
            {
                this.driver.Click(
                    By.XPath("//div[contains(@class, 'desktop-bar desktop-menu')]//span[text() = 'Выход из системы']"));
            }, $"Выход из системы");
        }
    }
}
