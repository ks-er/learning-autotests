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
            this.loginPage.NavigateAuth(String.Empty, String.Empty);
            var errorMessage = By.XPath($"//div[contains(h3,'{Lesson2Test.AuthorizationEmptyErrorMessage}')]");

            // Проверка появления сообщения об ошибке
            Assert.IsTrue(this.loginPage.IsElementExist(errorMessage));
        }

        [Test]
        public void TestAuthorizationPasswordError()
        {
            this.loginPage.NavigateAuth("performance_glitch_user", "performance_glitch_user");            
            var errorMessage = By.XPath($"//div[contains(h3,'{Lesson2Test.AuthorizationPasswordErrorMessage}')]");

            // Проверка появления сообщения об ошибке
            Assert.IsTrue(this.loginPage.IsElementExist(errorMessage));
        }

        [Test]
        public void TestSuccessAuth()
        {
            this.loginPage.NavigateAuth("performance_glitch_user", "performance_glitch_user");

            var titleElem = By.XPath($"//title[contains(text(),'Swag Labs')]");
            Assert.IsTrue(this.loginPage.IsElementExist(titleElem));
        }

        [Test]
        public void TestProductListCount()
        {
            this.loginPage.NavigateAuth("performance_glitch_user", "secret_sauce");
            Assert.That(6, Is.EqualTo(this.productListPage.ProductList().Count()));
        }

        [Test]
        public void TestAddToCard()
        {
            this.loginPage.NavigateAuth("performance_glitch_user", "secret_sauce");
            this.productListPage.AddToCart(this.productListPage.labsBackpackBtn);

            Assert.IsTrue(this.loginPage.IsElementExist(this.productListPage.labsBackpackRemoveBtn));
        }

        [Test]
        public void TestAddSomeProductsToCard()
        {
            this.loginPage.NavigateAuth("performance_glitch_user", "secret_sauce");
            this.productListPage.AddToCart(this.productListPage.labsBackpackBtn);
            this.productListPage.AddToCart(this.productListPage.fleeceJacketBtn);
            this.productListPage.AddToCart(this.productListPage.testAllTheThingsBtn);

            Assert.IsTrue(this.loginPage.IsElementExist(this.productListPage.productsOnCard));
        }

        [Test]
        public void TestSortProducts()
        {
            this.loginPage.NavigateAuth("performance_glitch_user", "secret_sauce");
            this.productListPage.SortProducts(this.productListPage.lohiSortBtn);
            
            Assert.IsTrue(this.productListPage.IsProductsSorted());
        }
    }
}