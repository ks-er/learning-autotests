namespace HuntingTests.Pages
{
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
            var element = this.driver.GetElement(By.XPath("//div[@class = 'title-body']"));
            if (element?.Text == this.registryName)
            {
                return;
            }

            this.driver.GoToUrl(path);
        }

        public void WaitRecords()
        {
            Thread.Sleep(3000);
        }

        public void SetDateOperator(
           string columnName,
           string btnText)
        {
            var columnHeader = this.GetColumnHeader(columnName);
            var id = this.GetColumnId(columnHeader);

            this.driver.MoveToElementAndClick(
                By.XPath($"//div[@id = 'gridcolumn-{id}']//tr[contains(@id, 'datefield-')]//div[contains(@class, '-operator-button')]"));
            this.driver.Click(By.XPath($"//a[contains(@class, 'x-menu-item-link')]//span[contains(text(), '{btnText}')]"),
                TimeSpan.FromSeconds(20));
        }

        public void ResetFilters()
        {
            this.driver.Click(By.XPath("//span[text() = 'Сбросить фильтрацию']"));
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
            var locator = By.XPath($"//div[@class = 'x-column-header-inner']//span[text() = '{columnName}']/../..//input");

            this.driver.SendKeys(locator, value, pressEnter);
        }

        /// <summary>
        /// Установка фильтра
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        public void SetStateFilter(string columnName, string value)
        {
            var columnLocator = By.XPath($"//div[@class = 'x-column-header-inner']//span[text() = '{columnName}']/../..//input");
            var elementToClick = this.driver.GetElement(columnLocator);

            this.driver.Click(elementToClick, columnLocator, TimeSpan.FromSeconds(20));

            this.driver.Click(
                By.XPath($"//div[contains(@class, 'x-boundlist-list-ct')]//li[text() = '{value}']"),
                TimeSpan.FromSeconds(20));

            elementToClick.SendKeys(Keys.Enter);
        }

        /// <summary>
        /// Установка фильтра
        /// </summary>
        /// <param name="filterName"></param>
        /// <param name="value"></param>
        public long GetRecordsCount()
        {
            var isNoRecords = this.driver.IsElementExist(
                By.XPath("//div[contains(text(), 'Нет данных для отображения')]"));

            if (isNoRecords)
            {
                return 0;
            }

            var pagingtoolbar = this.driver.GetElement(By.XPath("//div[contains(text(), 'Записи с')]"));
            var rowCount = pagingtoolbar.Text.Split(' ').Last();
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
            var gridView = this.driver.GetElement(
                By.XPath($"//div[contains(@id, '{this.gridIdPart}-') and contains(@id, '-body')]//div[contains(@class, 'x-grid-view')]"));
            this.driver.ScrollIntoElement(gridView, startValue);
        }
    }
}
