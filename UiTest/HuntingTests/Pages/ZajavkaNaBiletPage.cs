namespace HuntingTests.Pages
{
    /// <summary>
    /// Страница реестра Заявка на билет. Личный приём
    /// </summary>
    public class ZajavkaNaBiletPage : RegistryPage
    {
        public ZajavkaNaBiletPage(BaseDriver driver) 
            : base(driver,
                  "#ZajavkaNaBiletList2",
                  "rms-zajavkanabiletlist2",
                  "Заявка на билет. Личный приём")
        {
        }
    }
}
