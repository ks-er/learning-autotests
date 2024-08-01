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

        public RegistryPage(BaseDriver driver, string path, string gridIdPart) : base(driver, path)
        {
            this.gridIdPart = gridIdPart;
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
            this.driver.Click(
                By.XPath($"//div[@class = 'x-column-header-inner']//span[text() = '{columnName}']/../..//input"),
                TimeSpan.FromSeconds(20));

            this.driver.Click(
                By.XPath($"//div[contains(@class, 'x-boundlist-list-ct')]//li[text() = '{value}']"),
                TimeSpan.FromSeconds(20));
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

        public void ScrollGrid()
        {
            var gridView = this.driver.GetElement(
                By.XPath($"//div[contains(@id, '{this.gridIdPart}-') and contains(@id, '-body')]//div[contains(@class, 'x-grid-view')]"));
            this.driver.ScrollIntoElement(gridView);
        }
    }
}
