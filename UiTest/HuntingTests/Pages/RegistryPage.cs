namespace HuntingTests.Pages
{
    using Allure.Commons;
    using NUnit.Allure.Core;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;
    using SeleniumExtras.WaitHelpers;

    /// <summary>
    /// Основной класс с тестовыми методами, которые могут быть применены в реестре
    /// </summary>
    public abstract class RegistryPage : BaseWebPage
    {
        private string gridIdPart;
        private string registryName;

        public RegistryPage(
            BaseDriver driver, 
            string path, 
            string gridIdPart,
            string registryName) : base(driver, path)
        {
            this.gridIdPart = gridIdPart;
            this.registryName = registryName;
        }

        /// <summary>
        /// Открытие страницы
        /// </summary>
        public override void OpenPage()
        {
            IWebElement element = null;
            AllureLifecycle.Instance.WrapInStep(() =>
            {
                element = this.driver.GetElement(By.XPath("//div[@class = 'title-body']"));
                if (element?.Text == this.registryName)
                {
                    return;
                }
            }, $"Проверка открытого реестра '{this.registryName}'");

            AllureLifecycle.Instance.WrapInStep(() =>
            {
                this.driver.GoToUrl(path);
            }, $"Открытие страницы по {Constants.huntingUrl}{path}");
        }

        public void WaitRecords()
        {
            AllureLifecycle.Instance.WrapInStep(() =>
            {
                Thread.Sleep(3000);
            }, $"Ожидание записей в реестре");
        }

        public void SetDateOperator(
           string columnName,
           string btnText)
        {
            var columnHeader = this.GetColumnHeader(columnName);
            var id = this.GetColumnId(columnHeader);

            AllureLifecycle.Instance.WrapInStep(() =>
            {
                this.driver.MoveToElementAndClick(
                By.XPath($"//div[@id = 'gridcolumn-{id}']//tr[contains(@id, 'datefield-')]//div[contains(@class, '-operator-button')]"));
                this.driver.Click(By.XPath($"//a[contains(@class, 'x-menu-item-link')]//span[contains(text(), '{btnText}')]"),
                    TimeSpan.FromSeconds(20));
            }, $"Установка оператора сравнения даты '{btnText}' для столбца '{columnName}'");
        }

        public void ResetFilters()
        {
            AllureLifecycle.Instance.WrapInStep(() =>
            {
                this.driver.Click(By.XPath("//span[text() = 'Сбросить фильтрацию']"));
            }, $"Сброс фильтрации в реестре");
        }

        /// <summary>
        /// Установка фильтра
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        public void SetDateFilter(string columnName, DateTime dateTime)
        {
            var value = dateTime.ToString("d");
            this.SetFilter(columnName, value, true);
        }

        /// <summary>
        /// Установка фильтра
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <param name="pressEnter"></param>
        public void SetFilter(string columnName, string value, bool pressEnter = false)
        {
            By locator = null;

            AllureLifecycle.Instance.WrapInStep(() =>
            {
                locator = By.XPath($"//div[@class = 'x-column-header-inner']//span[text() = '{columnName}']/../..//input");
                this.driver.SendKeys(locator, value, pressEnter);
            }, $"Установка значения фильтра в значение:'{value}' в столбце:'{columnName}'");
        }

        /// <summary>
        /// Установка фильтра
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        public void SetStateFilter(string columnName, string value)
        {
            var columnLocator = By.XPath($"//div[@class = 'x-column-header-inner']//span[text() = '{columnName}']/../..//input");
            IWebElement elementToClick = null;  

            AllureLifecycle.Instance.WrapInStep(() =>
            {
                elementToClick = this.driver.GetElement(columnLocator);
                this.driver.Click(elementToClick, columnLocator, TimeSpan.FromSeconds(20));

                this.driver.Click(
                    By.XPath($"//div[contains(@class, 'x-boundlist-list-ct')]//li[text() = '{value}']"),
                    TimeSpan.FromSeconds(20));

                elementToClick.SendKeys(Keys.Enter);
            }, $"Установка значения фильтра в значение:'{value}' в столбце статуса:'{columnName}'");
        }

        /// <summary>
        /// Установка фильтра
        /// </summary>
        /// <param name="filterName"></param>
        /// <param name="value"></param>
        public long GetRecordsCount()
        {
            var isNoRecords = false;
            AllureLifecycle.Instance.WrapInStep(() =>
            {
                isNoRecords = this.driver.IsElementExist(
                 By.XPath("//div[contains(text(), 'Нет данных для отображения')]"));                
            }, $"Проверка на пустоту реестра");

            if (isNoRecords)
            {
                return 0;
            }

            IWebElement pagingtoolbar = null;
            string rowCount = null;

            AllureLifecycle.Instance.WrapInStep(() =>
            {
                isNoRecords = this.driver.IsElementExist(
                 By.XPath("//div[contains(text(), 'Нет данных для отображения')]"));
                pagingtoolbar = this.driver.GetElement(By.XPath("//div[contains(text(), 'Записи с')]"));
                rowCount = pagingtoolbar?.Text.Split(' ').Last();

            }, $"Получение количества записейв реестре");
            
            return Convert.ToInt64(rowCount);
        }

        public void ResreshGrid()
        {
            this.driver.Click(
                By.XPath("//span[contains(@class, 'x-tbar-loading')]"),
                TimeSpan.FromSeconds(30));
        }

        public void WaitGridStore()
        {
            this.driver.WaitGridStore(
                By.XPath($"//div[contains(@id, '{this.gridIdPart}-') and contains(@id, '-body')]"));
        }

        public void ScrollGrid(string? startValue = null)
        {
            IWebElement gridView = null;
            var scrollStr = startValue == "0" ? " в начало" : string.Empty;
            AllureLifecycle.Instance.WrapInStep(() =>
            {
                gridView = this.driver.GetElement(
                By.XPath($"//div[contains(@id, '{this.gridIdPart}-') and contains(@id, '-body')]//div[contains(@class, 'x-grid-view')]"));
                this.driver.ScrollIntoElement(gridView, startValue);
            }, $"Прокручивание грида реестра{scrollStr}");
        }
    }
}
