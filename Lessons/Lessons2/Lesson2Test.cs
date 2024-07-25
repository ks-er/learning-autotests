namespace Lessons2
{
    using System;
    using Lessons2.Impl;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    public class Lesson2Test
    {
        private const string AuthorizationEmptyErrorMessage = "Epic sadface: Username is required";
        private const string AuthorizationPasswordErrorMessage = 
            "Epic sadface: Username and password do not match any user in this service";

        private IWebDriver driver;
        private LoginPage loginPage;
        private ProductListPage productListPage;

        [OneTimeSetUp]
        public void Setup()
        {
            var dr = new ChromeDriver();
            this.driver = dr;
            this.loginPage = new LoginPage(dr);
            this.productListPage = new ProductListPage(dr);
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
            var errorMessage = By.XPath($"//div[contains(h3,'{Lesson2Test.AuthorizationEmptyErrorMessage}')]");

            this.loginPage.Authorization(String.Empty, String.Empty);

            // Проверка появления сообщения об ошибке
            Assert.IsTrue(this.loginPage.IsElementExist(errorMessage));
        }

        [Test]
        public void TestAuthorizationPasswordError()
        {
            this.driver.Navigate().GoToUrl(this.loginPage.url);
            var errorMessage = By.XPath($"//div[contains(h3,'{Lesson2Test.AuthorizationPasswordErrorMessage}')]");

            this.loginPage.Authorization("performance_glitch_user", "performance_glitch_user");

            // Проверка появления сообщения об ошибке
            Assert.IsTrue(this.loginPage.IsElementExist(errorMessage));
        }

        [Test]
        public void TestSuccessAuth()
        {
            this.driver.Navigate().GoToUrl(this.loginPage.url);
            var titleElem = By.XPath($"//title[contains(text(),'Swag Labs')]");

            this.loginPage.Authorization("performance_glitch_user", "performance_glitch_user");

            Assert.IsTrue(this.loginPage.IsElementExist(titleElem));
        }

        [Test]
        public void TestProductListCount()
        {
            this.driver.Navigate().GoToUrl(this.loginPage.url);            
            this.loginPage.Authorization("performance_glitch_user", "secret_sauce");
            Assert.That(6, Is.EqualTo(this.productListPage.ProductList().Count()));
        }

        [Test]
        public void TestAddToCard()
        {
            this.driver.Navigate().GoToUrl(this.loginPage.url);
            this.loginPage.Authorization("performance_glitch_user", "secret_sauce");
            this.productListPage.AddToCart(this.productListPage.labsBackpackBtn);

            Assert.IsTrue(this.loginPage.IsElementExist(this.productListPage.labsBackpackRemoveBtn));
        }

        [Test]
        public void TestAddSomeProductsToCard()
        {
            this.driver.Navigate().GoToUrl(this.loginPage.url);
            this.loginPage.Authorization("performance_glitch_user", "secret_sauce");
            this.productListPage.AddToCart(this.productListPage.labsBackpackBtn);
            this.productListPage.AddToCart(this.productListPage.fleeceJacketBtn);
            this.productListPage.AddToCart(this.productListPage.testAllTheThingsBtn);

            Assert.IsTrue(this.loginPage.IsElementExist(this.productListPage.productsOnCard));
        }

        [Test]
        public void TestSortProducts()
        {
            this.driver.Navigate().GoToUrl(this.loginPage.url);
            this.loginPage.Authorization("performance_glitch_user", "secret_sauce");
            this.productListPage.SortProducts(this.productListPage.lohiSortBtn);
            
            Assert.IsTrue(this.productListPage.IsProductsSorted());
        }
    }
}