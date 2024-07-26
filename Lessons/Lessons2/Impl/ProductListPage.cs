namespace Lessons2.Impl
{
    using OpenQA.Selenium;

    public class ProductListPage
    {
        internal string url = "https://www.saucedemo.com/";

        IWebDriver webDriver;
        By productList = By.XPath("//div[contains(@class, 'inventory_item_name')]");
        internal By labsBackpackBtn = By.XPath("//*[contains(text(), 'Sauce Labs Backpack')]/../../..//button");
        internal By labsBackpackRemoveBtn = 
            By.XPath("//*[contains(text(), 'Sauce Labs Backpack')]/../../..//button[text()='Remove']");
        internal By fleeceJacketBtn = By.XPath("//*[contains(text(), 'Sauce Labs Fleece Jacket')]/../../..//button");
        internal By testAllTheThingsBtn = By.XPath("//*[contains(text(), 'Test.allTheThings()')]/../../..//button");
        internal By productsOnCard = By.XPath("//a[contains(@class, 'shopping_cart_link')]/../../..//span[text()='3']");
        internal By lohiSortBtn = 
            By.XPath("//select[contains(@class, 'product_sort_container')]//option[contains(@value, 'lohi')]");

        By priceList = By.XPath("//div[contains(@class, 'inventory_item_price')]");
        public ProductListPage(WebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        /// <summary>
        /// Возврщает список продуктов/товаров
        /// </summary>
        internal List<IWebElement> ProductList()
        {
            return webDriver.FindElements(productList).ToList();
        }


        /// <summary>
        /// Добавляет товар в корзину
        /// </summary>
        /// <param name="by"></param>
        internal void AddToCart(By by)
        {
            var btn = webDriver.FindElement(by);
            btn.Click();
        }

        /// <summary>
        /// Сортирует товары
        /// </summary>
        /// <param name="sortBtn"></param>
        internal void SortProducts(By sortBtn)
        {
            var btn = webDriver.FindElement(sortBtn);
            btn.Click();
        }

        /// <summary>
        /// Проверка на сортировку по возрастанию
        /// </summary>
        /// <returns></returns>
        internal bool IsProductsSorted()
        {
            var prices = this.GetPrices();
            return this.IsSortedAscending(prices);
        }

        /// <summary>
        /// Возвращает список цен
        /// </summary>
        private List<decimal> GetPrices()
        {
            var list = webDriver.FindElements(priceList);
            return list
                .Select(el => Convert.ToDecimal(el.Text.ToString().Substring(1).Replace('.', ',')))
                .ToList();
        }

        /// <summary>
        /// Метод для проверки на упорядоченность по возрастанию
        /// </summary>
        /// <param name="list">Список данных</param>
        /// <returns>true - данные отсортированы по возрастанию, false - в противном случае</returns>
        private bool IsSortedAscending(List<decimal> list)
        {
            for (var index = 0; index < list.Count() - 1; index++)
            {
                if (list[index].CompareTo(list[index + 1]) > 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}