namespace HuntingTests.Pages
{
    using OpenQA.Selenium;

    /// <summary>
    /// Основной класс с тестовыми методами, которые могут быть применены в реестре
    /// </summary>
    public class RegistryPage: BaseWebPage
    {
        public RegistryPage(BaseDriver driver) : base(driver, "#")
        {
        }

        /// <summary>
        /// Заполнение фильтра
        /// </summary>
        /// <param name="filterName"></param>
        /// <param name="value"></param>
        public void FillFilter(string filterName, string value)
        {
            
        }
    }
}
