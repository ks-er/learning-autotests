namespace HuntingTests.Tests
{
    using HuntingTests.Pages;

    /// <summary>
    /// Класс с тестами для "Заявка на билет"
    /// </summary>
    public class ZajavkaNaBiletTests : BaseTestClass
    {
        private ZajavkaNaBiletPage page;

        public override void SetUp()
        {
            base.SetUp();
            this.page = new ZajavkaNaBiletPage(this.Driver);
        }

        /// <summary>
        /// Проверяет есть ли заявки в реестре на текущую дату с ЕПГУ
        /// Поступают ежедневно
        /// </summary>
        [Test]
        public void ArrivedFromEpguTest()
        {
            this.page.OpenPage();
            this.page.ResetFilters();

            this.page.SetDateOperator("Дата заявления", "Равно");
            this.page.SetDateFilter("Дата заявления", DateTime.Now);
            this.page.ScrollGrid();

            this.page.SetStateFilter("Способ подачи заявки", "ЕПГУ");
            this.page.WaitRecords();

            Assert.IsTrue(this.page.GetRecordsCount() > 0);
        }
    }
}