namespace HuntingTests.Tests
{
    using HuntingTests.Pages;

    /// <summary>
    /// Класс с общими тестами
    /// </summary>
    public class CommonTests : BaseTestClass
    {
        private ControlCenterPage page;

        public override void SetUp()
        {
            this.ccPage = true;
            base.SetUp();
            this.page = new ControlCenterPage(this.Driver);
        }

        /// <summary>
        /// Проверяет проведены ли все миграции
        /// </summary>
        [Test]
        public void MigrationsCarriedOutTest()
        {
            this.page.OpenPage();
            this.page.ClickToSection("Миграция");

            Assert.IsTrue(this.page.CompareVersions());
            this.page.GoToStartPage();
        }
    }
}