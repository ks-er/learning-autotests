namespace HuntingTests.Tests
{
    using HuntingTests.Pages;

    /// <summary>
    /// Класс с тестами для "Заявка на разрешение 2017"
    /// </summary>
    public class ZajavkaNaRazreshenie2017Tests : BaseTestClass
    {
        private ZajavkaNaRazreshenie2017Page page;

        public override void SetUp()
        {
            base.SetUp();
            this.page = new ZajavkaNaRazreshenie2017Page(this.Driver);
        }

        /// <summary>
        /// Проверяет не зависла ли отправка статусов олпат на ЕПГУ
        /// </summary>
        [Test]
        public void SendPaidStateToEpguTest()
        {
            this.page.OpenPage();
            this.page.ResetFilters();
            this.page.ScrollGrid("0");

            this.page.SetDateOperator("Дата заявления", "Больше или равно");
            this.page.SetDateFilter("Дата заявления", DateTime.Now.AddMonths(-2));
            this.page.SetStateFilter("Статус оплаты", "Ожидает оплаты");
            this.page.ScrollGrid();
            this.page.SetStateFilter("Квитирование ГИС ГМП", "Сквитировано");
            this.page.WaitRecords();

            Assert.IsTrue(this.page.GetRecordsCount() < 100);
        }

        [Test]
        public void SendPaymentToGISTest()
        {
            this.page.OpenPage();
            this.page.ResetFilters();
            this.page.ScrollGrid("0");

            this.page.SetDateOperator("Дата заявления", "Больше или равно");
            this.page.SetDateFilter("Дата заявления", DateTime.Now.AddMonths(-1));
            this.page.SetStateFilter("Статус заявки", "Новая");
            this.page.SetStateFilter("Статус оплаты", "Не отправлено");
            this.page.ScrollGrid();
            this.page.SetStateFilter("Способ подачи заявки", "ЕПГУ");
            this.page.SetStateFilter("Этап проверки", "Пройдено");
            this.page.WaitRecords();

            Assert.IsTrue(this.page.GetRecordsCount() < 100);
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
            this.page.ScrollGrid("0");

            this.page.SetDateOperator("Дата заявления", "Равно");
            this.page.SetDateFilter("Дата заявления", DateTime.Now);
            this.page.ScrollGrid();

            this.page.SetStateFilter("Способ подачи заявки", "ЕПГУ");
            this.page.WaitRecords();

            Assert.IsTrue(this.page.GetRecordsCount() > 0);
        }
    }
}