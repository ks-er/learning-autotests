namespace HuntingTests.Pages
{
    /// <summary>
    /// Страница реестра Заявка на разрешение
    /// </summary>
    public class ZajavkaNaRazreshenie2017Page : RegistryPage
    {
        public ZajavkaNaRazreshenie2017Page(BaseDriver driver) 
            : base(driver, 
                  "#ZajavkaNaRazreshenie2017List", 
                  "rms-zajavkanarazreshenie2017list", 
                  "Заявка на разрешение")
        {
        }
    }
}
