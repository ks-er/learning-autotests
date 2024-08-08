namespace HuntingTests.Pages
{
    /// <summary>
    /// Страница реестра Заявка на участие в распределении
    /// </summary>
    public class ZajavkaNaUchastieVRaspredeleniiPage : RegistryPage
    {
        public ZajavkaNaUchastieVRaspredeleniiPage(BaseDriver driver) 
            : base(driver,
                  "#ZajavkaNaUchastieVRaspredeleniiList",
                  "rms-zajavkanauchastievraspredeleniilist",
                  "Реестр Заявка на участие в распределении")
        {
        }
    }
}
