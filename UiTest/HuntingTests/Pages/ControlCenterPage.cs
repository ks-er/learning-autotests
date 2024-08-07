﻿namespace HuntingTests.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Allure.Commons;
    using NUnit.Allure.Core;
    using OpenQA.Selenium;

    /// <summary>
    /// Класс с методами для сс
    /// </summary>
    public class ControlCenterPage: BaseWebPage
    {
        private By GoToMainLocator = By.XPath("//span[contains(text(), 'Перейти на главную')]");

        public ControlCenterPage(BaseDriver driver) : base(driver, "/action/setup") 
        {
        }

        public IWebElement GetVersions(string columnName)
        {
            var columHeader = this.driver.GetElement(
                By.XPath($"//div[@class = 'x-column-header-inner']//span[text() = '{columnName}'])"));
            return columHeader;
        }

        public bool CompareVersions()
        {
            var currVersionHeader = this.GetColumnHeader("Текущая версия");
            var newVersionHeader = this.GetColumnHeader("Новая версия");

            var currVersionList = this.GetVersionList(this.GetColumnId(currVersionHeader));
            var newVersionList = this.GetVersionList(this.GetColumnId(newVersionHeader));

            return currVersionList.SequenceEqual(newVersionList);
        }

        public void GoToStartPage()
        {
            AllureLifecycle.Instance.WrapInStep(() =>
            {
                this.driver.Click(this.GoToMainLocator);
            }, $"Переход на главную стартовую страницу");
        }

        /// <summary>
        /// Возвращает список цен
        /// </summary>
        private List<string> GetVersionList(string elementId)
        {
            var versions = By.XPath(
                $"//td[contains(@class, ' x-grid-cell x-grid-cell-gridcolumn-{elementId}')]//div[contains(@class, 'x-grid-cell-inner')]");
            var versionList = this.driver.GetElements(versions);
            return versionList
                .Select(el => el.Text.ToString())
                .ToList();
        }
    }
}
