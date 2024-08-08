namespace HuntingTests.Tests
{
    using HuntingTests.Pages;
    using NUnit.Allure.Attributes;
    using NUnit.Allure.Core;

    /// <summary>
    /// Класс с общими тестами
    /// </summary>
    [Parallelizable]
    [AllureParentSuite("Общий функционал")]
    [AllureSuite("Страница setup")]
    [AllureSubSuite("Раздел Миграции")]
    [TestFixture]
    [AllureNUnit]
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