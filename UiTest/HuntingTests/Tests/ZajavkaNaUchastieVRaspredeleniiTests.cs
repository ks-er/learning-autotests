namespace HuntingTests.Tests
{
    using HuntingTests.Pages;
    using NUnit.Allure.Attributes;
    using NUnit.Allure.Core;

    /// <summary>
    /// Класс с тестами для "Заявка на участие в распределении"
    /// </summary>
    [Parallelizable]
    [AllureParentSuite("Заявки")]
    [AllureSuite("Заявка на участие в распределении")]
    [AllureSubSuite("Реестр")]
    [TestFixture]
    [AllureNUnit]
    public class ZajavkaNaUchastieVRaspredeleniiTests : BaseTestClass
    {
        private ZajavkaNaUchastieVRaspredeleniiPage page;

        public override void SetUp()
        {
            base.SetUp();
            this.page = new ZajavkaNaUchastieVRaspredeleniiPage(this.Driver);
        }

        /// <summary>
        /// Проверяет есть ли заявки в реестре на текущую дату с ЕПГУ
        /// Должны быть ежедневно в сезон подачи заявок
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