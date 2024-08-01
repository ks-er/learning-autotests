namespace HuntingTests.Pages
{
    using OpenQA.Selenium;

    /// <summary>
    /// Основной класс с тестовыми методами, которые могут быть применены в реестре
    /// </summary>
    public class ZajavkaNaRazreshenie2017Page : RegistryPage
    {
        public ZajavkaNaRazreshenie2017Page(BaseDriver driver) 
            : base(driver, "#ZajavkaNaRazreshenie2017List", "rms-zajavkanarazreshenie2017list")
        {
        }
    }
}
