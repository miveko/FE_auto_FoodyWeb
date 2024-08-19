using OpenQA.Selenium;

namespace FE_Foody_automation.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {   }

        public string Url = BaseUrl + "/User/Login";

        public IWebElement UserNameInput => driver.FindElement(By.Id("username"));

        public IWebElement PasswordInput => driver.FindElement(By.Id("password"));

        public IWebElement SignInButton => 
            driver.FindElement(By.CssSelector(".btn-primary[type=\"submit\"]"));


        public void Login(string email, string password)
        {
            UserNameInput.SendKeys(email);
            PasswordInput.SendKeys(password);
            SignInButton.Click();
        }

        public void OpenPage()
        { 
            driver.Navigate().GoToUrl(Url);
        }
    }
}