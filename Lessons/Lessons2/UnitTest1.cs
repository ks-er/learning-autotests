namespace Lessons2
{
    using System;
    using Lessons2.Impl;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public class Tests
    {
        private const string AuthorizationEmptyErrorMessage = "Epic sadface: Username is required";
        private const string AuthorizationPasswordErrorMessage = 
            "Epic sadface: Username and password do not match any user in this service";

        private IWebDriver driver;
        private LoginPage loginPage;

        [OneTimeSetUp]
        public void Setup()
        {
            var dr = new ChromeDriver();
            this.driver = dr;
            this.loginPage = new LoginPage(dr);
        }

        [OneTimeTearDown]
        public void TearDown() 
        {
            this.driver.Quit(); 
        }

        [Test]
        public void TestAuthorizationEmptyError()
        {
            this.driver.Navigate().GoToUrl(this.loginPage.url);
            var errorMessage = By.XPath($"//div[contains(h3,'{Tests.AuthorizationEmptyErrorMessage}')]");

            this.loginPage.Authorization(String.Empty, String.Empty);

            // Проверка появления сообщения об ошибке
            Assert.IsTrue(this.loginPage.IsElementExist(errorMessage));
        }

        [Test]
        public void TestAuthorizationPasswordError()
        {
            this.driver.Navigate().GoToUrl(this.loginPage.url);
            var errorMessage = By.XPath($"//div[contains(h3,'{Tests.AuthorizationPasswordErrorMessage}')]");

            this.loginPage.Authorization("performance_glitch_user", "performance_glitch_user");

            // Проверка появления сообщения об ошибке
            Assert.IsTrue(this.loginPage.IsElementExist(errorMessage));
        }
    }
}