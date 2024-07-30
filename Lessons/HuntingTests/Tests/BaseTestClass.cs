namespace HuntingTests.Tests
{
    using OpenQA.Selenium;

    /// <summary>
    /// Класс помошник от которого наследуются все тестовые классы
    /// </summary>
    public class BaseTestClass
    {
        private By loginBtn = By.XPath("//button[contains(text(), 'Вход')]");
        private By loginPath = By.XPath("//input[@name = 'login']");
        private By passwordPath = By.XPath("//input[@name = 'password']");
        private By comeInBtnPath = By.XPath("//input[@value= 'Войти']");

        public BaseDriver Driver { get; private set; }

        protected bool ccPage = false;

        /// <summary>
        /// Запуск браузера и авторизация перед любым классом с тестами
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
            this.Driver = new BaseDriver();
            if (this.ccPage)
            {
                this.Login(Constants.ccLogin, Constants.ccPassword);
            }
            else
            {
                this.Login(Constants.login, Constants.password);
            }
        }

        /// <summary>
        /// Выключение драйвера после всех тестов в ондом классе
        /// </summary>
        [TearDown]
        public void Quit()
        {
            this.Driver.Queit();
        }

        /// <summary>
        /// Метод авторизации
        /// </summary>
        private void Login(string login, string password)
        {
            this.Driver.GoToUrl();
            this.Driver.Click(loginBtn);

            this.Driver.SendKeys(this.loginPath, login);
            this.Driver.SendKeys(this.passwordPath, password);

            this.Driver.Click(this.comeInBtnPath);
        }
    }
}
