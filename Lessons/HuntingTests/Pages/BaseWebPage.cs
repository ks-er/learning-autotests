﻿namespace HuntingTests.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
            this.driver.GoToUrl(path);
        }

        /// <summary>
        /// Нажимает на раздел
        /// </summary>
        /// <param name="sectionName"></param>
        public void ClickToSection(string sectionName)
        {
            this.driver.Click(By.XPath($"//div[contains(text(), '{sectionName}')]"));
        }

        public IWebElement GetColumnHeader(string columnName)
        {
            var columHeader = this.driver.GetElement(
                By.XPath($"//div[@class = 'x-column-header-inner']//span[text() = '{columnName}']"));
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
            this.driver.Click(
                By.XPath("//div[contains(@class, 'desktop-bar desktop-menu')]//span[text() = 'Выход из системы']"));
        }
    }
}
